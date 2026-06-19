import matplotlib.pyplot as plt
import pandas as pd
import sqlalchemy as sa
import os
import geopandas as gpd
import pymssql
from IPython.display import display
import numpy.typing as npt
import numpy as np
from math import sqrt
import sys

psw       = "teap227q"
server    = "info-mssql-etd"
user      = "etd15"
database = "BD_E15_VISU"
engine = sa.create_engine(f'mssql+pymssql://{user}:{psw}@{server}/{database}')
cnxn = engine.connect()


mois_en_lettres = {
    1: 'Janv', 2: 'Févr', 3: 'Mars', 4: 'Avril', 5: 'Mai', 6: 'Juin',
    7: 'Juil', 8: 'Août', 9: 'Sept', 10: 'Oct', 11: 'Nov', 12: 'Déc'
}


def requete_sql(date_debut, date_fin, categorie_risque, echelle, nom_zone=None):
   
    # ECHELLE
    if echelle == 'region':
        colonne_cible = "'Nouvelle-Aquitaine'"
        filtre_geo = ""
        titre_zone = "Nouvelle-Aquitaine"
    elif echelle == 'departement':
        colonne_cible = "DPT_NOM"
        filtre_geo = f"AND T_DEPARTEMENT_DPT.DPT_NOM = '{nom_zone}'"
        titre_zone = nom_zone
    else:
        colonne_cible = "CMN_NOM"
        filtre_geo = f"AND T_COMMUNE_CMN.CMN_NOM = '{nom_zone}'"
        titre_zone = nom_zone

    # COULEUR
    if categorie_risque == 'incendie':
        couleur_barre = '#FF0000'
    elif categorie_risque == 'PRECIPITATION':
        couleur_barre = '#1E88E5'
    else:
        couleur_barre = '#F57C00'

    # REQUETE
    requete_sql = f"""
        SELECT {colonne_cible} AS NOM_ZONE, DNN_VALEUR, DTJ_DATE_DEBUT, DTJ_DATE_FIN
       
        FROM T_DONNEES_DNN
        JOIN situé ON T_DONNEES_DNN.DNN_ID = situé.DNN_ID
        JOIN T_COMMUNE_CMN ON situé.CMN_ID = T_COMMUNE_CMN.CMN_ID
        JOIN T_DEPARTEMENT_DPT ON T_COMMUNE_CMN.DPT_ID = T_DEPARTEMENT_DPT.DPT_ID
        JOIN déroulé ON T_DONNEES_DNN.DNN_ID = déroulé.DNN_ID
        JOIN T_DATEJOURE_DTJ ON déroulé.ID_DTJ = T_DATEJOURE_DTJ.ID_DTJ
        JOIN possède ON T_DONNEES_DNN.DNN_ID = possède.DNN_ID
        JOIN T_CATEGORIE_CTG ON possède.CTG_ID = T_CATEGORIE_CTG.CTG_ID
       
        WHERE T_CATEGORIE_CTG.CTG_NOM LIKE '%{categorie_risque}%'
        AND T_DATEJOURE_DTJ.DTJ_DATE_DEBUT >= '{date_debut}'
        AND T_DATEJOURE_DTJ.DTJ_DATE_DEBUT <= '{date_fin}'
        AND (T_DATEJOURE_DTJ.DTJ_DATE_FIN IS NULL OR T_DATEJOURE_DTJ.DTJ_DATE_FIN <= '{date_fin}')
        {filtre_geo}
    """
    
    df_graphique = pd.read_sql(requete_sql, cnxn, coerce_float=False)
    df_graphique['DNN_VALEUR'] = pd.to_numeric(df_graphique['DNN_VALEUR'], errors='coerce')
    return df_graphique, titre_zone, couleur_barre

def violon(date_debut, date_fin, categorie_risque, echelle,  nom_zone=None):
    df_graphique, titre_zone, couleur_barre = requete_sql(date_debut, date_fin, categorie_risque, echelle, nom_zone)
    print(f"Lignes renvoyées par la requête : {len(df_graphique)}", file=sys.stderr)

    if df_graphique.empty:
        fig, ax = plt.subplots(figsize=(6, 4))
        ax.text(0.5, 0.5, "Aucune donnée disponible\npour cette période",
                horizontalalignment='center', verticalalignment='center',
                fontsize=12, color='gray', style='italic')
        ax.axis('off')
        plt.show()
        return df_graphique

    nom_colonne = f'{categorie_risque}'
    df_graphique = df_graphique.rename(columns={'DNN_VALEUR': nom_colonne})
    df_graphique['DTJ_DATE_DEBUT'] = pd.to_datetime(df_graphique['DTJ_DATE_DEBUT'])
    df_graphique['mois'] = df_graphique['DTJ_DATE_DEBUT'].dt.month

    seuil_minimum = 5  # en dessous, une densité estimée n'a pas vraiment de sens
    mois_valides, donnees_par_mois = [], []
    for m in sorted(df_graphique['mois'].unique()):
        valeurs = df_graphique.loc[df_graphique['mois'] == m, nom_colonne].dropna().values
        if len(valeurs) >= seuil_minimum:
            mois_valides.append(m)
            donnees_par_mois.append(valeurs)

    fig, ax = plt.subplots(figsize=(8, 6))

    if donnees_par_mois:
        vp = ax.violinplot(donnees_par_mois, positions=range(1, len(mois_valides) + 1),
                            showmeans=True, showmedians=True, showextrema=True)
        for corps in vp['bodies']:
            corps.set_facecolor(couleur_barre)
            corps.set_alpha(0.6)
        ax.set_xticks(range(1, len(mois_valides) + 1))
        ax.set_xticklabels([mois_en_lettres[m] for m in mois_valides])
        plt.title(f'Densité des {categorie_risque} par mois - {titre_zone} ({date_debut} - {date_fin})')

    plt.show()
    return df_graphique


def violon_comparaison(valeurs):
    df_graphique = []
    noms_colonnes = []
    
    for i in valeurs:
        df = violon(i[0], i[1], i[2], i[3], i[4], i[5], afficher=False)
        df_graphique.append(df)
    
    df_final = df_graphique[0]
    for df_suivant in df_graphique[1:]:
        df_final = pd.merge(df_final, df_suivant, on='NOM_ZONE', how='outer')
        
    colonnes_numeriques = [col for col in df_final.columns if col != 'NOM_ZONE']
    
    donnees_a_tracer = []
    labels_valides = []
    for col in colonnes_numeriques:
        valeurs_propres = df_final[col].dropna().values
        if len(valeurs_propres) > 0:
            donnees_a_tracer.append(valeurs_propres)
            labels_valides.append(col)
            
    #couleurs = generer_liste_couleurs(labels_valides)

    if donnees_a_tracer:
        fig, ax = plt.subplots(figsize=(10, 6))
        vp = ax.violinplot(donnees_a_tracer, showmeans=True)
        
        for i, corps in enumerate(vp['bodies']):
            #corps.set_facecolor(couleurs[i])
            corps.set_alpha(0.7)
            
        ax.set_xticks(range(1, len(labels_valides) + 1))
        ax.set_xticklabels(labels_valides, rotation=15)
        plt.title("Comparaison des Densités (Violon)")
        plt.savefig('graphique_comparaison_violon.png', bbox_inches='tight')
        plt.show()
    plt.close()

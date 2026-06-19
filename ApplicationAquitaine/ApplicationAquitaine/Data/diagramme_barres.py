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

start_date = str(sys.argv[1])
end_date = str(sys.argv[2])
risk_category = str(sys.argv[3])
scale = str(sys.argv[4])
aggregation = str(sys.argv[5])
zone_name = str(sys.argv[6])
comparaison = sys.argv[7].strip().lower() == "true"

chemin_fichier = os.path.join(os.path.dirname(os.path.abspath(__file__)), 'setting.png')


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
    elif categorie_risque == 'inondation':
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

def diagramme_barres(date_debut, date_fin, categorie_risque, echelle, agregation, nom_zone=None):
   
    df_graphique, titre_zone, couleur_barre = requete_sql(date_debut, date_fin, categorie_risque, echelle, nom_zone)
    print(f"Lignes renvoyées par la requête : {len(df_graphique)}", file=sys.stderr)

   

    # AGREGATIONS
    if agregation == 'avg':
        df_final = df_graphique.groupby('NOM_ZONE')['DNN_VALEUR'].mean().reset_index()
        titre = f'Moyenne des {categorie_risque} - {titre_zone} ({date_debut} - {date_fin})'
    elif agregation == 'max':
        df_final = df_graphique.groupby('NOM_ZONE')['DNN_VALEUR'].max().reset_index()
        titre = f'Plus grand {categorie_risque} - {titre_zone} ({date_debut} - {date_fin})'
    elif agregation == 'min':
        df_final = df_graphique.groupby('NOM_ZONE')['DNN_VALEUR'].min().reset_index()
        titre = f'Plus petit {categorie_risque} - {titre_zone} ({date_debut} - {date_fin})'
    elif agregation == 'sum':
        df_final = df_graphique.groupby('NOM_ZONE')['DNN_VALEUR'].sum().reset_index()
        titre = f'Somme des {categorie_risque} - {titre_zone} ({date_debut} - {date_fin})'
    elif agregation == 'count':
        df_final = df_graphique.groupby('NOM_ZONE')['DNN_VALEUR'].count().reset_index()
        titre = f'Nombre de {categorie_risque} - {titre_zone} ({date_debut} - {date_fin})'
    
    if df_graphique.empty:
        
        fig, ax = plt.subplots(figsize=(6, 4))
        
        ax.text(0.5, 0.5, "Aucune donnée disponible\npour cette période", 
                horizontalalignment='center', 
                verticalalignment='center', 
                fontsize=12, 
                color='gray',
                style='italic')
        
        ax.axis('off')
        
        plt.savefig(chemin_fichier, bbox_inches='tight')
        plt.close()
        
        return df_graphique

    nom_colonne = f"{categorie_risque}_{agregation}"
    df_final = df_final.rename(columns={'DNN_VALEUR': nom_colonne})

    #PLOT ET SAVE
    ax = df_final.plot(kind='bar', x='NOM_ZONE', y=nom_colonne, title=titre,legend=False, color=couleur_barre, rot=0)

    

    plt.savefig(chemin_fichier)
    plt.close()

def diagramme_barres_comparaison(valeurs):
    df_graphique = []
    for i in valeurs:
        df_graphique.append(diagramme_barres(*i))
    df_final = df_graphique[0]
    for df_suivant in df_graphique[1:]:
        df_final = pd.merge(df_final, df_suivant, on='NOM_ZONE', how='outer')
    
    liste_couleurs = []
    for col in df_final.columns:
        if col == 'NOM_ZONE':
            continue 

        if 'incendie' in col:
            liste_couleurs.append('#FF0000') 
        elif 'inondation' in col:
            liste_couleurs.append('#1E88E5') 
        else:
            liste_couleurs.append('#F57C00')

    titre = f'Comparaison de données'

    #PLOT ET SAVE
    ax = df_final.plot(kind='bar', x='NOM_ZONE',title=titre,color=liste_couleurs, legend=False, rot=0)

    nom_fichier = 'graphique.png'
    plt.savefig(nom_fichier, bbox_inches='tight')
    plt.close()

if (comparaison == False) :
    diagramme_barres(start_date, end_date, risk_category, scale, aggregation, zone_name)
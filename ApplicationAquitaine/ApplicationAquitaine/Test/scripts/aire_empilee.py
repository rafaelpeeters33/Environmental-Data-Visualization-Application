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


    # COULEUR (utilisée seulement quand il n'y a qu'une seule série au final)
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


def construire_serie(date_debut, date_fin, categorie_risque, echelle, agregation, nom_zone=None):
    
    df_graphique, titre_zone, _ = requete_sql(date_debut, date_fin, categorie_risque, echelle, nom_zone)

    if df_graphique.empty:
        return None, None, titre_zone

    df_graphique['DTJ_DATE_DEBUT'] = pd.to_datetime(df_graphique['DTJ_DATE_DEBUT'])
    
    date_deb_dt = pd.to_datetime(date_debut)
    date_fin_dt = pd.to_datetime(date_fin)
    nb_jours = (date_fin_dt - date_deb_dt).days
    month = False

    if nb_jours < 31:
        df_graphique['PERIODE'] = df_graphique['DTJ_DATE_DEBUT'].dt.strftime('%Y-%m-%d')
    elif nb_jours > 365:
        df_graphique['PERIODE'] = df_graphique['DTJ_DATE_DEBUT'].dt.year
    else:
        df_graphique['PERIODE'] = df_graphique['DTJ_DATE_DEBUT'].dt.strftime('%Y-%m')
        

    if agregation == 'avg':
        df_resultat = df_graphique.groupby('PERIODE')['DNN_VALEUR'].mean().reset_index()
    elif agregation == 'sum':
        df_resultat = df_graphique.groupby('PERIODE')['DNN_VALEUR'].sum().reset_index()
    else:
        df_resultat = df_graphique.groupby('PERIODE')['DNN_VALEUR'].count().reset_index()
        
    if agregation == 'count':
        unite = "nombre d'évènements"
    else:
        requete_unite = f"""SELECT DISTINCT T_DONNEES_DNN.DNN_NOM, T_DONNEES_DNN.DNN_UNITE
            FROM T_DONNEES_DNN
            JOIN possède ON possède.DNN_ID = T_DONNEES_DNN.DNN_ID
            JOIN T_CATEGORIE_CTG ON possède.CTG_ID = T_CATEGORIE_CTG.CTG_ID
            WHERE T_CATEGORIE_CTG.CTG_NOM LIKE '%{categorie_risque}%'"""
        df_unite = pd.read_sql(requete_unite, cnxn)
        unite = df_unite['DNN_UNITE'].iloc[0].strip() if not df_unite.empty else ""

    nom_serie = f"{titre_zone} - {categorie_risque}"
    df_resultat = df_resultat.rename(columns={'DNN_VALEUR': nom_serie})



    return df_resultat, unite, titre_zone


def trace_graphique(date_debut, date_fin, categories, echelle, agregation, zones=None):
   
    if echelle == 'region':
        zones_a_parcourir = [None]
    else:
        zones_a_parcourir = zones if zones else [None]

    series = []
    unites = set()

    for categorie in categories:
        for zone in zones_a_parcourir:
            df_mensuel, unite, _ = construire_serie(
                date_debut, date_fin, categorie, echelle, agregation, zone
            )
            if df_mensuel is not None:
                series.append(df_mensuel)
                unites.add(unite)


    if not series:
        fig, ax = plt.subplots(figsize=(6, 4))
        ax.text(0.5, 0.5, "Aucune donnée disponible\npour cette période",
                horizontalalignment='center', verticalalignment='center',
                fontsize=12, color='gray', style='italic')
        ax.axis('off')
        plt.show()
        return pd.DataFrame()

    df_final = series[0]
    for df_suivant in series[1:]:
        df_final = pd.merge(df_final, df_suivant, on='PERIODE', how='outer')

    df_final = df_final.sort_values(by='PERIODE').fillna(0)

    colonnes_series= []
    for c in df_final.columns:
        if c != 'PERIODE':
            colonnes_series.append(c)

    
    if len(colonnes_series) == 1 and len(unites) == 1:
        y_label = list(unites)[0]
        titre = colonnes_series[0]
    else:
        y_label = "Valeur"
        titre = "Comparaison des évolutions (aires empilées)"

    ax = df_final.plot(kind='area', x='PERIODE', y=colonnes_series, stacked=True,
                        alpha=0.7, legend=True, rot=0, figsize=(10, 6), title=titre)
    ax.set_ylabel(y_label)

    

    #if (month):
    #    mois_presents = sorted(df_final['PERIODE'].unique())
    #    ax.set_xticks(mois_presents)

    #    labels_mois = [mois_en_lettres[m] for m in mois_presents]
    #    ax.set_xticklabels(labels_mois, rotation=0)

    

    plt.close()

    return df_final




categories = [c.strip() for c in risk_category.split('|') if c.strip()]
zones = [z.strip() for z in zone_name.split('|') if z.strip()] if zone_name else []

trace_graphique(start_date, end_date, categories, scale, aggregation, zones)
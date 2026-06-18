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
zone_name = str(sys.argv[5])
comparaison = sys.argv[6].strip().lower() == "true"
chemin_fichier = os.path.join(os.path.dirname(os.path.abspath(__file__)), 'setting.png')


print(f"Arguments reçus : {sys.argv}", file=sys.stderr)


def requete_sql(start_date, end_date, risk_category, scale, zone_name=None):
   
    # ECHELLE
    if scale == 'region':
        target_column = "'Nouvelle-Aquitaine'"
        geo_filter = ""
        titre_zone = "Nouvelle-Aquitaine"
    elif scale == 'departement':
        target_column = "DPT_NOM"
        geo_filter = f"AND T_DEPARTEMENT_DPT.DPT_NOM = '{zone_name}'"
        titre_zone = zone_name
    else:
        target_column = "CMN_NOM"
        geo_filter = f"AND T_COMMUNE_CMN.CMN_NOM = '{zone_name}'"
        titre_zone = zone_name

    # COULEUR
    if risk_category == 'incendie':
        couleur_barre = '#FF0000'
    elif risk_category == 'inondation':
        couleur_barre = '#1E88E5'
    else:
        couleur_barre = '#F57C00'

    # REQUETE
    requete_sql = f"""
        select {target_column} AS NOM_ZONE, DNN_VALEUR 
        from T_DONNEES_DNN
        Join DÉROULÉ ON T_DONNEES_DNN.DNN_ID = DÉROULÉ.DNN_ID
        join T_DATEJOURE_DTJ ON DÉROULÉ.ID_DTJ = T_DATEJOURE_DTJ.ID_DTJ
        join POSSÈDE on T_DONNEES_DNN.DNN_ID = POSSÈDE.DNN_ID
        join T_CATEGORIE_CTG On T_CATEGORIE_CTG.CTG_ID = POSSÈDE.CTG_ID
        join SITUÉ ON SITUÉ.DNN_ID = T_DONNEES_DNN.DNN_ID
        join T_COMMUNE_CMN ON SITUÉ.CMN_ID = T_COMMUNE_CMN.CMN_ID
        join T_DEPARTEMENT_DPT ON T_COMMUNE_CMN.DPT_ID = T_DEPARTEMENT_DPT.DPT_ID 
        Where T_CATEGORIE_CTG.CTG_NOM Like '{risk_category}%'
        AND T_DATEJOURE_DTJ.DTJ_DATE_DEBUT >= '{start_date}'
        AND T_DATEJOURE_DTJ.DTJ_DATE_DEBUT <= '{end_date}'
        AND (T_DATEJOURE_DTJ.DTJ_DATE_FIN IS NULL OR T_DATEJOURE_DTJ.DTJ_DATE_FIN <= '{end_date}' )
        {geo_filter};
    """
    
    df_graphique = pd.read_sql(requete_sql, cnxn, coerce_float=True)
    df_graphique['DNN_VALEUR'] = pd.to_numeric(df_graphique['DNN_VALEUR'], errors='coerce')
    return df_graphique, titre_zone, couleur_barre


def boite_a_moustaches(start_date,end_date, risk_category, scale, zone_name=None):
    df_graphique, titre_zone, couleur_barre = requete_sql(start_date, end_date, risk_category, scale, zone_name)
    
    titre = f'Distribution des {risk_category} - {titre_zone} ({start_date} - {end_date})'
    if df_graphique.empty:
        print("Aucune donnée trouvée pour ces critères. Génération d'une image vide.")
        
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

    ax = df_graphique.plot(kind='box', column='DNN_VALEUR', by='NOM_ZONE', title=titre, legend=False, color=couleur_barre, rot=0, whis=(0, 100))
   

    

    plt.savefig(chemin_fichier)
    plt.close()


    return df_graphique

def boite_a_moustache_comparaison(valeurs):
    df_graphique = []
    for i in valeurs:
        df_graphique.append(boite_a_moustaches(*i))
    df_final = df_graphique[0]
    for df_suivant in df_graphique[1:]:
        df_final = pd.merge(df_final, df_suivant, on='NOM_ZONE', how='outer')
    """
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
    """
    titre = f'Comparaison de données'

    #PLOT ET SAVE
    ax = df_final.plot(kind='box', x='NOM_ZONE', title=titre,legend=False, rot=0)

    nom_fichier = 'graphique.png'
    plt.savefig(nom_fichier, bbox_inches='tight')
    plt.close()


if (comparaison == False) :
    boite_a_moustaches(start_date, end_date, risk_category, scale, zone_name)


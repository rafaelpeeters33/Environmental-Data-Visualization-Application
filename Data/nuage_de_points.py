import matplotlib.pyplot as plt
import pandas as pd
import sqlalchemy as sa
import os
import geopandas as gpd
import numpy.typing as npt
import numpy as np
from math import sqrt
from requete import connexion
from requete import requete_sql

def nuage_de_points(date_debut, date_fin, categorie_risque, echelle, nom_zone=None):
   
    df_graphique, titre_zone, couleur_barre, = requete_sql(date_debut, date_fin, categorie_risque, echelle, nom_zone)

    df_graphique['DTJ_DATE_DEBUT'] = pd.to_datetime(df_graphique['DTJ_DATE_DEBUT'])
    df_graphique['DTJ_DATE_FIN'] = pd.to_datetime(df_graphique['DTJ_DATE_FIN'])

    dateDebut = pd.to_datetime(date_debut, format='%Y%m%d')
    dateFin = pd.to_datetime(date_fin, format='%Y%m%d')
    periode= (dateFin-dateDebut).days
   
    if df_graphique.empty:
        print(f"Aucune donnée trouvée pour {titre_zone}.")
        return None
    
    if periode>365:
        df_graphique['PERIODE']=df_graphique['DTJ_DATE_DEBUT'].dt.year
    elif periode<=365:
        df_graphique['PERIODE']=df_graphique['DTJ_DATE_DEBUT'].dt.month

    titre = f'Evolution des {categorie_risque} - {titre_zone} ({date_debut} - {date_fin})'
     
    # PLOT ET SAVE
    ax = df_graphique.plot(kind='scatter', x='PERIODE', y='DNN_VALEUR', title=titre, legend=False, color=couleur_barre, rot=0)
    nom_fichier = 'graphique.png'
   
    plt.savefig(nom_fichier, bbox_inches='tight')
    plt.close()
    
    return df_graphique

def nuage_de_points_comparaison(valeurs):
    df_graphique = []
    for i in valeurs:
        df_graphique.append(nuage_de_points(*i))
    df_final = df_graphique[0]
    for df_suivant in df_graphique[1:]:
        df_final = pd.merge(df_final, df_suivant, on='NOM_ZONE', how='outer')
    
    #PLOT ET SAVE
    ax = df_final.plot(kind='scatter', x='PERIODE',legend=False, rot=0)

    nom_fichier = 'graphique.png'
    plt.savefig(nom_fichier, bbox_inches='tight')
    plt.close()

nuage_de_points(20250101,20250201,'INCENDIE','region')

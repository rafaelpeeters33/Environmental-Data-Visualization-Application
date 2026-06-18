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
from requete import connexion
from requete import requete_sql

def boite_a_moustaches(date_debut,date_fin, categorie_risque, echelle, nom_zone=None):
    df_graphique, titre_zone, couleur_barre, = requete_sql(date_debut, date_fin, categorie_risque, echelle, nom_zone)
    df_graphique['DNN_VALEUR'] = pd.to_numeric(df_graphique['DNN_VALEUR'], errors='coerce')
    
    titre = f'Distribution des {categorie_risque} - {titre_zone} ({date_debut} - {date_fin})'

    # PLOT ET SAVE
    ax = df_graphique.plot(kind='box', column='DNN_VALEUR', by='NOM_ZONE' , title=titre, legend=False, color=couleur_barre, rot=0)
   
    nom_fichier = 'graphique.png'
    plt.savefig(nom_fichier, bbox_inches='tight')
    plt.show()

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
    plt.show()

boite_a_moustaches(20250101,20250201,'INCENDIE','region')

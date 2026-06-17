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

def trace_ligne(date_debut, date_fin,categorie_risque, echelle, agregation, nom_zone=None):
   
    df_graphique, titre_zone, couleur_barre, = requete_sql(date_debut, date_fin, categorie_risque, echelle, nom_zone)
    
    df_graphique['DTJ_DATE_DEBUT'] = pd.to_datetime(df_graphique['DTJ_DATE_DEBUT'])
    df_graphique['DTJ_DATE_FIN'] = pd.to_datetime(df_graphique['DTJ_DATE_FIN'])

    dateDebut = pd.to_datetime(date_debut, format='%Y%m%d')
    dateFin = pd.to_datetime(date_fin, format='%Y%m%d')
    periode= (dateFin-dateDebut).days
   
    if periode>365:
        df_graphique['PERIODE']=df_graphique['DTJ_DATE_DEBUT'].dt.year
    elif periode<=365:
        df_graphique['PERIODE']=df_graphique['DTJ_DATE_DEBUT'].dt.month
       
    # AGEGATIONS
    if agregation == 'avg':
        df_final = df_graphique.groupby('PERIODE')['DNN_VALEUR'].mean().reset_index()
        titre = f'Evolution de la moyenne des {categorie_risque} - {titre_zone} ({date_debut} - {date_fin})'
    elif agregation == 'sum':
        df_final = df_graphique.groupby('PERIODE')['DNN_VALEUR'].sum().reset_index()
        titre = f'Evolution de la somme des {categorie_risque} - {titre_zone} ({date_debut} - {date_fin})'
    elif agregation == 'count':
        df_final = df_graphique.groupby('PERIODE')['DNN_VALEUR'].count().reset_index()
        titre = f'Evolution du nombre de {categorie_risque} - {titre_zone} ({date_debut} - {date_fin})'
     
    # PLOT ET SAVE
    ax = df_final.plot( kind='line', x='PERIODE', y='DNN_VALEUR', title=titre, legend=False, color=couleur_barre, rot=0)
   
    nom_fichier = 'graphique.png'
    plt.savefig(nom_fichier, bbox_inches='tight')
    plt.close()
    
    return df_final

def trace_ligne_comparaison(valeurs):
    df_graphique = []
    for i in valeurs:
        df_graphique.append(trace_ligne(i[0],i[1],i[2],i[3],i[4],i[5]))
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
    ax = df_final.plot(kind='line', x='PERIODE',title=titre, legend=False, rot=0)

    nom_fichier = 'graphique.png'
    plt.savefig(nom_fichier, bbox_inches='tight')
    plt.close()



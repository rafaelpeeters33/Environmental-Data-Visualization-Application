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

def trace_aires_empilees(date_debut, date_fin, categorie_risque, echelle, agregation, nom_zone=None):
    
    df_graphique, titre_zone, couleur_barre = requete_sql(date_debut, date_fin, categorie_risque, echelle, nom_zone)
    
    df_graphique['DTJ_DATE_DEBUT'] = pd.to_datetime(df_graphique['DTJ_DATE_DEBUT'])
    dateDebut = pd.to_datetime(date_debut, format='%Y%m%d')
    dateFin = pd.to_datetime(date_fin, format='%Y%m%d')
    periode = (dateFin - dateDebut).days
   
    if df_graphique.empty: return None

    if periode > 365:
        df_graphique['PERIODE'] = df_graphique['DTJ_DATE_DEBUT'].dt.year
    else:
        df_graphique['PERIODE'] = df_graphique['DTJ_DATE_DEBUT'].dt.month
       
    if agregation == 'avg':
        df_final = df_graphique.groupby('PERIODE')['DNN_VALEUR'].mean().reset_index()
        titre = f'Aires empilées (Moyenne) : {categorie_risque}'
    elif agregation == 'sum':
        df_final = df_graphique.groupby('PERIODE')['DNN_VALEUR'].sum().reset_index()
        titre = f'Aires empilées (Somme) : {categorie_risque}'
    elif agregation == 'count':
        df_final = df_graphique.groupby('PERIODE')['DNN_VALEUR'].count().reset_index()
        titre = f'Aires empilées (Nombre) : {categorie_risque}'

    nom_colonne = f'{categorie_risque}_{agregation}'
    df_final = df_final.rename(columns={'DNN_VALEUR': nom_colonne})

   
    ax = df_final.plot(kind='area', x='PERIODE', y=nom_colonne, title=titre, legend=True, color=couleur_barre, alpha=0.6, rot=0)
    plt.savefig('graphique_aires.png', bbox_inches='tight')
    plt.show()
    plt.close()
    
    return df_final

def trace_aires_empilees_comparaison(valeurs):
    df_graphique = []
    for i in valeurs:
        df_graphique.append(trace_aires_empilees(*i))
    
    df_final = df_graphique[0]
    for df_suivant in df_graphique[1:]:
        df_final = pd.merge(df_final, df_suivant, on='PERIODE', how='outer')
    
    df_final = df_final.sort_values(by='PERIODE')
    
    #couleurs = generer_liste_couleurs(df_final.columns)  color=couleurs,
    
    ax = df_final.plot(kind='area', x='PERIODE', stacked=True, alpha=0.7, legend=True, rot=0, figsize=(10, 6), title="Comparaison des Évolutions (Aires Empilées)")
    plt.savefig('graphique_comparaison_aires.png', bbox_inches='tight')
    plt.show()
    plt.close()

trace_aires_empilees(19900101, 20200201, 'incendie', 'commune', 'avg', 'Pessac')
trace_aires_empilees(19900101, 20200201, 'incendie', 'commune', 'count', 'Pessac')
trace_aires_empilees(19900101, 20200201, 'incendie', 'commune', 'sum', 'Pessac')
valeurs = [[19900101, 20200201, 'incendie', 'commune', 'avg', 'Pessac'], [19900101, 20200201, 'incendie', 'commune', 'sum', 'Pessac'], [19900101, 20200201, 'incendie', 'commune', 'count', 'Pessac']]
trace_aires_empilees_comparaison(valeurs)
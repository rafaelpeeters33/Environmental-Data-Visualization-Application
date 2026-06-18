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

def diagramme_barres(date_debut, date_fin, categorie_risque, echelle, agregation, nom_zone=None):
   
    df_graphique, titre_zone, couleur_barre, = requete_sql(date_debut, date_fin, categorie_risque, echelle, nom_zone)

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
    
    nom_colonne = f"{categorie_risque}_{agregation}"
    df_final = df_final.rename(columns={'DNN_VALEUR': nom_colonne})

    #PLOT ET SAVE
    ax = df_final.plot(kind='bar', x='NOM_ZONE', y=nom_colonne, title=titre,legend=False, color=couleur_barre, rot=0)
    
    nom_fichier = 'graphique.png'
    plt.savefig(nom_fichier, bbox_inches='tight')
    plt.close()
    
    return df_final

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

diagramme_barres('1990-01-01', '2022-02-01', 'incendie', 'commune', 'avg')
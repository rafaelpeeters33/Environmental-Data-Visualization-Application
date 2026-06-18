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


def violon(date_debut, date_fin, categorie_risque, echelle, agregation, nom_zone=None, afficher=True):
    df_graphique, titre_zone, couleur_barre = requete_sql(date_debut, date_fin, categorie_risque, echelle, nom_zone)
    
    titre = f'Densité des {categorie_risque} - {titre_zone}'

    nom_colonne = f'{categorie_risque}_{agregation}'
    df_graphique = df_graphique.rename(columns={'DNN_VALEUR': nom_colonne})

    if afficher:
        fig, ax = plt.subplots(figsize=(8, 6))
        data_propre = df_graphique[nom_colonne].dropna().values
        
        if len(data_propre) > 0:
            vp = ax.violinplot(data_propre, showmeans=True, showextrema=True)
            for corps in vp['bodies']:
                corps.set_facecolor(couleur_barre)
                corps.set_alpha(0.6)
            ax.set_xticks([1])
            ax.set_xticklabels([nom_zone])
            plt.title(titre)
            plt.savefig('graphique_violon.png', bbox_inches='tight')
            plt.show()
        plt.close()
        
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

violon(19900101,20200201,'incendie','region')
valeurs = [['19900101','20200201','incendie','region'],['19900101','20200201','incendie','departement','Gironde'],['19900101','20200201','incendie','commune','Pessac']]
violon_comparaison(valeurs)
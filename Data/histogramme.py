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

def histogramme(date_debut, date_fin,categorie_risque,echelle, nom_zone=None):
   
    df_graphique, titre_zone, couleur_barre = requete_sql(date_debut,date_fin,categorie_risque,echelle,nom_zone)

    df_graphique['DNN_VALEUR'] = pd.to_numeric(
        df_graphique['DNN_VALEUR'],
        errors='coerce'
    )

    df_graphique = df_graphique.dropna()

    titre = f'Distribution de {categorie_risque} - {titre_zone} - ({date_debut} - {date_fin})'

    fig, ax = plt.subplots()

    df_graphique['DNN_VALEUR'].plot(kind='hist',bins=10,color=couleur_barre,edgecolor='black',title=titre,legend=False,ax=ax)

    ax.set_xlabel(f'{categorie_risque} en mm')
    ax.set_ylabel("Nombre de jours")

    plt.savefig("graphique.png", bbox_inches="tight")
    plt.show()

    return df_graphique

histogramme('19501231','20251231','TEMP_MOY','region')
histogramme('20000101','20260101','PRECIPITATION','region')


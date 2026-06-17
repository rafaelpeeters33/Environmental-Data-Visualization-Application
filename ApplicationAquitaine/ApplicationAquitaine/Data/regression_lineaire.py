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

################################################# CONNEXION ET REQUETE ###########################################################

psw       = "ETD"
server    = "info-mssql-etd"
user      = "ETD"
database = "MLR12345"
engine = sa.create_engine(f'mssql+pymssql://{user}:{psw}@{server}/{database}')
cnxn = engine.connect()

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
        AND T_DATEJOURE_DTJ.DTJ_DATE_FIN <= '{date_fin}'
        {filtre_geo}
    """
    
    df_graphique = pd.read_sql(requete_sql, cnxn, coerce_float=False)
    return df_graphique, titre_zone, couleur_barre

################################################# FONCTIONS REGRESSION ###########################################################
def moyenne(x:npt.NDArray[np.float64])->float:
    somme = 0
    for i in range(len(x)):
        somme +=x[i]
    return somme/len(x)
    
def variance(x:npt.NDArray[np.float64]) ->float:
    return moyenne(x**2)-(moyenne(x))**2

def covariance(x:npt.NDArray[np.float64],y:npt.NDArray[np.float64]) ->float:
    return moyenne([a*b for a,b in zip(x,y)])-moyenne(x)*moyenne(y)

def ecartType(x:npt.NDArray[np.float64]) ->float:
    return sqrt(variance(x))

def regression(x:npt.NDArray[np.float64],y:npt.NDArray[np.float64]) ->tuple:
    a= covariance(x,y)/variance(x)
    b=moyenne(y)-a*moyenne(x)
    r=covariance(x,y)/(ecartType(x)*ecartType(y))
    return a,b,r


def traceRegressionLineaire(a:float,b:float,borne1:int,borne2:int,nbPoints:int):
    min = borne1
    max = borne2
    xAbscisses = np.linspace(min,max,nbPoints)
    yOrdonnees = a*xAbscisses + b
    plt.plot(xAbscisses,yOrdonnees, '-r')
    plt.title("y="+str(round(a,2))+" x + "+str(round(b,2)))
    plt.show()


def traceAnalyse(x:npt.NDArray[np.float64],y:npt.NDArray[np.float64]):
    plt.scatter(x,y)
    a,b,R = regression(x,y)
    (xbar,ybar) = moyenne(x),moyenne(y)
    plt.scatter(xbar,ybar)
    traceRegressionLineaire(a,b,min(x),max(x),10)
    print("Coefficient de corrélation linéaire:",R)

################################################# GRAPHIQUE REGRESSION ###########################################################

def regression_lineaire(date_debut, date_fin, cat_x, cat_y, echelle, agregation, nom_zone=None):
    
    df_x, titre_zone, _ = requete_sql(date_debut, date_fin, cat_x, echelle, nom_zone)
    df_y, _, _ = requete_sql(date_debut, date_fin, cat_y, echelle, nom_zone)

    df_x['DTJ_DATE_DEBUT'] = pd.to_datetime(df_x['DTJ_DATE_DEBUT'])
    df_y['DTJ_DATE_DEBUT'] = pd.to_datetime(df_y['DTJ_DATE_DEBUT'])

    dateDebut = pd.to_datetime(date_debut, format='%Y%m%d')
    dateFin = pd.to_datetime(date_fin, format='%Y%m%d')
    periode = (dateFin - dateDebut).days

    if periode > 365:
        df_x['PERIODE'] = df_x['DTJ_DATE_DEBUT'].dt.year
        df_y['PERIODE'] = df_y['DTJ_DATE_DEBUT'].dt.year
    else:
        df_x['PERIODE'] = df_x['DTJ_DATE_DEBUT'].dt.month
        df_y['PERIODE'] = df_y['DTJ_DATE_DEBUT'].dt.month


    if agregation == 'sum':
        df_x_agg = df_x.groupby('PERIODE')['DNN_VALEUR'].sum().reset_index()
        df_y_agg = df_y.groupby('PERIODE')['DNN_VALEUR'].sum().reset_index()
    elif agregation == 'count':
        df_x_agg = df_x.groupby('PERIODE')['DNN_VALEUR'].count().reset_index()
        df_y_agg = df_y.groupby('PERIODE')['DNN_VALEUR'].count().reset_index()
    else: 
        df_x_agg = df_x.groupby('PERIODE')['DNN_VALEUR'].mean().reset_index()
        df_y_agg = df_y.groupby('PERIODE')['DNN_VALEUR'].mean().reset_index()


    df_merge = pd.merge(df_x_agg, df_y_agg, on='PERIODE', suffixes=('_X', '_Y')).fillna(0)

 
    x_vals = df_merge['DNN_VALEUR_X'].values.astype(float)
    y_vals = df_merge['DNN_VALEUR_Y'].values.astype(float)

  
    
    plt.figure(figsize=(8, 6))
    plt.xlabel(f"{agregation} de {cat_x}")
    plt.ylabel(f"{agregation} de {cat_y}")
    plt.title(f"\n--- Régression : {cat_x} (X) vs {cat_y} (Y) sur {titre_zone} ---")


    traceAnalyse(x_vals, y_vals)
    
    
    return df_merge


regression_lineaire('19900101', '20200201', 'incendie', 'incendie', 'region', 'avg')

regression_lineaire('19900101', '20200201', 'incendie', 'incendie', 'region', 'count')

regression_lineaire('19900101', '20200201', 'incendie', 'incendie', 'region', 'sum')
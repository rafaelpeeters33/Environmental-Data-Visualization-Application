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


def regression_lineaire(date_debut, date_fin, categorie_risque, echelle, nom_zone=None):
    
    df, titre_zone, couleur_barre = requete_sql(date_debut, date_fin, categorie_risque, echelle, nom_zone)

    df['DNN_VALEUR'] = pd.to_numeric(df['DNN_VALEUR'], errors='coerce')
    df['DTJ_DATE_DEBUT'] = pd.to_datetime(df['DTJ_DATE_DEBUT'])

    dateDebut = pd.to_datetime(date_debut, format='%Y%m%d')
    dateFin   = pd.to_datetime(date_fin,   format='%Y%m%d')
    periode = (dateFin - dateDebut).days

    if periode > 365 :
        df['PERIODE'] = df['DTJ_DATE_DEBUT'].dt.year
    else:
        df['PERIODE'] = df['DTJ_DATE_DEBUT'].dt.month

    df_final = df.groupby('PERIODE')['DNN_VALEUR'].mean().reset_index()
    df_final = df_final.dropna()

    x = df_final['PERIODE'].values.astype(float)
    y = df_final['DNN_VALEUR'].values.astype(float)
    
    a, b, r = regression(x, y)
    x_line = np.linspace(x.min(), x.max(), 100)
    y_line = a * x_line + b

    plt.figure(figsize=(10, 5))
    
    plt.plot(x_line, y_line, color=couleur_barre)
    plt.scatter(x, y)
    plt.xlabel('Années')
    plt.ylabel(f'{categorie_risque} en °C')
    plt.title(f'Régression linéaire – {categorie_risque} – {titre_zone} ({date_debut}–{date_fin})')

    plt.savefig('graphique.png', bbox_inches ='tight')
    plt.show()

    return df_final

regression_lineaire('19500101', '20251231', 'PRECIPITATION', 'region')
regression_lineaire('19500101', '20251231', 'TEMP_MOY', 'region')

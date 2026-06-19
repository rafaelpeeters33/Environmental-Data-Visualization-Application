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
import sys
from datetime import datetime

start_date = str(sys.argv[1])
end_date = str(sys.argv[2])
risk_category = str(sys.argv[3])
scale = str(sys.argv[4])
zone_name = str(sys.argv[5])
comparaison = sys.argv[6].strip().lower() == "true"
chemin_fichier = os.path.join(os.path.dirname(os.path.abspath(__file__)), 'setting.png')

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
    
    cnxn = connexion()

    df = requete_sql(cnxn, date_debut, categorie_risque, echelle, nom_zone, date_fin)
    

    df['DNN_VALEUR'] = pd.to_numeric(df['DNN_VALEUR'], errors='coerce')
    df['DTJ_DATE_DEBUT'] = pd.to_datetime(df['DTJ_DATE_DEBUT'])

    dateDebut = pd.to_datetime(date_debut, format='%Y-%m-%d')
    dateFin   = pd.to_datetime(date_fin, format='%Y-%m-%d')
    periode = (dateFin - dateDebut).days

    if periode > 365 :
        df['PERIODE'] = df['DTJ_DATE_DEBUT'].dt.year
        xlabel = 'Années'
    else:
        df['PERIODE'] = df['DTJ_DATE_DEBUT'].dt.month
        xlabel = 'mois'

    df_final = df.groupby('PERIODE')['DNN_VALEUR'].mean().reset_index()
    df_final = df_final.dropna()

    x = df_final['PERIODE'].values.astype(float)
    y = df_final['DNN_VALEUR'].values.astype(float)
    
    a, b, r = regression(x, y)
    x_line = np.linspace(x.min(), x.max(), 100)
    y_line = a * x_line + b


    plt.figure(figsize=(10, 5))
    
    plt.plot(x_line, y_line)
    plt.scatter(x, y)
    plt.xlabel(xlabel)
    plt.ylabel(f'{categorie_risque} en °C')
    plt.title(f'Régression linéaire – {categorie_risque} – {zone_name} ({date_debut}–{date_fin})')

    plt.savefig(chemin_fichier)
    plt.close()

    plt.show()

    return df_final

if (comparaison == False) :
    regression_lineaire(start_date, end_date, risk_category, scale, zone_name)



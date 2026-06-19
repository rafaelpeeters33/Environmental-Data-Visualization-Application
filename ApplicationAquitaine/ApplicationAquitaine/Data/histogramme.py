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


psw       = "teap227q"
server    = "info-mssql-etd"
user      = "etd15"
database = "BD_E15_VISU"
engine = sa.create_engine(f'mssql+pymssql://{user}:{psw}@{server}/{database}')
cnxn = engine.connect()

start_date = str(sys.argv[1])
end_date = str(sys.argv[2])
risk_category = str(sys.argv[3])
scale = str(sys.argv[4])
zone_name = str(sys.argv[5])
comparaison = sys.argv[6].strip().lower() == "true"
chemin_fichier = os.path.join(os.path.dirname(os.path.abspath(__file__)), 'setting.png')


mois_en_lettres = {
    1: 'Janv', 2: 'Févr', 3: 'Mars', 4: 'Avril', 5: 'Mai', 6: 'Juin',
    7: 'Juil', 8: 'Août', 9: 'Sept', 10: 'Oct', 11: 'Nov', 12: 'Déc'
}

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
        AND T_DATEJOURE_DTJ.DTJ_DATE_DEBUT <= '{date_fin}'
        AND (T_DATEJOURE_DTJ.DTJ_DATE_FIN IS NULL OR T_DATEJOURE_DTJ.DTJ_DATE_FIN <= '{date_fin}')
        {filtre_geo}
    """
    
    df_graphique = pd.read_sql(requete_sql, cnxn, coerce_float=False)
    df_graphique['DNN_VALEUR'] = pd.to_numeric(df_graphique['DNN_VALEUR'], errors='coerce')
    return df_graphique, titre_zone, couleur_barre


def construire_serie(date_debut, date_fin, categorie_risque, echelle, nom_zone=None):
    
    df_graphique, titre_zone, _ = requete_sql(date_debut, date_fin, categorie_risque, echelle, nom_zone)

    if df_graphique.empty:
        return None, None, titre_zone

    df_graphique['DTJ_DATE_DEBUT'] = pd.to_datetime(df_graphique['DTJ_DATE_DEBUT'])
    
    date_deb_dt = pd.to_datetime(date_debut)
    date_fin_dt = pd.to_datetime(date_fin)
    nb_jours = (date_fin_dt - date_deb_dt).days
    month = False

    if nb_jours < 31:
        df_graphique['PERIODE'] = df_graphique['DTJ_DATE_DEBUT'].dt.strftime('%Y-%m-%d')
    elif nb_jours > 365:
        df_graphique['PERIODE'] = df_graphique['DTJ_DATE_DEBUT'].dt.year
    else:
        df_graphique['PERIODE'] = df_graphique['DTJ_DATE_DEBUT'].dt.strftime('%Y-%m')
        

    
    requete_unite = f"""SELECT DISTINCT T_DONNEES_DNN.DNN_NOM, T_DONNEES_DNN.DNN_UNITE
        FROM T_DONNEES_DNN
        JOIN possède ON possède.DNN_ID = T_DONNEES_DNN.DNN_ID
        JOIN T_CATEGORIE_CTG ON possède.CTG_ID = T_CATEGORIE_CTG.CTG_ID
        WHERE T_CATEGORIE_CTG.CTG_NOM LIKE '%{categorie_risque}%'"""
    df_unite = pd.read_sql(requete_unite, cnxn)
    unite = df_unite['DNN_UNITE'].iloc[0].strip() if not df_unite.empty else ""

    nom_serie = f"{titre_zone} - {categorie_risque}"
    df_resultat = df_resultat.rename(columns={'DNN_VALEUR': nom_serie})

    return df_resultat, unite, titre_zone

def histogramme(date_debut, date_fin,categorie_risque,echelle, nom_zone=None):
   
    df_graphique, titre_zone, couleur_barre = requete_sql(date_debut,date_fin,categorie_risque,echelle,nom_zone)

    df_graphique['DNN_VALEUR'] = pd.to_numeric(
        df_graphique['DNN_VALEUR'],
        errors='coerce'
    )

    #df_graphique = df_graphique.dropna()

    titre = f'Distribution de {categorie_risque} - {titre_zone} entre {date_debut} et {date_fin}'

    if df_graphique.empty:
        print("Aucune donnée trouvée pour ces critères. Génération d'une image vide.")
        
        fig, ax = plt.subplots(figsize=(6, 4))
        
        ax.text(0.5, 0.5, "Aucune donnée disponible\npour cette période", 
                horizontalalignment='center', 
                verticalalignment='center', 
                fontsize=12, 
                color='gray',
                style='italic')
        
        ax.axis('off')
        
        plt.savefig(chemin_fichier, bbox_inches='tight')
        plt.close()
        
        return df_graphique


    fig, ax = plt.subplots()

    df_graphique['DNN_VALEUR'].plot(kind='hist',bins=10,color=couleur_barre,edgecolor='black',title=titre,legend=False,ax=ax)

    ax.set_xlabel(categorie_risque)
    ax.set_ylabel("Nombre de jours")

    plt.savefig(chemin_fichier, bbox_inches="tight")
    plt.show()


    return df_graphique

if (comparaison == False):
    histogramme(start_date, end_date, risk_category, scale, zone_name)

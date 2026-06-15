import matplotlib.pyplot as plt
import pandas as pd
import sqlalchemy as sa
import os
import geopandas as gpd
import pymssql 
from IPython.display import display

psw       = "ETD"
server    = "info-mssql-etd"
user      = "ETD"
database = "MLR12345"
engine = sa.create_engine(f'mssql+pymssql://{user}:{psw}@{server}/{database}')
cnxn = engine.connect()

def generer_graphe_barres(date_debut, date_fin, categorie_risque, echelle, agregation, nom_zone=None):
    
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
        SELECT {colonne_cible} AS NOM_ZONE, DNN_VALEUR 
        
        FROM T_DONNEES_DNN 
        JOIN situé ON T_DONNEES_DNN.DNN_ID = situé.DNN_ID
        JOIN T_COMMUNE_CMN ON situé.CMN_ID = T_COMMUNE_CMN.CMN_ID
        JOIN T_DEPARTEMENT_DPT ON T_COMMUNE_CMN.DPT_ID = T_DEPARTEMENT_DPT.DPT_ID 
        JOIN déroulé ON T_DONNEES_DNN.DNN_ID = déroulé.DNN_ID
        JOIN T_DATEJOURE_DTJ ON déroulé.ID_DTJ = T_DATEJOURE_DTJ.ID_DTJ
        JOIN possède ON T_DONNEES_DNN.DNN_ID = possède.DNN_ID
        JOIN T_CATEGORIE_CTG ON possède.CTG_ID = T_CATEGORIE_CTG.CTG_ID
       
        WHERE T_CATEGORIE_CTG.CTG_NOM = '{categorie_risque}'
        AND T_DATEJOURE_DTJ.DTJ_DATE_DEBUT >= '{date_debut}'
        AND T_DATEJOURE_DTJ.DTJ_DATE_FIN <= '{date_fin}'
        {filtre_geo}
    """
    df_graphes = pd.read_sql(requete_sql, cnxn, coerce_float=False)

    # AGREGATIONS
    if agregation == 'avg':
        df_final = df_graphes.groupby('NOM_ZONE').mean().reset_index()
        graphTitle = f'Moyenne des {categorie_risque} - {titre_zone} ({date_debut} - {date_fin})'
    elif agregation == 'max':
        df_final = df_graphes.groupby('NOM_ZONE').max().reset_index()
        graphTitle = f'Plus grand {categorie_risque} - {titre_zone} ({date_debut} - {date_fin})'
    elif agregation == 'min':
        df_final = df_graphes.groupby('NOM_ZONE').min().reset_index()
        graphTitle = f'Plus petit {categorie_risque} - {titre_zone} ({date_debut} - {date_fin})'
    elif agregation == 'sum':
        df_final = df_graphes.groupby('NOM_ZONE').sum().reset_index()
        graphTitle = f'Somme des {categorie_risque} - {titre_zone} ({date_debut} - {date_fin})'
    elif agregation == 'count':
        df_final = df_graphes.groupby('NOM_ZONE').count().reset_index()
        graphTitle = f'Nombre de {categorie_risque} - {titre_zone} ({date_debut} - {date_fin})'
    
    # PLOT
    ax = df_final.plot(kind='bar', x='NOM_ZONE', y='DNN_VALEUR', title=graphTitle,legend=False, color=couleur_barre, rot=0)
    ax.set_xlabel('Zone géographique') 
    ax.set_ylabel('Valeur')

    nom_fichier = f'{categorie_risque}_{titre_zone}_{agregation}.png'
    plt.savefig(nom_fichier, bbox_inches='tight')
    plt.show()

    return nom_fichier


def generer_graphe_boite_a_moustaches(date_debut, date_fin, categorie_risque, echelle, nom_zone=None):
    
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
        SELECT {colonne_cible} AS NOM_ZONE, DNN_VALEUR 
        FROM T_DONNEES_DNN 
        JOIN situé ON T_DONNEES_DNN.DNN_ID = situé.DNN_ID
        JOIN T_COMMUNE_CMN ON situé.CMN_ID = T_COMMUNE_CMN.CMN_ID
        JOIN T_DEPARTEMENT_DPT ON T_COMMUNE_CMN.DPT_ID = T_DEPARTEMENT_DPT.DPT_ID 
        JOIN déroulé ON T_DONNEES_DNN.DNN_ID = déroulé.DNN_ID
        JOIN T_DATEJOURE_DTJ ON déroulé.ID_DTJ = T_DATEJOURE_DTJ.ID_DTJ
        JOIN possède ON T_DONNEES_DNN.DNN_ID = possède.DNN_ID
        JOIN T_CATEGORIE_CTG ON possède.CTG_ID = T_CATEGORIE_CTG.CTG_ID
        WHERE T_CATEGORIE_CTG.CTG_NOM = '{categorie_risque}'
        AND T_DATEJOURE_DTJ.DTJ_DATE_DEBUT >= '{date_debut}'
        AND T_DATEJOURE_DTJ.DTJ_DATE_FIN <= '{date_fin}'
        {filtre_geo}
    """
   
    df_graphes = pd.read_sql(requete_sql, cnxn, coerce_float=True)
    graphTitle = f'Distribution des {categorie_risque} - {titre_zone} ({date_debut} - {date_fin})'

    # PLOT
    ax = df_graphes.plot(kind='box', column='DNN_VALEUR', by='NOM_ZONE' , title=graphTitle, legend=False, color=couleur_barre, rot=0)

    
    nom_fichier = f'{categorie_risque}_{titre_zone}_box.png'
    plt.savefig(nom_fichier, bbox_inches='tight')
    plt.show()

    return nom_fichier

generer_graphe_boite_a_moustaches('20200101', '20200201', 'incendie', 'commune', 'Pessac')
generer_graphe_barres('20200101','20200201','incendie','commune', 'avg', 'Pessac')
plt.show()
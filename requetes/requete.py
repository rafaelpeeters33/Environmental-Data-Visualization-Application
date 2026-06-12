import matplotlib.pyplot as plt
import pandas as pd
import sqlalchemy as sa
import os
import geopandas as gpd
import pymssql 
from IPython.display import display

psw      = "ETD"
server   = "info-mssql-etd"
user     = "ETD"
database = "MLR12345"
engine = sa.create_engine(f'mssql+pymssql://{user}:{psw}@{server}/{database}')
cnxn = engine.connect()

def generer_graphe_barres(date_debut, date_fin, categorie_risque, echelle, agregation):
    if echelle == 'region':
        colonne_cible = 'Nouvelle-Aquitaine' 
        jointure_sup = ""
    elif echelle == 'departement':
        colonne_cible = "dpt.DPT_NOM"
        jointure_sup = "JOIN T_DEPARTEMENT_DPT dpt ON c.DPT_ID = dpt.DPT_ID"
    else:
        colonne_cible = "c.CMN_NOM"
        jointure_sup = ""
    
    requete_sql = f"""
        SELECT {colonne_cible} AS NOM_ZONE, d.DNN_VALEUR
        FROM T_DONNEES_DNN d
        JOIN situé s ON d.DNN_ID = s.DNN_ID
        JOIN T_COMMUNE_CMN c ON s.CMN_ID = c.CMN_ID
        {jointure_sup}
        JOIN déroulé der ON d.DNN_ID = der.DNN_ID
        JOIN T_DATEJOURE_DTJ dtj ON der.ID_DTJ = dtj.ID_DTJ
        JOIN possède p ON d.DNN_ID = p.DNN_ID
        JOIN T_CATEGORIE_CTG cat ON p.CTG_ID = cat.CTG_ID
        WHERE cat.CTG_NOM = '{categorie_risque}'
        AND dtj.DTJ_DATE_DEBUT >= '{date_debut}'
        AND dtj.DTJ_DATE_FIN <= '{date_fin}'
    """

    df_graphes = pd.read_sql(requete_sql, cnxn, coerce_float=False)

    if agregation == 'avg':
        df_final = df_graphes.groupby('NOM_ZONE').mean().reset_index()
        titre = 'moyenne'
    elif agregation == 'max':
        df_final = df_graphes.groupby('NOM_ZONE').max().reset_index()
        titre = 'maximum'
    elif agregation == 'min':
        df_final = df_graphes.groupby('NOM_ZONE').min().reset_index()
        titre = 'minimum'
    elif agregation == 'sum':
        df_final = df_graphes.groupby('NOM_ZONE').sum().reset_index()
        titre = 'somme'
    elif agregation == 'count':
        df_final = df_graphes.groupby('NOM_ZONE').count().reset_index()
        titre = 'nombre'
    
    ax = df_final.plot(
        kind='bar', 
        x='NOM_ZONE', 
        y='DNN_VALEUR', 
        title=f'{titre} des {categorie_risque} en {echelle} ({date_debut} - {date_fin})',
        legend=False
    )
    
    nom_fichier = f'{categorie_risque}_{echelle}.png'
    plt.savefig(nom_fichier, bbox_inches='tight')
    plt.show()

    return nom_fichier

generer_graphe_barres('20200101','20200201','incendie','Gironde', 'sum')
plt.show()





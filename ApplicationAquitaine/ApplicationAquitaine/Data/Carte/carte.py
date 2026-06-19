import geopandas as gpd
import matplotlib.pyplot as plt
import pymssql
import pandas as pd
import sqlalchemy as sa
import sys 
import os

engine = sa.create_engine(f'mssql+pymssql://etd15:teap227q@info-mssql-etd/BD_E15_VISU')
cnxn = engine.connect()
conn = pymssql.connect (server = 'info-mssql-etd', user = 'etd15', password = 'teap227q', database = 'BD_E15_VISU')
cursor = conn.cursor(as_dict=True)

zone_name = str(sys.argv[1])
end_date = str(sys.argv[2])
risk_category = str(sys.argv[3])
scale = str(sys.argv[4])
zone_name = str(sys.argv[5])
comparaison = sys.argv[6].strip().lower() == "true"
chemin_fichier = os.path.join(os.path.dirname(os.path.abspath(__file__)), 'setting.png')

dossier_script = os.path.dirname(os.path.abspath(__file__))
chemin_geojson = os.path.join(dossier_script, "communes-nouvelle-aquitaine.geojson")
dataframe_carte = gpd.read_file(chemin_geojson)




def nomToID(name):
    print(f"Recherche du département pour : {repr(name)}")
    request = (f"""SELECT DPT_ID
               FROM T_DEPARTEMENT_DPT
               WHERE DPT_NOM = '{name}'""")
    cursor.execute(request)
    data = cursor.fetchone()
    if data is None:
        print(f"Aucun département trouvé pour {repr(name)}")
        return None
    return data['DPT_ID']



def donnees():
    df_donnee=pd.read_sql("SELECT * FROM T_DONNEES_DNN",cnxn)
    df_dep=pd.read_sql("SELECT * FROM T_DEPARTEMENT_DPT",cnxn)
    df_SITUE=pd.read_sql("SELECT * FROM SITUÉ",cnxn)
    df_com=pd.read_sql("SELECT * FROM T_COMMUNE_CMN",cnxn)
    df_sit=pd.read_sql("SELECT * FROM POSSÈDE",cnxn)
    df_cat=pd.read_sql("SELECT * FROM T_CATEGORIE_CTG ",cnxn)
    df=df_donnee.merge(df_SITUE, on='DNN_ID')
    df=df.merge(df_com, on='CMN_ID')
    df=df.merge(df_dep, on='DPT_ID')
    df=df.merge(df_sit, on='DNN_ID')
    df=df.merge(df_cat, on='CTG_ID')
    return df





def afficherCarte(type, zone=None):
    print("1 - entrée dans afficherCarte")
    
    request = (f"""SELECT CTG_ID FROM T_CATEGORIE_CTG WHERE LOWER(CTG_NOM) = LOWER('{type}')""")
    cursor.execute(request)
    result = cursor.fetchone()
    print(f"2 - résultat requête CTG : {result}")
    if result is None:
        print("STOP : catégorie introuvable")
        return
    type = result['CTG_ID']

    dataframe_donnees = donnees()
    print(f"3 - taille donnees avant filtre CTG : {len(dataframe_donnees)}")
    
    dataframe_donnees = dataframe_donnees[dataframe_donnees['CTG_ID'] == type]
    print(f"4 - taille après filtre CTG : {len(dataframe_donnees)}")
    
    if dataframe_donnees.empty:
        print("STOP : dataframe vide après filtre CTG")
        return

    if zone is not None:
        id_zone = nomToID(zone)
        print(f"5 - id_zone : {id_zone}")
        if id_zone is None:
            print("STOP : zone introuvable")
            return
        dataframe_donnees = dataframe_donnees[dataframe_donnees['DPT_ID'] == id_zone]
        print(f"6 - taille après filtre zone : {len(dataframe_donnees)}")

    print("7 - on arrive au merge")
   


afficherCarte(risk_category,zone_name)
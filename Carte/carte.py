import geopandas as gpd
import matplotlib.pyplot as plt
import pymssql
import pandas as pd
import sqlalchemy as sa

engine = sa.create_engine(f'mssql+pymssql://etd15:teap227q@info-mssql-etd/BD_E15_VISU')
cnxn = engine.connect()
conn = pymssql.connect (server = 'info-mssql-etd', user = 'etd15', password = 'teap227q', database = 'BD_E15_VISU')
cursor = conn.cursor(as_dict=True)

def nomToID(name) :
    request = (f"""SELECT DPT_ID
               FROM T_DEPARTEMENT_DPT
               WHERE DPT_NOM = '{name}'""")
    cursor.execute(request)
    data = cursor.fetchone()
    return data['DPT_ID']




def donnees():
    df_donnee=pd.read_sql("SELECT * FROM T_DONNEES_DNN",cnxn)
    df_dep=pd.read_sql("SELECT * FROM T_DEPARTEMENT_DPT",cnxn)
    df_SITUE=pd.read_sql("SELECT * FROM SITUÉ",cnxn)
    df_com=pd.read_sql("SELECT * FROM T_COMMUNE_CMN",cnxn)
    df_sit=pd.read_sql("SELECT * FROM POSSÈDE",cnxn)
    df_cat=pd.read_sql("SELECT * FROM T_CATEGORIE_CTG",cnxn)
    df=df_donnee.merge(df_SITUE, on='DNN_ID')
    df=df.merge(df_com, on='CMN_ID')
    df=df.merge(df_dep, on='DPT_ID')
    df=df.merge(df_sit, on='DNN_ID')
    df=df.merge(df_cat, on='CTG_ID')
    return df





def afficherCarte(type, zone=None) :
    request = (f"""SELECT CTG_ID
               FROM T_CATEGORIE_CTG
               WHERE CTG_NOM = '{type}'""")
    cursor.execute(request)
    type = cursor.fetchone()['CTG_ID']
    dataframe_carte = gpd.read_file("communes-nouvelle-aquitaine.geojson")
    dataframe_carte.crs
    dataframe_donnees=donnees()
    dataframe_donnees=dataframe_donnees[dataframe_donnees['CTG_ID']==type]
    dataframe_donnees['DPT_ID'] = dataframe_donnees['DPT_ID'].astype(int)
    dataframe_donnees['CMN_ID'] = dataframe_donnees['CMN_ID'].astype(int)
    if(zone!=None):
        dataframe_donnees=dataframe_donnees[dataframe_donnees['DPT_ID']==nomToID(zone)]
    if dataframe_donnees.empty:
        print(f"Aucune donnée trouvée pour {zone} avec les critères fournis.")
        return
    dataframe_carte['code'] = dataframe_carte['code'].astype(int)
    stations_merge = dataframe_carte.merge(dataframe_donnees, left_on='code', right_on='CMN_ID')
    fig, ax = plt.subplots(figsize=(10, 10))
    stations_merge.plot(
        column="DNN_VALEUR", 
        ax=ax, 
        cmap='OrRd',
        legend=True,
        missing_kwds={'color': 'lightgrey'}, 
        edgecolor='black', 
        linewidth=0.3
    )
    plt.show()
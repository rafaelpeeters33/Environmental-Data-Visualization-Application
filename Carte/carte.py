import geopandas as gpd
import matplotlib.pyplot as plt
import pymssql
import pandas as pd
import sqlalchemy as sa
from matplotlib import colormaps
import IPython.display
import requete


list(colormaps)

engine = sa.create_engine(f'mssql+pymssql://etd15:teap227q@info-mssql-etd/BD_E15_VISU')
cnxn = engine.connect()
conn = pymssql.connect (server = 'info-mssql-etd', user = 'etd15', password = 'teap227q', database = 'BD_E15_VISU')
cursor = conn.cursor(as_dict=True)





stations2 = gpd.read_file("departements-nouvelle-aquitaine.geojson")
stations2.crs

stations3 = gpd.read_file("region-nouvelle-aquitaine.geojson")
stations3.crs

def carte_region_detaillee():
    fig, ax = plt.subplots(figsize=(10, 10))
    stations.plot(ax=ax, color='lightblue', edgecolor='white', linewidth=0.3)
    stations2.plot(ax=ax, facecolor='none', edgecolor='black', linewidth=1.5, cmap="binary",legend=True)
    plt.show()

#carte_region_detaillee()

def carte_communes_du_dep(nom_dep):
    fig, ax = plt.subplots(figsize=(8, 8))
    department = stations2.query(f"nom == '{nom_dep}'")
    communes_du_dep = gpd.sjoin(stations, department, predicate='within')
    communes_du_dep.plot(ax=ax, color='lightgreen', edgecolor='white', linewidth=0.5)
    department.plot(ax=ax, facecolor='none', edgecolor='darkgreen', linewidth=2, cmap="binary",legend=True)
    plt.show()

#carte_communes_du_dep('Lot-et-Garonne')

def carte_communes(nom_com):
    fig, ax = plt.subplots(figsize=(8, 8))
    municipality = stations.query(f"nom == '{nom_com}'")
    municipality.plot(ax=ax, color='lightgreen', edgecolor='darkgreen', linewidth=2, cmap="binary",legend=True)
    plt.show()

#carte_communes('Agen')

def dataPlaces(places,type,timeStart,timeEnd):
    df=pd.read_sql("""SELECT * 
               FROM T_DONNEES_DNN
               INNER JOIN SITUÉ ON T_DONNEES_DNN.DNN_ID = SITUÉ.CMN_ID
               INNER JOIN POSSÈDE ON T_DONNEES_DNN.DNN_ID = POSSÈDE.CTG_ID
               INNER JOIN DÉROULÉ ON T_DONNEES_DNN.DNN_ID = DÉROULÉ.ID_DTJ
               INNER JOIN T_COMMUNE_CMN ON T_COMMUNE_CMN.CMN_ID = SITUÉ.CMN_ID
               INNER JOIN T_DATEJOURE_DTJ ON T_DATEJOURE_DTJ.ID_DTJ = DÉROULÉ.ID_DTJ
               INNER JOIN T_CATEGORIE_CTG ON T_CATEGORIE_CTG.CTG_ID = POSSÈDE.CTG_ID""")
    
    if (places == "Nouvelle-Aquitaine"):
        request_dep = ("""SELECT DPT_NOM
               FROM T_DEPARTEMENT_DPT""")
        cursor.execute(request_dep)
        dep = cursor.fetchall()
        muni = []
        for i in range(len(dep)):
            request_muni = ("""SELECT CMN_NOM
                        FROM T_COMMUNE_CMN
                        WHERE DPT_ID (SELECT DPT_ID FROM T_DEPARTEMENT_DPT WHERE DPT_NOM = '{data[i]['DPT_NOM']}')""")
            cursor.execute(request_muni)
            muni.append(cursor.fetchall())
        for j in range(len(muni)):
            request = ("""SELECT * 
               FROM T_DONNEES_DNN
               INNER JOIN SITUÉ ON T_DONNEES_DNN.DNN_ID = SITUÉ.CMN_ID
               INNER JOIN POSSÈDE ON T_DONNEES_DNN.DNN_ID = POSSÈDE.CTG_ID
               INNER JOIN DÉROULÉ ON T_DONNEES_DNN.DNN_ID = DÉROULÉ.ID_DTJ
               INNER JOIN T_COMMUNE_CMN ON T_COMMUNE_CMN.CMN_ID = SITUÉ.CMN_ID
               INNER JOIN T_DATEJOURE_DTJ ON T_DATEJOURE_DTJ.ID_DTJ = DÉROULÉ.ID_DTJ
               INNER JOIN T_CATEGORIE_CTG ON T_CATEGORIE_CTG.CTG_ID = POSSÈDE.CTG_ID
               WHERE CMN_NOM = '{muni[j]['CMN_NOM']}'
               AND CGT_NOM = '{type}'
               AND DTJ_DATE_DEBUT >= '{timeStart}'
               AND DTJ_DATE_DEBUT <= '{timeEnd}'""")
            df = request.pd.read_sql()

    elif (isDep(places)):
        request_muni = (f"""SELECT CMN_NOM
                        FROM T_COMMUNE_CMN
                        WHERE DPT_ID (SELECT DPT_ID FROM T_DEPARTEMENT_DPT WHERE DPT_NOM = '{places}')""")
        cursor.execute(request_muni)
        muni = cursor.fetchall()
        #for j in range(len(muni)):
        request = (f"""SELECT * 
               FROM T_DONNEES_DNN
               INNER JOIN SITUÉ ON T_DONNEES_DNN.DNN_ID = SITUÉ.CMN_ID
               INNER JOIN POSSÈDE ON T_DONNEES_DNN.DNN_ID = POSSÈDE.CTG_ID
               INNER JOIN DÉROULÉ ON T_DONNEES_DNN.DNN_ID = DÉROULÉ.ID_DTJ
               INNER JOIN T_COMMUNE_CMN ON T_COMMUNE_CMN.CMN_ID = SITUÉ.CMN_ID
               INNER JOIN T_DATEJOURE_DTJ ON T_DATEJOURE_DTJ.ID_DTJ = DÉROULÉ.ID_DTJ
               INNER JOIN T_CATEGORIE_CTG ON T_CATEGORIE_CTG.CTG_ID = POSSÈDE.CTG_ID
               WHERE CMN_NOM in ({request_muni})
               
               AND CGT_NOM = '{type}'
               AND DTJ_DATE_DEBUT >= '{timeStart}'
               AND DTJ_DATE_DEBUT <= '{timeEnd}'""")
        df = request.pd.read_sql()
            

    
    else:
        request = ("""SELECT * 
                    FROM T_DONNEES_DNN
                    INNER JOIN SITUÉ ON T_DONNEES_DNN.DNN_ID = SITUÉ.CMN_ID
                    INNER JOIN POSSÈDE ON T_DONNEES_DNN.DNN_ID = POSSÈDE.CTG_ID
                    INNER JOIN DÉROULÉ ON T_DONNEES_DNN.DNN_ID = DÉROULÉ.ID_DTJ
                    INNER JOIN T_COMMUNE_CMN ON T_COMMUNE_CMN.CMN_ID = SITUÉ.CMN_ID
                    INNER JOIN T_DATEJOURE_DTJ ON T_DATEJOURE_DTJ.ID_DTJ = DÉROULÉ.ID_DTJ
                    INNER JOIN T_CATEGORIE_CTG ON T_CATEGORIE_CTG.CTG_ID = POSSÈDE.CTG_ID
                    WHERE CMN_NOM = '{places}'
                    AND CGT_NOM = '{type}'
                    AND DTJ_DATE_DEBUT >= '{timeStart}'
                    AND DTJ_DATE_DEBUT <= '{timeEnd}'""")
        df = request.pd.read_sql()
    stations_avec_data = stations.merge(df, left_on='nom', right_on='CMN_NOM', how='left')
    return stations_avec_data


def isDep(name):
    request = (f"""SELECT DPT_NOM
               FROM T_DEPARTEMENT_DPT
               WHERE DPT_NOM = '{name}'""")
    cursor.execute(request)
    data = cursor.fetchall()
    return data==[]

def afficher_carte_coloree(df_merge):
    fig, ax = plt.subplots(figsize=(10, 10))
    
    df_merge.plot(column='valeur', 
                  ax=ax, 
                  cmap='OrRd',
                  legend=True,
                  missing_kwds={'color': 'lightgrey'}, 
                  edgecolor='white', 
                  linewidth=0.3)
    
    plt.show()

#afficher_carte_coloree(dataPlaces('AGEN','INCENDIE','18000101','20270101'))

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
    print(dataframe_donnees)
    dataframe_donnees['DPT_ID'] = dataframe_donnees['DPT_ID'].astype(int)
    dataframe_donnees['CMN_ID'] = dataframe_donnees['CMN_ID'].astype(int)
    if(zone!=None):
        print('dep')
        dataframe_donnees=dataframe_donnees[dataframe_donnees['DPT_ID']==nomToID(zone)]


    print("dataframe_donnees :")
    print(dataframe_donnees)
    
    if dataframe_donnees.empty:
        print(f"Aucune donnée trouvée pour {zone} avec les critères fournis.")
        return
    
    
    dataframe_carte['code'] = dataframe_carte['code'].astype(int)


    print("station :")
    print(dataframe_carte)


    stations_merge = dataframe_carte.merge(dataframe_donnees, left_on='code', right_on='CMN_ID')

    print("stations_merge :")
    print(stations_merge)
    
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

afficherCarte("INCENDIE")
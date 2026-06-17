import matplotlib.pyplot as plt
import pandas as pd
import sqlalchemy as sa
import os
import geopandas as gpd
import sys
import pymssql 
from IPython.display import display

psw      = "teap227q"
server   = "info-mssql-etd"
user     = "etd15"
database = "BD_E15_VISU"
engine = sa.create_engine(f'mssql+pymssql://{user}:{psw}@{server}/{database}')
cnxn = engine.connect()

start_date = '20200101' #str(sys.argv[1])
end_date = '20260101'#str(sys.argv[2])
risk_category = 'no2' #str(sys.argv[3])
scale = 'departement' #str(sys.argv[4])
aggregation = 'sum' #str(sys.argv[5])
zone_name = 'Gironde' #str(sys.argv[6])


    
# SCALE
if scale == 'region':
    target_column = f"* AS NOM_ZONE" 
    #geo_filter = ""
    zone_title = "Nouvelle-Aquitaine"
elif scale == 'departement':
    target_column = f"DPT_NOM AS NOM_ZONE"
    geo_filter = f"AND T_DEPARTEMENT_DPT.DPT_NOM LIKE '{zone_name}'"
    zone_title = zone_name
else: 
    target_column = f"CMN_NOM AS NOM_ZONE"
    geo_filter = f"AND T_COMMUNE_CMN.CMN_NOM LIKE '{zone_name}'"
    zone_title = zone_name

# COLOR
if risk_category.lower() == 'incendie':
    bar_color = "#F10C0C"
elif risk_category.lower() == 'inondation':
    bar_color = '#1E88E5' 
else:
    bar_color = "#DF319F" 

# REQUETE 
requete_sql = f"""
    SELECT {target_column}, DNN_VALEUR,DTJ_DATE_DEBUT,DTJ_DATE_FIN
        
    from T_DONNEES_DNN
    Join DÉROULÉ ON T_DONNEES_DNN.DNN_ID = DÉROULÉ.DNN_ID
    join T_DATEJOURE_DTJ ON DÉROULÉ.ID_DTJ = T_DATEJOURE_DTJ.ID_DTJ
    join POSSÈDE on T_DONNEES_DNN.DNN_ID = POSSÈDE.DNN_ID
    join T_CATEGORIE_CTG On T_CATEGORIE_CTG.CTG_ID = POSSÈDE.CTG_ID
    join SITUÉ ON SITUÉ.DNN_ID = T_DONNEES_DNN.DNN_ID
    join T_COMMUNE_CMN ON SITUÉ.CMN_ID = T_COMMUNE_CMN.CMN_ID
    join T_DEPARTEMENT_DPT ON T_COMMUNE_CMN.DPT_ID = T_DEPARTEMENT_DPT.DPT_ID 
    Where T_CATEGORIE_CTG.CTG_NOM Like '{risk_category}%'
    AND T_DATEJOURE_DTJ.DTJ_DATE_DEBUT >= '{start_date}'
    AND T_DATEJOURE_DTJ.DTJ_DATE_DEBUT <= '{end_date}'
    AND (T_DATEJOURE_DTJ.DTJ_DATE_FIN IS NULL OR T_DATEJOURE_DTJ.DTJ_DATE_FIN <= '{end_date}' )
    {geo_filter};
"""

df_graphes = pd.read_sql(requete_sql, cnxn, coerce_float=False)
df_graphes['DTJ_DATE_DEBUT'] = pd.to_datetime(df_graphes['DTJ_DATE_DEBUT'])
df_graphes['DTJ_DATE_FIN'] = pd.to_datetime(df_graphes['DTJ_DATE_FIN'])
df_graphes['DNN_VALEUR'] = pd.to_numeric(df_graphes['DNN_VALEUR'], errors='coerce')

startDate = pd.to_datetime(start_date, format='%Y%m%d')
endDate = pd.to_datetime(end_date, format='%Y%m%d')
period= (endDate-startDate).days
    
if df_graphes.empty:
    print(f"Aucune donnée trouvée pour {zone_title}.")
#On verifie si superieur a un an
if period>365:
    df_graphes['PERIODE'] = df_graphes['DTJ_DATE_DEBUT'].dt.year
elif period >31:
    df_graphes['PERIODE']=df_graphes['DTJ_DATE_DEBUT'].dt.month
else :
    df_graphes['PERIODE']= df_graphes['DTJ_DATE_DEBUT'].dt.day

        


# AGREGATIONS
if aggregation == 'avg':
    df_final = df_graphes.groupby('PERIODE')['DNN_VALEUR'].mean().reset_index()
    graphTitle = f'Moyenne des {risk_category} - {zone_title} ({start_date} - {end_date})'
elif aggregation == 'sum':
    df_final = df_graphes.groupby('PERIODE')['DNN_VALEUR'].sum().reset_index()
    graphTitle = f'Somme des {risk_category} - {zone_title} ({start_date} - {end_date})'
elif aggregation == 'count':
    df_final = df_graphes.groupby('PERIODE')['DNN_VALEUR'].count().reset_index()
    graphTitle = f'Nombre de {risk_category} - {zone_title} ({start_date} - {end_date})'
     
labels = ['Lundi', 'Mardi', 'Mercredi', 'Jeudi']


mois_fr = {1: 'Jan', 2: 'Fév', 3: 'Mar', 4: 'Avr', 5: 'Mai', 6: 'Juin', 
           7: 'Juil', 8: 'Aoû', 9: 'Sep', 10: 'Oct', 11: 'Nov', 12: 'Déc'}

if period > 365:
    labels = [str(int(annee)) for annee in df_final['PERIODE']]
elif period > 31:
    labels = [mois_fr[int(m)] for m in df_final['PERIODE']]
else:
    labels = [f"J{int(j)}" for j in df_final['PERIODE']]

# PLOT
ax = df_final.plot(
    kind='line', 
    x='PERIODE', 
    y='DNN_VALEUR', 
    marker='o',        
    linewidth=2,
    title=graphTitle,
    legend=False,
    color=bar_color 
)

plt.xticks(df_final['PERIODE'], labels=labels)

chemin_fichier = os.path.join(os.path.dirname(os.path.abspath(__file__)), 'setting.png')

plt.savefig(chemin_fichier)




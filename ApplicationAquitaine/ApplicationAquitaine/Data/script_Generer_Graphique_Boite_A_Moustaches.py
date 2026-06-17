import matplotlib.pyplot as plt
import pandas as pd
import sqlalchemy as sa
import os
import sys

psw      = "ETD"
server   = "info-mssql-etd"
user     = "ETD"
database = "BD_E15_VISU"
engine = sa.create_engine(f'mssql+pymssql://{user}:{psw}@{server}/{database}')
cnxn = engine.connect()

start_date = str(sys.argv[1])
end_date = str(sys.argv[2])
risk_category = str(sys.argv[3])
scale = str(sys.argv[4])
zone_name = str(sys.argv[5])

# ECHELLE 
if scale == 'region':
    target_column = "'Nouvelle-Aquitaine'"
    geo_filter = ""
    zone_title = "Nouvelle-Aquitaine"
elif scale == 'departement':
    target_column = "DPT_NOM"
    geo_filter = f"AND T_DEPARTEMENT_DPT.DPT_NOM LIKE '{zone_name}'"
    zone_title = zone_name
else:
    target_column = "CMN_NOM"
    geo_filter = f"AND T_COMMUNE_CMN.CMN_NOM LIKE '{zone_name}'"
    zone_title = zone_name

# REQUETE
requete_sql = f"""
    select {target_column} AS NOM_ZONE, DNN_VALEUR 
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


df_graphes = pd.read_sql(requete_sql, cnxn, coerce_float=True)
graphTitle = f'Distribution des {risk_category} - {zone_title} ({start_date} - {end_date})'

#Vérifie qu'il y a des données avant de tracer
df_graphes['DNN_VALEUR'] = pd.to_numeric(df_graphes['DNN_VALEUR'], errors='coerce')

# Retire les lignes où la conversion a échoué (valeurs non numériques)
df_graphes = df_graphes.dropna(subset=['DNN_VALEUR'])

if df_graphes.empty:
    print("Aucune donnée numérique valide après conversion.")
    sys.exit(1)


ax = df_graphes.plot(kind='box', column='DNN_VALEUR', by='NOM_ZONE',
                      title=graphTitle, legend=False, rot=0)


chemin_fichier = os.path.join(os.path.dirname(os.path.abspath(__file__)), 'setting.png')

plt.savefig(chemin_fichier)

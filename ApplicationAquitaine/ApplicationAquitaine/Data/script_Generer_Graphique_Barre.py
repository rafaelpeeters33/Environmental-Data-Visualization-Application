import matplotlib.pyplot as plt
import pandas as pd
import sqlalchemy as sa
import os
import geopandas as gpd
import sys
import pymssql 
from IPython.display import display

psw       = "teap227q"
server    = "info-mssql-etd"
user      = "etd15"
database = "BD_E15_VISU"
engine = sa.create_engine(f'mssql+pymssql://{user}:{psw}@{server}/{database}')
cnxn = engine.connect()

date_debut = int(sys.argv[1])
date_fin = int(sys.argv[2])
categorie_risque = str(sys.argv[3])
echelle = str(sys.argv[4])
agregation = str(sys.argv[5])
nom_zone = str(sys.argv[6])


    
# ECHELLE 
if echelle == 'region':
    colonne_cible = "'Nouvelle-Aquitaine'" 
    filtre_geo = ""
    titre_zone = "Nouvelle-Aquitaine"
elif echelle == 'departement':
    colonne_cible = "DPT_NOM"
    filtre_geo = f"AND T_DEPARTEMENT_DPT.DPT_NOM LIKE '{nom_zone}'"
    titre_zone = nom_zone
else: 
    colonne_cible = "CMN_NOM"
    filtre_geo = f"AND T_COMMUNE_CMN.CMN_NOM LIKE '{nom_zone}'"
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
    select {colonne_cible} AS NOM_ZONE, DNN_VALEUR 
    from T_DONNEES_DNN
    Join DÉROULÉ ON T_DONNEES_DNN.DNN_ID = DÉROULÉ.DNN_ID
    join T_DATEJOURE_DTJ ON DÉROULÉ.ID_DTJ = T_DATEJOURE_DTJ.ID_DTJ
    join POSSÈDE on T_DONNEES_DNN.DNN_ID = POSSÈDE.DNN_ID
    join T_CATEGORIE_CTG On T_CATEGORIE_CTG.CTG_ID = POSSÈDE.CTG_ID
    join SITUÉ ON SITUÉ.DNN_ID = T_DONNEES_DNN.DNN_ID
    join T_COMMUNE_CMN ON SITUÉ.CMN_ID = T_COMMUNE_CMN.CMN_ID
    join T_DEPARTEMENT_DPT ON T_COMMUNE_CMN.DPT_ID = T_DEPARTEMENT_DPT.DPT_ID 
    Where T_CATEGORIE_CTG.CTG_NOM Like '{categorie_risque}%'
    AND T_DATEJOURE_DTJ.DTJ_DATE_DEBUT >= '{date_debut}'
    AND T_DATEJOURE_DTJ.DTJ_DATE_DEBUT <= '{date_fin}'
    AND (T_DATEJOURE_DTJ.DTJ_DATE_FIN IS NULL OR T_DATEJOURE_DTJ.DTJ_DATE_FIN <= '{date_fin}' )
    {filtre_geo};
    """

df_graphes = pd.read_sql(requete_sql, cnxn, coerce_float=False)

df_graphes['DNN_VALEUR'] = pd.to_numeric(df_graphes['DNN_VALEUR'], errors='coerce')
df_graphes = df_graphes.dropna(subset=['DNN_VALEUR'])

if df_graphes.empty:
    print("Aucune donnée numérique valide trouvée en base pour ces filtres.")
    sys.exit(1)

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
ax.set_xlabel('ZONE GEOGRAPHIQUE') 
ax.set_ylabel('VALEUR')



chemin_fichier = os.path.join(os.path.dirname(os.path.abspath(__file__)), 'setting.png')

plt.savefig(chemin_fichier)
plt.show()






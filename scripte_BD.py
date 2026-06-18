import matplotlib.pyplot as plt
import pandas as pd
import sqlalchemy as sa
import os
import geopandas as gpd
import pymssql 

# on se connecte à la base 
psw      = "ETD"
server   = "info-mssql-etd"
user     = "ETD"
database = "BD_E15_VISU"
engine = sa.create_engine(f'mssql+pymssql://{user}:{psw}@{server}/{database}')
#cnxn = engine.connect()

# on récupère certaine colonne du fichier csv dans un data frame 
france_df = pd.read_csv("dossier_csv/communes-france-2025.csv", sep=",",low_memory=False,usecols=["code_insee","nom_sans_pronom","code_insee","dep_nom","dep_code","reg_nom","population", "code_postal"]) 

# on ne garde que les informations qui sont en nouvelle aquitaine 
france_df = france_df[france_df["reg_nom"] == "Nouvelle-Aquitaine"]

for index, row in france_df.iterrows():
    dep_id = row["dep_code"]
    dep_nom = row["dep_nom"]
    for ["code_insee"] in france_df.iterrows():
        cmn_id = row["code_insee"]
        cmn_nom = row["nom_sans_pronom"]
        cmn_code_postal = row["code_postal"]
        cmn_population = row["population"]


print("Import terminé !")
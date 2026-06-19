import sqlalchemy as sa
import pandas as pd
import os 
from requete import connexion

cnxn = connexion()

requete = "SELECT DPT_NOM FROM T_DEPARTEMENT_DPT"
df = pd.read_sql(requete, cnxn)

chemin_fichier = os.path.join(os.path.dirname(os.path.abspath(__file__)), 'All_Dep.txt')
    
with open(chemin_fichier, 'w', encoding='utf-8') as f:
    for val in df['DPT_NOM']:
        f.write(val.strip() + '\n')
import sys
import pandas as pd
import sqlalchemy as sa
from requete import connexion

cnxn = connexion()

nom_dpt = str(sys.argv[1])

requete = f"""
        SELECT T_COMMUNE_CMN.CMN_NOM 
        FROM T_COMMUNE_CMN
        JOIN T_DEPARTEMENT_DPT ON T_COMMUNE_CMN.DPT_ID = T_DEPARTEMENT_DPT.DPT_ID
        WHERE T_DEPARTEMEN T_DPT.DPT_NOM = '{nom_dpt}'
    """
df = pd.read_sql(requete, cnxn)

chemin_fichier = os.path.join(os.path.dirname(os.path.abspath(__file__)), 'Dep_All_Com.txt')
with open(chemin_fichier, 'w', encoding='utf-8') as f:
    for val in df['CMN_NOM']: 
        print(val.strip())
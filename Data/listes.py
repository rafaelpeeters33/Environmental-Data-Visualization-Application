
import sys
import pandas as pd
import sqlalchemy as sa

psw      = "ETD"
server   = "info-mssql-etd"
user     = "ETD"
database = "BD_E15_VISU"
engine = sa.create_engine(f'mssql+pymssql://{user}:{psw}@{server}/{database}')
cnxn = engine.connect()

fonction = str(sys.argv[1])

def get_departements():
    requete = "SELECT DPT_NOM FROM T_DEPARTEMENT_DPT"
    df = pd.read_sql(requete, cnxn)



    chemin_fichier = os.path.join(os.path.dirname(os.path.abspath(__file__)), 'All_Dep.txt')
    
    with open(chemin_fichier, 'w', encoding='utf-8') as f:
        for val in df['DPT_NOM']:
            f.write(val.strip() + '\n')

def get_communes(nom_dpt):
    requete = f"""
        SELECT T_COMMUNE_CMN.CMN_NOM 
        FROM T_COMMUNE_CMN
        JOIN T_DEPARTEMENT_DPT ON T_COMMUNE_CMN.DPT_ID = T_DEPARTEMENT_DPT.DPT_ID
        WHERE T_DEPARTEMENT_DPT.DPT_NOM = '{nom_dpt}'
    """
    df = pd.read_sql(requete, cnxn)
    for val in df['CMN_NOM']: 
        print(val.strip())

if fonction == "get_departements" : 
    get_departements()

elif fonction == "get_communes" :
    dep = str(sys.argv[2])
    get_communes(dep)









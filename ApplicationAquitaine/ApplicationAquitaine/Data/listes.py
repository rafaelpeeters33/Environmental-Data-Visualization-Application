import sys
import pandas as pd
import sqlalchemy as sa

psw      = "ETD"
server   = "info-mssql-etd"
user     = "ETD"
database = "MLR12345"
engine = sa.create_engine(f'mssql+pymssql://{user}:{psw}@{server}/{database}')
cnxn = engine.connect()

def get_departements():
    requete = "SELECT DPT_NOM FROM T_DEPARTEMENT_DPT"
    df = pd.read_sql(requete, cnxn)
    for val in df['DPT_NOM']: 
        print(val.strip())

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


if __name__ == '__main__':
    commande_recue = input().strip()
    if commande_recue == "departements":
        get_departements()
    elif commande_recue.startswith("communes|"):
        nom_departement_choisi = commande_recue.split('|')[1]
        get_communes(nom_departement_choisi)
 
        #creer csv ce serait cool
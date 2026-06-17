
import os
import pandas as pd
import sqlalchemy as sa

psw      = "ETD"
server   = "info-mssql-etd"
user     = "ETD"
database = "BD_E15_VISU"
engine = sa.create_engine(f'mssql+pymssql://{user}:{psw}@{server}/{database}')
cnxn = engine.connect()


request = "SELECT CTG_NOM FROM T_CATEGORIE_CTG"
df = pd.read_sql(request, cnxn)



file_path = os.path.join(os.path.dirname(os.path.abspath(__file__)), 'All_CTG.txt')
    
with open(file_path, 'w', encoding='utf-8') as f:
    for val in df['CTG_NOM']:
        f.write(val.strip() + '\n')











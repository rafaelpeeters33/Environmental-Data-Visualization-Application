import pymssql
import pandas as pd
import matplotlib.pyplot as plt
import os
import sys

psw      = "ETD"
server   = "info-mssql-etd"
user     = "ETD"
database = "test_E15"
cnxn =  pymssql.connect (server = server, user = user,      
                        password = psw, database = database)

cursor =  cnxn.cursor()

res = str(sys.argv[1])

cursor.execute("SELECT * FROM T_A WHERE A_NOM LIKE %s", (res + '%',))

rows = cursor.fetchall()  # récupère toutes les lignes d'un coup
x = []
y = []
for row in rows:
    x.append(row[0])
    y.append(row[2])


dossier_script = os.path.dirname(os.path.abspath(__file__))
chemin_image = os.path.join(dossier_script, 'test.png')

plt.plot(x,  y, '-o')

plt.savefig(chemin_image)


plt.show()




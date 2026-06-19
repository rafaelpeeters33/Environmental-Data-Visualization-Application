import pymssql
import matplotlib.pyplot as plt
import os
import sys

# permet de se connecté à la base SQL
psw      = "ETD"
server   = "info-mssql-etd"
user     = "ETD"
database = "test_E15"
cnxn =  pymssql.connect (server = server, user = user,      
                        password = psw, database = database)

# permet d'executer des requête et de prendre le résultat 
cursor =  cnxn.cursor()

#s'il n'y a pas d'argument ( sécurité ) on qui le scripte 
if len(sys.argv) < 2:
    print("Erreur : aucun argument fourni")
    sys.exit(1)

# sinon on prend le 1er argument et on le met dans res (l'argument d'indice 0 est le nom du scripte)
res = str(sys.argv[1])

#on execute une requête 
cursor.execute("SELECT * FROM T_A WHERE A_NOM LIKE %s", (res + '%',))
 # on récupère toutes les lignes d'un coup
rows = cursor.fetchall() 

# et on remplie les listes pour l'axe des x et des y 
x = []
y = []
for row in rows:
    x.append(row[0])
    y.append(row[2])

# permet de construire le chemin absolue :
# __file__ est le chemin du scripte 
# os.path.abspath permet de transformé le chemin en chemin absolue ( pour être sur)
# os.path.dirname permet d'avoir le chemin jusqu'au dossier où est enregistrer le scripte 
dossier_script = os.path.dirname(os.path.abspath(__file__))
# os.path.join(dossier_script, 'test.png') permet de mettre le nom test.png à la fin du chemin 
chemin_image = os.path.join(dossier_script, 'test.png')

# on crée le graphique 
plt.plot(x,  y, '-o')

# on l'enregistre 
plt.savefig(chemin_image)





import os 
import gzip
for file in os.listdir("dossier_csv") :
    if file.endswith("csv.gz") :
        with gzip.open("dossier_csv/" + file, 'rt') as f:
            data = f.read()
        with open("dossier_csv/" + file[:-3], 'wt') as f:
            f.write(data)
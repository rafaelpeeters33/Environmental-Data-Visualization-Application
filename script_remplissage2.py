import pymssql
import pandas as pd
import os

cnxn = pymssql.connect ( server = 'info-mssql-etd', user = 'ETD',password = 'ETD', database = 'BD_E15_VISU' )
cursor = cnxn.cursor(as_dict=True)

liste = {"RR" : ["PRECIPITATION","mm"],
         "TN" : ["TEMP_MIN","°C"], 
         "TX" : ["TEMP_MAX","°C"], 
         "TM" : ["TEMP_MOY","°C"], 
         "FXI" : ["MOY_VIT_VENT","km/h"]}



def indexDep() :
    cursor.execute("INSERT INTO T_DONNEES_DNN (DNN_NOM,DNN_UNITE,DNN_VALEUR) VALUES ('-1','-1',-1)")
    cnxn.commit()
    donne_id="(select DNN_ID FROM T_DONNEES_DNN where DNN_NOM='-1' and DNN_UNITE='-1' and DNN_VALEUR=-1)"
    cursor.execute(donne_id)
    dataIndex=cursor.fetchone()['DNN_ID']
    cursor.execute("DELETE FROM T_DONNEES_DNN WHERE(DNN_NOM = '-1')")
    cnxn.commit()
    return dataIndex



def verifeData(Date) :
    cursor.execute("select ID_DTJ from T_DATEJOURE_DTJ where DTJ_DATE_DEBUT='%s'"%(Date))
    return cursor.fetchall()==[]





def remplirMeteo(indexDep) :
    dataIndex=indexDep
    listecol2 = ["NOM_USUEL",
                "AAAAMMJJ",
                "RR", "QRR",
                "TN", "QTN",
                "TX", "QTX",
                "TM", "QTM",
                "FXI", "QFXI"]

    for file in os.listdir("dossier_csv") :
        if file.startswith("Q") : 
            fichier2 = pd.read_csv("dossier_csv/" + file, sep=';',usecols=listecol2)
            df_fichier2 = fichier2[fichier2["AAAAMMJJ"].astype(str).str.endswith('01')]
            for index, row in df_fichier2.iterrows() :
                commune_id="(select CMN_ID from T_COMMUNE_CMN where CMN_NOM='%s')" % (row["NOM_USUEL"].replace("'","*"))
                cursor.execute(commune_id)
                a=cursor.fetchall()
                if a!=[] : 
                    if len(a)==1 and row["NOM_USUEL"]!=None and row["AAAAMMJJ"]!=None:
                        for col in liste.keys() : 
                            if row["Q"+col]==1  or row[col]!='':
                                request1="INSERT INTO T_DONNEES_DNN (DNN_nom,DNN_unite,DNN_valeur) VALUES('%s', '%s', '%s')" % (liste[col][0],liste[col][1],row[col])
                                cursor.execute(request1)

                                if verifeData(row["AAAAMMJJ"]) :
                                    request4="INSERT INTO T_DATEJOURE_DTJ(DTJ_DATE_DEBUT) VALUES('%s')" % (row["AAAAMMJJ"])
                                    cursor.execute(request4)
                                
                                dataIndex+=1
                                
                                request2="INSERT INTO SITUÉ (DNN_ID,CMN_ID) VALUES(%s,%s)" % (dataIndex, commune_id)
                                cursor.execute(request2)
                                
                                
                                DateIndex=("(select ID_DTJ from T_DATEJOURE_DTJ where DTJ_DATE_DEBUT='%s')")%(row["AAAAMMJJ"])
                                cursor.execute(DateIndex)
                                a=cursor.fetchall()

                                request3="INSERT INTO DÉROULÉ (DNN_ID,ID_DTJ) VALUES (%s,%s)" % (dataIndex, a[0]['ID_DTJ'])
                                cursor.execute(request3)


                                mesure_id="(select CTG_ID from T_CATEGORIE_CTG where CTG_NOM='%s')"%(liste[col][0])
                                request5="INSERT INTO POSSÈDE(CTG_ID,DNN_ID) VALUES (%s,%s)" % (mesure_id,dataIndex)
                                cursor.execute(request5)
                                
                                cnxn.commit()


         



remplirMeteo(indexDep())


cnxn.close()

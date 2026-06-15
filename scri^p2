import pymssql
import pandas as pd
import os

cnxn = pymssql.connect ( server = 'info-mssql-etd', user = 'ETD',password = 'ETD', database = 'BD_E15_VISU' )
cursor = cnxn.cursor(as_dict=True)
dataList={}
indice_Nouvelle_Aquitaine_df = pd.read_csv("dossier_csv/ind_nouvelle_aquitaine.csv", sep=",",low_memory=False,usecols=["date_ech","code_qual","type_zone","code_zone",
                                                                                                    "code_no2","code_so2","code_o3","code_pm10","code_pm25"])
# incendie_insert = "INSERT INTO T_CATEGORIE_CTG (CTG_NOM) VALUES('INCENDIE')"
# cursor.execute(incendie_insert)
# inon_insert = "INSERT INTO T_CATEGORIE_CTG (CTG_NOM) VALUES('RISQUE_INONDATION')"
# cursor.execute(inon_insert)
dic_pol = { "no2" : ["code_no2", "polluant dioxyde d'azote", "0 - 7"],
        "so2" : ["code_so2", "polluant dioxyde de soufre", "0 - 7"],
        "o3" : ["code_o3", "polluant ozone", "0 - 7"],
        "pm10" : ["code_pm10", "particules fines", "micromètre"],
        "pm25" : ["code_pm25","particules très fines","micromètre"]}
# for pol in dic_pol.keys() :
#     pol_insert = "INSERT INTO T_CATEGORIE_CTG (CTG_NOM) VALUES('%s')"%(pol)
#     cursor.execute(pol_insert)
# cnxn.commit()

# cursor.execute("""select * from T_CATEGORIE_CTG""")
# row = cursor.fetchall()
# print(row)
# while row :
#     print(row)
#     row = cursor.fetchone()




listeDate = []
france_df = pd.read_csv("dossier_csv/communes-france-2025.csv", sep=",",low_memory=False,usecols=["code_insee","nom_sans_pronom","dep_nom","dep_code","reg_nom","population", "code_postal"]) 

france_df = france_df[france_df["reg_nom"] == "Nouvelle-Aquitaine"]
liste_dep = []
liste_cmn = []
for index, row in france_df.iterrows():
    dep_id = row["dep_code"]
    dep_nom = row["dep_nom"]
    cmn_id = row["code_insee"]
    cmn_nom = row["nom_sans_pronom"]
    cmn_code_postal = row["code_postal"]
    cmn_population = row["population"]
    if dep_nom not in liste_dep :
        requestDep="INSERT INTO T_DEPARTEMENT_DPT(DPT_ID, DPT_NOM) VALUES (%s, '%s')" % (dep_id,dep_nom)
        liste_dep.append(dep_id)
        #cursor.execute(requestDep)
    if cmn_id not in liste_cmn :
        requestCom="INSERT INTO T_COMMUNE_CMN(CMN_ID, DPT_ID, CMN_NOM, CMN_COD_POSTAL, CMN_POPULATION) VALUES (%s, %s, '%s', %s, %s)" % (cmn_id, dep_id, cmn_nom.replace("'","*"), cmn_code_postal, cmn_population)
        liste_cmn.append(cmn_id)
    #cursor.execute(requestCom)
    #cnxn.commit()
listeCol = ["Code INSEE",
           "Nom de la commune",
           "Date de première alerte",
           "Surface parcourue (m2)",
           "Nature"]

fichier = pd.read_csv("dossier_csv/Incendies.csv",sep=';',usecols=listeCol)
fichierIncendie = fichier.merge(france_df, how='inner', right_on='code_insee', left_on='Code INSEE')





data=""
j=0

for index, row in fichierIncendie.iterrows() :  
    dataList[j]=[]  
    if row["Code INSEE"] in liste_cmn :
        surface = row["Surface parcourue (m2)"]
        if surface not in dataList.values() :
            request1="INSERT INTO T_DONNEES_DNN (DNN_NOM,DNN_UNITE,DNN_VALEUR) VALUES ('SURFACE', 'm2', %s)" % (surface)
            cursor.execute(request1)
        dataList[j].append(surface)
        j+=1
        date = row["Date de première alerte"][:-9].replace("-", "")
        cnxn.commit()
        if date not in listeDate :
            request4="INSERT INTO T_DATEJOURE_DTJ(DTJ_DATE_DEBUT) VALUES ('%s')" % (date)
            listeDate.append(date)
            cursor.execute(request4)

# donne_id="(select TOP 1 DNN_ID from T_DONNEES_DNN)"
# cursor.execute(donne_id)
# dataIndexCursor=cursor.fetchone()
# dataIndex=dataIndexCursor['DNN_ID']
i=0
incendie =[]
situé=[]

for index, row in fichierIncendie.iterrows() :    
    if row["Code INSEE"] in liste_cmn :    
        date = row["Date de première alerte"][:-9].replace("-", "")   
        dataIndex = ("(Select DNN_ID From T_DONNEES_DNN Where DNN_NOM='SURFACE' and DNN_VALEUR=%s)") % (dataList[i][0])
        print(dataIndex)
        cursor.execute(dataIndex)
        dataIndexCursor=cursor.fetchone()
        print(dataIndexCursor)
        if (dataIndexCursor['DNN_ID'],row["Code INSEE"]) not in situé:
            request2="INSERT INTO SITUÉ (DNN_ID,CMN_ID) VALUES (%d,'%s')" % (dataIndexCursor['DNN_ID'],row["Code INSEE"])
            cursor.execute(request2)
            situé.append((dataIndexCursor['DNN_ID'],row["Code INSEE"]))
        DateIndex=("(select ID_DTJ from T_DATEJOURE_DTJ where DTJ_DATE_DEBUT='%s')")%(row["Date de première alerte"][:-9]+" 00:00:00.000")
        DateIndex=("(select ID_DTJ from T_DATEJOURE_DTJ where DTJ_DATE_DEBUT='%s')")%(date)
        if (dataIndexCursor['DNN_ID'],DateIndex) in incendie : 
            request3="INSERT INTO DÉROULÉ (DNN_ID,ID_DTJ) VALUES (%s,%s)" % (dataIndexCursor['DNN_ID'],DateIndex)
            cursor.execute(request3)
            incendie.append((dataIndexCursor['DNN_ID'],DateIndex))
        i+=1
        cnxn.commit()

dataIndex = ("(Select DNN_ID From T_DONNEES_DNN)")
cursor.execute(dataIndex)
a=cursor.fetchall()
for i in a :
    incendi_id="(select CTG_ID from T_CATEGORIE_CTG where CTG_NOM='INCENDIE')"

    request5="INSERT INTO POSSÈDE(CTG_ID,DNN_ID) VALUES (%s,%d)" % (incendi_id,i['DNN_ID'])
    cursor.execute(request5)
    cnxn.commit()




# # listecol2 = ["NOM_USUEL",
# #              "AAAAMMJJ",
# #              "RR", "QRR",
# #              "TN", "QTN",
# #              "TX", "QTX",
# #              "TM", "QTM",
# #              "FXI", "QFXI",
# #              "DRR", "QDRR"]
# # liste = {"RR" : ["PRECIPITATION","mm"],
# #          "TN" : ["TEMP_MIN","°C"], 
# #          "TX" : ["TEMP_MAX","°C"], 
# #          "TM" : ["TEMP_MOY","°C"], 
# #          "FXI" : ["MOY_VIT_VENT","km/h"], 
# #          "DRR" : ["DUREE_PRECIPITION","h"]}
# # for file in os.listdir("dossier_csv") :
# #     if file.startswith("Q") : 
# #         fichier2 = pd.read_csv("dossier_csv/" + file, sep=';',usecols=listecol2)
# #         for index, row in fichier2.iterrows() :
# #             for col in liste.keys() : 
# #                 if row["Q"+col]==1 :
# #                     request1="INSERT INTO T_DONNES_DNN (DNN_nom,DNN_unite,DNN_valeur) VALUES('%s', '%s', '%s')" % (liste[col][0],liste[col][1],row[col])
# #                     # cursor.execute(request1)
# #                     donne_id="(select top 1 from T_DONNEES_DNN order by DNN_ID)"
# #                     commune_id="(select CMN_ID from T_COMMNE_CMN where CMN_NOM='%s')" % (row["NOM_USUEL"])
# #                     request2="INSERT INTO SITUÉ (DNN_ID,CMM_ID) VALUES(%s,%s)" % (donne_id, commune_id)
# #                     # cursor.execute(request2)
# #                     request3="INSERT INTO DÉROULÉ (DNN_ID,ID_DTJ) VALUES (%s,%s)" % (donne_id, row["AAAAMMJJ"])
# #                     # cursor.execute(request3)
# #                     annee_id="(select top 1 from DÉROULÉ order by ID_DTJ)"
# #                     request4="IF NOT EXIST (SELECT DTJ_DATE_DEBUT FROM T_DATEJOURE_DTJ WHERE DTJ_DATE_DEBUT=%s) INSERT INTO T_DATEJOURE_DTJ(ID_DTJ,TDJ_DATE_DEBUT) VALUES(%s,%s)" % (row["AAAAMMJJ"], annee_id,row["AAAAMMJJ"])
# #                     # cursor.execute(request4)
# #                     cnxn.commit()

    


liste_col_inondation_risque=["cod_nat_azi",
                             "lib_azi",
                             "lib_bassin_risque",
                             "list_risques",
                             "cod_commune",
                             "lib_commune",
                             "dat_program_deb",
                             "dat_program_fin",]

listeDateDeb={}
listeDateFin={}
i=0
inondation_risque = pd.read_csv("dossier_csv/gaspar/azi_gaspar.csv", sep=';', usecols=liste_col_inondation_risque)
request1="INSERT INTO T_DONNEES_DNN (DNN_nom,DNN_unite,DNN_valeur) VALUES ('RISQUE', 'RISQUE', 'RISQUE_INONDATION')"
cursor.execute(request1)
for index, row in inondation_risque.iterrows() :
    if row["cod_commune"] in liste_cmn :
        dateDeb = str(row["dat_program_deb"])[:-9].replace("-", "")
        listeDateDeb[i]=dateDeb
        dateFin = str(row["dat_program_fin"])[:-9].replace("-", "")
        listeDateFin[i]=dateFin
        if dateDeb not in listeDate :
            request4="INSERT INTO T_DATEJOURE_DTJ(DTJ_DATE_DEBUT, DTJ_DATE_FIN) VALUES ('%s', '%s')" % (dateDeb, dateFin)
            listeDate.append(dateDeb)
            cursor.execute(request4)
        i+=1
        cnxn.commit()

i=0
for index, row in inondation_risque.iterrows() :
    if row["cod_commune"] in liste_cmn :
        dateDeb = listeDateDeb[i]
        dateFin = listeDateFin[i]
        DateIndex=("(select ID_DTJ from T_DATEJOURE_DTJ where DTJ_DATE_DEBUT='%s' and DTJ_DATE_FIN='%s')")%(dateDeb,dateFin)
        DataIndex=("(select DNN_ID from T_DONNEES_DNN where DNN_VALEUR='RISQUE_INONDATION')")
        request2="INSERT INTO SITUÉ (DNN_ID,CMN_ID) VALUES (%s,'%s')" % (DataIndex,row["cod_commune"])
        cursor.execute(request2)
        request3="INSERT INTO DÉROULÉ (DNN_ID,ID_DTJ) VALUES (%s,%s)" % (DataIndex,DateIndex)
        cursor.execute(request3)
        inon_ris_id="(select CTG_ID from T_CATEGORIE_CTG where CTG_NOM='RISQUE_INONDATION')"
        request5="INSERT INTO POSSÈDE(CTG_ID,DNN_ID) VALUES (%s,%s)" % (inon_ris_id,DataIndex)
        cursor.execute(request5)
        i+=1
        cnxn.commit()




# for index, row in indice_Nouvelle_Aquitaine_df.iterrows():
#     print()
#     if str(row["code_zone"]) in liste_cmn :
#         dnn_to_cmn = row["code_zone"]
#         date_ind = row["date_ech"][:-10].replace("-", "")

#         print(date_ind)
#         if(row["type_zone"] == "commune"):
#             for pol in dic_pol.keys() :
#                 print(pol)
#                 if "'%s', '%s', '%s'" % (dic_pol[pol][1].replace("'"," "), dic_pol[pol][2],row[dic_pol[pol][0]]) not in dataList :
#                     request1="INSERT INTO T_DONNEES_DNN (DNN_nom,DNN_unite,DNN_valeur) VALUES('%s', '%s', '%s')" % (dic_pol[pol][1].replace("'"," "), dic_pol[pol][2],row[dic_pol[pol][0]])
#                     cursor.execute(request1)
#                 dataList[j]=("'%s', '%s', '%s'" % (dic_pol[pol][1].replace("'"," "), dic_pol[pol][2],row[dic_pol[pol][0]]))
#                 j+=1
#             if date_ind not in listeDate :
#                 request4="INSERT INTO T_DATEJOURE_DTJ(DTJ_DATE_DEBUT) VALUES('%s')" % (date_ind)
#                 listeDate.append(date_ind)
#                 cursor.execute(request4)
#             cnxn.commit()

# k=j
# for index, row in indice_Nouvelle_Aquitaine_df.iterrows():
#     if str(row["code_zone"]) in liste_cmn :
#         dnn_to_cmn = row["code_zone"]
#         date_ind = row["date_ech"]
#         if(row["type_zone"] == "commune"):
#             for pol in dic_pol.keys() :
#                 dataIndex = "(Select DNN_ID from T_DONNEES_DNN where )"
#                 request2="INSERT INTO SITUÉ (DNN_ID,CMN_ID) VALUES(%s,'%s')" % (dataIndex, dnn_to_cmn)
#                 cursor.execute(request2)
#                 request3="INSERT INTO DÉROULÉ (DNN_ID,ID_DTJ) VALUES (%s,'%s')" % (dataIndex, date_ind)
#                 cursor.execute(request3)
#                 MESURE_id="(select CTG_ID from T_CATEGORIE_CTG where CTG_NOM='%s')"%(pol)
#                 request5="INSERT INTO POSSÈDE(CTG_ID,DNN_ID) VALUES (%s,%s)" % (MESURE_id,dataIndex)
#                 cnxn.commit()
cnxn.close()





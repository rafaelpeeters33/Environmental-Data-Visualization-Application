import pymssql
cnxn = pymssql.connect ( server = 'info-mssql-etd', user = 'ETD',password = 'ETD', database = 'BD_E15_VISU' )
cursor = cnxn.cursor(as_dict=True)

cursor.execute("DELETE FROM DÉROULÉ")
cursor.execute("DELETE FROM POSSÈDE")
cursor.execute("DELETE FROM SITUÉ")
cursor.execute("DELETE FROM T_DONNEES_DNN")
cursor.execute("DELETE FROM T_DATEJOURE_DTJ")
cnxn.commit()
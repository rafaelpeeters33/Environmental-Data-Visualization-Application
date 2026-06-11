import pandas as pd
import matplotlib.pyplot as plt
import pymssql

psw = input("ETD password: ")
cnxn = pymssql.connect ( server = 'info-mssql-etd', user = 'ETD',      
                        password = psw, database = 'bd' )


def generer_graphe_barres(annee_debut, annee_fin, categorie_risque, echelle):
    if echelle == 'region':
        colonne_cible = "'Nouvelle-Aquitaine'" 
        jointure_sup = ""
    elif echelle == 'departement':
        colonne_cible = "dpt.DPT_NOM"
        jointure_sup = "JOIN T_DEPARTEMENT_DPT dpt ON c.DPT_ID = dpt.DPT_ID"
    else:
        colonne_cible = "c.CMN_NOM"
        jointure_sup = ""

    requete_sql = f"""
        SELECT {colonne_cible} AS NOM_ZONE, d.DNN_VALEUR
        FROM T_DONNEES_DNN d
        JOIN situé s ON d.DNN_ID = s.DNN_ID
        JOIN T_COMMUNE_CMN c ON s.CMN_ID = c.CMN_ID
        {jointure_sup}
        JOIN déroulé der ON d.DNN_ID = der.DNN_ID
        JOIN T_DATEJOURE_DTJ dtj ON der.ID_DTJ = dtj.ID_DTJ
        JOIN possède p ON d.DNN_ID = p.DNN_ID
        JOIN T_CATEGORIE_CTG cat ON p.CTG_ID = cat.CTG_ID
        WHERE cat.CTG_NOM = '{categorie_risque}'
        AND YEAR(dtj.DTJ_DATE_DEBUT) >= {annee_debut}
        AND YEAR(dtj.DTJ_DATE_FIN) <= {annee_fin}
    """

    df_graphes = pd.read_sql(requete_sql, cnxn, coerce_float=False)
    df_final = df_graphes.groupby('NOM_ZONE').mean().reset_index()

    ax = df_final.plot(
        kind='bar', 
        x='NOM_ZONE', 
        y='DNN_VALEUR', 
        title=f'{categorie_risque} moyen par {echelle} ({annee_debut} - {annee_fin})',
        legend=False
    )
    
    nom_fichier = f'export_graphe_{categorie_risque}_{echelle}.png'
    plt.savefig(nom_fichier, bbox_inches='tight')
    plt.close() 

    return nom_fichier


def generer_nuage_points(annee_debut, annee_fin, categorie_risque, echelle):
    if echelle == 'region':
        colonne_cible = "'Nouvelle-Aquitaine'" 
        jointure_sup = ""
    elif echelle == 'departement':
        colonne_cible = "dpt.DPT_NOM"
        jointure_sup = "JOIN T_DEPARTEMENT_DPT dpt ON c.DPT_ID = dpt.DPT_ID"
    else:
        colonne_cible = "c.CMN_NOM"
        jointure_sup = ""
    requete_sql = f"""
        SELECT {colonne_cible} AS NOM_ZONE, d.DNN_VALEUR
        FROM T_DONNEES_DNN d
        JOIN situé s ON d.DNN_ID = s.DNN_ID
        JOIN T_COMMUNE_CMN c ON s.CMN_ID = c.CMN_ID
        {jointure_sup}
        JOIN déroulé der ON d.DNN_ID = der.DNN_ID
        JOIN T_DATEJOURE_DTJ dtj ON der.ID_DTJ = dtj.ID_DTJ
        JOIN possède p ON d.DNN_ID = p.DNN_ID
        JOIN T_CATEGORIE_CTG cat ON p.CTG_ID = cat.CTG_ID
        WHERE cat.CTG_NOM = '{categorie_risque}'
        AND YEAR(dtj.DTJ_DATE_DEBUT) >= {annee_debut}
        AND YEAR(dtj.DTJ_DATE_FIN) <= {annee_fin}
    """
    
    df_graphes = pd.read_sql(requete_sql, cnxn, coerce_float=False)
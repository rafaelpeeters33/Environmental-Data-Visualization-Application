from diagramme_barres import diagramme_barres, diagramme_barres_comparaison
from trace_ligne import trace_ligne, trace_ligne_comparaison
from nuage_de_points import nuage_de_points, nuage_de_points_comparaison   
from boite_moustache import boite_a_moustaches, boite_a_moustache_comparaison
from aire_empilee import trace_aires_empilees, trace_aires_empilees_comparaison
from violon import violon, violon_comparaison
#from coordonnees_paralleles import coordonnees_paralleles
from regression_lineaire import regression_lineaire

# --- FONCTION DE LÉGENDE POUR C# ---
def exporter_legende(texte):
    """Exporte le texte explicatif dans un fichier lu par l'interface C#."""
    with open('legende.txt', 'w', encoding='utf-8') as f:
        f.write(texte)

# --- SCRIPT DE TEST COMPLET ---
def tester_tous_les_graphiques():
    print("=== DÉMARRAGE DES TESTS DE GÉNÉRATION (SAÉ S2.04) ===\n")

    # Listes de test standardisées
    valeurs_agg = [
        ['19900101', '20200201', 'incendie', 'region', 'sum'],
        ['19900101', '20200201', 'incendie', 'departement', 'sum', 'Gironde'],
        ['19900101', '20200201', 'incendie', 'commune', 'sum', 'Pessac']
    ]
    valeurs_dist = [
        ['19900101', '20200201', 'incendie', 'region'],
        ['19900101', '20200201', 'incendie', 'departement', 'Gironde'],
        ['19900101', '20200201', 'incendie', 'commune', 'Pessac']
    ]
    valeurs_paralleles = [
        ['19900101', '20200201', 'incendie', 'region', 'sum'],
        ['19900101', '20200201', 'inondation', 'region', 'sum']
    ]

    # ---------------------------------------------------------
    # 🟢 FAIBLE COMPLEXITÉ
    # ---------------------------------------------------------
    print("1. BAR CHART (Simple) :")
    diagramme_barres('19900101', '20200201', 'incendie', 'region', 'sum')
    exporter_legende("Volume total des incendies en Nouvelle-Aquitaine.")
    print("-> Ce qu'il montre : Un seul bâton avec la somme des sinistres.")

    print("2. BAR CHART (Comparaison) :")
    diagramme_barres_comparaison(valeurs_agg)
    exporter_legende("Comparaison du volume d'incendies : Région, Gironde, Pessac.")
    print("-> Ce qu'il montre : Trois bâtons côte à côte pour comparer les échelles.\n")

    print("3. LINE CHART (Simple) :")
    trace_ligne('19900101', '20200201', 'incendie', 'region', 'sum')
    exporter_legende("Évolution annuelle des incendies en Nouvelle-Aquitaine.")
    print("-> Ce qu'il montre : La tendance temporelle (pics et creux) des feux.")

    print("4. LINE CHART (Comparaison) :")
    trace_ligne_comparaison(valeurs_agg)
    exporter_legende("Comparaison des évolutions : Région vs Gironde vs Pessac.")
    print("-> Ce qu'il montre : 3 courbes superposées pour voir si la commune suit la tendance régionale.\n")

    print("5. SCATTER PLOT (Nuage de points) :")
    nuage_de_points('19900101', '20200201', 'incendie', 'region', 'sum')
    exporter_legende("Dispersion temporelle des incendies en Nouvelle-Aquitaine.")
    print("-> Ce qu'il montre : Des points isolés par année pour repérer les anomalies (outliers).\n")

    # ---------------------------------------------------------
    # 🟡 COMPLEXITÉ MODÉRÉE
    # ---------------------------------------------------------
    print("6. BOX PLOT (Simple) :")
    boite_a_moustaches('19900101', '20200201', 'incendie', 'region')
    exporter_legende("Répartition statistique des incendies (Médiane et Quartiles).")
    print("-> Ce qu'il montre : La moyenne des événements et les années extrêmes (points hors de la boîte).")

    print("7. BOX PLOT (Comparaison) :")
    boite_a_moustache_comparaison(valeurs_dist)
    exporter_legende("Comparaison des disparités statistiques entre échelles.")
    print("-> Ce qu'il montre : 3 boîtes côte à côte. La boîte régionale sera très étirée, celle de Pessac très écrasée.\n")

    print("8. STACKED AREA (Simple) :")
    trace_aires_empilees('19900101', '20200201', 'incendie', 'region', 'sum')
    exporter_legende("Volume cumulé des incendies dans le temps.")
    print("-> Ce qu'il montre : Une vague rouge sous la courbe d'évolution.")

    print("9. STACKED AREA (Comparaison) :")
    trace_aires_empilees_comparaison(valeurs_agg)
    exporter_legende("Accumulation des risques par échelle territoriale.")
    print("-> Ce qu'il montre : Les aires s'empilent, utile pour voir le poids de la Gironde dans le total régional.\n")

    print("10. VIOLIN PLOT (Simple) :")
    violon('19900101', '20200201', 'incendie', 'region')
    exporter_legende("Densité de probabilité des incendies en Nouvelle-Aquitaine.")
    print("-> Ce qu'il montre : Une forme bombée là où les années ont eu un nombre similaire de sinistres.")

    print("11. VIOLIN PLOT (Comparaison) :")
    violon_comparaison(valeurs_dist)
    exporter_legende("Comparaison des densités d'événements selon la zone.")
    print("-> Ce qu'il montre : 3 violons pour comparer où se concentrent les sinistres majeurs.\n")

    # ---------------------------------------------------------
    # 🔴 HAUTE COMPLEXITÉ
    # ---------------------------------------------------------
    print("12. COORDONNÉES PARALLÈLES (Comparaison/Croisement) :")
    # Utilise la fonction de comparaison qui prend plusieurs risques
    coordonnees_paralleles_comparaison(valeurs_paralleles)
    exporter_legende("Profil des années climatiques : Incendies vs Inondations.")
    print("-> Ce qu'il montre : Les lignes qui croisent permettent de voir si une année avec beaucoup de feux a aussi beaucoup d'inondations (profil météo complet).")

    print("\n=== TESTS TERMINÉS AVEC SUCCÈS ===")

# --- Exécution ---
if __name__ == "__main__":
    tester_tous_les_graphiques()
from scripts.diagramme_barres import diagramme_barres, diagramme_barres_comparaison
from scripts.boite_moustache import boite_a_moustaches, boite_a_moustache_comparaison
from scripts.histogramme import histogramme
from scripts.nuage_de_points import nuage_de_points
from scripts.regression_lineaire import regression_lineaire
from scripts.trace_ligne import trace_ligne
from scripts.violon import violon


diagramme_barres('19900101','20201231', 'PRECIPITATION', 'region', 'avg')

boite_a_moustaches('19900101','20201231', 'TEMP_MOY', 'region')

histogramme('19900101','20201231', 'PRECIPITATION', 'region', 'avg')

regression_lineaire('19900101', '20201231', 'PRECIPITATIONS', 'TEMP_MOY', 'region', 'avg')

trace_ligne('20200101','20201231', 'TEMP_MOY', 'region', 'avg')


violon('19900101','20201231', 'TEMP_MOY', 'region')

#a faire demain adrien stppp
#nuage_de_points('19900101','20201231', 'PRECIPITATION', 'region')

/*
 ----------------------------------------------------------------------------
             Génération d'une base de données pour
                        SQL Server 2005
                       (10/6/2026 16:52:07)
 ----------------------------------------------------------------------------
     Nom de la base : BD_E15_VISU
     Projet : Espace de travail
     Auteur : CRIIUT
     Date de dernière modification : 10/6/2026 16:52:02
 ----------------------------------------------------------------------------
*/
use master
go


-- on passe en mode single user 
ALTER DATABASE BD_E15_VISU SET SINGLE_USER WITH ROLLBACK IMMEDIATE;

-- on supprime la base de donnée 
DROP DATABASE BD_E15_VISU;

--on crée un utilisateur 
create user ETD

-- on ajoute les droits 
GRANT SELECT, INSERT, UPDATE, DELETE TO ETD


/* -----------------------------------------------------------------------------
      OUVERTURE DE LA BASE BD_E15_VISU
----------------------------------------------------------------------------- */

create database BD_E15_VISU
go

use BD_E15_VISU
go



/* -----------------------------------------------------------------------------
      TABLE : T_DONNEES_DNN
----------------------------------------------------------------------------- */

create table T_DONNEES_DNN
  (
     DNN_ID int identity (1, 1)   ,
     DNN_NOM char(32)  not null  ,
     DNN_UNITE char(32)  not null  ,
     DNN_VALEUR char(32)  not null  
     ,
     constraint PK_T_DONNEES_DNN primary key (DNN_ID)
  ) 
go



/* -----------------------------------------------------------------------------
      TABLE : T_DATEJOURE_DTJ
----------------------------------------------------------------------------- */

create table T_DATEJOURE_DTJ
  (
     ID_DTJ int identity (1, 1)   ,
     DTJ_DATE_DEBUT datetime  not null  ,
     DTJ_DATE_FIN datetime  null  
     ,
     constraint PK_T_DATEJOURE_DTJ primary key (ID_DTJ)
  ) 
go



/* -----------------------------------------------------------------------------
      TABLE : T_COMMUNE_CMN
----------------------------------------------------------------------------- */

create table T_COMMUNE_CMN
  (
     CMN_ID char(40)  not null  ,
     DPT_ID int  not null  ,
     CMN_NOM char(60)  not null  ,
     CMN_COD_POSTAL int  not null  ,
     CMN_POPULATION int  null  
     ,
     constraint PK_T_COMMUNE_CMN primary key (CMN_ID)
  ) 
go



/* -----------------------------------------------------------------------------
      TABLE : T_DEPARTEMENT_DPT
----------------------------------------------------------------------------- */

create table T_DEPARTEMENT_DPT
  (
     DPT_ID int  not null  ,
     DPT_NOM char(32)  not null  
     ,
     constraint PK_T_DEPARTEMENT_DPT primary key (DPT_ID)
  ) 
go



/* -----------------------------------------------------------------------------
      TABLE : T_CATEGORIE_CTG
----------------------------------------------------------------------------- */

create table T_CATEGORIE_CTG
  (
     CTG_ID int identity (1, 1)   ,
     CTG_NOM char(32)  not null  
     ,
     constraint PK_T_CATEGORIE_CTG primary key (CTG_ID)
  ) 
go



/* -----------------------------------------------------------------------------
      TABLE : POSSÈDE
----------------------------------------------------------------------------- */

create table POSSÈDE
  (
     CTG_ID int  not null  ,
     DNN_ID int  not null  
     ,
     constraint PK_POSSÈDE primary key (CTG_ID, DNN_ID)
  ) 
go



/* -----------------------------------------------------------------------------
      TABLE : DÉROULÉ
----------------------------------------------------------------------------- */

create table DÉROULÉ
  (
     DNN_ID int  not null  ,
     ID_DTJ int  not null  
     ,
     constraint PK_DÉROULÉ primary key (DNN_ID, ID_DTJ)
  ) 
go



/* -----------------------------------------------------------------------------
      TABLE : SITUÉ
----------------------------------------------------------------------------- */

create table SITUÉ
  (
     DNN_ID int  not null  ,
     CMN_ID char(40)  not null  
     ,
     constraint PK_SITUÉ primary key (DNN_ID, CMN_ID)
  ) 
go



/* -----------------------------------------------------------------------------
      REFERENCES SUR LES TABLES
----------------------------------------------------------------------------- */



alter table T_COMMUNE_CMN 
     add constraint FK_T_COMMUNE_CMN_T_DEPARTEMENT_DPT foreign key (DPT_ID) 
               references T_DEPARTEMENT_DPT (DPT_ID)
go




alter table POSSÈDE 
     add constraint FK_POSSÈDE_T_CATEGORIE_CTG foreign key (CTG_ID) 
               references T_CATEGORIE_CTG (CTG_ID)
go




alter table POSSÈDE 
     add constraint FK_POSSÈDE_T_DONNEES_DNN foreign key (DNN_ID) 
               references T_DONNEES_DNN (DNN_ID)
go




alter table DÉROULÉ 
     add constraint FK_DÉROULÉ_T_DONNEES_DNN foreign key (DNN_ID) 
               references T_DONNEES_DNN (DNN_ID)
go




alter table DÉROULÉ 
     add constraint FK_DÉROULÉ_T_DATEJOURE_DTJ foreign key (ID_DTJ) 
               references T_DATEJOURE_DTJ (ID_DTJ)
go




alter table SITUÉ 
     add constraint FK_SITUÉ_T_DONNEES_DNN foreign key (DNN_ID) 
               references T_DONNEES_DNN (DNN_ID)
go




alter table SITUÉ 
     add constraint FK_SITUÉ_T_COMMUNE_CMN foreign key (CMN_ID) 
               references T_COMMUNE_CMN (CMN_ID)
go




/*
 -----------------------------------------------------------------------------
               FIN DE GENERATION
 -----------------------------------------------------------------------------
*/
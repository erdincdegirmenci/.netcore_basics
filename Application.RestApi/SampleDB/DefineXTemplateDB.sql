CREATE DATABASE  IF NOT EXISTS `definextemplatedbv2` /*!40100 DEFAULT CHARACTER SET latin5 */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `definextemplatedbv2`;
-- MySQL dump 10.13  Distrib 8.0.20, for Win64 (x86_64)
--
-- Host: localhost    Database: definextemplatedbv2
-- ------------------------------------------------------
-- Server version	8.0.20

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `tbllog`
--

DROP TABLE IF EXISTS `tbllog`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbllog` (
  `Source` varchar(200) NOT NULL,
  `Message` varchar(21845) NOT NULL,
  `Severity` varchar(50) NOT NULL,
  `TransactionId` varchar(50) DEFAULT NULL,
  `CreateDate` datetime NOT NULL,
  `CreateUser` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin5;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tbllog`
--

LOCK TABLES `tbllog` WRITE;
/*!40000 ALTER TABLE `tbllog` DISABLE KEYS */;
INSERT INTO `tbllog` VALUES ('application-restapi','Message : login başladı | Exception :  ','Info',NULL,'2021-04-11 19:29:12',NULL),('application-restapi','Message : Login Failed. User:string | Exception :  ','Info',NULL,'2021-04-11 19:29:17',NULL),('application-restapi','Message : Login Failed. User Not Found:string | Exception :  ','Info',NULL,'2021-04-11 19:29:19',NULL),('application-restapi','Message : Exception Message : You have an error in your SQL syntax; check the manual that corresponds to your MySQL server version for the right syntax to use near \'Group,Type,IsActive,Order from tblParameter\' at line 1 || methodDescriptor : Application.RestApi.Controller.ParameterController.GetParameters    at MySql.Data.MySqlClient.MySqlStream.ReadPacket()\r\n   at MySql.Data.MySqlClient.NativeDriver.GetResult(Int32& affectedRow, Int64& insertedId)\r\n   at MySql.Data.MySqlClient.Driver.GetResult(Int32 statementId, Int32& affectedRows, Int64& insertedId)\r\n   at MySql.Data.MySqlClient.Driver.NextResult(Int32 statementId, Boolean force)\r\n   at MySql.Data.MySqlClient.MySqlDataReader.NextResult()\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteReader(CommandBehavior behavior)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteDbDataReader(CommandBehavior behavior)\r\n   at System.Data.Common.DbCommand.System.Data.IDbCommand.ExecuteReader(CommandBehavior behavior)\r\n   at Dapper.SqlMapper.ExecuteReaderWithFlagsFallback(IDbCommand cmd, Boolean wasClosed, CommandBehavior behavior) in /_/Dapper/SqlMapper.cs:line 1051\r\n   at Dapper.SqlMapper.QueryImpl[T](IDbConnection cnn, CommandDefinition command, Type effectiveType)+MoveNext() in /_/Dapper/SqlMapper.cs:line 1079\r\n   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)\r\n   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)\r\n   at Dapper.SqlMapper.Query[T](IDbConnection cnn, String sql, Object param, IDbTransaction transaction, Boolean buffered, Nullable`1 commandTimeout, Nullable`1 commandType) in /_/Dapper/SqlMapper.cs:line 721\r\n   at DefineXwork.Library.DataAccess.Manager.MysqlDatabaseManager.Select[T](String sql, Object param)\r\n   at DefineXwork.Library.DataAccess.BaseDAO`1.Select[T](String sql, Object param)\r\n   at DefineXwork.Library.DataAccess.BaseDAO`1.Select[T](String sql)\r\n   at DefineXwork.Library.DataAccess.BaseDAO`1.SelectWithTemplate[T](String queryname)\r\n   at Application.Domain.DataAccess.DAO.ParameterDAO.GetParameters() in C:\\Mehmet\\DefineX\\Proje\\framework\\Template-application\\application-api\\Application.Domain\\DataAccess\\DAO\\ParameterDAO.cs:line 23\r\n   at Application.Business.Service.ParameterService.GetParametersFromCache() in C:\\Mehmet\\DefineX\\Proje\\framework\\Template-application\\application-api\\Application.Business\\Service\\ParameterService.cs:line 58\r\n   at Application.Business.Service.ParameterService.GetParameters(Int32 type, String group) in C:\\Mehmet\\DefineX\\Proje\\framework\\Template-application\\application-api\\Application.Business\\Service\\ParameterService.cs:line 39\r\n   at Application.RestApi.Controller.ParameterController.GetParameters(ParameterRestReqestModel request) in C:\\Mehmet\\DefineX\\Proje\\framework\\Template-application\\application-api\\Application.RestApi\\Controller\\ParameterController.cs:line 36\r\n   at lambda_method(Closure , Object , Object[] )\r\n   at Microsoft.Extensions.Internal.ObjectMethodExecutor.Execute(Object target, Object[] parameters)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ActionMethodExecutor.SyncActionResultExecutor.Execute(IActionResultTypeMapper mapper, ObjectMethodExecutor executor, Object controller, Object[] arguments)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.InvokeActionMethodAsync()\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.Next(State& next, Scope& scope, Object& state, Boolean& isCompleted)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.InvokeNextActionFilterAsync()\r\n--- End of stack trace from previous location where exception was thrown ---\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.Rethrow(ActionExecutedContextSealed context)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.Next(State& next, Scope& scope, Object& state, Boolean& isCompleted)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.InvokeInnerFilterAsync()\r\n--- End of stack trace from previous location where exception was thrown ---\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeNextExceptionFilterAsync>g__Awaited|25_0(ResourceInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted) | Exception :  ','Error',NULL,'2021-04-11 19:38:44',NULL),('application-restapi','Message : Log Out. User:mehmet.karli@teamdefinex.com | Exception :  ','Info',NULL,'2021-04-11 19:38:52',NULL);
/*!40000 ALTER TABLE `tbllog` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tblparameter`
--

DROP TABLE IF EXISTS `tblparameter`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tblparameter` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Code` varchar(100) DEFAULT NULL,
  `Type` int NOT NULL,
  `Value` varchar(250) NOT NULL,
  `Order` int NOT NULL,
  `Group` varchar(100) DEFAULT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `CreateDate` datetime NOT NULL,
  `CreateUser` varchar(50) DEFAULT NULL,
  `LastUpdateDate` datetime DEFAULT NULL,
  `LastUpdateUser` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=latin5;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tblparameter`
--

LOCK TABLES `tblparameter` WRITE;
/*!40000 ALTER TABLE `tblparameter` DISABLE KEYS */;
INSERT INTO `tblparameter` VALUES (5,'1',1,'System',1,NULL,1,'2021-04-11 12:00:00',NULL,NULL,NULL),(6,'2',1,'Client',1,NULL,1,'2021-04-11 12:00:00',NULL,NULL,NULL);
/*!40000 ALTER TABLE `tblparameter` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tblpermission`
--

DROP TABLE IF EXISTS `tblpermission`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tblpermission` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) NOT NULL,
  `DisplayText` varchar(100) NOT NULL,
  `Description` varchar(250) DEFAULT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `CreateDate` datetime NOT NULL,
  `CreateUser` varchar(50) DEFAULT NULL,
  `LastUpdateDate` datetime DEFAULT NULL,
  `LastUpdateUser` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=latin5;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tblpermission`
--

LOCK TABLES `tblpermission` WRITE;
/*!40000 ALTER TABLE `tblpermission` DISABLE KEYS */;
INSERT INTO `tblpermission` VALUES (5,'List','Listing','Listing Permission',1,'2021-05-04 00:00:00',NULL,NULL,NULL);
/*!40000 ALTER TABLE `tblpermission` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tblrole`
--

DROP TABLE IF EXISTS `tblrole`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tblrole` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) NOT NULL,
  `DisplayText` varchar(100) NOT NULL,
  `Description` varchar(250) DEFAULT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `CreateDate` datetime NOT NULL,
  `CreateUser` varchar(50) DEFAULT NULL,
  `LastUpdateDate` datetime DEFAULT NULL,
  `LastUpdateUser` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=latin5;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tblrole`
--

LOCK TABLES `tblrole` WRITE;
/*!40000 ALTER TABLE `tblrole` DISABLE KEYS */;
INSERT INTO `tblrole` VALUES (4,'Admin','Site Administrator','Site Administrator',1,'2021-04-11 12:00:00',NULL,NULL,NULL);
/*!40000 ALTER TABLE `tblrole` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tblsetting`
--

DROP TABLE IF EXISTS `tblsetting`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tblsetting` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Domain` varchar(50) NOT NULL,
  `Section` varchar(50) NOT NULL,
  `Key` varchar(50) NOT NULL,
  `Value` varchar(500) NOT NULL,
  `Definition` varchar(250) DEFAULT NULL,
  `CacheDuration` int DEFAULT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `CreateDate` datetime NOT NULL,
  `CreateUser` varchar(50) DEFAULT NULL,
  `LastUpdateDate` datetime DEFAULT NULL,
  `LastUpdateUser` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin5;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tblsetting`
--

LOCK TABLES `tblsetting` WRITE;
/*!40000 ALTER TABLE `tblsetting` DISABLE KEYS */;
/*!40000 ALTER TABLE `tblsetting` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tbluser`
--

DROP TABLE IF EXISTS `tbluser`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbluser` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Username` varchar(100) NOT NULL,
  `Password` varchar(250) NOT NULL,
  `Name` varchar(250) DEFAULT NULL,
  `Surname` varchar(250) DEFAULT NULL,
  `TenantId` int DEFAULT NULL,
  `RefreshToken` varchar(250) DEFAULT NULL,
  `RefreshTokenCreateDate` datetime DEFAULT NULL,
  `UserType` varchar(20) NOT NULL,
  `IsActive` tinyint(1) DEFAULT '1',
  `IsDeleted` tinyint(1) DEFAULT '0',
  `LoginFailedCount` int DEFAULT '0',
  `LastLoginFailedDate` datetime DEFAULT NULL,
  `CreateDate` datetime NOT NULL,
  `CreateUser` varchar(50) DEFAULT NULL,
  `LastUpdateDate` datetime DEFAULT NULL,
  `LastUpdateUser` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin5;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tbluser`
--

LOCK TABLES `tbluser` WRITE;
/*!40000 ALTER TABLE `tbluser` DISABLE KEYS */;
INSERT INTO `tbluser` VALUES (2,'testuser@teamdefinex.com','13K44UYX8Y8UAClL+5UWjwU2xSP7Obskax6zmObXDuo=','Mehmet İkbal','KARLI',NULL,'','2021-04-11 19:38:36','client',1,0,0,NULL,'2021-04-11 19:38:05',NULL,NULL,NULL);
/*!40000 ALTER TABLE `tbluser` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tbluserpermission`
--

DROP TABLE IF EXISTS `tbluserpermission`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbluserpermission` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `UserId` int NOT NULL,
  `PermissionId` int NOT NULL,
  `IsDeny` tinyint(1) DEFAULT '0',
  `CreateDate` datetime NOT NULL,
  `CreateUser` varchar(50) DEFAULT NULL,
  `LastUpdateDate` datetime DEFAULT NULL,
  `LastUpdateUser` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_UserPermissionUserId` (`UserId`),
  KEY `FK_UserPermissionPermissionId` (`PermissionId`),
  CONSTRAINT `FK_UserPermissionPermissionId` FOREIGN KEY (`PermissionId`) REFERENCES `tblpermission` (`Id`),
  CONSTRAINT `FK_UserPermissionUserId` FOREIGN KEY (`UserId`) REFERENCES `tbluser` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=latin5;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tbluserpermission`
--

LOCK TABLES `tbluserpermission` WRITE;
/*!40000 ALTER TABLE `tbluserpermission` DISABLE KEYS */;
INSERT INTO `tbluserpermission` VALUES (8,2,5,'2021-05-04 00:00:00',NULL,NULL,NULL);
/*!40000 ALTER TABLE `tbluserpermission` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tbluserrole`
--

DROP TABLE IF EXISTS `tbluserrole`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbluserrole` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `UserId` int NOT NULL,
  `RoleId` int NOT NULL,
  `CreateDate` datetime NOT NULL,
  `CreateUser` varchar(50) DEFAULT NULL,
  `LastUpdateDate` datetime DEFAULT NULL,
  `LastUpdateUser` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_UserRoleUserId` (`UserId`),
  KEY `FK_UserRoleRoleId` (`RoleId`),
  CONSTRAINT `FK_UserRoleRoleId` FOREIGN KEY (`RoleId`) REFERENCES `tblrole` (`Id`),
  CONSTRAINT `FK_UserRoleUserId` FOREIGN KEY (`UserId`) REFERENCES `tbluser` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=latin5;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tbluserrole`
--

LOCK TABLES `tbluserrole` WRITE;
/*!40000 ALTER TABLE `tbluserrole` DISABLE KEYS */;
INSERT INTO `tbluserrole` VALUES (6,2,4,'2021-04-11 12:00:00',NULL,NULL,NULL);
/*!40000 ALTER TABLE `tbluserrole` ENABLE KEYS */;
UNLOCK TABLES;


DROP TABLE IF EXISTS `tblrolepermission`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tblrolepermission` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `RoleId` int NOT NULL,
  `PermissionId` int NOT NULL,
  `CreateDate` datetime NOT NULL,
  `CreateUser` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_RolePermissionUserId` (`RoleId`),
  KEY `FK_RolePermissionPermissionId` (`PermissionId`),
  CONSTRAINT `FK_RolePermissionPermissionId` FOREIGN KEY (`PermissionId`) REFERENCES `tblpermission` (`Id`),
  CONSTRAINT `FK_RolePermissionRoleId` FOREIGN KEY (`RoleId`) REFERENCES `tblrole` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=latin5;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tblrolepermission`
--

--
-- Table structure for table `tbluserverification`
--

DROP TABLE IF EXISTS `tbluserverification`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbluserverification` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `UserId` int NOT NULL,
  `VerificationCode` varchar(500) DEFAULT NULL,
  `VerificationDate` datetime DEFAULT NULL,
  `ExpireTime` datetime DEFAULT NULL,
  `VerificationType` varchar(20) NOT NULL,
  `IsVerificate` tinyint(1) NOT NULL,
  `CreateDate` datetime NOT NULL,
  `CreateUser` varchar(50) DEFAULT NULL,
  `LastUpdateDate` datetime DEFAULT NULL,
  `LastUpdateUser` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_UserVerificationUserId` (`UserId`),
  CONSTRAINT `FK_UserVerificationUserId` FOREIGN KEY (`UserId`) REFERENCES `tbluser` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=latin5;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tbluserverification`
--

LOCK TABLES `tbluserverification` WRITE;
/*!40000 ALTER TABLE `tbluserverification` DISABLE KEYS */;
INSERT INTO `tbluserverification` VALUES (4,2,'NmIxMzg4YmUtNGViOS00MzFjLWI5ZTEtMzkxMzVhNDk5ZjVi','2021-04-11 19:38:19',NULL,'1',1,'2021-04-11 19:38:06',NULL,'2021-04-11 19:38:19',NULL);
/*!40000 ALTER TABLE `tbluserverification` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2021-05-06 16:28:44

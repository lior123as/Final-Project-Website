-- MySQL dump 10.13  Distrib 8.0.45, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: vividdatabase
-- ------------------------------------------------------
-- Server version	8.0.45

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
-- Table structure for table `acquiredheroes`
--

DROP TABLE IF EXISTS `acquiredheroes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `acquiredheroes` (
  `id` int NOT NULL AUTO_INCREMENT,
  `playerid` int NOT NULL,
  `heroesid` int NOT NULL,
  `acquired` int DEFAULT '0',
  `level` int DEFAULT NULL,
  `exp` int DEFAULT NULL,
  `skillpoints` int DEFAULT NULL,
  PRIMARY KEY (`id`,`playerid`,`heroesid`),
  KEY `player_to_ah_idx` (`playerid`),
  KEY `heroes_to_ah_idx` (`heroesid`),
  CONSTRAINT `heroes_to_ah` FOREIGN KEY (`heroesid`) REFERENCES `heroes` (`id`),
  CONSTRAINT `player_to_ah` FOREIGN KEY (`playerid`) REFERENCES `player` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `acquiredheroes`
--

LOCK TABLES `acquiredheroes` WRITE;
/*!40000 ALTER TABLE `acquiredheroes` DISABLE KEYS */;
INSERT INTO `acquiredheroes` VALUES (1,2,1,1,3,200,1);
/*!40000 ALTER TABLE `acquiredheroes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `acquiredskill`
--

DROP TABLE IF EXISTS `acquiredskill`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `acquiredskill` (
  `id` int NOT NULL AUTO_INCREMENT,
  `acquiredheroesid` int NOT NULL,
  `skillid` int NOT NULL,
  `isaquired` int DEFAULT '0',
  PRIMARY KEY (`id`,`acquiredheroesid`,`skillid`),
  KEY `ah_to_as_idx` (`acquiredheroesid`),
  KEY `skill_to_as_idx` (`skillid`),
  CONSTRAINT `ah_to_as` FOREIGN KEY (`acquiredheroesid`) REFERENCES `acquiredheroes` (`id`),
  CONSTRAINT `skill_to_as` FOREIGN KEY (`skillid`) REFERENCES `skill` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `acquiredskill`
--

LOCK TABLES `acquiredskill` WRITE;
/*!40000 ALTER TABLE `acquiredskill` DISABLE KEYS */;
/*!40000 ALTER TABLE `acquiredskill` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `connections`
--

DROP TABLE IF EXISTS `connections`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `connections` (
  `id` int NOT NULL AUTO_INCREMENT,
  `skill` int DEFAULT NULL,
  `skill_after` int DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_skillcon_skill1_idx` (`skill`),
  KEY `fk_skillcon_skillafter_idx` (`skill_after`),
  CONSTRAINT `fk_skillcon_skill` FOREIGN KEY (`skill`) REFERENCES `skill` (`id`),
  CONSTRAINT `fk_skillcon_skillafter` FOREIGN KEY (`skill_after`) REFERENCES `skill` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `connections`
--

LOCK TABLES `connections` WRITE;
/*!40000 ALTER TABLE `connections` DISABLE KEYS */;
/*!40000 ALTER TABLE `connections` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `devban`
--

DROP TABLE IF EXISTS `devban`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `devban` (
  `id` int NOT NULL AUTO_INCREMENT,
  `isbanned` int DEFAULT '0',
  `iswarned` int DEFAULT '0',
  `bantime` datetime DEFAULT NULL,
  `banreason` varchar(15000) DEFAULT NULL,
  `userid` int DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `user_to_ban_idx` (`userid`),
  CONSTRAINT `user_to_ban` FOREIGN KEY (`userid`) REFERENCES `users` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=23 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `devban`
--

LOCK TABLES `devban` WRITE;
/*!40000 ALTER TABLE `devban` DISABLE KEYS */;
INSERT INTO `devban` VALUES (1,0,0,'2026-05-01 11:55:00',NULL,1),(2,1,0,'2026-05-20 12:00:00','Cheating detected in match',2),(3,0,1,'2026-05-13 12:00:00','Spamming chat',3),(4,0,0,'2026-05-13 12:00:00',NULL,4),(5,1,1,'2026-06-01 09:00:00','Exploiting a bug for advantage',5),(6,0,1,'2026-05-13 12:00:00','Inappropriate username',6),(7,1,0,'2026-05-27 18:30:00','Team killing repeatedly',7),(8,0,0,'2026-05-13 12:00:00',NULL,8),(9,1,1,'2026-06-05 14:00:00','Sharing account with others',9),(10,0,1,'2026-05-13 12:00:00','Harassment in chat',10),(11,1,0,'2026-05-22 20:15:00','Using unauthorized mods',11),(12,0,0,'2026-05-13 12:00:00',NULL,12),(13,1,1,'2026-05-30 08:45:00','Offensive language reported',13),(14,0,1,'2026-05-13 12:00:00','Repeated minor infractions',14),(15,1,0,'2026-06-10 16:30:00','Exploiting map glitches',15),(16,0,0,'2026-05-13 12:00:00',NULL,16),(17,1,1,'2026-05-25 11:20:00','Harassment reported multiple times',17),(18,0,1,'2026-05-13 12:00:00','Suspicious account activity',18),(19,1,0,'2026-05-28 19:50:00','Repeated cheating',19),(20,0,0,'2026-05-13 12:00:00',NULL,20),(21,1,1,'2026-06-02 10:00:00','Using prohibited third-party software',21),(22,0,0,'2026-05-15 10:05:13','',22);
/*!40000 ALTER TABLE `devban` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `enemies`
--

DROP TABLE IF EXISTS `enemies`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `enemies` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(1000) DEFAULT NULL,
  `description` varchar(10000) DEFAULT NULL,
  `initialhp` int DEFAULT NULL,
  `attacks` varchar(1000) DEFAULT NULL,
  `initialattackdamagerange` varchar(1000) DEFAULT NULL,
  `attacktype` varchar(1000) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `enemies`
--

LOCK TABLES `enemies` WRITE;
/*!40000 ALTER TABLE `enemies` DISABLE KEYS */;
INSERT INTO `enemies` VALUES (1,'Bat','A fast-flying creature that swoops through dark caves, attacking from above with quick strikes.',800,'FangBite','400-600','melee'),(2,'Goblin','A sneaky green scavenger armed with crude weapons and a talent for ambushes.',1200,'Scratch','800-1200','melee'),(3,'Skeleton','An undead warrior held together by dark magic, relentless even after death.',1500,'Slash','1200-1500','melee'),(4,'Slime','A bouncing blob of goo that slowly creeps toward enemies and dissolves anything it touches.',1000,'Cover','400-700','melee'),(5,'Spider','A giant crawling predator that traps victims in webs before delivering a poisonous bite.',1000,'WebSling','700-1200','ranged');
/*!40000 ALTER TABLE `enemies` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `heroes`
--

DROP TABLE IF EXISTS `heroes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `heroes` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(200) DEFAULT NULL,
  `description` varchar(1000) DEFAULT NULL,
  `starterhp` int DEFAULT NULL,
  `type` varchar(1000) DEFAULT NULL,
  `attacktype` varchar(1000) DEFAULT NULL,
  `basicattack` varchar(1000) DEFAULT NULL,
  `specialattack` varchar(1000) DEFAULT NULL,
  `ultimateattack` varchar(1000) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `heroes`
--

LOCK TABLES `heroes` WRITE;
/*!40000 ALTER TABLE `heroes` DISABLE KEYS */;
INSERT INTO `heroes` VALUES (1,'Ms. Edna','The owner of a small mystic shop at the edge of town',1000,'Ice','Melee',NULL,NULL,NULL);
/*!40000 ALTER TABLE `heroes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `inventory`
--

DROP TABLE IF EXISTS `inventory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `inventory` (
  `id` int NOT NULL AUTO_INCREMENT,
  `playerid` int NOT NULL,
  `itemid` int NOT NULL,
  `quantity` int DEFAULT '0',
  PRIMARY KEY (`id`),
  KEY `player_to_inventory_idx` (`playerid`),
  KEY `item_to_inventory_idx` (`itemid`),
  CONSTRAINT `item_to_inventory` FOREIGN KEY (`itemid`) REFERENCES `item` (`id`),
  CONSTRAINT `player_to_inventory` FOREIGN KEY (`playerid`) REFERENCES `player` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=203 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `inventory`
--

LOCK TABLES `inventory` WRITE;
/*!40000 ALTER TABLE `inventory` DISABLE KEYS */;
INSERT INTO `inventory` VALUES (1,2,2,2),(2,2,3,5),(3,2,4,4),(4,4,3,5),(5,4,7,2),(6,4,11,9),(7,4,15,4),(8,4,20,1),(9,5,1,6),(10,5,4,3),(11,5,8,7),(12,5,13,2),(13,5,18,5),(14,5,21,1),(15,6,2,4),(16,6,5,8),(17,6,9,3),(18,6,12,6),(19,6,16,1),(20,6,19,7),(21,6,20,2),(22,7,3,10),(23,7,6,2),(24,7,10,4),(25,7,14,5),(26,7,17,1),(27,8,1,3),(28,8,7,6),(29,8,11,2),(30,8,15,8),(31,8,18,4),(32,8,21,5),(33,9,2,9),(34,9,5,3),(35,9,8,6),(36,9,13,2),(37,9,16,7),(38,10,4,5),(39,10,6,1),(40,10,9,8),(41,10,12,4),(42,10,15,3),(43,10,20,6),(44,11,1,7),(45,11,3,2),(46,11,7,5),(47,11,10,4),(48,11,14,9),(49,11,18,1),(50,11,21,8),(51,12,2,5),(52,12,6,3),(53,12,11,7),(54,12,16,2),(55,12,19,4),(56,13,5,8),(57,13,9,1),(58,13,13,6),(59,13,17,3),(60,13,20,5),(61,14,1,4),(62,14,4,9),(63,14,8,2),(64,14,12,7),(65,14,18,5),(66,14,21,1),(67,15,3,6),(68,15,7,2),(69,15,10,8),(70,15,14,3),(71,15,17,5),(72,16,2,7),(73,16,6,1),(74,16,9,4),(75,16,13,8),(76,16,16,2),(77,16,20,6),(78,17,5,3),(79,17,8,7),(80,17,11,1),(81,17,15,9),(82,17,18,4),(83,18,1,5),(84,18,4,2),(85,18,7,6),(86,18,12,3),(87,18,16,8),(88,18,19,1),(89,19,2,9),(90,19,6,4),(91,19,10,2),(92,19,14,7),(93,19,17,5),(94,20,3,8),(95,20,8,1),(96,20,11,6),(97,20,15,2),(98,20,18,4),(99,20,21,7),(100,21,1,3),(101,21,5,9),(102,21,9,2),(103,21,13,5),(104,21,16,7),(105,22,2,6),(106,22,6,1),(107,22,10,8),(108,22,14,3),(109,22,17,4),(110,22,20,5),(111,23,4,7),(112,23,7,2),(113,23,11,6),(114,23,15,1),(115,23,18,9),(116,24,1,4),(117,24,5,8),(118,24,8,3),(119,24,12,6),(120,24,16,2),(121,24,21,5),(122,25,3,10),(123,25,6,2),(124,25,9,5),(125,25,13,1),(126,25,17,7),(127,25,20,4),(128,26,2,3),(129,26,7,9),(130,26,11,4),(131,26,15,2),(132,26,18,6),(133,27,1,8),(134,27,5,1),(135,27,8,7),(136,27,12,3),(137,27,16,5),(138,27,19,2),(139,28,4,6),(140,28,9,2),(141,28,13,8),(142,28,17,1),(143,28,20,7),(144,29,3,5),(145,29,6,9),(146,29,10,2),(147,29,14,4),(148,29,18,6),(149,30,1,7),(150,30,5,3),(151,30,8,9),(152,30,11,1),(153,30,15,4),(154,30,21,8),(155,31,2,6),(156,31,7,2),(157,31,12,5),(158,31,16,1),(159,31,19,7),(160,32,4,8),(161,32,9,3),(162,32,13,6),(163,32,17,2),(164,32,20,5),(165,33,1,9),(166,33,5,4),(167,33,8,2),(168,33,11,7),(169,33,15,1),(170,33,18,6),(171,34,3,5),(172,34,6,8),(173,34,10,2),(174,34,14,7),(175,34,19,3),(176,35,2,4),(177,35,7,9),(178,35,12,1),(179,35,16,5),(180,35,20,6),(181,36,1,8),(182,36,5,2),(183,36,9,7),(184,36,13,3),(185,36,17,6),(186,36,21,1),(187,37,4,5),(188,37,8,2),(189,37,11,9),(190,37,15,4),(191,37,18,6),(192,38,2,7),(193,38,6,1),(194,38,10,8),(195,38,14,3),(196,38,19,5),(197,39,1,6),(198,39,5,2),(199,39,9,7),(200,39,12,3),(201,39,16,8),(202,39,20,4);
/*!40000 ALTER TABLE `inventory` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `item`
--

DROP TABLE IF EXISTS `item`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `item` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(100) DEFAULT NULL,
  `description` varchar(1000) DEFAULT NULL,
  `rarity` float DEFAULT NULL,
  `price` int DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=22 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `item`
--

LOCK TABLES `item` WRITE;
/*!40000 ALTER TABLE `item` DISABLE KEYS */;
INSERT INTO `item` VALUES (1,'Paper','A piece of paper that can be found at certain locations in the Nightmare Veil, can be used to upgrade character',0.5,50),(2,'Veil Residue','A smoky substance left behind when nightmares dissolve inside the Veil.',0.5,140),(3,'Lucid Shard','A crystalline fragment formed from concentrated human dreams.',1,420),(4,'Domeglass Fragment','A chipped piece of the city dome humming with protective energy.',2.5,890),(5,'Nightmare Ichor','Black liquid harvested from fallen nightmare creatures.',2,1450),(6,'Sleepwalker Dust','Fine gray powder found where lost dreamers vanished.',0.5,110),(7,'Echo Bloom Petal','A pale flower petal that repeats faint whispers at night.',1,380),(8,'Veilstone Ore','Dense ore mined from the shifting ground beyond the dome.',1.5,760),(9,'Hollow Eye Pearl','A dark pearl recovered from eyeless nightmare beasts.',2,1620),(10,'Dream Sap','Sticky luminous sap collected from twisted Veil trees.',1,470),(11,'Fading Memory Thread','An unstable thread woven from fragmented memories.',2.5,2480),(12,'Moonless Ash','Cold ash gathered after Veil storms consume the land.',1.5,820),(13,'Whisper Moss','Soft glowing moss that reacts to fearful thoughts.',0.5,130),(14,'Somnus Crystal','A rare crystal pulsing with dormant dream energy.',2.5,2750),(15,'Fractured Halo Fragment','A radiant shard recovered from ruined sanctuaries in the Veil.',2,1710),(16,'Blackwater Resin','Dark resin seeping from nightmare-corrupted roots.',1,520),(17,'Silent Ember','A dim ember that burns without heat or sound.',1.5,910),(18,'Dreamer Bone Dust','Powdered remains of ancient dreambound wanderers.',2,1580),(19,'Veilstorm Core','A volatile core found after massive Veil disturbances.',3,5400),(20,'Eclipsed Tear','A black crystal droplet formed from concentrated despair.',3,6200),(21,'Lullaby Fiber','Soft silver fibers harvested from dream cocoons near the city edge.',1,450);
/*!40000 ALTER TABLE `item` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `level`
--

DROP TABLE IF EXISTS `level`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `level` (
  `id` int NOT NULL,
  `skillpointsadded` int DEFAULT NULL,
  `exptolevel` int DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `level`
--

LOCK TABLES `level` WRITE;
/*!40000 ALTER TABLE `level` DISABLE KEYS */;
/*!40000 ALTER TABLE `level` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `player`
--

DROP TABLE IF EXISTS `player`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `player` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(100) DEFAULT NULL,
  `rem` int DEFAULT NULL,
  `type` varchar(45) DEFAULT NULL,
  `users_id` int NOT NULL,
  PRIMARY KEY (`id`,`users_id`),
  KEY `user_to_player_idx` (`users_id`),
  CONSTRAINT `user_to_player` FOREIGN KEY (`users_id`) REFERENCES `users` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=40 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `player`
--

LOCK TABLES `player` WRITE;
/*!40000 ALTER TABLE `player` DISABLE KEYS */;
INSERT INTO `player` VALUES (2,'Marcus',0,'Male',1),(3,'Ng',0,'Female',1),(4,'ShadowWolf',1200,'male',2),(5,'CrystalRose',950,'female',2),(6,'NightHunter',1800,'male',3),(7,'FireBlade',750,'male',3),(8,'MoonWhisper',1600,'female',3),(9,'StormBreaker',2200,'male',4),(10,'SilentArrow',1300,'female',5),(11,'DarkKnight',2000,'male',5),(12,'SilverFang',1750,'male',6),(13,'RubyFlame',900,'female',6),(14,'IcePhoenix',2100,'female',7),(15,'ThunderStrike',1100,'male',8),(16,'VenomSoul',1450,'female',8),(17,'GhostRider',1950,'male',8),(18,'GoldenBlade',800,'male',9),(19,'ScarletMage',1700,'female',10),(20,'IronClaw',1250,'male',10),(21,'RapidShadow',2400,'male',11),(22,'MysticBloom',950,'female',11),(23,'NovaStrike',1850,'male',12),(24,'FrozenTear',1400,'female',13),(25,'InfernoKing',2600,'male',13),(26,'SilentViper',980,'female',14),(27,'TitanHeart',2000,'male',15),(28,'CelestialRay',1750,'female',15),(29,'VoidWalker',1500,'male',16),(30,'BlazeQueen',2300,'female',17),(31,'SteelHunter',1150,'male',17),(32,'FrostDancer',1350,'female',18),(33,'DragonSoul',2500,'male',18),(34,'ShadowLancer',1650,'male',19),(35,'EmeraldWing',900,'female',20),(36,'NightFury',2150,'male',20),(37,'CrystalBlade',1550,'female',21),(38,'RapidWolf',1800,'male',21),(39,'LunarSpirit',1900,'female',22);
/*!40000 ALTER TABLE `player` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `skill`
--

DROP TABLE IF EXISTS `skill`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `skill` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(1000) DEFAULT NULL,
  `description` varchar(10000) DEFAULT NULL,
  `levelrequired` int DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `skill`
--

LOCK TABLES `skill` WRITE;
/*!40000 ALTER TABLE `skill` DISABLE KEYS */;
/*!40000 ALTER TABLE `skill` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `users` (
  `id` int NOT NULL AUTO_INCREMENT,
  `username` varchar(100) DEFAULT NULL,
  `password` varchar(20) DEFAULT NULL,
  `email` varchar(100) DEFAULT NULL,
  `isdev` int DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=23 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES (1,'ProjectX','1524XZS12!l','shlior701@gmail.com',1),(2,'OliviaSmith','Passw0rd!','oliviasmith@example.com',0),(3,'LiamJohnson','StrongP@ss1','liamjohnson@example.com',0),(4,'EmmaBrown','MyP@ssw0rd2','emmabrown@example.com',0),(5,'NoahJones','SafePass#3','noahjones@example.com',0),(6,'AvaGarcia','Secure!4u','avagarcia@example.com',0),(7,'ElijahMartinez','A1b2C3$d','elijahmartinez@example.com',0),(8,'SophiaRodriguez','HelloW0rld!','sophiarodriguez@example.com',0),(9,'WilliamLee','P@ssword8','williamlee@example.com',0),(10,'IsabellaWalker','Test123#A','isabellawalker@example.com',0),(11,'JamesHall','Adm1n$Pass','jameshall@example.com',0),(12,'MiaAllen','Qwerty!12','miaallen@example.com',0),(13,'BenjaminYoung','ZxCvB1#2','benjaminyoung@example.com',0),(14,'CharlotteHernandez','L0gin@123','charlottehernandez@example.com',0),(15,'LucasKing','Pa$$w0rd14','lucasking@example.com',0),(16,'AmeliaWright','R3gular#15','ameliawright@example.com',0),(17,'HenryLopez','My$ecure16','henrylopez@example.com',0),(18,'EvelynHill','T3st!User17','evelynhill@example.com',0),(19,'AlexanderScott','Demo#Pass18','alexanderscott@example.com',0),(20,'HarperGreen','User19$Test','harpergreen@example.com',0),(21,'DanielAdams','Pass!Word20','danieladams@example.com',0),(22,'123EvilForYou','1524XZS12!l','Evil@gmail.com',0);
/*!40000 ALTER TABLE `users` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2026-05-24  7:10:26

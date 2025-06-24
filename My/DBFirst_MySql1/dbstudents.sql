-- --------------------------------------------------------
-- 主機:                           127.0.0.1
-- 伺服器版本:                        11.7.1-MariaDB - mariadb.org binary distribution
-- 伺服器作業系統:                      Win64
-- HeidiSQL 版本:                  12.8.0.6908
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- 傾印 dbstudents 的資料庫結構
CREATE DATABASE IF NOT EXISTS `dbstudents` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_uca1400_ai_ci */;
USE `dbstudents`;

-- 傾印  資料表 dbstudents.tstudent 結構
CREATE TABLE IF NOT EXISTS `tstudent` (
  `fStuId` char(6) NOT NULL,
  `fName` varchar(30) NOT NULL DEFAULT '',
  `fEmail` varchar(40) NOT NULL DEFAULT '',
  `fScore` int(11) DEFAULT 0,
  PRIMARY KEY (`fStuId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;

-- 正在傾印表格  dbstudents.tstudent 的資料：~3 rows (近似值)
INSERT INTO `tstudent` (`fStuId`, `fName`, `fEmail`, `fScore`) VALUES
	('112001', '王普丁111', 'ding@gmail.com', 92),
	('112002', '習維尼2222', 'nee@gmail.com', 68),
	('2AA', 'AAAAAA', 'sasdas', 22222222);

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;

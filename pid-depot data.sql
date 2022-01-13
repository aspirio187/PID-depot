-- phpMyAdmin SQL Dump
-- version 5.1.1
-- https://www.phpmyadmin.net/
--
-- Hôte : localhost:3306
-- Généré le : jeu. 13 jan. 2022 à 13:26
-- Version du serveur : 5.7.33
-- Version de PHP : 7.4.19

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de données : `pid-depot`
--
CREATE DATABASE IF NOT EXISTS `pid-depot` DEFAULT CHARACTER SET latin1 COLLATE latin1_swedish_ci;
USE `pid-depot`;

--
-- Déchargement des données de la table `lessons`
--

INSERT INTO `lessons` (`id`, `name`, `description`) VALUES
(1, 'test1', 'teztzetzetzet'),
(2, 'test2', ''),
(3, 'test2', 'ezzezerzerzer'),
(4, 'dfzdzdfz', '');

--
-- Déchargement des données de la table `roles`
--

INSERT INTO `roles` (`id`, `name`) VALUES
('3dcc2feb-672e-11ec-b893-e4e749988ea4', 'Admin'),
('45194bcb-672e-11ec-b893-e4e749988ea4', 'Client'),
('47f60935-672e-11ec-b893-e4e749988ea4', 'Teacher'),
('ce76b531-6133-11ec-bb4c-8c1645b816b6', 'value');

--
-- Déchargement des données de la table `users`
--

INSERT INTO `users` (`id`, `email`, `normalized_email`, `password`, `firstname`, `lastname`, `birth_date`, `registration_number`, `security_stamp`, `concurrency_stamp`, `is_activated`) VALUES
('89764bdb-69a5-11ec-9b55-e4e749988ea4', 'soultan_98@hotmail.com', 'SOULTAN_98@HOTMAIL.COM', '7092fd8d6c7051cefb3e18246579594f7fdd753a8bf8b843c2e6fcd065673784', 'soultan', 'hatsijevi', '2012-12-11', 'u987654321', 'af929f14-69a5-11ec-9b55-e4e749988ea4', '282306f3-69a6-11ec-9b55-e4e749988ea4', 0);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

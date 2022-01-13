-- phpMyAdmin SQL Dump
-- version 5.1.1
-- https://www.phpmyadmin.net/
--
-- Hôte : localhost:3306
-- Généré le : jeu. 13 jan. 2022 à 13:24
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

-- --------------------------------------------------------

--
-- Structure de la table `lessons`
--

CREATE TABLE `lessons` (
  `id` int(11) NOT NULL,
  `name` tinytext NOT NULL,
  `description` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Structure de la table `lesson_details`
--

CREATE TABLE `lesson_details` (
  `id` int(11) NOT NULL,
  `title` tinytext NOT NULL,
  `details` longtext NOT NULL,
  `lesson_timetable_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Structure de la table `lesson_files`
--

CREATE TABLE `lesson_files` (
  `id` int(11) NOT NULL,
  `file_path` text NOT NULL,
  `lesson_detail_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Structure de la table `lesson_timetables`
--

CREATE TABLE `lesson_timetables` (
  `id` int(11) NOT NULL,
  `starts_at` datetime NOT NULL,
  `ends_at` datetime NOT NULL,
  `lesson_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Structure de la table `roles`
--

CREATE TABLE `roles` (
  `id` char(36) NOT NULL,
  `name` tinytext NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Structure de la table `users`
--

CREATE TABLE `users` (
  `id` char(36) NOT NULL,
  `email` mediumtext NOT NULL,
  `normalized_email` mediumtext NOT NULL,
  `password` text NOT NULL,
  `firstname` tinytext NOT NULL,
  `lastname` tinytext NOT NULL,
  `birth_date` date NOT NULL,
  `registration_number` char(10) NOT NULL,
  `security_stamp` char(36) NOT NULL,
  `concurrency_stamp` char(36) NOT NULL,
  `is_activated` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Structure de la table `users_lessons`
--

CREATE TABLE `users_lessons` (
  `id` int(11) NOT NULL,
  `user_id` char(36) NOT NULL,
  `lesson_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Structure de la table `users_roles`
--

CREATE TABLE `users_roles` (
  `user_id` char(36) NOT NULL,
  `role_id` char(36) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Structure de la table `users_tokens`
--

CREATE TABLE `users_tokens` (
  `id` int(11) NOT NULL,
  `token_type` varchar(100) NOT NULL,
  `token_value` text NOT NULL,
  `delivery_date` datetime NOT NULL,
  `expiration_date` datetime NOT NULL,
  `user_id` char(36) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Index pour les tables déchargées
--

--
-- Index pour la table `lessons`
--
ALTER TABLE `lessons`
  ADD PRIMARY KEY (`id`);

--
-- Index pour la table `lesson_details`
--
ALTER TABLE `lesson_details`
  ADD PRIMARY KEY (`id`),
  ADD KEY `lesson_timetable_id_fk` (`lesson_timetable_id`);

--
-- Index pour la table `lesson_files`
--
ALTER TABLE `lesson_files`
  ADD PRIMARY KEY (`id`),
  ADD KEY `cf_lesson_detail_id` (`lesson_detail_id`);

--
-- Index pour la table `lesson_timetables`
--
ALTER TABLE `lesson_timetables`
  ADD PRIMARY KEY (`id`),
  ADD KEY `ct_lesson_id_fk` (`lesson_id`);

--
-- Index pour la table `roles`
--
ALTER TABLE `roles`
  ADD PRIMARY KEY (`id`);

--
-- Index pour la table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`id`);

--
-- Index pour la table `users_lessons`
--
ALTER TABLE `users_lessons`
  ADD PRIMARY KEY (`id`),
  ADD KEY `ul_lesson_id_fk` (`lesson_id`) USING BTREE,
  ADD KEY `ul_user_id_fk` (`user_id`);

--
-- Index pour la table `users_roles`
--
ALTER TABLE `users_roles`
  ADD KEY `ur_user_id_fk` (`user_id`),
  ADD KEY `ur_role_id_fk` (`role_id`);

--
-- Index pour la table `users_tokens`
--
ALTER TABLE `users_tokens`
  ADD PRIMARY KEY (`id`),
  ADD KEY `ut_user_id_fk` (`user_id`);

--
-- AUTO_INCREMENT pour les tables déchargées
--

--
-- AUTO_INCREMENT pour la table `lessons`
--
ALTER TABLE `lessons`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT pour la table `lesson_details`
--
ALTER TABLE `lesson_details`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT pour la table `lesson_files`
--
ALTER TABLE `lesson_files`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT pour la table `lesson_timetables`
--
ALTER TABLE `lesson_timetables`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT pour la table `users_lessons`
--
ALTER TABLE `users_lessons`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT pour la table `users_tokens`
--
ALTER TABLE `users_tokens`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- Contraintes pour les tables déchargées
--

--
-- Contraintes pour la table `lesson_details`
--
ALTER TABLE `lesson_details`
  ADD CONSTRAINT `cd_class_timetable_id_fk` FOREIGN KEY (`lesson_timetable_id`) REFERENCES `lesson_timetables` (`id`) ON DELETE CASCADE;

--
-- Contraintes pour la table `lesson_files`
--
ALTER TABLE `lesson_files`
  ADD CONSTRAINT `cf_class_detail_id` FOREIGN KEY (`lesson_detail_id`) REFERENCES `lesson_details` (`id`) ON DELETE CASCADE;

--
-- Contraintes pour la table `lesson_timetables`
--
ALTER TABLE `lesson_timetables`
  ADD CONSTRAINT `ct_class_id_fk` FOREIGN KEY (`lesson_id`) REFERENCES `lessons` (`id`) ON DELETE CASCADE;

--
-- Contraintes pour la table `users_lessons`
--
ALTER TABLE `users_lessons`
  ADD CONSTRAINT `uc_class_id_fk` FOREIGN KEY (`lesson_id`) REFERENCES `lessons` (`id`) ON DELETE CASCADE,
  ADD CONSTRAINT `uc_user_id_fk` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Contraintes pour la table `users_roles`
--
ALTER TABLE `users_roles`
  ADD CONSTRAINT `ur_role_id_fk` FOREIGN KEY (`role_id`) REFERENCES `roles` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `ur_user_id_fk` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Contraintes pour la table `users_tokens`
--
ALTER TABLE `users_tokens`
  ADD CONSTRAINT `ut_user_id_fk` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

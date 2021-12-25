-- phpMyAdmin SQL Dump
-- version 5.1.1
-- https://www.phpmyadmin.net/
--
-- Hôte : localhost:3306
-- Généré le : ven. 24 déc. 2021 à 09:35
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

--
-- Déchargement des données de la table `roles`
--

INSERT INTO `roles` (`id`, `name`) VALUES
('ce76b531-6133-11ec-bb4c-8c1645b816b6', 'value');

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
  `registration_number` char(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Structure de la table `users_lessons`
--

CREATE TABLE `users_lessons` (
  `id` int(11) NOT NULL,
  `user_id` char(36) NOT NULL,
  `role_id` char(36) NOT NULL,
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
  ADD KEY `uc_lesson_id_fk` (`lesson_id`) USING BTREE,
  ADD KEY `uc_role_id_fk` (`role_id`),
  ADD KEY `uc_user_id_fk` (`user_id`);

--
-- Index pour la table `users_roles`
--
ALTER TABLE `users_roles`
  ADD KEY `ur_user_id_fk` (`user_id`),
  ADD KEY `ur_role_id_fk` (`role_id`);

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
  ADD CONSTRAINT `uc_role_id_fk` FOREIGN KEY (`role_id`) REFERENCES `roles` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `uc_user_id_fk` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Contraintes pour la table `users_roles`
--
ALTER TABLE `users_roles`
  ADD CONSTRAINT `ur_role_id_fk` FOREIGN KEY (`role_id`) REFERENCES `roles` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `ur_user_id_fk` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

DELIMITER $$
--
-- Procédures
--
CREATE DEFINER=`root`@`localhost` PROCEDURE `spCreateLesson` (IN `name` TINYTEXT, IN `description` TEXT)  INSERT INTO lessons (`name`, `description`)
VALUES (name, description)$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `spCreateLessonDetail` (IN `title` TINYTEXT, IN `details` LONGTEXT, IN `timetable_id` INT)  INSERT INTO lesson_details (`title`, `details`, `lesson_timetable_id`)
VALUES (title, details, timetable_id)$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `spCreateLessonFile` (IN `filepath` TEXT, IN `lesson_detail_id` INT)  INSERT INTO lesson_files (`file_path`, `lesson_detail_id`)
VALUES (filepath, lesson_detail_id)$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `spCreateLessonTimetable` (IN `lesson_id` INT, IN `starts_at` DATETIME, IN `ends_at` DATETIME)  INSERT INTO lesson_timetables (`starts_at`, `ends_at`, `lesson_id`)
VALUES (starts_at, ends_at, lesson_id)$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `spCreateRole` (IN `name` TINYTEXT)  BEGIN
	DECLARE new_id CHAR(36) DEFAULT UUID();
	INSERT INTO roles (`id`,`name`)
	VALUES (new_id, name);
    SELECT new_id AS 'id';
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `spCreateUser` (IN `email` MEDIUMTEXT, IN `password` TEXT, IN `firstname` TINYTEXT, IN `lastname` TINYTEXT, IN `birth_date` DATETIME, IN `registration_number` CHAR(10))  BEGIN
	DECLARE new_id CHAR(36) DEFAULT UUID();
    
	INSERT INTO users (`id`, `email`, `normalized_email`,
		`password`, `firstname`, `lastname`, `birth_date`, 
                       `registration_number`)
	VALUES 
    (new_id, email, UPPER(email), SHA2(password, 256), 		firstname, lastname, birth_date, registration_number);
    SELECT new_id AS 'id';
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `spDeleteLesson` (IN `lesson_id` INT)  DELETE FROM lessons
WHERE lessons.id = lesson_id$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `spDeleteLessonDetail` (IN `detail_id` INT)  DELETE FROM lesson_details
WHERE lesson_details.id = detail_id$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `spDeleteLessonFile` (IN `file_id` INT)  DELETE FROM lesson_files
WHERE lesson_files.id = file_id$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `spDeleteLessonTimetable` (IN `timetable_id` INT)  DELETE FROM lesson_timetables
WHERE lesson_timetables.id = timetable_id$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `spDeleteUser` (IN `user_id` CHAR(36))  DELETE FROM users
WHERE users.id = user_id$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `spGetLesson` (IN `id` INT)  READS SQL DATA
SELECT * FROM lessons WHERE lessons.id = id$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `spGetLessonDetail` (IN `lesson_id` INT, IN `timetable_id` INT)  READS SQL DATA
SELECT lesson_details.id, lesson_details.title, lesson_details.details, timetable.starts_at, timetable.ends_at, lessons.id AS 'lesson_id', lessons.name, lessons.description
FROM lesson_details
LEFT JOIN lesson_timetables AS timetable ON timetable.id = lesson_details.lesson_timetable_id 
LEFT JOIN lessons ON lessons.id = timetable.lesson_id
WHERE lessons.id = lesson_id && timetable.id = timetable_id$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `spGetLessonDetails` (IN `lesson_id` INT)  READS SQL DATA
SELECT lesson_details.id, lesson_details.title, lesson_details.details, timetable.starts_at, timetable.ends_at, lessons.id AS 'lesson_id', lessons.name, lessons.description
FROM lesson_details
LEFT JOIN lesson_timetables AS timetable ON timetable.id = lesson_details.lesson_timetable_id 
LEFT JOIN lessons ON lessons.id = timetable.lesson_id
WHERE lessons.id = lesson_id$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `spGetLessonFiles` (IN `lesson_id` INT)  READS SQL DATA
SELECT lessons.id AS 'lesson_id', lesson_files.file_path
FROM lesson_files
LEFT JOIN lesson_details ON lesson_details.id = lesson_files.lesson_detail_id
LEFT JOIN lesson_timetables ON lesson_timetables.id = lesson_details.lesson_timetable_id
LEFT JOIN lessons ON lessons.id = lesson_timetables.lesson_id
WHERE lessons.id = lesson_id$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `spGetLessons` ()  READS SQL DATA
SELECT * FROM lessons$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `spGetLessonTimetables` (IN `lesson_id` INT)  READS SQL DATA
SELECT lesson_timetables.id, starts_at, ends_at, lesson.id as 'lesson_id', lesson.name, lessons.description 
FROM class_timetable 
LEFT JOIN lessons ON lesson_timetables.lesson_id = lessons.id
WHERE lessons.id = lesson_id$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `spGetRole` (IN `id` CHAR(36))  READS SQL DATA
SELECT * FROM roles WHERE roles.id = id$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `spGetRoles` ()  READS SQL DATA
SELECT * FROM roles$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `spGetUser` (IN `user_id` VARCHAR(255))  READS SQL DATA
SELECT * FROM users WHERE users.id = user_id$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `spGetUsers` ()  READS SQL DATA
SELECT * FROM users$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `spUpdateLesson` (IN `lesson_id` INT, IN `name` TINYTEXT, IN `description` TEXT)  UPDATE lessons
SET lessons.name = name, lessons.description = description
WHERE lessons.id = lesson_id$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `spUpdateLessonDetail` (IN `detail_id` INT, IN `title` TINYTEXT, IN `details` LONGTEXT)  UPDATE lesson_details
SET lesson_details.title = title, lesson_details.details = details
WHERE lesson_details.id = detail_id$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `spUpdateLessonFile` (IN `file_id` INT, IN `filepath` TEXT)  UPDATE lesson_files
SET lesson_files.file_path = filepath
WHERE lesson_files.id = file_id$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `spUpdateLessonTimetable` (IN `timetable_id` INT, IN `starts_at` DATETIME, IN `ends_at` DATETIME)  UPDATE lesson_timetables
SET lesson_timetables.starts_at = starts_at, lesson_timetables.ends_at = ends_at
WHERE lesson_timetables.id = timetable_id$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `spUpdateRole` (IN `role_id` CHAR(36), IN `name` TINYTEXT)  UPDATE roles
SET roles.name = name
WHERE roles.id = role_id$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `spUpdateUser` (IN `user_id` CHAR(36), IN `firstname` TINYTEXT, IN `lastname` TINYTEXT, IN `birth_date` DATE)  UPDATE users
SET users.firstname = firstname, users.lastname = lastname, users.birth_date = birth_date
WHERE users.id = user_id$$

DELIMITER ;
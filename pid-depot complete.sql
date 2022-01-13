-- phpMyAdmin SQL Dump
-- version 5.1.1
-- https://www.phpmyadmin.net/
--
-- Hôte : localhost:3306
-- Généré le : jeu. 13 jan. 2022 à 13:28
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

DELIMITER $$
--
-- Procédures
--
CREATE DEFINER=`root`@`localhost` PROCEDURE `spActivateAccount` (IN `user_id` CHAR(36))  BEGIN
	UPDATE users
    SET users.is_activated = true, users.security_stamp = UUID()
    WHERE users.id = user_id;
    
    SELECT ROW_COUNT();
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `spCreateLesson` (IN `name` TINYTEXT, IN `description` TEXT)  BEGIN

	INSERT INTO lessons (`name`, `description`)
	VALUES (name, description);
	
    SELECT LAST_INSERT_ID();
    
END$$

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
    
	INSERT INTO users (`id`, `email`, `normalized_email`, `password`, `firstname`, `lastname`, `birth_date`, `registration_number`, `is_activated`, `security_stamp`, `concurrency_stamp`)
	VALUES 
    (new_id, email, UPPER(email), SHA2(password, 256), 		firstname, lastname, birth_date, registration_number, false, UUID(), UUID());
    SELECT new_id AS 'id';
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `spCreateUserToken` (IN `user_id` CHAR(36), IN `token_type` VARCHAR(100))  BEGIN
	
    DECLARE user_stamp CHAR(36) DEFAULT '';
    
    SET user_stamp = (SELECT @user_stamp := users.security_stamp FROM users WHERE users.id = user_id);

	INSERT INTO users_tokens (`user_id`, `token_type`, `token_value`, `delivery_date`, `expiration_date`)
	VALUES (user_id, token_type, fnGenerateToken(user_id, user_stamp) , NOW(), NOW() + INTERVAL 1 DAY);

	SELECT LAST_INSERT_ID();
    
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `spDeleteLesson` (IN `lesson_id` INT)  BEGIN
	DELETE FROM lessons
	WHERE lessons.id = lesson_id;
    
    SELECT ROW_COUNT();
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `spDeleteLessonDetail` (IN `detail_id` INT)  BEGIN
	DELETE FROM lesson_details
	WHERE lesson_details.id = detail_id;
    
    SELECT ROW_COUNT();
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `spDeleteLessonFile` (IN `file_id` INT)  BEGIN
	DELETE FROM lesson_files
	WHERE lesson_files.id = file_id;
    
    SELECT ROW_COUNT();
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `spDeleteLessonTimetable` (IN `timetable_id` INT)  BEGIN
	DELETE FROM lesson_timetables
	WHERE lesson_timetables.id = timetable_id;
    
    SELECT ROW_COUNT();
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `spDeleteRole` (IN `role_id` INT)  BEGIN
	DELETE FROM roles
	WHERE roles.id = role_id;
    
    SELECT ROW_COUNT();
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `spDeleteUser` (IN `user_id` CHAR(36))  BEGIN
	DELETE FROM users
	WHERE users.id = user_id;
    
    SELECT ROW_COUNT();
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `spDeleteUserToken` (IN `id` INT)  BEGIN
	DELETE FROM users_tokens WHERE users_tokens.id = id;
    
    SELECT ROW_COUNT();
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `spEmailExist` (IN `email` MEDIUMTEXT)  SELECT EXISTS(SELECT id FROM users WHERE users.normalized_email = UPPER(email))$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `spGetLesson` (IN `id` INT)  READS SQL DATA
SELECT * FROM lessons WHERE lessons.id = id$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `spGetLessonDetail` (IN `timetable_id` INT)  READS SQL DATA
SELECT lesson_details.id, lesson_details.title, lesson_details.details, lesson_details.lesson_timetable_id
FROM lesson_details
LEFT JOIN lesson_timetables AS timetable ON timetable.id = lesson_details.lesson_timetable_id
WHERE timetable.id = timetable_id$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `spGetLessonDetailFiles` (IN `details_id` INT)  READS SQL DATA
SELECT lesson_files.id, lesson_files.file_path, lesson_files.lesson_detail_id
FROM lesson_files
LEFT JOIN lesson_details AS details ON details.id = lesson_files.id
WHERE details.id = details_id$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `spGetLessonDetails` (IN `lesson_id` INT)  READS SQL DATA
SELECT lesson_details.id, lesson_details.title, lesson_details.details, lesson_details.lesson_timetable_id
FROM lesson_details
LEFT JOIN lesson_timetables AS timetable ON timetable.id = lesson_details.lesson_timetable_id 
LEFT JOIN lessons ON lessons.id = timetable.lesson_id
WHERE lessons.id = lesson_id$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `spGetLessonFiles` (IN `lesson_id` INT)  READS SQL DATA
SELECT lesson_files.id, lesson_files.file_path, lesson_files.lesson_detail_id
FROM lesson_files
LEFT JOIN lesson_details ON lesson_details.id = lesson_files.lesson_detail_id
LEFT JOIN lesson_timetables ON lesson_timetables.id = lesson_details.lesson_timetable_id
LEFT JOIN lessons ON lessons.id = lesson_timetables.lesson_id
WHERE lessons.id = lesson_id$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `spGetLessons` ()  READS SQL DATA
SELECT * FROM lessons$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `spGetLessonTimetables` (IN `lesson_id` INT)  READS SQL DATA
SELECT lesson_timetables.id,lesson_timetables.starts_at, lesson_timetables.ends_at, lesson_timetables.lesson_id
FROM lesson_timetables
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

CREATE DEFINER=`root`@`localhost` PROCEDURE `spGetUserToken` (IN `id` INT)  READS SQL DATA
SELECT * FROM users_tokens WHERE users_tokens.id = id$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `spGetUserTokens` (IN `user_id` CHAR(36))  SELECT * FROM users_tokens WHERE users_tokens.user_id = user_id$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `spTokenIsValid` (IN `user_id` CHAR(36), IN `token` TEXT)  BEGIN 
	DECLARE user_security_stamp CHAR(36) DEFAULT '';
    DECLARE expiration_date DATETIME DEFAULT CAST('0001-01-01 00:00:00' AS DATETIME);
    
	SET user_security_stamp = (SELECT users.security_stamp FROM users WHERE users.id = user_id);
    
    IF (fnCompareToken(token, user_id, user_security_stamp)) THEN
    	SET expiration_date = 
        	(SELECT users_tokens.expiration_date FROM users_tokens WHERE users_tokens.token_value = token AND users_tokens.delivery_date = 
             	(SELECT MAX(users_tokens.delivery_date) FROM users_tokens WHERE users_tokens.user_id = user_id AND users_tokens.token_value = token));
        
        IF (expiration_date > NOW()) THEN
    		SELECT true;
        ELSE
        	SELECT false;
		END IF;
	ELSE
    	SELECT false;
	END IF;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `spUpdateLesson` (IN `lesson_id` INT, IN `name` TINYTEXT, IN `description` TEXT)  BEGIN
	UPDATE lessons
	SET lessons.name = name, lessons.description = description
	WHERE lessons.id = lesson_id;
    
    SELECT ROW_COUNT();
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `spUpdateLessonDetail` (IN `detail_id` INT, IN `title` TINYTEXT, IN `details` LONGTEXT)  BEGIN
	UPDATE lesson_details
	SET lesson_details.title = title, lesson_details.details = details
	WHERE lesson_details.id = detail_id;
    
    SELECT ROW_COUNT();
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `spUpdateLessonFile` (IN `file_id` INT, IN `filepath` TEXT)  BEGIN
	UPDATE lesson_files
	SET lesson_files.file_path = filepath
	WHERE lesson_files.id = file_id;
    
    SELECT ROW_COUNT();
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `spUpdateLessonTimetable` (IN `timetable_id` INT, IN `starts_at` DATETIME, IN `ends_at` DATETIME)  BEGIN
	UPDATE lesson_timetables
	SET lesson_timetables.starts_at = starts_at, lesson_timetables.ends_at = ends_at
	WHERE lesson_timetables.id = timetable_id;
    
    SELECT ROW_COUNT();
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `spUpdateRole` (IN `role_id` CHAR(36), IN `name` TINYTEXT)  BEGIN
	UPDATE roles
	SET roles.name = name
	WHERE roles.id = role_id;
    
    SELECT ROW_COUNT();
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `spUpdateUser` (IN `user_id` CHAR(36), IN `firstname` TINYTEXT, IN `lastname` TINYTEXT, IN `birth_date` DATE)  BEGIN
	UPDATE users
	SET users.firstname = firstname, users.lastname = lastname, users.birth_date = birth_date, concurrency_stamp = UUID()
	WHERE users.id = user_id;
	
    SELECT ROW_COUNT();
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `spUpdateUserPassword` (IN `user_id` CHAR(36), IN `old_password_confirmation` TEXT, IN `new_password` TEXT)  BEGIN
	DECLARE old_password TEXT DEFAULT '';
    
    SET old_password = (SELECT users.password FROM users WHERE users.id = user_id);
    
    IF (SHA2(old_password_confirmation, 256) LIKE old_password) THEN
    	UPDATE users SET users.password = SHA2(new_password, 256), users.security_stamp = UUID() WHERE users.id = user_id;
	END IF;
    
    SELECT ROW_COUNT();
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `spUserLogin` (IN `auth_email` MEDIUMTEXT, IN `auth_password` TEXT)  BEGIN

DECLARE passwordHash TEXT DEFAULT SHA2(auth_password, 256);
    
SELECT * FROM users WHERE users.normalized_email = UPPER(auth_email) AND users.password = passwordHash LIMIT 1;

END$$

--
-- Fonctions
--
CREATE DEFINER=`root`@`localhost` FUNCTION `fnCompareToken` (`token` TEXT, `user_id` CHAR(36), `security_stamp` CHAR(36)) RETURNS TINYINT(1) BEGIN 
	RETURN token = fnGenerateToken(user_id, security_stamp);
END$$

CREATE DEFINER=`root`@`localhost` FUNCTION `fnGenerateToken` (`user_id` CHAR(36), `security_stamp` CHAR(36)) RETURNS TEXT CHARSET latin1 BEGIN
	RETURN SHA2(CONCAT(user_id, security_stamp, 'pid-depot'),256);
END$$

DELIMITER ;

-- --------------------------------------------------------

--
-- Structure de la table `lessons`
--

CREATE TABLE `lessons` (
  `id` int(11) NOT NULL,
  `name` tinytext NOT NULL,
  `description` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Déchargement des données de la table `lessons`
--

INSERT INTO `lessons` (`id`, `name`, `description`) VALUES
(1, 'test1', 'teztzetzetzet'),
(2, 'test2', ''),
(3, 'test2', 'ezzezerzerzer'),
(4, 'dfzdzdfz', '');

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
('3dcc2feb-672e-11ec-b893-e4e749988ea4', 'Admin'),
('45194bcb-672e-11ec-b893-e4e749988ea4', 'Client'),
('47f60935-672e-11ec-b893-e4e749988ea4', 'Teacher'),
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
  `registration_number` char(10) NOT NULL,
  `security_stamp` char(36) NOT NULL,
  `concurrency_stamp` char(36) NOT NULL,
  `is_activated` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Déchargement des données de la table `users`
--

INSERT INTO `users` (`id`, `email`, `normalized_email`, `password`, `firstname`, `lastname`, `birth_date`, `registration_number`, `security_stamp`, `concurrency_stamp`, `is_activated`) VALUES
('89764bdb-69a5-11ec-9b55-e4e749988ea4', 'soultan_98@hotmail.com', 'SOULTAN_98@HOTMAIL.COM', '7092fd8d6c7051cefb3e18246579594f7fdd753a8bf8b843c2e6fcd065673784', 'soultan', 'hatsijevi', '2012-12-11', 'u987654321', 'af929f14-69a5-11ec-9b55-e4e749988ea4', '282306f3-69a6-11ec-9b55-e4e749988ea4', 0);

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
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

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

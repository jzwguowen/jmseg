CREATE TABLE `users` (
  `id` int(10) NOT NULL AUTO_INCREMENT,
  `name` varchar(500) NOT NULL,
  `email` varchar(500) NOT NULL,
  `password` varchar(500) NOT NULL,
  PRIMARY KEY (ID),
  UNIQUE KEY(email)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
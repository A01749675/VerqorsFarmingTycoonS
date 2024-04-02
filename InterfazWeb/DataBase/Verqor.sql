-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Servidor: localhost
-- Tiempo de generación: 02-04-2024 a las 15:37:58
-- Versión del servidor: 10.4.28-MariaDB
-- Versión de PHP: 8.2.4

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `Verqor`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `Cosecha`
--

CREATE TABLE `Cosecha` (
  `id` int(11) NOT NULL,
  `id_progreso` int(11) NOT NULL,
  `trigo` int(11) NOT NULL,
  `tomate` int(11) NOT NULL,
  `chile` int(11) NOT NULL,
  `maiz` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `Cultivo`
--

CREATE TABLE `Cultivo` (
  `id` int(11) NOT NULL,
  `id_progreso` int(11) NOT NULL,
  `posx` float NOT NULL,
  `posy` float NOT NULL,
  `semilla` int(11) NOT NULL,
  `estado` int(11) NOT NULL,
  `hora_plantacion` time NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `Cultivo`
--

INSERT INTO `Cultivo` (`id`, `id_progreso`, `posx`, `posy`, `semilla`, `estado`, `hora_plantacion`) VALUES
(1, 24, 12, 12, 1, 1, '20:23:30');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `Progreso`
--

CREATE TABLE `Progreso` (
  `id` int(11) NOT NULL,
  `id_usuario` int(11) NOT NULL,
  `dinero` float NOT NULL DEFAULT 0,
  `ciclo` int(11) NOT NULL DEFAULT 0,
  `seguro` int(11) NOT NULL,
  `practica` int(11) DEFAULT NULL,
  `financiamiento` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `Progreso`
--

INSERT INTO `Progreso` (`id`, `id_usuario`, `dinero`, `ciclo`, `seguro`, `practica`, `financiamiento`) VALUES
(1, 9, 200, 0, 1, 1, 1),
(24, 25, 100, 0, 1, 1, 1),
(25, 10, 500, 0, 0, 0, 3),
(26, 26, 0, 0, 1, NULL, 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `Semillas`
--

CREATE TABLE `Semillas` (
  `id` int(11) NOT NULL,
  `id_progreso` int(11) NOT NULL,
  `maiz` int(11) NOT NULL,
  `trigo` int(11) NOT NULL,
  `tomate` int(11) NOT NULL,
  `chile` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `Semillas`
--

INSERT INTO `Semillas` (`id`, `id_progreso`, `maiz`, `trigo`, `tomate`, `chile`) VALUES
(1, 1, 12, 12, 12, 12);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `usuarios`
--

CREATE TABLE `usuarios` (
  `id` int(11) NOT NULL,
  `usuario` varchar(255) NOT NULL,
  `correo` varchar(255) NOT NULL,
  `contraseña` varchar(255) NOT NULL,
  `tipo_usuario` varchar(50) NOT NULL,
  `fecha_nacimiento` date DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `usuarios`
--

INSERT INTO `usuarios` (`id`, `usuario`, `correo`, `contraseña`, `tipo_usuario`, `fecha_nacimiento`) VALUES
(9, 'Marzy', 'm@gmail.com', '$2y$10$8nrqPY4aCw4JCo9qUA5iUuT.CjuTZXpPHoEadPTzuJErIziSvhf.e', 'inversionista', NULL),
(10, 'Alma', 'alma@gmail.com', '$2y$10$RPyVeM.UOmkJoxYvbRyYqeX082U.hoWqucT0D4aQlRao0QLu5Sn0u', 'cliente_regular', '2004-01-03'),
(12, 'Chevez', 'c@gmail.com', '$2y$10$TT23iLP27ESdxWEnk4M6deudETOjgUS1INlZGZ/W6TDgCzeDoKueS', 'administrador', '0000-00-00'),
(13, 'Iker', 'iker@gmail.com', '$2y$10$rqbD22NvIgh51e6UWb/gYujxQSltM1i3qGV8rQjDY7Llt7Qk6mqta', 'publico_general', '2004-12-06'),
(25, 'user', 'user@correo.com', '$2y$10$n0vdiIpEs5Ct2m..zk/sRuUSwyqRYc/CcZ.gDCB1uWZWiMAJ7bkNK', 'publico_general', '2024-03-02'),
(26, 'Alan', 'alan@yahoo.com.mx', '$2y$10$7N365EmCLAFV5qwtO7mbHO0WoKzIePyFD8nboDg/fRQQA6rahtf46', 'cliente_regular', '1990-02-15'),
(27, 'Nuevo', 'usuario@gmail.com', '$2y$10$oM3BiXdMWRTZaG6gimK66OygQuPJQr8ujtxGQzdaAACz7HJbcpwXq', 'publico_general', '2007-01-04');

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `Cosecha`
--
ALTER TABLE `Cosecha`
  ADD PRIMARY KEY (`id`),
  ADD KEY `id_progreso` (`id_progreso`);

--
-- Indices de la tabla `Cultivo`
--
ALTER TABLE `Cultivo`
  ADD PRIMARY KEY (`id`),
  ADD KEY `id_progreso` (`id_progreso`);

--
-- Indices de la tabla `Progreso`
--
ALTER TABLE `Progreso`
  ADD PRIMARY KEY (`id`),
  ADD KEY `id_usuario` (`id_usuario`);

--
-- Indices de la tabla `Semillas`
--
ALTER TABLE `Semillas`
  ADD PRIMARY KEY (`id`),
  ADD KEY `id_progreso` (`id_progreso`);

--
-- Indices de la tabla `usuarios`
--
ALTER TABLE `usuarios`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `Cultivo`
--
ALTER TABLE `Cultivo`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT de la tabla `Progreso`
--
ALTER TABLE `Progreso`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=27;

--
-- AUTO_INCREMENT de la tabla `Semillas`
--
ALTER TABLE `Semillas`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT de la tabla `usuarios`
--
ALTER TABLE `usuarios`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=28;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `Cosecha`
--
ALTER TABLE `Cosecha`
  ADD CONSTRAINT `cosecha_ibfk_1` FOREIGN KEY (`id_progreso`) REFERENCES `Progreso` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Filtros para la tabla `Cultivo`
--
ALTER TABLE `Cultivo`
  ADD CONSTRAINT `cultivo_ibfk_1` FOREIGN KEY (`id_progreso`) REFERENCES `Progreso` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Filtros para la tabla `Progreso`
--
ALTER TABLE `Progreso`
  ADD CONSTRAINT `progreso_ibfk_1` FOREIGN KEY (`id_usuario`) REFERENCES `usuarios` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Filtros para la tabla `Semillas`
--
ALTER TABLE `Semillas`
  ADD CONSTRAINT `semillas_ibfk_1` FOREIGN KEY (`id_progreso`) REFERENCES `Progreso` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

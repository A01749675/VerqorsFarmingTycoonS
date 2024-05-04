
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
  `trigo` int(11) DEFAULT NULL,
  `tomate` int(11) DEFAULT NULL,
  `chile` int(11) DEFAULT NULL,
  `maiz` int(11) DEFAULT NULL,
  `aguacate` int(11) DEFAULT NULL,
  `frijol` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `Mejoras`
--

CREATE TABLE `Mejoras` (
  `id` int(11) NOT NULL,
  `id_progreso` int(11) DEFAULT NULL,
  `id_mejora` int(11) DEFAULT NULL,
  `estado` tinyint(1) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `Parcela`
--

CREATE TABLE `Parcela` (
  `id` int(11) NOT NULL,
  `id_progreso` int(11) DEFAULT NULL,
  `id_parcela` int(11) DEFAULT NULL,
  `estado` int(11) DEFAULT NULL,
  `cantidad` int(11) DEFAULT NULL,
  `agua` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `Progreso`
--

CREATE TABLE `Progreso` (
  `id` int(11) NOT NULL,
  `id_usuario` int(11) NOT NULL,
  `dinero` float NOT NULL DEFAULT 0,
  `deuda` float DEFAULT NULL,
  `ciclo` int(11) NOT NULL DEFAULT 0,
  `seguro` int(11) DEFAULT NULL,
  `financiamiento` int(11) DEFAULT NULL,
  `practica` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `Semillas`
--

CREATE TABLE `Semillas` (
  `id` int(11) NOT NULL,
  `id_progreso` int(11) NOT NULL,
  `maiz` int(11) DEFAULT NULL,
  `trigo` int(11) DEFAULT NULL,
  `tomate` int(11) DEFAULT NULL,
  `chile` int(11) DEFAULT NULL,
  `aguacate` int(11) DEFAULT NULL,
  `frijol` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

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
  `fecha_nacimiento` date DEFAULT NULL,
  `token_recuperacion` varchar(255) DEFAULT NULL,
  `token_expiracion` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

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
-- Indices de la tabla `Mejoras`
--
ALTER TABLE `Mejoras`
  ADD PRIMARY KEY (`id`),
  ADD KEY `id_progreso` (`id_progreso`);

--
-- Indices de la tabla `Parcela`
--
ALTER TABLE `Parcela`
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
-- AUTO_INCREMENT de la tabla `Cosecha`
--
ALTER TABLE `Cosecha`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `Mejoras`
--
ALTER TABLE `Mejoras`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `Parcela`
--
ALTER TABLE `Parcela`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `Progreso`
--
ALTER TABLE `Progreso`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `Semillas`
--
ALTER TABLE `Semillas`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `usuarios`
--
ALTER TABLE `usuarios`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `Cosecha`
--
ALTER TABLE `Cosecha`
  ADD CONSTRAINT `cosecha_ibfk_1` FOREIGN KEY (`id_progreso`) REFERENCES `Progreso` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Filtros para la tabla `Mejoras`
--
ALTER TABLE `Mejoras`
  ADD CONSTRAINT `mejoras_ibfk_1` FOREIGN KEY (`id_progreso`) REFERENCES `Progreso` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Filtros para la tabla `Parcela`
--
ALTER TABLE `Parcela`
  ADD CONSTRAINT `parcela_ibfk_1` FOREIGN KEY (`id_progreso`) REFERENCES `Progreso` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

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

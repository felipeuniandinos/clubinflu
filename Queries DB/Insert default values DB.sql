-- Inserts
INSERT INTO Pais (pais) VALUES 
('España'),
('Estados Unidos');

INSERT INTO EstadoUsuario (estadoUsuario) VALUES 
('Pendiente'),
('Activo'),
('Inactivo'),
('Suspendido');

INSERT INTO Genero (genero) VALUES 
('Masculino'),
('Femenino'),
('No Binario');

INSERT INTO RedSocial (redSocial) VALUES 
('Facebook'),
('Twitter'),
('Instagram');

INSERT INTO Estado (idPais, estado) VALUES 
(1, 'Andalucía'),
(1, 'Cataluña'),
(1, 'Comunidad Valenciana'),
(1, 'Galicia'),
(2, 'California'),
(2, 'Florida'),
(2, 'Nevada'),
(2, 'Texas');

INSERT INTO Ciudad (idEstado, ciudad) VALUES 
(1, 'Sevilla'),
(1, 'Málaga'),
(2, 'Barcelona'),
(2, 'Girona'),
(3, 'Valencia'),
(4, 'Pontevedra'),
(5, 'Los Ángeles'),
(5, 'San Francisco'),
(6, 'Miami'),
(7, 'Las Vegas'),
(8, 'Dallas');

INSERT INTO Empresa (
    idCiudad, nombre, nif, url, numeroContacto, sector, direccion
) VALUES (
    1, 'TechCorp', '123456', 'https://techcorp.com', '1234567890', 'Tecnología', 'Calle 123, Ciudad Ejemplo'
);

INSERT INTO TarjetaPago (
    idEmpresa, numeroTarjeta, nombreTitular, fechaExpiracion, codigoSeguridad, activo
) VALUES (
    1, '4111111111111111', 'Yadira Pérez', '2026-12-31', '123', true
);

INSERT INTO UsuarioEmpresa (
    idEmpresa, idEstadoUsuario, correo, clave, fechaCreacion, fechaActualizacion
) VALUES (
    1, 2, 'empresa1@gmail.com', 'Eosj/4VND6pDNCiyXozVXA4jyukkszFGRX8paiay5p9rC3I+Dg7wtTStlC3MSbyY', CURRENT_DATE, CURRENT_DATE
);

INSERT INTO Influencer (
    idCiudad, idCiudad2, idCiudad3, idCiudad4, idGenero, nombre, fechaNacimiento, numeroContacto
) VALUES (
    1, NULL, NULL, NULL, 1, 'Juan Pérez', '1990-05-20', '5551234567'
);

INSERT INTO UsuarioInfluencer (
    idInfluencer, idEstadoUsuario, correo, clave, fechaCreacion, fechaActualizacion
) VALUES (
    1, 2, 'influ1@gmail.com', 'Eosj/4VND6pDNCiyXozVXA4jyukkszFGRX8paiay5p9rC3I+Dg7wtTStlC3MSbyY', '2023-02-01', '2023-02-01'
);

INSERT INTO UsuarioAdministrador (
    idEstadoUsuario, correo, clave, fechaCreacion, fechaActualizacion
) VALUES (
    2, 'admin@gmail.com', 'Eosj/4VND6pDNCiyXozVXA4jyukkszFGRX8paiay5p9rC3I+Dg7wtTStlC3MSbyY', '2023-02-01', '2023-02-01'
);

INSERT INTO InfluencerRedSocial (
    idInfluencer, idRedSocial, numeroSeguidores, activo, videoEstadisticas, fechaCreacion, fechaActualizacion
) VALUES 
    (1, 1, 50000, true, 'demo.mp4', '2023-01-01', '2023-01-01'),
    (1, 2, 120000, true, 'demo.mp4', '2023-02-15', '2023-02-15'),
    (1, 3, 75000, false, 'demo.mp4', '2023-03-10', '2023-03-10');

INSERT INTO EstadoCupon (estadoCupon) VALUES
('No redimido'),
('Redimido'),
('Expirado');

INSERT INTO CategoriaOferta (nombre) VALUES
('Gastronomía'),
('Tecnología'),
('Bienestar');

INSERT INTO OfertaServicio (
    nombre, direccion, imagen, descripcion, fechaInicio, fechaFin,
    horaInicio, horaFin, cuposDisponibles, fechaCreacion, activo,
    idCategoriaOferta, idEmpresa
) VALUES 
('Cena para dos', 'Calle Luna 123', 'demo.jpg', 'Cena romántica en restaurante exclusivo.',
 '2025-06-01', '2025-06-30', '19:00', '22:00', 20, CURRENT_DATE, true, 1, 1),
 
('Taller de Arduino', 'Calle Sol 456', 'demo.jpg', 'Curso práctico de electrónica y Arduino.',
 '2025-06-10', '2025-06-20', '10:00', '14:00', 15, CURRENT_DATE, true, 2, 1),
 
('Spa Relax', 'Calle Agua 789', 'demo.jpg', 'Circuito de spa completo con masaje.',
 '2025-05-15', '2025-06-15', '09:00', '18:00', 10, CURRENT_DATE, true, 3, 1),
 
('Cena Sushi', 'Calle Mar 321', 'demo.jpg', 'Buffet libre de sushi para dos personas.',
 '2025-06-01', '2025-06-30', '18:00', '22:00', 25, CURRENT_DATE, true, 1, 1),

('Curso de Python', 'Calle Código 654', 'demo.jpg', 'Aprende Python desde cero.',
 '2025-07-01', '2025-07-15', '16:00', '20:00', 30, CURRENT_DATE, true, 2, 1),

('Yoga al aire libre', 'Parque Central', 'demo.jpg', 'Sesión matutina de yoga al aire libre.',
 '2025-06-05', '2025-06-25', '07:00', '08:30', 50, CURRENT_DATE, true, 3, 1),

('Cata de vinos', 'Calle Uva 12', 'demo.jpg', 'Degustación guiada de vinos nacionales.', 
 '2025-06-15', '2025-06-20', '18:00', '20:00', 12, CURRENT_DATE, true, 1, 1),

('Curso de impresión 3D', 'Calle Maker 101', 'demo.jpg', 'Aprende modelado e impresión 3D.', 
 '2025-07-10', '2025-07-20', '14:00', '17:00', 20, CURRENT_DATE, true, 2, 1),

('Taller de meditación', 'Centro Zen', 'demo.jpg', 'Aprende técnicas de respiración y mindfulness.', 
 '2025-06-05', '2025-06-25', '08:00', '09:00', 25, CURRENT_DATE, true, 3, 1),

('Desayuno buffet', 'Hotel Central', 'demo.jpg', 'Desayuno completo para dos personas.', 
 '2025-06-01', '2025-06-30', '07:00', '10:00', 40, CURRENT_DATE, true, 1, 1),

('Introducción a la robótica', 'Calle Robot 88', 'demo.jpg', 'Clase básica con robots educativos.', 
 '2025-07-05', '2025-07-15', '10:00', '13:00', 18, CURRENT_DATE, true, 2, 1),

('Masaje terapéutico', 'Centro Bienestar', 'demo.jpg', 'Sesión de masaje con aromaterapia.', 
 '2025-06-10', '2025-07-10', '10:00', '19:00', 15, CURRENT_DATE, true, 3, 1),

('Noche de tapas', 'Bar La Plaza', 'demo.jpg', 'Tapas y vino para compartir.', 
 '2025-07-01', '2025-07-31', '20:00', '23:00', 30, CURRENT_DATE, true, 1, 1),

('Curso de desarrollo web', 'Calle Código 101', 'demo.jpg', 'Aprende HTML, CSS y JS desde cero.', 
 '2025-07-10', '2025-07-25', '15:00', '18:00', 22, CURRENT_DATE, true, 2, 1),

('Clase de pilates', 'Gimnasio Vida', 'demo.jpg', 'Sesión grupal para principiantes.', 
 '2025-06-20', '2025-07-20', '09:00', '10:00', 35, CURRENT_DATE, true, 3, 1),

('Menú degustación', 'Restaurante Gourmet', 'demo.jpg', 'Cinco tiempos con maridaje.', 
 '2025-08-01', '2025-08-15', '19:00', '22:00', 10, CURRENT_DATE, true, 1, 1),

('Workshop de IA', 'Calle Datos 99', 'demo.jpg', 'Curso introductorio sobre inteligencia artificial.', 
 '2025-07-01', '2025-07-10', '10:00', '14:00', 20, CURRENT_DATE, true, 2, 1),

('Spa para parejas', 'Spa Azul', 'demo.jpg', 'Circuito de relajación para dos.', 
 '2025-06-15', '2025-07-15', '11:00', '17:00', 12, CURRENT_DATE, true, 3, 1),

('Taller de cocina mediterránea', 'Cocina Viva', 'demo.jpg', 'Prepara platos típicos mediterráneos.', 
 '2025-07-05', '2025-07-20', '17:00', '20:00', 15, CURRENT_DATE, true, 1, 1),

('Laboratorio de apps móviles', 'Calle Android 33', 'demo.jpg', 'Crea tu primera app móvil.', 
 '2025-06-20', '2025-07-05', '13:00', '16:00', 20, CURRENT_DATE, true, 2, 1),

('Yoga y meditación', 'Parque del Lago', 'demo.jpg', 'Sesión mixta al aire libre.', 
 '2025-06-10', '2025-06-30', '07:30', '09:00', 40, CURRENT_DATE, true, 3, 1);


INSERT INTO CuponServicio (
    codigo, fechaRedencion, idOfertaServicio, idEstadoCupon, idinfluencer 
) VALUES
('CUPON1001', NULL, 1, 1, NULL),
('CUPON1002', '2025-04-21', 1, 2, 1),
('CUPON1003', '2025-03-24', 1, 3, 1),
('CUPON1004', NULL, 2, 1, NULL),
('CUPON1005', '2025-04-23', 2, 2, 1),
('CUPON1006', '2025-04-16', 2, 3, 1),
('CUPON1007', NULL, 3, 1, 1),
('CUPON1008', '2025-05-02', 3, 2, 1),
('CUPON1009', '2025-03-30', 3, 3, 1),
('CUPON1010', NULL, 4, 1, 1),
('CUPON1011', '2025-05-14', 4, 2, 1),
('CUPON1012', '2025-04-02', 4, 3, 1),
('CUPON1013', NULL, 5, 1, NULL),
('CUPON1014', '2025-04-24', 5, 2, 1),
('CUPON1015', '2025-04-04', 5, 3, 1),
('CUPON1016', NULL, 6, 1, NULL),
('CUPON1017', '2025-05-13', 6, 2, 1),
('CUPON1018', '2025-04-06', 6, 3, 1),
('CUPON1019', NULL, 7, 1, 1),
('CUPON1020', '2025-04-21', 7, 2, 1),
('CUPON1021', '2025-04-16', 7, 3, 1),
('CUPON1022', NULL, 8, 1, NULL),
('CUPON1023', '2025-05-15', 8, 2, 1),
('CUPON1024', '2025-04-14', 8, 3, 1),
('CUPON1025', NULL, 9, 1, 1),
('CUPON1026', '2025-05-02', 9, 2, 1),
('CUPON1027', '2025-04-18', 9, 3, 1),
('CUPON1028', NULL, 10, 1, 1),
('CUPON1029', '2025-05-12', 10, 2, 1),
('CUPON1030', '2025-04-01', 10, 3, 1),
('CUPON1031', NULL, 11, 1, 1),
('CUPON1032', '2025-04-29', 11, 2, 1),
('CUPON1033', '2025-04-18', 11, 3, 1),
('CUPON1034', NULL, 12, 1, NULL),
('CUPON1035', '2025-05-13', 12, 2, 1),
('CUPON1036', '2025-04-11', 12, 3, 1),
('CUPON1037', NULL, 13, 1, 1),
('CUPON1038', '2025-05-03', 13, 2, 1),
('CUPON1039', '2025-04-07', 13, 3, 1),
('CUPON1040', NULL, 14, 1, 1),
('CUPON1041', '2025-04-29', 14, 2, 1),
('CUPON1042', '2025-04-17', 14, 3, 1),
('CUPON1043', NULL, 15, 1, NULL),
('CUPON1044', '2025-04-30', 15, 2, 1),
('CUPON1045', '2025-04-15', 15, 3, 1),
('CUPON1046', NULL, 16, 1, NULL),
('CUPON1047', '2025-05-09', 16, 2, 1),
('CUPON1048', '2025-04-03', 16, 3, 1),
('CUPON1049', NULL, 17, 1, 1),
('CUPON1050', '2025-04-24', 17, 2, 1),
('CUPON1051', '2025-04-16', 17, 3, 1),
('CUPON1052', NULL, 18, 1, NULL),
('CUPON1053', '2025-05-04', 18, 2, 1),
('CUPON1054', '2025-03-24', 18, 3, 1),
('CUPON1055', NULL, 19, 1, 1),
('CUPON1056', '2025-05-04', 19, 2, 1),
('CUPON1057', '2025-04-12', 19, 3, 1),
('CUPON1058', NULL, 20, 1, 1),
('CUPON1059', '2025-05-16', 20, 2, 1),
('CUPON1060', '2025-04-15', 20, 3, 1),
('CUPON1061', NULL, 21, 1, 1),
('CUPON1062', '2025-05-12', 21, 2, 1),
('CUPON1063', '2025-03-27', 21, 3, 1);


INSERT INTO VideoPublicidad (
	videopublicidad, fechacreacion, idcuponservicio  
) VALUES 
('videoDemo.mp4', CURRENT_DATE, 2),
('videoDemo.mp4', CURRENT_DATE, 2),
('videoDemo.mp4', CURRENT_DATE, 2),
('videoDemo.mp4', CURRENT_DATE, 5),
('videoDemo.mp4', CURRENT_DATE, 5),
('videoDemo.mp4', CURRENT_DATE, 5),
('videoDemo.mp4', CURRENT_DATE, 8),
('videoDemo.mp4', CURRENT_DATE, 8),
('videoDemo.mp4', CURRENT_DATE, 11),
('videoDemo.mp4', CURRENT_DATE, 11),
('videoDemo.mp4', CURRENT_DATE, 11),
('videoDemo.mp4', CURRENT_DATE, 14),
('videoDemo.mp4', CURRENT_DATE, 14),
('videoDemo.mp4', CURRENT_DATE, 17),
('videoDemo.mp4', CURRENT_DATE, 17),
('videoDemo.mp4', CURRENT_DATE, 17),
('videoDemo.mp4', CURRENT_DATE, 20),
('videoDemo.mp4', CURRENT_DATE, 20),
('videoDemo.mp4', CURRENT_DATE, 23),
('videoDemo.mp4', CURRENT_DATE, 23),
('videoDemo.mp4', CURRENT_DATE, 26),
('videoDemo.mp4', CURRENT_DATE, 26),
('videoDemo.mp4', CURRENT_DATE, 29),
('videoDemo.mp4', CURRENT_DATE, 29),
('videoDemo.mp4', CURRENT_DATE, 32),
('videoDemo.mp4', CURRENT_DATE, 32),
('videoDemo.mp4', CURRENT_DATE, 35),
('videoDemo.mp4', CURRENT_DATE, 35),
('videoDemo.mp4', CURRENT_DATE, 35),
('videoDemo.mp4', CURRENT_DATE, 38),
('videoDemo.mp4', CURRENT_DATE, 38),
('videoDemo.mp4', CURRENT_DATE, 41),
('videoDemo.mp4', CURRENT_DATE, 41),
('videoDemo.mp4', CURRENT_DATE, 41),
('videoDemo.mp4', CURRENT_DATE, 44),
('videoDemo.mp4', CURRENT_DATE, 44),
('videoDemo.mp4', CURRENT_DATE, 44),
('videoDemo.mp4', CURRENT_DATE, 47),
('videoDemo.mp4', CURRENT_DATE, 47),
('videoDemo.mp4', CURRENT_DATE, 47),
('videoDemo.mp4', CURRENT_DATE, 50),
('videoDemo.mp4', CURRENT_DATE, 50),
('videoDemo.mp4', CURRENT_DATE, 53),
('videoDemo.mp4', CURRENT_DATE, 53),
('videoDemo.mp4', CURRENT_DATE, 56),
('videoDemo.mp4', CURRENT_DATE, 56),
('videoDemo.mp4', CURRENT_DATE, 59),
('videoDemo.mp4', CURRENT_DATE, 59),
('videoDemo.mp4', CURRENT_DATE, 59),
('videoDemo.mp4', CURRENT_DATE, 62),
('videoDemo.mp4', CURRENT_DATE, 62),
('videoDemo.mp4', CURRENT_DATE, 62);




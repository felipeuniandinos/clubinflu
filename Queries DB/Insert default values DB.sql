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
    1, 2, 'usuario@example.com', 'Eosj/4VND6pDNCiyXozVXA4jyukkszFGRX8paiay5p9rC3I+Dg7wtTStlC3MSbyY', CURRENT_DATE, CURRENT_DATE
);

INSERT INTO Influencer (
    idCiudad, idCiudad2, idCiudad3, idCiudad4, idGenero, nombre, fechaNacimiento, numeroContacto
) VALUES (
    1, NULL, NULL, NULL, 1, 'Juan Pérez', '1990-05-20', '5551234567'
);

INSERT INTO UsuarioInfluencer (
    idInfluencer, idEstadoUsuario, correo, clave, fechaCreacion, fechaActualizacion
) VALUES (
    1, 2, 'usuario1@ejemplo.com', 'Eosj/4VND6pDNCiyXozVXA4jyukkszFGRX8paiay5p9rC3I+Dg7wtTStlC3MSbyY', '2023-02-01', '2023-02-01'
);

INSERT INTO UsuarioAdministrador (
    idEstadoUsuario, correo, clave, fechaCreacion, fechaActualizacion
) VALUES (
    2, 'usuario1@ejemplo.com', 'Eosj/4VND6pDNCiyXozVXA4jyukkszFGRX8paiay5p9rC3I+Dg7wtTStlC3MSbyY', '2023-02-01', '2023-02-01'
);

INSERT INTO InfluencerRedSocial (
    idInfluencer, idRedSocial, numeroSeguidores, activo, fechaCreacion, fechaActualizacion
) VALUES 
    (1, 1, 50000, true, '2023-01-01', '2023-01-01'),
    (1, 2, 120000, true, '2023-02-15', '2023-02-15'),
    (1, 3, 75000, false, '2023-03-10', '2023-03-10');

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
('Cena para dos', 'Calle Luna 123', 'imagenes/ofertas_servicios/oferta_1.png', 'Cena romántica en restaurante exclusivo.',
 '2025-06-01', '2025-06-30', '19:00', '22:00', 20, CURRENT_DATE, true, 1, 1),
 
('Taller de Arduino', 'Calle Sol 456', 'imagenes/ofertas_servicios/oferta_2.png', 'Curso práctico de electrónica y Arduino.',
 '2025-06-10', '2025-06-20', '10:00', '14:00', 15, CURRENT_DATE, true, 2, 1),
 
('Spa Relax', 'Calle Agua 789', 'imagenes/ofertas_servicios/oferta_3.png', 'Circuito de spa completo con masaje.',
 '2025-05-15', '2025-06-15', '09:00', '18:00', 10, CURRENT_DATE, true, 3, 1),
 
('Cena Sushi', 'Calle Mar 321', 'imagenes/ofertas_servicios/oferta_4.png', 'Buffet libre de sushi para dos personas.',
 '2025-06-01', '2025-06-30', '18:00', '22:00', 25, CURRENT_DATE, true, 1, 1),

('Curso de Python', 'Calle Código 654', 'imagenes/ofertas_servicios/oferta_5.png', 'Aprende Python desde cero.',
 '2025-07-01', '2025-07-15', '16:00', '20:00', 30, CURRENT_DATE, true, 2, 1),

('Yoga al aire libre', 'Parque Central', 'imagenes/ofertas_servicios/oferta_6.png', 'Sesión matutina de yoga al aire libre.',
 '2025-06-05', '2025-06-25', '07:00', '08:30', 50, CURRENT_DATE, true, 3, 1),

('Cata de vinos', 'Calle Uva 12', 'imagenes/ofertas_servicios/oferta_7.png', 'Degustación guiada de vinos nacionales.', 
 '2025-06-15', '2025-06-20', '18:00', '20:00', 12, CURRENT_DATE, true, 1, 1),

('Curso de impresión 3D', 'Calle Maker 101', 'imagenes/ofertas_servicios/oferta_8.png', 'Aprende modelado e impresión 3D.', 
 '2025-07-10', '2025-07-20', '14:00', '17:00', 20, CURRENT_DATE, true, 2, 1),

('Taller de meditación', 'Centro Zen', 'imagenes/ofertas_servicios/oferta_9.png', 'Aprende técnicas de respiración y mindfulness.', 
 '2025-06-05', '2025-06-25', '08:00', '09:00', 25, CURRENT_DATE, true, 3, 1),

('Desayuno buffet', 'Hotel Central', 'imagenes/ofertas_servicios/oferta_10.png', 'Desayuno completo para dos personas.', 
 '2025-06-01', '2025-06-30', '07:00', '10:00', 40, CURRENT_DATE, true, 1, 1),

('Introducción a la robótica', 'Calle Robot 88', 'imagenes/ofertas_servicios/oferta_11.png', 'Clase básica con robots educativos.', 
 '2025-07-05', '2025-07-15', '10:00', '13:00', 18, CURRENT_DATE, true, 2, 1),

('Masaje terapéutico', 'Centro Bienestar', 'imagenes/ofertas_servicios/oferta_12.png', 'Sesión de masaje con aromaterapia.', 
 '2025-06-10', '2025-07-10', '10:00', '19:00', 15, CURRENT_DATE, true, 3, 1),

('Noche de tapas', 'Bar La Plaza', 'imagenes/ofertas_servicios/oferta_13.png', 'Tapas y vino para compartir.', 
 '2025-07-01', '2025-07-31', '20:00', '23:00', 30, CURRENT_DATE, true, 1, 1),

('Curso de desarrollo web', 'Calle Código 101', 'imagenes/ofertas_servicios/oferta_14.png', 'Aprende HTML, CSS y JS desde cero.', 
 '2025-07-10', '2025-07-25', '15:00', '18:00', 22, CURRENT_DATE, true, 2, 1),

('Clase de pilates', 'Gimnasio Vida', 'imagenes/ofertas_servicios/oferta_15.png', 'Sesión grupal para principiantes.', 
 '2025-06-20', '2025-07-20', '09:00', '10:00', 35, CURRENT_DATE, true, 3, 1),

('Menú degustación', 'Restaurante Gourmet', 'imagenes/ofertas_servicios/oferta_16.png', 'Cinco tiempos con maridaje.', 
 '2025-08-01', '2025-08-15', '19:00', '22:00', 10, CURRENT_DATE, true, 1, 1),

('Workshop de IA', 'Calle Datos 99', 'imagenes/ofertas_servicios/oferta_17.png', 'Curso introductorio sobre inteligencia artificial.', 
 '2025-07-01', '2025-07-10', '10:00', '14:00', 20, CURRENT_DATE, true, 2, 1),

('Spa para parejas', 'Spa Azul', 'imagenes/ofertas_servicios/oferta_18.png', 'Circuito de relajación para dos.', 
 '2025-06-15', '2025-07-15', '11:00', '17:00', 12, CURRENT_DATE, true, 3, 1),

('Taller de cocina mediterránea', 'Cocina Viva', 'imagenes/ofertas_servicios/oferta_19.png', 'Prepara platos típicos mediterráneos.', 
 '2025-07-05', '2025-07-20', '17:00', '20:00', 15, CURRENT_DATE, true, 1, 1),

('Laboratorio de apps móviles', 'Calle Android 33', 'imagenes/ofertas_servicios/oferta_20.png', 'Crea tu primera app móvil.', 
 '2025-06-20', '2025-07-05', '13:00', '16:00', 20, CURRENT_DATE, true, 2, 1),

('Yoga y meditación', 'Parque del Lago', 'imagenes/ofertas_servicios/oferta_21.png', 'Sesión mixta al aire libre.', 
 '2025-06-10', '2025-06-30', '07:30', '09:00', 40, CURRENT_DATE, true, 3, 1);


INSERT INTO CuponServico (
    codigo, fechaRedencion, idOfertaServicio, idEstadoCupon, idInfluencer
) VALUES
('CUPON1001', NULL, 1, 1, null),
('CUPON1002', '2025-06-12', 2, 2, 1),
('CUPON1003', NULL, 3, 1, 1),
('CUPON1004', '2025-06-08', 4, 2, 1),
('CUPON1005', NULL, 5, 1, 1),
('CUPON1006', '2025-07-01', 6, 3, 1),
('CUPON1007', NULL, 7, 1, NULL),
('CUPON1008', NULL, 8, 1, NULL),
('CUPON1009', NULL, 9, 1, NULL),
('CUPON1010', NULL, 10, 1, NULL),
('CUPON1011', NULL, 11, 1, NULL),
('CUPON1012', NULL, 12, 1, NULL),
('CUPON1013', NULL, 13, 1, NULL),
('CUPON1014', NULL, 14, 1, NULL),
('CUPON1015', NULL, 15, 1, NULL),
('CUPON1016', NULL, 16, 1, NULL),
('CUPON1017', NULL, 17, 1, NULL),
('CUPON1018', NULL, 18, 1, NULL),
('CUPON1019', NULL, 19, 1, NULL),
('CUPON1020', NULL, 20, 1, NULL),
('CUPON1021', NULL, 21, 1, NULL);


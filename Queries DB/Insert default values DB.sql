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
('Cena para dos', 'Calle Luna 123', 'img1.jpg', 'Cena romántica en restaurante exclusivo.',
 '2025-06-01', '2025-06-30', '19:00', '22:00', 20, CURRENT_DATE, true, 1, 1),
 
('Taller de Arduino', 'Calle Sol 456', 'img2.jpg', 'Curso práctico de electrónica y Arduino.',
 '2025-06-10', '2025-06-20', '10:00', '14:00', 15, CURRENT_DATE, true, 2, 1),
 
('Spa Relax', 'Calle Agua 789', 'img3.jpg', 'Circuito de spa completo con masaje.',
 '2025-05-15', '2025-06-15', '09:00', '18:00', 10, CURRENT_DATE, true, 3, 1),
 
('Cena Sushi', 'Calle Mar 321', 'img4.jpg', 'Buffet libre de sushi para dos personas.',
 '2025-06-01', '2025-06-30', '18:00', '22:00', 25, CURRENT_DATE, true, 1, 1),

('Curso de Python', 'Calle Código 654', 'img5.jpg', 'Aprende Python desde cero.',
 '2025-07-01', '2025-07-15', '16:00', '20:00', 30, CURRENT_DATE, true, 2, 1),

('Yoga al aire libre', 'Parque Central', 'img6.jpg', 'Sesión matutina de yoga al aire libre.',
 '2025-06-05', '2025-06-25', '07:00', '08:30', 50, CURRENT_DATE, true, 3, 1);

INSERT INTO CuponServico (
    codigo, fechaRedencion, idOfertaServicio, idEstadoCupon, idInfluencer
) VALUES
('CUPON1001', NULL, 1, 1, null),
('CUPON1002', '2025-06-12', 2, 2, 1),
('CUPON1003', NULL, 3, 1, 1),
('CUPON1004', '2025-06-08', 4, 2, 1),
('CUPON1005', NULL, 5, 1, 1),
('CUPON1006', '2025-07-01', 6, 3, 1);


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
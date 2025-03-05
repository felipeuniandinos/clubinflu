-- Eliminar tablas en orden inverso para evitar errores de referencia
DROP TABLE IF EXISTS UsuarioEmpresa;
DROP TABLE IF EXISTS InfluencerRedSocial;
DROP TABLE IF EXISTS UsuarioInfluencer;
DROP TABLE IF EXISTS Influencer;
DROP TABLE IF EXISTS RedSocial;
DROP TABLE IF EXISTS Empresa;
DROP TABLE IF EXISTS EstadoUsuario;
DROP TABLE IF EXISTS Ciudad;
DROP TABLE IF EXISTS Pais;
DROP TABLE IF EXISTS Genero;

-- Crear tablas
CREATE TABLE EstadoUsuario (
    idEstadoUsuario BIGSERIAL PRIMARY KEY,
    estadoUsuario VARCHAR(100) NOT NULL,
    activo BOOLEAN DEFAULT TRUE
);

CREATE TABLE Pais (
    idPais BIGSERIAL PRIMARY KEY,
    pais VARCHAR(100) NOT NULL,
    activo BOOLEAN DEFAULT TRUE
);

CREATE TABLE Ciudad (
    idCiudad BIGSERIAL PRIMARY KEY,
    idPais BIGINT NOT NULL,
    ciudad VARCHAR(100) NOT NULL,
    activo BOOLEAN DEFAULT TRUE,
    FOREIGN KEY (idPais) REFERENCES Pais(idPais)
);

CREATE TABLE Genero (
    idGenero BIGSERIAL PRIMARY KEY,
    genero VARCHAR(100) NOT NULL,
    activo BOOLEAN DEFAULT TRUE
);

CREATE TABLE RedSocial (
    idRedSocial BIGSERIAL PRIMARY KEY,
    redSocial VARCHAR(100) NOT NULL,
    activo BOOLEAN DEFAULT TRUE
);

CREATE TABLE Empresa (
    idEmpresa BIGSERIAL PRIMARY KEY,
    idCiudad BIGINT NOT NULL,
    idCiudad2 BIGINT NULL,
    idCiudad3 BIGINT NULL,
    idCiudad4 BIGINT NULL,
    nombre VARCHAR(100) NOT NULL,
    nif VARCHAR(20) NOT NULL,
    url VARCHAR(100),
    numeroContacto VARCHAR(10),
    sector TEXT NOT NULL,
    direccion VARCHAR(500) NOT NULL,
    FOREIGN KEY (idCiudad) REFERENCES Ciudad(idCiudad),
    FOREIGN KEY (idCiudad2) REFERENCES Ciudad(idCiudad),
    FOREIGN KEY (idCiudad3) REFERENCES Ciudad(idCiudad),
    FOREIGN KEY (idCiudad4) REFERENCES Ciudad(idCiudad)
);

CREATE TABLE UsuarioEmpresa (
    idUsuarioEmpresa BIGSERIAL PRIMARY KEY,
    idEmpresa BIGINT NOT NULL,
    idEstadoUsuario BIGINT NOT NULL,
    correo VARCHAR(500) NOT NULL,
    clave VARCHAR(500) NOT NULL,
    fechaCreacion DATE NOT NULL,
    fechaActualizacion DATE NOT NULL,
    FOREIGN KEY (idEmpresa) REFERENCES Empresa(idEmpresa),
    FOREIGN KEY (idEstadoUsuario) REFERENCES EstadoUsuario(idEstadoUsuario)
);

CREATE TABLE Influencer (
    idInfluencer BIGSERIAL PRIMARY KEY,
    idCiudad BIGINT NOT NULL,
    idCiudad2 BIGINT NULL,
    idCiudad3 BIGINT NULL,
    idCiudad4 BIGINT NULL,
    idGenero BIGINT NOT NULL,
    nombre VARCHAR(100) NOT NULL,
    fechaNacimiento DATE NOT NULL,
    numeroContacto VARCHAR(10),
    FOREIGN KEY (idCiudad) REFERENCES Ciudad(idCiudad),
    FOREIGN KEY (idCiudad2) REFERENCES Ciudad(idCiudad),
    FOREIGN KEY (idCiudad3) REFERENCES Ciudad(idCiudad),
    FOREIGN KEY (idCiudad4) REFERENCES Ciudad(idCiudad),
    FOREIGN KEY (idGenero) REFERENCES Genero(idGenero)
);

CREATE TABLE UsuarioInfluencer (
    idUsuarioInfluencer BIGSERIAL PRIMARY KEY,
    idInfluencer BIGINT NOT NULL,
    idEstadoUsuario BIGINT NOT NULL,
    correo VARCHAR(500) NOT NULL,
    clave VARCHAR(500) NOT NULL,
    fechaCreacion DATE NOT NULL,
    fechaActualizacion DATE NOT NULL,
    FOREIGN KEY (idInfluencer) REFERENCES Influencer(idInfluencer),
    FOREIGN KEY (idEstadoUsuario) REFERENCES EstadoUsuario(idEstadoUsuario)
);

CREATE TABLE UsuarioAdministrador (
    idUsuarioAdministrador BIGSERIAL PRIMARY KEY,
    idEstadoUsuario BIGINT NOT NULL,
    correo VARCHAR(500) NOT NULL,
    clave VARCHAR(500) NOT NULL,
    fechaCreacion DATE NOT NULL,
    fechaActualizacion DATE NOT NULL,
    FOREIGN KEY (idEstadoUsuario) REFERENCES EstadoUsuario(idEstadoUsuario)
);

CREATE TABLE InfluencerRedSocial (
    idInfluencerRedSocial BIGSERIAL PRIMARY KEY,
    idInfluencer BIGINT NOT NULL,
    idRedSocial BIGINT NOT NULL,
    numeroSeguidores INT NOT NULL,
    activo BOOLEAN DEFAULT TRUE,
    fechaCreacion DATE NOT NULL,
    fechaActualizacion DATE NOT NULL,
    FOREIGN KEY (idInfluencer) REFERENCES Influencer(idInfluencer),
    FOREIGN KEY (idRedSocial) REFERENCES RedSocial(idRedSocial)
);

-- Inserts
INSERT INTO Pais (pais) VALUES 
('Colombia'),
('México'),
('Argentina'),
('España'),
('Estados Unidos');

INSERT INTO Ciudad (idPais, ciudad) VALUES 
(1, 'Bogotá'),
(1, 'Medellín'),
(2, 'Ciudad de México'),
(2, 'Guadalajara'),
(3, 'Buenos Aires'),
(3, 'Córdoba'),
(4, 'Madrid'),
(4, 'Barcelona'),
(5, 'Nueva York'),
(5, 'Los Ángeles');

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

INSERT INTO Empresa 
(idCiudad, nombre, nif, url, numeroContacto, sector, direccion)
VALUES 
(1, 'TechCorp', '123456', 'https://techcorp.com', '1234567890', 'Tecnología', 'Calle 123, Ciudad Ejemplo');

INSERT INTO UsuarioEmpresa 
(idEmpresa, idEstadoUsuario, correo, clave, fechaCreacion, fechaActualizacion)
VALUES 
(1, 2, 'usuario@example.com', 'claveSegura123', CURRENT_DATE, CURRENT_DATE);

INSERT INTO Influencer 
(idCiudad, idCiudad2, idCiudad3, idCiudad4, idGenero, nombre, fechaNacimiento, numeroContacto)
VALUES
  (1, NULL, NULL, NULL, 1, 'Juan Pérez', '1990-05-20', '5551234567');

INSERT INTO UsuarioInfluencer (idInfluencer, idEstadoUsuario, correo, clave, fechaCreacion, fechaActualizacion)
VALUES
  (1, 2, 'usuario1@ejemplo.com', 'clave_secreta1', '2023-02-01', '2023-02-01');

INSERT INTO UsuarioAdministrador (idEstadoUsuario, correo, clave, fechaCreacion, fechaActualizacion)
VALUES
  (2, 'usuario1@ejemplo.com', 'clave_secreta1', '2023-02-01', '2023-02-01');

INSERT INTO InfluencerRedSocial (idInfluencer, idRedSocial, numeroSeguidores, activo, fechaCreacion, fechaActualizacion)
VALUES
  (1, 1, 50000, true, '2023-01-01', '2023-01-01'),
  (1, 2, 120000, true, '2023-02-15', '2023-02-15'),
  (1, 3, 75000, false, '2023-03-10', '2023-03-10');


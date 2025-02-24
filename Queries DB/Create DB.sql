drop table UsuarioEmpresa;
drop table Empresa;
drop table EstadoUsuario;
drop table Ciudad;
drop table Pais;


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
    activo BOOLEAN DEFAULT true,
    FOREIGN KEY (idPais) REFERENCES Pais(idPais)
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


/*INSERT INTO Empresa 
(idCiudad, nombre, nif, url, numeroContacto, sector, direccion)
VALUES 
(1, 'TechCorp', '123456', 'https://techcorp.com', '1234567890', 'Tecnología', 'Calle 123, Ciudad Ejemplo');

INSERT INTO UsuarioEmpresa 
(idEmpresa, idEstadoUsuario, correo, clave, fechaCreacion, fechaActualizacion)
VALUES 
(1, 1, 'usuario@example.com', 'claveSegura123', CURRENT_DATE, CURRENT_DATE);*/


select * from Empresa;

select * from UsuarioEmpresa;


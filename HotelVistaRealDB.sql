-- Hotel Vista Real - Sistema de Gestión Hotelera (SGH)
-- Script de creación de la base de datos, tablas y datos de prueba.

IF DB_ID('HotelVistaRealDB') IS NOT NULL
BEGIN
 ALTER DATABASE HotelVistaRealDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
 DROP DATABASE HotelVistaRealDB;
END
GO
CREATE DATABASE HotelVistaRealDB;
GO
USE HotelVistaRealDB;
GO

CREATE TABLE Roles(
 IdRol INT IDENTITY PRIMARY KEY,
 NombreRol VARCHAR(50) NOT NULL UNIQUE,
 Descripcion VARCHAR(150)
);

CREATE TABLE Usuarios(
 IdUsuario INT IDENTITY PRIMARY KEY,
 Nombre VARCHAR(80) NOT NULL,
 NombreUsuario VARCHAR(40) NOT NULL UNIQUE,
 ContrasenaHash VARCHAR(100) NOT NULL,
 IdRol INT REFERENCES Roles(IdRol),
 Activo BIT DEFAULT 1
);

CREATE TABLE Clientes(
 IdCliente INT IDENTITY PRIMARY KEY,
 Identificacion VARCHAR(20) NOT NULL UNIQUE,
 Nombre VARCHAR(50) NOT NULL,
 Apellidos VARCHAR(80) NOT NULL,
 Telefono VARCHAR(20),
 Correo VARCHAR(80),
 Direccion VARCHAR(150)
);

CREATE TABLE Habitaciones(
 IdHabitacion INT IDENTITY PRIMARY KEY,
 NumeroHabitacion INT NOT NULL UNIQUE,
 Tipo VARCHAR(30) NOT NULL,
 Capacidad INT NOT NULL,
 Tarifa DECIMAL(10,2) NOT NULL,
 Estado VARCHAR(20) NOT NULL DEFAULT 'Disponible'
);

CREATE TABLE Reservaciones(
 IdReservacion INT IDENTITY PRIMARY KEY,
 IdCliente INT REFERENCES Clientes(IdCliente),
 IdHabitacion INT REFERENCES Habitaciones(IdHabitacion),
 FechaReservacion DATETIME DEFAULT GETDATE(),
 FechaEntrada DATE NOT NULL,
 FechaSalida DATE NOT NULL,
 Estado VARCHAR(20) NOT NULL DEFAULT 'Activa'
);

CREATE TABLE Hospedajes(
 IdHospedaje INT IDENTITY PRIMARY KEY,
 IdReservacion INT REFERENCES Reservaciones(IdReservacion),
 FechaCheckIn DATETIME,
 FechaCheckOut DATETIME,
 Estado VARCHAR(20) NOT NULL DEFAULT 'Activo'
);

CREATE TABLE Servicios(
 IdServicio INT IDENTITY PRIMARY KEY,
 Nombre VARCHAR(60) NOT NULL,
 Descripcion VARCHAR(150),
 Precio DECIMAL(10,2) NOT NULL
);

CREATE TABLE DetalleServicios(
 IdDetalleServicio INT IDENTITY PRIMARY KEY,
 IdHospedaje INT REFERENCES Hospedajes(IdHospedaje),
 IdServicio INT REFERENCES Servicios(IdServicio),
 Cantidad INT NOT NULL,
 Subtotal DECIMAL(10,2) NOT NULL
);

CREATE TABLE Facturas(
 IdFactura INT IDENTITY PRIMARY KEY,
 IdHospedaje INT REFERENCES Hospedajes(IdHospedaje),
 FechaFactura DATETIME DEFAULT GETDATE(),
 Subtotal DECIMAL(10,2),
 Impuesto DECIMAL(10,2),
 Descuento DECIMAL(10,2),
 Total DECIMAL(10,2)
);

INSERT INTO Roles(NombreRol,Descripcion) VALUES
('Administrador','Gestiona usuarios y todo el sistema'),
('Recepcionista','Atiende clientes, reservaciones y hospedaje'),
('Facturacion','Genera y consulta facturas'),
('Gerente','Consulta reportes e ingresos');

INSERT INTO Clientes(Identificacion,Nombre,Apellidos,Telefono,Correo,Direccion) VALUES
('101110234','Mario','Morales Soto','88884512','mario@correo.com','Heredia'),
('202220345','Lucía','Vargas Rojas','88776655','lucia@correo.com','Alajuela'),
('303330456','Carlos','Jiménez Mora','88112233','carlos@correo.com','San José'),
('404440567','Andrea','Castro Solano','88445566','andrea@correo.com','Cartago');

INSERT INTO Habitaciones(NumeroHabitacion,Tipo,Capacidad,Tarifa,Estado) VALUES
(101,'Sencilla',1,35000,'Disponible'),
(102,'Doble',2,50000,'Disponible'),
(103,'Doble',2,50000,'Ocupada'),
(201,'Familiar',4,75000,'Disponible'),
(202,'Suite',2,110000,'Mantenimiento');

INSERT INTO Servicios(Nombre,Descripcion,Precio) VALUES
('Desayuno','Desayuno buffet por persona',8000),
('Lavandería','Lavado de ropa por servicio',6000),
('Spa','Masaje relajante',25000);

INSERT INTO Reservaciones(IdCliente,IdHabitacion,FechaEntrada,FechaSalida,Estado) VALUES
(1,3,'2026-11-01','2026-11-04','Activa'),
(2,2,'2026-11-10','2026-11-12','Activa');

INSERT INTO Hospedajes(IdReservacion,FechaCheckIn,FechaCheckOut,Estado) VALUES
(1,'2026-11-01 14:00',NULL,'Activo');

INSERT INTO DetalleServicios(IdHospedaje,IdServicio,Cantidad,Subtotal) VALUES
(1,1,2,16000);

INSERT INTO Facturas(IdHospedaje,Subtotal,Impuesto,Descuento,Total) VALUES
(1,166000,21580,0,187580);

SELECT * FROM Habitaciones;

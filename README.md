# Sistema de Gestión Hotelera (SGH) - Hotel Vista Real

Proyecto final de **Programación V (ISB 6-1)**, Universidad Tecnológica Costarricense.
Entregable 2: Desarrollo técnico parcial del sistema.

## Integrantes

- Joshua Gabriel Quesada
- Bryan Alvarado Hidalgo
- Gustavo Alonso Aguilar

## Descripción

Sistema para gestionar las operaciones del Hotel Vista Real (una empresa simulada): habitaciones, clientes, reservaciones, hospedaje y facturación.

En este avance está implementado el módulo de **Habitaciones**, con su CRUD completo, conectado a SQL Server y en una aplicación de consola.

## Qué incluye este avance

- Base de datos en SQL Server con 9 tablas relacionadas y datos de prueba.
- Clases y entidades: `Habitacion` (clase abstracta), `HabitacionEstandar` y `HabitacionSuite` (herencia y polimorfismo).
- Interfaz `iHabitacionDatos` implementada por `HabitacionDatos` (acceso a datos con ADO.NET).
- Validaciones y reglas de negocio en la capa de negocio.
- Uso de `List`, `Dictionary`, LINQ y expresiones lambda.
- Manejo de excepciones con `try-catch-finally`.
- Respaldo de habitaciones en un archivo JSON (escritura, lectura, serialización y deserialización).

## Estructura del proyecto

- `Entidades`: clases del sistema (`Habitacion`, `HabitacionEstandar`, `HabitacionSuite`, `HabitacionRespaldo`).
- `Datos`: acceso a la base de datos y al archivo JSON (`iHabitacionDatos`, `HabitacionDatos`, `RespaldoJson`).
- `Negocio`: reglas y consultas (`HabitacionNegocio`).
- `Program.cs`: menú de la aplicación de consola.

## Cómo ejecutarlo

1. Abrir SQL Server Management Studio y conectarse a `localhost\SQLEXPRESS`.
2. Abrir el archivo `HotelVistaRealDB.sql` y ejecutarlo. Crea la base de datos `HotelVistaRealDB`, las tablas y los datos de prueba.
3. Abrir `HotelVistaReal.slnx` en Visual Studio. Si falta el paquete NuGet `Microsoft.Data.SqlClient`, se restaura o se instala desde Proyecto > Administrar paquetes NuGet.
4. Si el nombre del servidor es distinto a `localhost\SQLEXPRESS`, cambiarlo en la cadena de conexión de `HabitacionDatos.cs`.
5. Ejecutar con F5.

## Opciones del menú

1. Registrar habitación
2. Mostrar todas las habitaciones
3. Buscar habitación por número
4. Cambiar tarifa
5. Cambiar estado
6. Eliminar habitación
7. Mostrar habitaciones disponibles
8. Mostrar habitaciones ordenadas por tarifa
9. Cantidad de habitaciones por estado
10. Exportar habitaciones a JSON (respaldo)
11. Leer respaldo JSON
12. Salir

## Tecnologías

C#, .NET, SQL Server, ADO.NET, LINQ, JSON, Git y GitHub.

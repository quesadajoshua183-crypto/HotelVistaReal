# Sistema de Gestión Hotelera (SGH) - Hotel Vista Real

Proyecto final de **Programación V (ISB 6-1)**, Universidad Tecnológica Costarricense.
Entregable 2: Desarrollo técnico parcial del sistema.

## Integrantes

- Joshua Gabriel Quesada
- Bryan Alvarado Hidalgo 
- 

## Descripción

Sistema para administrar las operaciones del Hotel Vista Real, una empresa simulada: habitaciones, clientes, reservaciones, hospedaje y facturación.

Este avance incluye el módulo de **Habitaciones**: registrar, consultar, modificar y eliminar habitaciones. Funciona como aplicación de consola y guarda la información en SQL Server.

## Qué incluye este avance

- Base de datos en SQL Server con 9 tablas relacionadas y datos de prueba.
- Clases del sistema: `Habitacion` (clase abstracta), `HabitacionEstandar` y `HabitacionSuite`, que heredan de ella.
- Interfaz `iHabitacionDatos`, implementada por `HabitacionDatos`, que se conecta a la base de datos con ADO.NET.
- Validaciones y reglas de negocio, por ejemplo: no se permiten habitaciones con número repetido ni tarifas menores o iguales a cero.
- Uso de `List`, `Dictionary` y consultas LINQ.
- Manejo de errores con `try-catch-finally`.
- Respaldo de las habitaciones en un archivo JSON, que se puede guardar y volver a leer.

## Estructura del proyecto

- **Entidades:** las clases del sistema (`Habitacion`, `HabitacionEstandar`, `HabitacionSuite`, `HabitacionRespaldo`).
- **Datos:** el acceso a la base de datos y al archivo JSON (`iHabitacionDatos`, `HabitacionDatos`, `RespaldoJson`).
- **Negocio:** las reglas y consultas (`HabitacionNegocio`).
- **Program.cs:** el menú de la aplicación.

## Cómo ejecutarlo

1. Abrir SQL Server Management Studio y conectarse a `localhost\SQLEXPRESS`.
2. Abrir el archivo `HotelVistaRealDB.sql` y ejecutarlo. Crea la base de datos `HotelVistaRealDB`, sus tablas y los datos de prueba.
3. Abrir `HotelVistaReal.slnx` en Visual Studio. El paquete `Microsoft.Data.SqlClient` se descarga solo al compilar. Si marca error, instalarlo desde Proyecto > Administrar paquetes NuGet.
4. Si el servidor tiene otro nombre, cambiarlo en la cadena de conexión del archivo `HabitacionDatos.cs`.
5. Presionar F5 para ejecutar.

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
10. Guardar respaldo en JSON
11. Leer respaldo JSON
12. Salir

## Tecnologías

C#, .NET, SQL Server, ADO.NET, LINQ, JSON, Git y GitHub.



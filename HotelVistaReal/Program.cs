using HotelVistaReal.Datos;
using HotelVistaReal.Entidades;
using HotelVistaReal.Negocio;
using Microsoft.Data.SqlClient;
using System.Text.Json;

namespace HotelVistaReal
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            HabitacionNegocio negocio = new HabitacionNegocio(new HabitacionDatos());
            bool continuar = true;

            while (continuar)
            {
                Console.WriteLine();
                Console.WriteLine("===== HOTEL VISTA REAL - HABITACIONES =====");
                Console.WriteLine("1. Registrar habitación");
                Console.WriteLine("2. Mostrar todas las habitaciones");
                Console.WriteLine("3. Buscar habitación por número");
                Console.WriteLine("4. Cambiar tarifa");
                Console.WriteLine("5. Cambiar estado");
                Console.WriteLine("6. Eliminar habitación");
                Console.WriteLine("7. Mostrar habitaciones disponibles");
                Console.WriteLine("8. Mostrar habitaciones ordenadas por tarifa");
                Console.WriteLine("9. Cantidad de habitaciones por estado");
                Console.WriteLine("10. Exportar habitaciones a JSON (respaldo)");
                Console.WriteLine("11. Leer respaldo JSON");
                Console.WriteLine("12. Salir");
                Console.Write("Seleccione una opción: ");

                string opcion = Console.ReadLine();

                try
                {
                    switch (opcion)
                    {
                        case "1":
                            RegistrarHabitacion(negocio);
                            break;
                        case "2":
                            MostrarLista(negocio.ListarHabitaciones());
                            break;
                        case "3":
                            BuscarHabitacion(negocio);
                            break;
                        case "4":
                            CambiarTarifa(negocio);
                            break;
                        case "5":
                            CambiarEstado(negocio);
                            break;
                        case "6":
                            EliminarHabitacion(negocio);
                            break;
                        case "7":
                            MostrarLista(negocio.ListarDisponibles());
                            break;
                        case "8":
                            MostrarLista(negocio.ListarPorTarifaDescendente());
                            break;
                        case "9":
                            MostrarConteoPorEstado(negocio);
                            break;
                        case "10":
                            ExportarRespaldo(negocio);
                            break;
                        case "11":
                            MostrarRespaldo();
                            break;
                        case "12":
                            continuar = false;
                            Console.WriteLine("Saliendo del sistema...");
                            break;
                        default:
                            throw new Exception("Opción de menú inválida. Elija un número del 1 al 12");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error: debe ingresar un valor numérico válido");
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Error de base de datos: " + ex.Message);
                }
                catch (JsonException)
                {
                    Console.WriteLine("Error: el archivo de respaldo no tiene un formato JSON válido");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                finally
                {
                    Console.WriteLine("----------------------------------------");
                }
            }
        }

        static void RegistrarHabitacion(HabitacionNegocio negocio)
        {
            Console.Write("Número de habitación: ");
            int numero = int.Parse(Console.ReadLine());

            Console.Write("Tipo (Sencilla, Doble, Familiar o Suite): ");
            string tipo = Console.ReadLine();

            Console.Write("Capacidad: ");
            int capacidad = int.Parse(Console.ReadLine());

            Console.Write("Tarifa por noche: ");
            decimal tarifa = decimal.Parse(Console.ReadLine());

            negocio.RegistrarHabitacion(numero, tipo, capacidad, tarifa);
            Console.WriteLine("Habitación registrada correctamente.");
        }

        static void MostrarLista(List<Habitacion> habitaciones)
        {
            if (habitaciones.Count == 0)
            {
                Console.WriteLine("No hay habitaciones para mostrar.");
                return;
            }

            foreach (Habitacion habitacion in habitaciones)
            {
                habitacion.MostrarInformacion();
                Console.WriteLine("   Tarifa por noche: " + habitacion.CalcularTarifaNoche());
            }
        }

        static void BuscarHabitacion(HabitacionNegocio negocio)
        {
            Console.Write("Número de habitación: ");
            int numero = int.Parse(Console.ReadLine());

            Habitacion habitacion = negocio.BuscarHabitacion(numero);

            if (habitacion == null)
            {
                Console.WriteLine("La habitación no está registrada.");
            }
            else
            {
                habitacion.MostrarInformacion();
                Console.WriteLine("   Tarifa por noche: " + habitacion.CalcularTarifaNoche());
            }
        }

        static void CambiarTarifa(HabitacionNegocio negocio)
        {
            Console.Write("Número de habitación: ");
            int numero = int.Parse(Console.ReadLine());

            Console.Write("Nueva tarifa: ");
            decimal nuevaTarifa = decimal.Parse(Console.ReadLine());

            negocio.CambiarTarifa(numero, nuevaTarifa);
            Console.WriteLine("Tarifa actualizada correctamente.");
        }

        static void CambiarEstado(HabitacionNegocio negocio)
        {
            Console.Write("Número de habitación: ");
            int numero = int.Parse(Console.ReadLine());

            Console.Write("Nuevo estado (Disponible, Reservada, Ocupada, Limpieza o Mantenimiento): ");
            string nuevoEstado = Console.ReadLine();

            negocio.CambiarEstado(numero, nuevoEstado);
            Console.WriteLine("Estado actualizado correctamente.");
        }

        static void EliminarHabitacion(HabitacionNegocio negocio)
        {
            Console.Write("Número de habitación a eliminar: ");
            int numero = int.Parse(Console.ReadLine());

            negocio.EliminarHabitacion(numero);
            Console.WriteLine("Habitación eliminada correctamente.");
        }

        static void MostrarConteoPorEstado(HabitacionNegocio negocio)
        {
            Dictionary<string, int> conteo = negocio.ContarPorEstado();

            foreach (KeyValuePair<string, int> item in conteo)
            {
                Console.WriteLine(item.Key + ": " + item.Value);
            }
        }

        static void ExportarRespaldo(HabitacionNegocio negocio)
        {
            RespaldoJson respaldo = new RespaldoJson();
            respaldo.Exportar(negocio.ListarHabitaciones());
            Console.WriteLine("Respaldo guardado en: " + respaldo.ObtenerRuta());
        }

        static void MostrarRespaldo()
        {
            RespaldoJson respaldo = new RespaldoJson();
            List<HabitacionRespaldo> habitaciones = respaldo.Leer();

            foreach (HabitacionRespaldo habitacion in habitaciones)
            {
                Console.WriteLine("Habitación: " + habitacion.NumeroHabitacion + " | Tipo: " + habitacion.Tipo + " | Capacidad: " + habitacion.Capacidad + " | Tarifa: " + habitacion.Tarifa + " | Estado: " + habitacion.Estado);
            }
        }
    }
}
using HotelVistaReal.Datos;
using HotelVistaReal.Entidades;

namespace HotelVistaReal.Negocio
{
    public class HabitacionNegocio
    {
        private iHabitacionDatos datos;

        private List<string> tiposValidos = new List<string> { "Sencilla", "Doble", "Familiar", "Suite" };

        private List<string> estadosValidos = new List<string> { "Disponible", "Reservada", "Ocupada", "Limpieza", "Mantenimiento" };

        public HabitacionNegocio(iHabitacionDatos datos)
        {
            this.datos = datos;
        }

        public void RegistrarHabitacion(int numero, string tipo, int capacidad, decimal tarifa)
        {
            if (!tiposValidos.Contains(tipo))
            {
                throw new Exception("Tipo de habitación inválido. Use: Sencilla, Doble, Familiar o Suite");
            }

            if (datos.BuscarPorNumero(numero) != null)
            {
                throw new Exception("Ya existe una habitación con ese número");
            }

            Habitacion habitacion;

            if (tipo == "Suite")
            {
                habitacion = new HabitacionSuite(0, numero, tipo, capacidad, tarifa, "Disponible");
            }
            else
            {
                habitacion = new HabitacionEstandar(0, numero, tipo, capacidad, tarifa, "Disponible");
            }

            datos.Insertar(habitacion);
        }

        public List<Habitacion> ListarHabitaciones()
        {
            return datos.ObtenerTodas();
        }

        public Habitacion BuscarHabitacion(int numero)
        {
            return datos.BuscarPorNumero(numero);
        }

        public void CambiarTarifa(int numero, decimal nuevaTarifa)
        {
            if (nuevaTarifa <= 0)
            {
                throw new Exception("La tarifa debe ser mayor que cero");
            }

            Habitacion habitacion = datos.BuscarPorNumero(numero);

            if (habitacion == null)
            {
                throw new Exception("No existe una habitación con ese número");
            }

            habitacion.Tarifa = nuevaTarifa;
            datos.Actualizar(habitacion);
        }

        public void CambiarEstado(int numero, string nuevoEstado)
        {
            if (!estadosValidos.Contains(nuevoEstado))
            {
                throw new Exception("Estado inválido. Use: Disponible, Reservada, Ocupada, Limpieza o Mantenimiento");
            }

            Habitacion habitacion = datos.BuscarPorNumero(numero);

            if (habitacion == null)
            {
                throw new Exception("No existe una habitación con ese número");
            }

            habitacion.Estado = nuevoEstado;
            datos.Actualizar(habitacion);
        }

        public void EliminarHabitacion(int numero)
        {
            Habitacion habitacion = datos.BuscarPorNumero(numero);

            if (habitacion == null)
            {
                throw new Exception("No existe una habitación con ese número");
            }

            datos.Eliminar(habitacion.IdHabitacion);
        }

        /* Consultas con LINQ */

        public List<Habitacion> ListarDisponibles()
        {
            return datos.ObtenerTodas().Where(h => h.Estado == "Disponible").ToList();
        }

        public List<Habitacion> ListarPorTarifaDescendente()
        {
            return datos.ObtenerTodas().OrderByDescending(h => h.Tarifa).ToList();
        }

        public List<Habitacion> ListarPorCapacidad(int capacidadMinima)
        {
            return datos.ObtenerTodas().Where(h => h.Capacidad >= capacidadMinima).OrderBy(h => h.NumeroHabitacion).ToList();
        }

        /* Diccionario: cantidad de habitaciones por estado */

        public Dictionary<string, int> ContarPorEstado()
        {
            Dictionary<string, int> conteo = new Dictionary<string, int>();

            foreach (Habitacion habitacion in datos.ObtenerTodas())
            {
                if (conteo.ContainsKey(habitacion.Estado))
                {
                    conteo[habitacion.Estado] = conteo[habitacion.Estado] + 1;
                }
                else
                {
                    conteo[habitacion.Estado] = 1;
                }
            }

            return conteo;
        }
    }
}
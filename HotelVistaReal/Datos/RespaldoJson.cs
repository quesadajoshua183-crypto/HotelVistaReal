using HotelVistaReal.Entidades;
using System.Text.Json;

namespace HotelVistaReal.Datos
{
    public class RespaldoJson
    {
        private string ruta = "respaldo_habitaciones.json";

        public void Exportar(List<Habitacion> habitaciones)
        {
            List<HabitacionRespaldo> respaldo = new List<HabitacionRespaldo>();

            foreach (Habitacion habitacion in habitaciones)
            {
                HabitacionRespaldo item = new HabitacionRespaldo();
                item.NumeroHabitacion = habitacion.NumeroHabitacion;
                item.Tipo = habitacion.Tipo;
                item.Capacidad = habitacion.Capacidad;
                item.Tarifa = habitacion.Tarifa;
                item.Estado = habitacion.Estado;
                respaldo.Add(item);
            }

            string json = JsonSerializer.Serialize(respaldo, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(ruta, json);
        }

        public List<HabitacionRespaldo> Leer()
        {
            if (!File.Exists(ruta))
            {
                throw new FileNotFoundException("No existe el archivo de respaldo. Primero exporte las habitaciones");
            }

            string json = File.ReadAllText(ruta);
            return JsonSerializer.Deserialize<List<HabitacionRespaldo>>(json);
        }

        public string ObtenerRuta()
        {
            return Path.GetFullPath(ruta);
        }
    }
}
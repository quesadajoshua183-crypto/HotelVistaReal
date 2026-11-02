using HotelVistaReal.Entidades;
using Microsoft.Data.SqlClient;

namespace HotelVistaReal.Datos
{
    public class HabitacionDatos : iHabitacionDatos
    {
        private string conexion = "Server=localhost\\SQLEXPRESS;Database=HotelVistaRealDB;Trusted_Connection=True;TrustServerCertificate=True;";

        public void Insertar(Habitacion habitacion)
        {
            string sql = "INSERT INTO Habitaciones (NumeroHabitacion, Tipo, Capacidad, Tarifa, Estado) VALUES (@numero, @tipo, @capacidad, @tarifa, @estado);";

            using (SqlConnection con = new SqlConnection(conexion))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@numero", habitacion.NumeroHabitacion);
                cmd.Parameters.AddWithValue("@tipo", habitacion.Tipo);
                cmd.Parameters.AddWithValue("@capacidad", habitacion.Capacidad);
                cmd.Parameters.AddWithValue("@tarifa", habitacion.Tarifa);
                cmd.Parameters.AddWithValue("@estado", habitacion.Estado);
                cmd.ExecuteNonQuery();
            }
        }

        public List<Habitacion> ObtenerTodas()
        {
            List<Habitacion> habitaciones = new List<Habitacion>();
            string sql = "SELECT IdHabitacion, NumeroHabitacion, Tipo, Capacidad, Tarifa, Estado FROM Habitaciones ORDER BY NumeroHabitacion;";

            using (SqlConnection con = new SqlConnection(conexion))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);

                using (SqlDataReader respuestaBD = cmd.ExecuteReader())
                {
                    while (respuestaBD.Read())
                    {
                        habitaciones.Add(CrearHabitacion(respuestaBD));
                    }
                }
            }

            return habitaciones;
        }

        public Habitacion BuscarPorNumero(int numeroHabitacion)
        {
            string sql = "SELECT IdHabitacion, NumeroHabitacion, Tipo, Capacidad, Tarifa, Estado FROM Habitaciones WHERE NumeroHabitacion = @numero;";

            using (SqlConnection con = new SqlConnection(conexion))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@numero", numeroHabitacion);

                using (SqlDataReader respuestaBD = cmd.ExecuteReader())
                {
                    if (respuestaBD.Read())
                    {
                        return CrearHabitacion(respuestaBD);
                    }
                }
            }

            return null;
        }

        public void Actualizar(Habitacion habitacion)
        {
            string sql = "UPDATE Habitaciones SET NumeroHabitacion = @numero, Tipo = @tipo, Capacidad = @capacidad, Tarifa = @tarifa, Estado = @estado WHERE IdHabitacion = @id;";

            using (SqlConnection con = new SqlConnection(conexion))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@numero", habitacion.NumeroHabitacion);
                cmd.Parameters.AddWithValue("@tipo", habitacion.Tipo);
                cmd.Parameters.AddWithValue("@capacidad", habitacion.Capacidad);
                cmd.Parameters.AddWithValue("@tarifa", habitacion.Tarifa);
                cmd.Parameters.AddWithValue("@estado", habitacion.Estado);
                cmd.Parameters.AddWithValue("@id", habitacion.IdHabitacion);

                int filasAfectadas = cmd.ExecuteNonQuery();

                if (filasAfectadas == 0)
                {
                    throw new Exception("No se encontró la habitación que se quiere modificar");
                }
            }
        }

        public void Eliminar(int idHabitacion)
        {
            string sql = "DELETE FROM Habitaciones WHERE IdHabitacion = @id;";

            using (SqlConnection con = new SqlConnection(conexion))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", idHabitacion);

                int filasAfectadas = cmd.ExecuteNonQuery();

                if (filasAfectadas == 0)
                {
                    throw new Exception("No se encontró la habitación que se quiere eliminar");
                }
            }
        }

        private Habitacion CrearHabitacion(SqlDataReader respuestaBD)
        {
            int id = Convert.ToInt32(respuestaBD["IdHabitacion"]);
            int numero = Convert.ToInt32(respuestaBD["NumeroHabitacion"]);
            string tipo = respuestaBD["Tipo"].ToString();
            int capacidad = Convert.ToInt32(respuestaBD["Capacidad"]);
            decimal tarifa = Convert.ToDecimal(respuestaBD["Tarifa"]);
            string estado = respuestaBD["Estado"].ToString();

            if (tipo == "Suite")
            {
                return new HabitacionSuite(id, numero, tipo, capacidad, tarifa, estado);
            }

            return new HabitacionEstandar(id, numero, tipo, capacidad, tarifa, estado);
        }
    }
}
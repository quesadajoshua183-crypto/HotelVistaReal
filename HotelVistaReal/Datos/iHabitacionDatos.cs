using HotelVistaReal.Entidades;

namespace HotelVistaReal.Datos
{
    public interface iHabitacionDatos
    {
        void Insertar(Habitacion habitacion);

        List<Habitacion> ObtenerTodas();

        Habitacion BuscarPorNumero(int numeroHabitacion);

        void Actualizar(Habitacion habitacion);

        void Eliminar(int idHabitacion);
    }
}
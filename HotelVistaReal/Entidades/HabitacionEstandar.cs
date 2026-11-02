namespace HotelVistaReal.Entidades
{
    public class HabitacionEstandar : Habitacion
    {
        public HabitacionEstandar(int idHabitacion, int numeroHabitacion, string tipo, int capacidad, decimal tarifa, string estado)
            : base(idHabitacion, numeroHabitacion, tipo, capacidad, tarifa, estado)
        {
        }

        public override decimal CalcularTarifaNoche()
        {
            return Tarifa;
        }
    }
}
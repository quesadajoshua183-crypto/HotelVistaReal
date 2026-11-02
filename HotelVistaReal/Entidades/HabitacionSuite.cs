namespace HotelVistaReal.Entidades
{
    public class HabitacionSuite : Habitacion
    {
        public HabitacionSuite(int idHabitacion, int numeroHabitacion, string tipo, int capacidad, decimal tarifa, string estado)
            : base(idHabitacion, numeroHabitacion, tipo, capacidad, tarifa, estado)
        {
        }

        public override decimal CalcularTarifaNoche()
        {
            return Tarifa * 1.15m;
        }
    }
}
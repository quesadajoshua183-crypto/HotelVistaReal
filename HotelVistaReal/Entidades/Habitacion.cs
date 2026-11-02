namespace HotelVistaReal.Entidades
{
    public abstract class Habitacion
    {
        public int IdHabitacion { get; set; }
        public int NumeroHabitacion { get; set; }
        public string Tipo { get; set; }
        public int Capacidad { get; set; }
        public decimal Tarifa { get; set; }
        public string Estado { get; set; }

        public Habitacion(int idHabitacion, int numeroHabitacion, string tipo, int capacidad, decimal tarifa, string estado)
        {
            if (numeroHabitacion <= 0)
            {
                throw new Exception("El número de habitación debe ser mayor que cero");
            }

            if (capacidad <= 0)
            {
                throw new Exception("La capacidad debe ser mayor que cero");
            }

            if (tarifa <= 0)
            {
                throw new Exception("La tarifa debe ser mayor que cero");
            }

            if (string.IsNullOrWhiteSpace(tipo))
            {
                throw new Exception("El tipo de habitación no puede estar vacío");
            }

            IdHabitacion = idHabitacion;
            NumeroHabitacion = numeroHabitacion;
            Tipo = tipo;
            Capacidad = capacidad;
            Tarifa = tarifa;
            Estado = estado;
        }

        public void MostrarInformacion()
        {
            Console.WriteLine("Habitación: " + NumeroHabitacion + " | Tipo: " + Tipo + " | Capacidad: " + Capacidad + " | Tarifa: " + Tarifa + " | Estado: " + Estado);
        }

        public abstract decimal CalcularTarifaNoche();
    }
}
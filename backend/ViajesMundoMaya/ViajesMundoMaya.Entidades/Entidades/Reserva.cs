using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViajesMundoMaya.Entidades.Entidades
{
	public class Reserva
	{
		public int CodigoReserva { get; set; }
		public int CodigoCliente { get; set; }
		public int CodigoEmpleado { get; set; }
		public int CodigoPaquete { get; set; }
		public DateOnly FechaViajeInicio { get; set; }
		public DateOnly FechaViajeFin { get; set; }
		public string EstadoReserva { get; set; } = string.Empty;
		public string Observaciones { get; set; } = string.Empty;
		public DateTime FechaReserva { get; set; }
		public string Cliente { get; set; } = string.Empty;
		public string Empleado { get; set; } = string.Empty;
		public string NombrePaquete { get; set; } = string.Empty;
		public bool Estado { get; set; }
	}
}

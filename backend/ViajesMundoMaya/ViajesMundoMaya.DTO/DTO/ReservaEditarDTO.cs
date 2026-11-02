using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViajesMundoMaya.DTO.DTO
{
	public class ReservaEditarDTO
	{
		public int CodigoReserva { get; set; }
		public int CodigoCliente { get; set; }
		public int CodigoEmpleado { get; set; }
		public int CodigoPaquete { get; set; }
		public DateOnly FechaViajeInicio { get; set; }
		public DateOnly FechaViajeFin { get; set; }
		public string EstadoReserva { get; set; } = string.Empty;
		public string Observaciones { get; set; } = string.Empty;
		public bool Estado { get; set; }
	}
}

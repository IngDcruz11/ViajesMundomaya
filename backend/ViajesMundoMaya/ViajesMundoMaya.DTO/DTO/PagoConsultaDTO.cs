using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViajesMundoMaya.DTO.DTO
{
	public class PagoConsultaDTO
	{
		public int CodigoPago { get; set; }
		public int CodigoReserva { get; set; }
		public int CodigoMetodoPago { get; set; }
		public decimal Monto { get; set; }
		public string Referencia { get; set; } = string.Empty;
		public string NumeroTransaccion { get; set; } = string.Empty;
		public string EstadoPago { get; set; } = string.Empty;
		public DateTime FechaPago { get; set; }
		public string NombreMetodoPago { get; set; } = string.Empty;
		public bool Estado { get; set; }
	}
}

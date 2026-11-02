using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViajesMundoMaya.DTO.DTO
{
	public class PaqueteHotelConsultaDTO
	{
		public int CodigoPaqueteHotel { get; set; }
		public int CodigoPaquete { get; set; }
		public int CodigoHotel { get; set; }
		public int Noches { get; set; }
		public string TipoHabitacion { get; set; } = string.Empty;
		public bool IncluyeDesayuno { get; set; }
		public decimal PrecioPorNoche { get; set; }
		public string NombrePaquete { get; set; } = string.Empty;
		public string NombreHotel { get; set; } = string.Empty;
		public bool Estado { get; set; }
	}
}

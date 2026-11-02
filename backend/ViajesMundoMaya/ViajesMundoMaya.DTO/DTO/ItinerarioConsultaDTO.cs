using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViajesMundoMaya.DTO.DTO
{
	public class ItinerarioConsultaDTO
	{
		public int CodigoItinerario { get; set; }
		public int CodigoPaquete { get; set; }
		public int Dia { get; set; }
		public string DescripcionActividad { get; set; } = string.Empty;
		public string Lugar { get; set; } = string.Empty;
		public TimeOnly? HoraInicio { get; set; }
		public TimeOnly? HoraFin { get; set; }
		public string NombrePaquete { get; set; } = string.Empty;
		public bool Estado { get; set; }
	}
}

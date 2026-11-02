using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViajesMundoMaya.DTO.DTO
{
	public class TourAgregarDTO
	{
		public int CodigoDestino { get; set; }
		public string NombreTour { get; set; } = string.Empty;
		public string Descripcion { get; set; } = string.Empty;
		public int DuracionHoras { get; set; }
		public decimal PrecioPorPersona { get; set; }
		public string Incluye { get; set; } = string.Empty;
	}
}

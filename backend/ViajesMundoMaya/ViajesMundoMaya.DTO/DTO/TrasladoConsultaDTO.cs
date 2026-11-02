using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViajesMundoMaya.DTO.DTO
{
	public class TrasladoConsultaDTO
	{
		public int CodigoTraslado { get; set; }
		public int CodigoPaquete { get; set; }
		public int CodigoTipoTransporte { get; set; }
		public string Origen { get; set; } = string.Empty;
		public string Destino { get; set; } = string.Empty;
		public DateOnly FechaServicio { get; set; }
		public TimeOnly? HoraSalida { get; set; }
		public TimeOnly? HoraLlegada { get; set; }
		public string NombrePaquete { get; set; } = string.Empty;
		public string TipoTransporte { get; set; } = string.Empty;
		public bool Estado { get; set; }
	}
}

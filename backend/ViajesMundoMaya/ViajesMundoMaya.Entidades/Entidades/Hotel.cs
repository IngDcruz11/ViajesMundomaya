using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViajesMundoMaya.Entidades.Entidades
{
	public class Hotel
	{
		public int CodigoHotel { get; set; }
		public int CodigoDestino { get; set; }
		public string NombreHotel { get; set; } = string.Empty;
		public string Categoria { get; set; } = string.Empty;
		public string Direccion { get; set; } = string.Empty;
		public string Telefono { get; set; } = string.Empty;
		public string ServiciosIncluidos { get; set; } = string.Empty;
		public string NombreDestino { get; set; } = string.Empty;
		public bool Estado { get; set; }
	}
}

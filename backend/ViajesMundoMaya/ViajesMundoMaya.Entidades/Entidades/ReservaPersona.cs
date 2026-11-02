using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViajesMundoMaya.Entidades.Entidades
{
	public class ReservaPersona
	{
		public int CodigoReservaPersona { get; set; }
		public int CodigoReserva { get; set; }
		public string Nombres { get; set; } = string.Empty;
		public string Apellidos { get; set; } = string.Empty;
		public string DPI { get; set; } = string.Empty;
		public DateOnly? FechaNacimiento { get; set; }
		public string Parentesco { get; set; } = string.Empty;
		public int? Edad { get; set; }
		public bool Estado { get; set; }
	}
}

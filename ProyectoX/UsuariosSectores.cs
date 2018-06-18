using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoX
{
    class UsuariosSectores
    {
        public UsuariosSectores()
        {

        }
        public int id { get; set; }
        public int idSector { get; set; }
        public string nombreSector { get; set; }
        public int idUsuario { get; set; }
        public string nombreUsuario { get; set; }
        public string dni { get; set; }
        public string email { get; set; }
        public DateTime fechaModi { get; set; }
        public string roles { get; set; }
    }
}

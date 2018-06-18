using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoX
{
    class UsuarioRolHorario
    {
        public int id { get; set; }
        public int idUsuarioSector { get; set; }
        public string nombreUsuario { get; set; }
        public string rolesTemporales { get; set; }
        public string email { get; set; }
        public DateTime fechaModificacion { get; set; }
        public DateTime fechaInicio { get; set; }
        public DateTime fechaFin { get; set; }
        public bool vigente { get; set; }
        public bool emailChked { get; set; }

        public UsuarioRolHorario()
        {

        }
    }
}

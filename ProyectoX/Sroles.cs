using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoX
{
    class Sroles
    {
        public Sroles()
        {
        }

        public Sroles(Roles roles, bool RolSeleccionado)
        {
            this.roles = roles;
            this.RolSeleccionado = RolSeleccionado;
        }

        public bool RolSeleccionado { get; set; }
        public Roles roles { get; set; }
    }
}

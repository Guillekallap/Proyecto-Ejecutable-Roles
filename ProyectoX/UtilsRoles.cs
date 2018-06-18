using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoX
{
    class UtilsRoles
    {
        public string comprobarRoles(UsuariosSectores user, UsuarioRolHorario userHorario)
        {
            List<string> listaRoles = user.roles.Split(',').ToList();
            List<string> listaRolesTemporales = userHorario.rolesTemporales.Split(',').ToList();
            List<Roles> rolesTotales = (new Conexion()).ListarTodosRoles();
            List<Roles> auxiliar = new List<Roles>();

            //No puede darse este caso
            if (listaRoles == null && listaRolesTemporales == null){return null;}

            //Al encontrarse con roles distintos de nulo en User.
            if(listaRoles != null && (user.fechaModi < userHorario.fechaModificacion))
            {
                foreach(var rolUSRH in listaRolesTemporales)
                {
                    bool encontrado = listaRoles.Contains(rolUSRH);
                    if(encontrado == false) {listaRoles.Add(rolUSRH);}else{listaRoles.Remove(rolUSRH);}
                }
            }
            else
            {
                if (user.fechaModi>userHorario.fechaModificacion)
                {
                    (new UtilsEmail()).EnviarCorreo(userHorario.email, userHorario.nombreUsuario, userHorario.rolesTemporales, false);//en realidad deberia ser un email explicando que los roles se han modificado internamente y no como se tenia programado.
                }
                return userHorario.rolesTemporales;
            }

            string rolesComprobados = OrdenarRolesPorID(rolesTotales, listaRoles);
            return rolesComprobados;
        }


        public string OrdenarRolesPorID(List<Roles> listaClaseRoles, List<string> listaRolesString)
        {

            List<Roles> listaRolesAObtener = new List<Roles>();
            List<string> listaRolesOrdenados = new List<string>();
            //Ordenar Roles dados por el usuario
            foreach (string rolString in listaRolesString)
            {
                Roles rolClaseTemp = listaClaseRoles.Find(x => x.rol == rolString);
                listaRolesAObtener.Add(rolClaseTemp);
            }

            listaRolesAObtener = listaRolesAObtener.OrderBy(x => x.id).ToList();
            foreach (Roles rolNombre in listaRolesAObtener)
            {
                listaRolesOrdenados.Add(rolNombre.rol);
            }
            string rolesArreglado = string.Join(",", listaRolesOrdenados.ToArray());
            return rolesArreglado;
        }
    }
}

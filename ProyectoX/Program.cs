using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoX
{
    class Program
    {
        static void Main(string[] args)
        {
            Conexion insConexion = new Conexion();
            List<UsuarioRolHorario> listaUsuarioRolHorario = new List<UsuarioRolHorario>();

            Console.WriteLine("Bienvenido al programa Ejecutable.");

            insConexion.ActualizarVigente();
            listaUsuarioRolHorario = insConexion.ObtenerDatosUsuarioRolHorario();
            //Logica para identificar aquellos que cumplen para ser modificados en la tabla UsuariosSectores de Klinicos. Importancia Alta.
            foreach(var userHorario in listaUsuarioRolHorario)
            {
                if (DateTime.Now < userHorario.fechaFin && DateTime.Now > userHorario.fechaInicio && userHorario.vigente)
                {
                    UsuariosSectores usuario = insConexion.BuscarUsuarioSector(userHorario.idUsuarioSector);
                    //Parte del funcionamiento principal del programa.
                    string rolesComprobados = (new UtilsRoles()).comprobarRoles(usuario,userHorario);
                    insConexion.ModificarRolesUsuarioSector(userHorario.idUsuarioSector, rolesComprobados);//Guarda en Klinicos los roles comprobados
                    if (userHorario.emailChked == true)
                    {
                        (new UtilsEmail()).EnviarCorreo(userHorario.email, userHorario.nombreUsuario, rolesComprobados, true);
                    }
                    Console.Write("Finalización correcta.");
                    Console.ReadKey();
                }
                else
                {
                    Console.Write("No hubo Cambios.");
                    Console.ReadKey();
                }
            }
            if (listaUsuarioRolHorario.Count == 0) { Console.Write("Nothing To Do Here");Console.ReadKey(); }
        }
    }
}

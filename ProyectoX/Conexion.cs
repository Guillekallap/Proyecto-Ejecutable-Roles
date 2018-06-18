using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;

namespace ProyectoX
{
    class Conexion
    {
        private SqlConnection con;

        private void connection(string nombreBase)
        {
            string constr = ConfigurationManager.ConnectionStrings[nombreBase].ToString();
            con = new SqlConnection(constr);

        }

        public void ActualizarVigente()
        {
            connection("klinicos_interno");
            SqlCommand com = new SqlCommand("ActualizarVigente", con);
            com.CommandType = CommandType.StoredProcedure;

        }

        public List<UsuarioRolHorario> ObtenerDatosUsuarioRolHorario()
        {
            connection("klinicos_interno");
            List<UsuarioRolHorario> listaUsSec = new List<UsuarioRolHorario>();
            SqlCommand com = new SqlCommand("ObtenerDatosUsuarioRolHorario", con);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            try
            {

                con.Open();

                da.Fill(dt);
                con.Close();
                listaUsSec = (from DataRow dr in dt.Rows

                              select new UsuarioRolHorario()
                              {
                                  id = Convert.ToInt32(dr["id"]),
                                  idUsuarioSector = Convert.ToInt32(dr["idUsuarioSector"]),
                                  nombreUsuario = Convert.ToString(dr["nombreUsuario"]),
                                  rolesTemporales = Convert.ToString(dr["rolesTemporales"]),
                                  email = Convert.ToString(dr["email"]),
                                  emailChked = Convert.ToBoolean(dr["emailChked"]),
                                  fechaInicio = Convert.ToDateTime(dr["fechaInicio"]),
                                  fechaFin = Convert.ToDateTime(dr["fechaFin"]),
                                  fechaModificacion = Convert.ToDateTime(dr["fechaModificacion"]),
                                  vigente = Convert.ToBoolean(dr["vigente"])
                              }).ToList();

                return listaUsSec;


            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void ModificarRolesUsuarioSector(int id, string roles)
        {
            connection("getconn");
            SqlCommand com = new SqlCommand("ModificarRolesUsuarioSector", con);

            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();

            com.Parameters.AddWithValue("@id", id);

            com.Parameters.AddWithValue("@roles", string.IsNullOrEmpty(roles) ? (object)DBNull.Value : roles);

            try
            {
                con.Open();
                da.Fill(dt);
                con.Close();

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<Roles> ListarTodosRoles()
        {
            connection("getconn");
            List<Roles> listarol = new List<Roles>();

            SqlCommand com = new SqlCommand("ListarRoles", con);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();

            listarol = (from DataRow dr in dt.Rows

                        select new Roles()
                        {
                            id = Convert.ToInt32(dr["id"]),
                            rol = Convert.ToString(dr["rol"]),
                            descripcion = Convert.ToString(dr["descripcion"]),
                        }).ToList();

            return listarol;
        }

        public UsuariosSectores BuscarUsuarioSector(int i)
        {

            connection("getconn");
            SqlCommand com = new SqlCommand("BuscarUsuarioSector", con);

            UsuariosSectores usec = new UsuariosSectores();
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();

            com.Parameters.AddWithValue("@id", i);

            try
            {

                con.Open();
                da.Fill(dt);
                con.Close();
                usec = (from DataRow dr in dt.Rows
                        select new UsuariosSectores()
                        {

                            id = Convert.ToInt32(dr["id"]),
                            idSector = Convert.ToInt32(dr["idSector"]),
                            nombreSector = Convert.ToString(dr["nombreSector"]),
                            idUsuario = Convert.ToInt32(dr["idUsuario"]),
                            nombreUsuario = Convert.ToString(dr["nombreUsuario"]),
                            dni = Convert.ToString(dr["dni"]),
                            email = Convert.ToString(dr["email"]),
                            fechaModi = Convert.ToDateTime(dr["fechaModi"]),
                            roles = Convert.ToString(dr["roles"]),
                        }).FirstOrDefault();

                return (usec);


            }
            catch (Exception e)
            {
                throw new Exception(e.Message);

            }

        }

    }
}


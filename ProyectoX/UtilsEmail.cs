using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Text;
using System.Net;
using System.Net.Mail;

namespace ProyectoX
{
    public class UtilsEmail
    {
        public void EnviarCorreo(string email, string nombreUsuario, string roles, bool cambioCorrecto)
        {
            /*-------------------------MENSAJE DE CORREO----------------------*/

            string salida = null;
            //Creamos un nuevo Objeto de mensaje
            MailMessage mmsg = new MailMessage();

            if (email != "")
            {

                //Direccion de correo electronico a la que queremos enviar el mensaje
                mmsg.To.Add(new MailAddress(email));
                mmsg.From = new MailAddress("soportetecnicosalud@gmail.com");

                //Nota: La propiedad To es una colección que permite enviar el mensaje a más de un destinatario

                //Asunto
                mmsg.Subject = "Se han Modificado sus roles en la cuenta de Usuario: " + nombreUsuario;
                mmsg.SubjectEncoding = Encoding.UTF8;

                //Direccion de correo electronico que queremos que reciba una copia del mensaje
                //mmsg.Bcc.Add("destinatariocopia@servidordominio.com"); //Opcional

                //Cuerpo del Mensaje
                if (cambioCorrecto)
                {
                    mmsg.Body = "Le comento que se han modificado ciertos valores de sus roles de acuerdo a la solicitud que se ha enviado. Actualmente posee los roles de : " + roles;
                }
                else
                {
                    mmsg.Body = "Le comento que se han modificado ciertos valores de sus roles de acuerdo a la solicitud que se ha enviado." +
                           " Por lo que tenemos entendido se han modificado internamente en el centro de atención/hospital." +
                           " Actualmente posee los roles de: " + roles;
                }

                mmsg.BodyEncoding = Encoding.UTF8;
                mmsg.IsBodyHtml = false; //Si no queremos que se envíe como HTML

                //Correo electronico desde la que enviamos el mensaje


                /*-------------------------CLIENTE DE CORREO----------------------*/

                //Creamos un objeto de cliente de correo
                SmtpClient cliente = new SmtpClient();

                cliente.UseDefaultCredentials = false;
                //Hay que crear las credenciales del correo emisor
                cliente.Credentials = new NetworkCredential("soportetecnicosalud@gmail.com", "SoporteTecnico2018");

                //Lo siguiente es obligatorio si enviamos el mensaje desde Gmail

                cliente.Port = 587;
                cliente.EnableSsl = true;

                cliente.Host = "smtp.gmail.com"; //Para Gmail "smtp.gmail.com";

                /*-------------------------ENVIO DE CORREO----------------------*/

                try
                {
                    //Enviamos el mensaje
                    cliente.Send(mmsg);
                    mmsg.Dispose();
                    salida = "Correo electronico enviado correctamente.";
                }


                catch (SmtpException ex)
                {
                    salida = "Error enviando correo electrónico. " + ex.Message;//Aquí gestionamos los errores al intentar enviar el correo
                }
            }
            Console.WriteLine(salida);
        }

    }
}
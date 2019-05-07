using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mardis.Engine.Business
{
    // Agregamos un nuevo Using a la clase.
    using System.Net.Mail;
    using System.Net.Mime;

    // El código de la clase es:
    public class Correos
    {
        /*
         * Cliente SMTP
         * Gmail:  smtp.gmail.com  puerto:587
         * Hotmail: smtp.liva.com  puerto:25
         */
        SmtpClient server = new SmtpClient("smtp.gmail.com", 587);

        public Correos()
        {
            /*
             * Autenticacion en el Servidor
             * Utilizaremos nuestra cuenta de correo
             *
             * Direccion de Correo (Gmail o Hotmail)
             * y Contrasena correspondiente
             */
            server.Credentials = new System.Net.NetworkCredential("mardisresearch.engine@gmail.com", "M@rdis2018");
            server.EnableSsl = true;
        }

        public void MandarCorreo(MailMessage mensaje)
        {
            server.Send(mensaje);
        }
        public void enviar(string _subject, string _to, string _body, string mercaderista)
        {

            try
            {
                Correos Cr = new Correos();
                MailMessage mnsj = new MailMessage();
                mnsj.IsBodyHtml = true;
                mnsj.Subject = _subject;
                string[] destinatarios = _to.Split(',');

                foreach (string destinatario in destinatarios)
                {
                    if(mercaderista == destinatario)
                    {
                        mnsj.To.Add("faguirre@mardisresearch.com");
                        mnsj.To.Add("dsamueza@mardisresearch.com");
                        mnsj.To.Add(new MailAddress(destinatario));
                    }
                    
                }


                mnsj.From = new MailAddress("mardisresearch.engine@gmail.com", "Mardis Research");

                AlternateView plainView = AlternateView.CreateAlternateViewFromString("Logo Mardis", Encoding.UTF8, MediaTypeNames.Text.Plain);

                /* Si deseamos Adjuntar algún archivo*/
                //if (adjunto != "")
                //{
                //    mnsj.Attachments.Add(new Attachment(adjunto));
                //}

                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(_body, Encoding.UTF8, MediaTypeNames.Text.Html);

                // Creamos el recurso a incrustar. Observad
                // que el ID que le asignamos (arbitrario) está
                // referenciado desde el código HTML como origen
                // de la imagen (resaltado en amarillo)...

                LinkedResource img = new LinkedResource("../logoMardis.jpeg", MediaTypeNames.Image.Jpeg);
                img.ContentId = "imagen";

                // Lo incrustamos en la vista HTML...

                htmlView.LinkedResources.Add(img);

                // Por último, vinculamos ambas vistas al mensaje...

                mnsj.AlternateViews.Add(plainView);
                mnsj.AlternateViews.Add(htmlView);

                /* Enviar */
                Cr.MandarCorreo(mnsj);
                //  Enviado = true;


            }
            catch (Exception ex)
            {

            }

        }
    }
}

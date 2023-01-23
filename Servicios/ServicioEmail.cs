using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace Servicios
{
    public class ServicioEmail
    {
        private MailMessage email;
        private SmtpClient smtp;
        public ServicioEmail()
        {
            try
            {
                smtp = new SmtpClient();
                smtp.Credentials = new NetworkCredential("6dd5f76512f7b3", "9d53556cb70915");
                smtp.EnableSsl = true;
                smtp.Host = "smtp.mailtrap.io";
                smtp.Port = 2525;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void armarEmail(string emailDestino, string asunto, string cuerpo)
        {
            try
            {
                email = new MailMessage();
                email.From = new MailAddress("noresponder@catalogoweb.com");
                email.To.Add(emailDestino);
                email.Subject = asunto;
                email.IsBodyHtml = true;
                email.Body = cuerpo;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void enviarEmail()
        {
            try
            {
                smtp.Send(email);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}

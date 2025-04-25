using ClubInfluApp.Models;
using System.Net.Mail;
using System.Net;

namespace ClubInfluApp.Helpers
{
    public static class NotificacionesCorreoHelper
    {
        private static ConfiguracionCorreo _config;

        public static void Configurar(IConfiguration configuration)
        {
            _config = configuration.GetSection("NotificacionesCorreo").Get<ConfiguracionCorreo>();
        }

        public static void EnviarCorreo(List<string> destinatarios, string asunto, string cuerpoHtml)
        {
            if (_config == null)
            {
                throw new InvalidOperationException("El helper no está configurado. Llama primero a Configurar().");
            }

            using (SmtpClient smtp = new SmtpClient(_config.Servidor, _config.Puerto))
            {
                smtp.Credentials = new NetworkCredential(_config.Correo, _config.Clave);
                smtp.EnableSsl = _config.Ssl;

                foreach (var destino in destinatarios)
                {
                    var mensaje = new MailMessage
                    {
                        From = new MailAddress(_config.Correo),
                        Subject = asunto,
                        Body = cuerpoHtml,
                        IsBodyHtml = true
                    };
                    mensaje.To.Add(destino);

                    smtp.Send(mensaje);
                }
            }
        }
    }
}

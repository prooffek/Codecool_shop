using System;
using System.Net;
using System.Net.Mail;
using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Services
{
    public class SendEmailService
    {
        private string _fromSendEmail = "CodecoolTravel@gmail.com";
        
        public void SendEmail(string email, Cart cart)
        {
            MailAddress toSend = new MailAddress(email);
            MailAddress fromSend = new MailAddress(_fromSendEmail);
            
            MailMessage message = new MailMessage(fromSend, toSend);
            message.Subject = "Potwierdzenie zamówienia w CodecoolTravel";
            message.Body = $"Dziękujemy za zamówienie w naszym sklepie." +
                           $"{cart.ToString()}" +
                           $"Zapraszamy ponownie";
            
            
            SmtpClient client = new SmtpClient();  
            client.Host = "smtp.gmail.com";  
            System.Net.NetworkCredential ntwd = new NetworkCredential();  
            ntwd.UserName = "CodecoolTravel@gmail.com"; //Your Email ID  
            ntwd.Password = "U5yc7OenQjHKeIz"; // Your Password  
            client.UseDefaultCredentials = true;  
            client.Credentials = ntwd;  
            client.Port = 587;  
            client.EnableSsl = true;  
              

            // SmtpClient client = new SmtpClient("smtp.server.address", 2525)
            // {
            //     UseDefaultCredentials = false,
            //     Credentials = new NetworkCredential("codecooltravel@interia.pl", "U5yc7OenQjHKeIz"),
            //     EnableSsl = true,
            //     Port = 465,
            //     Host = "poczta.interia.pl"
            // };
            // code in brackets above needed if authentication required

            try
            {
                client.Send(message);
            }
            catch (SmtpException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
using MVC1__DAL_.Models;
using System;
using System.Net;
using System.Net.Mail;

namespace MVC1.Helper
{
    public class EmailSettings
    {
        public static void SendEmail(Email email)
        {
			try
			{
				var client = new SmtpClient("smtp.gmail.com", 587)
				{
					EnableSsl = true,
					Credentials = new NetworkCredential("mostafaafify291@gmail.com", "jldlepzxwelwmkip") 
				};

				client.Send("mostafaafify291@gmail.com", email.Reciepints, email.Subject, email.Body);
			}
			catch (SmtpException smtpEx)
			{
				// Log or handle SMTP-specific exceptions
				Console.WriteLine($"SMTP Exception: {smtpEx.Message}");
			}
			catch (Exception ex)
			{
				// Log or handle general exceptions
				Console.WriteLine($"General Exception: {ex.Message}");
			}

		}

    }
}

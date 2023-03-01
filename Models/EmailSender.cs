using System;
using System.Net;
using System.Net.Mail;
namespace CI_Platform_three_tier.Models
{


    /*public class EmailSender
    {
        public void SendEmail(string recipient, string subject, string body)
        {
            // Create a MailMessage object
            MailMessage mail = new MailMessage();

            // Set the sender and recipient addresses
            mail.From = new MailAddress("forptoject1402@gmail.com");
            mail.To.Add(recipient);

            // Set the subject and body of the email
            mail.Subject = subject;
            mail.Body = body;

            // Create a SmtpClient object
            SmtpClient smtp = new SmtpClient();

            // Set the SMTP server and port number
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;

            // Set the credentials for the SMTP server (if required)
            smtp.Credentials = new NetworkCredential("forptoject1402@gmail.com", "Y7206833");

            // Enable SSL/TLS encryption (if required)
            smtp.EnableSsl = true;

            try
            {
                // Send the email
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending email: " + ex.Message);
            }
        }
    }*/


    /*using Amazon.SimpleEmail;
    using Amazon.SimpleEmail.Model;

    public class EmailSender
    {
        public void SendEmail(string recipient, string subject, string body)
        {
            // Create a new instance of the Amazon SES client
            AmazonSimpleEmailServiceClient client = new AmazonSimpleEmailServiceClient();

            // Create a new SendEmailRequest object
            SendEmailRequest request = new SendEmailRequest
            {
                Source = "your_email_address@example.com",
                Destination = new Destination
                {
                    ToAddresses = new List<string> { recipient }
                },
                Message = new Message
                {
                    Subject = new Content(subject),
                    Body = new Body
                    {
                        Html = new Content(body)
                    }
                }
            };

            try
            {
                // Send the email using the Amazon SES client
                SendEmailResponse response = client.SendEmail(request);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending email: " + ex.Message);
            }
        }
    }*/



    using System;
    using System.Net;
    using System.Net.Mail;

    public class EmailSender
    {
        public void SendEmail(string recipient, string subject, string body)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("harpesh123456@outlook.com");
            mail.To.Add(recipient);
            mail.Subject = subject;
            mail.Body = body;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp-mail.outlook.com";
            smtp.Port = 587;
            smtp.Credentials = new NetworkCredential("harpesh123456@outlook.com", "Vh@12345");
            smtp.EnableSsl = true;
            try
            {
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending email: " + ex.Message);
            }
        }
    }


}

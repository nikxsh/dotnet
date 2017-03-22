using System;
using System.Net.Mail;

namespace DotNetDemos.CSharpExamples
{
    class SendAMail
    {
        private string senderAddress;
        private string clientAddress;
        private string netPassword;
        public SendAMail(string sender, string Password, string client)
        {
            senderAddress = sender;
            netPassword = Password;
            clientAddress = client;
        }

        public void SendEMail(string recipient, string subject, string message)
        {

            System.Net.Mail.SmtpClient client = new SmtpClient("smtp-mail.outlook.com");

            client.Port = 587;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            System.Net.NetworkCredential credentials =
                new System.Net.NetworkCredential(senderAddress, netPassword);
            client.EnableSsl = true;
            client.Credentials = credentials;

            try
            {
                var mail = new MailMessage(senderAddress.Trim(), recipient.Trim());
                mail.Subject = subject;
                mail.Body = message;
                mail.IsBodyHtml = true;
                //System.Net.Mail.Attachment attachment;
                //attachment = new Attachment(@"C:\Users\shinde_n\Downloads\TestAttach.jpg");
                //mail.Attachments.Add(attachment);

                client.Send(mail);
                Console.WriteLine("Message Sent");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
    }
}

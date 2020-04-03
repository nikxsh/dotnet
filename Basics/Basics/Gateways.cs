using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Basics
{
	public class Gateways
	{
		public void Play()
		{
			MailGateway();
			SMSGateway();
		}

		private void MailGateway()
		{
			SendAMail objMail = new SendAMail("nikhilesh.shinde@hotmail.com", "******", "smtp-mail.outlook.com");
			objMail.SendEMail("shinde.nikhilesh90@gmail.com", "Password Reset Link", "Reset your Password <a href='http://localhost.safetychain.com/ForgotPassword?1234-5678-91234'>Here.</a>");
		}

		private void SMSGateway()
		{
			SendAnSMS sendAnSMS = new SendAnSMS();
			sendAnSMS.SendSMS();
		}

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

				System.Net.Mail.SmtpClient client = new SmtpClient("smtp-mail.outlook.com")
				{
					Port = 587,
					DeliveryMethod = SmtpDeliveryMethod.Network,
					UseDefaultCredentials = false
				};
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

		class SendAnSMS
		{
			public void SendSMS()
			{
				string value = PostData("http://site21.way2sms.com/Login1.action", "username=9028269355&password=Poseidon");
				string token = ParseToken(value);
				value = PostData("http://site23.way2sms.com/smstoss.action;jsessionid=" + token, "ssaction=ss&Token=" + token + "&mobile=9028269355&message=Test From C#");
			}

			public string PostData(string address, string data)
			{
				string returnURL = string.Empty;

				HttpWebRequest request = WebRequest.Create(address) as HttpWebRequest;
				request.AllowAutoRedirect = false;
				request.Method = "POST";
				request.ContentType = "application/x-www-form-urlencoded";
				byte[] postData = Encoding.UTF8.GetBytes(data);
				request.ContentLength = postData.Length;

				Stream requestStream = request.GetRequestStream();
				requestStream.Write(postData, 0, postData.Length);

				WebResponse response = request.GetResponse();
				returnURL = response.Headers["Location"];

				return returnURL;
			}

			public string ParseToken(string URL)
			{
				string[] values = URL.Split('?');
				return values[1].Split('=')[1];
			}
		}
	}
}
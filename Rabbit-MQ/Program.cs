using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace Rabbit_MQ
{
	class Program
	{
		private const string HostName = "localhost";
		private const string UserName = "guest";
		private const string Password = "guest";
		private const string ExchangeName = "LocalExchange";
		private const string QueueName = "LocalQueue";
		private const string BindingName = "LocalBind";

		static void Main(string[] args)
		{
			var connectionFactory = new RabbitMQ.Client.ConnectionFactory
			{
				UserName = UserName,
				Password = Password,
				HostName = HostName
			};

			using (var connection = connectionFactory.CreateConnection())
			{
				var model = connection.CreateModel();
				model.QueueDeclare(QueueName, true, false, false, null);
				Console.WriteLine("LocalQueue Created");
				model.ExchangeDeclare(ExchangeName, ExchangeType.Fanout);
				Console.WriteLine("LocalExchange Created");
				model.QueueBind(QueueName, ExchangeName, BindingName);
				Console.WriteLine("LocalQueue Binded to LocalExchange");
				var properties = model.CreateBasicProperties();
				properties.Persistent = false;

				//Serilize
				byte[] messageBuffer = Encoding.Default.GetBytes("Long Live no one!");
				//Send Message
				model.BasicPublish(ExchangeName, QueueName, properties, messageBuffer);
				Console.WriteLine("Message Sent!");

				Console.ReadKey();
			}
		}
	}
}

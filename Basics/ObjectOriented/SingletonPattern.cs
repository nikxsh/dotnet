using System;
using System.Collections.Generic;

namespace ObjectOriented.Patterns
{

	/// <summary>
	/// - Creational Patterns
	/// - Ensure a class has only one instance and provide a global point of access to it.
	/// </summary>
	class SingletonPattern
	{
		public SingletonPattern()
		{
			LoadBalancer b1 = LoadBalancer.GetInstance();
			LoadBalancer b2 = LoadBalancer.GetInstance();
			LoadBalancer b3 = LoadBalancer.GetInstance();
			LoadBalancer b4 = LoadBalancer.GetInstance();

			if (b1 == b2 && b2 == b3 && b3 == b4)
				Console.WriteLine("Same innstance");

			LoadBalancer balancer = LoadBalancer.GetInstance();
				
			for (int i = 0; i < 20; i++)
			{
				string server = balancer.Server;
				Console.WriteLine($"Dispatch Request {server}");
			}
		}

		class LoadBalancer
		{
			private static LoadBalancer _loadBalancer;
			private List<string> _servers = new List<string>();
			private Random _random = new Random();

			private static object syncLock = new object();
			protected LoadBalancer()
			{
				_servers.Add("Server 1");
				_servers.Add("Server 2");
				_servers.Add("Server 3");
				_servers.Add("Server 4");
				_servers.Add("Server 5");
			}

			public static LoadBalancer GetInstance()
			{
				if (_loadBalancer == null)
				{
					lock (syncLock)
					{
						if (_loadBalancer == null)
							_loadBalancer = new LoadBalancer();
					}
				}

				return _loadBalancer;
			}

			public string Server
			{
				get
				{
					int r = _random.Next(_servers.Count);
					return _servers[r].ToString();
				}
			}
		}
	}
}

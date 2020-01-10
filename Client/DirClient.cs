// Made by Kelvin
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Dir
{
	public class DirClient
	{
		private string path;

		public DirClient(string path)
		{
			this.path = path;
		}

		public void getListing()
		{
			try
			{
				int port = 8888;
				IPAddress address = IPAddress.Parse("127.0.0.1");

				IPEndPoint ipe = new IPEndPoint(address, port);
				Socket socket = new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

				socket.Connect(ipe);

				if (socket.Connected)
				{
					string request = "GET " + path + " HTTP/1.1\r\nUser-agent: native\r\n\r\n";
					Byte[] bytesSent = Encoding.ASCII.GetBytes(request);
					Byte[] bytesReceived = new Byte[256];

					socket.Send(bytesSent, bytesSent.Length, 0);

					int bytes = 0;

					bytes = socket.Receive(bytesReceived, bytesReceived.Length, 0);
					Console.WriteLine(Encoding.ASCII.GetString(bytesReceived, 0, bytes));
					socket.Close();
				}
			}
			catch (ArgumentNullException e)
			{
				Console.WriteLine("Argument Null Exception: {0}", e);
			}
			catch (SocketException e)
			{
				Console.WriteLine("SocketException: {0}", e);
			}
		}
	}
}
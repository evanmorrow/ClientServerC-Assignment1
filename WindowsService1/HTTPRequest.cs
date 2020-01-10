using System;
using System.Text;
using System.Net;
using System.Net.Sockets;


namespace WindowsService1
{
    public class HTTPRequest
    {
        private Socket socket;

        //Written by Alina
        public HTTPRequest(Socket socket)
        {
            this.socket = socket;
        }

        //Written by Alina
        public string Write()
        {
            string message = "";
            byte[] bytesStored = new byte[socket.ReceiveBufferSize];
            int k1 = socket.Receive(bytesStored);
            for (int i = 0; i < k1; i++)
            {
                message += Convert.ToChar(bytesStored[i]).ToString();
            }
            return message; // PRINT THE RESPONSE
        }
    }


}

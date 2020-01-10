using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Diagnostics;

namespace WindowsService1

{
    // Reflection written by Evan & Kelvin
    public class DirServerThread
    {
        private Socket socket = null;
        private Type servlet;

        // Written by Alina
        public DirServerThread(Socket socket)
        {
            Service1.serverLogger.Connection("Test");
            this.socket = socket;
            var thread = new Thread(new ThreadStart(this.Run));
            thread.Start();
        }

        // Written by Alina
        public void Run()
        {
            HTTPRequest request = new HTTPRequest(socket);
            HTTPResponse response = new HTTPResponse(socket);
            servlet = typeof(DirServlet);
            DirServlet myServlet = (DirServlet)Activator.CreateInstance(servlet);
            myServlet.DoGet(request, response);
        }
    }
   
}

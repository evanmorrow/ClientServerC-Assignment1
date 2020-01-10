using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using Microsoft.Win32;

namespace WindowsService1
{
    // Added by Evan
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
            if (!System.Diagnostics.EventLog.SourceExists("WindowsServiceSource"))
                System.Diagnostics.EventLog.CreateEventSource("WindowsServiceSource", "WindowsServiceLog");
            eventLog1.Source = "WindowsServiceSource";
            eventLog1.Log = "WindowsServiceLog";
        }

        // Written by Alina
        public static Logger serverLogger;

        public static EventLog appLog;

        //Written by Taylor
        public static void EventLogger(string s)
        {
            appLog.WriteEntry(s);
        }

        protected override void OnStart(string[] args)
        {
            eventLog1.WriteEntry("WindowsService1 started.");

            var thread = new Thread(new ThreadStart(this.StartThread));
            thread.Start();
        }

        // Added by Evan
        protected void StartThread()
        {
            //Written by Taylor
            serverLogger = new Logger();
            FileLogger fl = new FileLogger("dirServer.log");

            RegistryKey rk = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Comp4945");

            //Written by Taylor and Jason
            //Checks if the registry key is set to allow for logging to Windows application logs
            if (rk != null && rk.GetValue("logEvents").ToString().Equals("true"))
            {
                appLog = new EventLog();

                if (!EventLog.SourceExists("DirServerSource"))
                    EventLog.CreateEventSource("DirServerSource", "DirServerLog");
                appLog.Source = "DirServerSource";
                appLog.Log = "DirServerLog";

                serverLogger = new Logger();

                // Subscribe the Event Logger
                serverLogger.Log += new Logger.LogHandler(EventLogger);
            }

            serverLogger.Log += new Logger.LogHandler(fl.Logger);

            // Written by Alina
            // Establish the local endpoint  
            // for the socket. Dns.GetHostName 
            // returns the name of the host  
            // running the application.
            IPAddress ipAddr = IPAddress.Parse("142.232.253.25");
            IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 8889);

            // Creation TCP/IP Socket using  
            // Socket Class Costructor 
            Socket serverSocket = new Socket(ipAddr.AddressFamily,
                         SocketType.Stream, ProtocolType.Tcp);

            try
            {
                serverSocket.Bind(localEndPoint);
                serverSocket.Listen(CoreCount.CoreCounter()) ;

                while (true)
                {
                    DirServerThread serverThread = new DirServerThread(serverSocket.Accept());
                }
            }

            catch (Exception e)
            {
                Console.Error.WriteLine(e + " Error creating Socket.");
                serverLogger.Error(e.ToString());
            }

            serverSocket.Close();
            fl.Close();
        }

        // Evan
        protected override void OnStop()
        {
            eventLog1.WriteEntry("WindowsService1 stopped.");
        }
    }
}

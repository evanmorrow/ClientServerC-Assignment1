using System;
using System.IO;
using System.Diagnostics;
using System.Threading;
// The FileLogger class merely encapsulates the file I/O
// Added to by Taylor Laan
namespace WindowsService1
{
    public class FileLogger
    {
		private static Mutex mutex = new Mutex();
		const int fileLimit = 1024*10;
        FileStream fileStream;
        StreamWriter streamWriter;

        // Constructor
        public FileLogger(string filename)
        {
            fileStream = new FileStream(filename, FileMode.Create);
            streamWriter = new StreamWriter(fileStream);
        }

        // Member Function which is used in the Delegate
        public void Logger(string s)
        {
			mutex.WaitOne();
			if(fileStream.Position + s.Length < fileLimit) {			
				streamWriter.WriteLine(s);
			} else {
				fileStream.Position = 0;
				streamWriter.WriteLine(s);
			}
            streamWriter.Flush();
            fileStream.Flush();
			mutex.ReleaseMutex();
        }

        public void Close()
        {
            streamWriter.Close();
            fileStream.Close();
        }
    }
}
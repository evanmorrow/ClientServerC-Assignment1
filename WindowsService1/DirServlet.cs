using System;
using System.IO;
using System.Text;

namespace WindowsService1
{
    public class DirServlet : Servlet
    {
        public DirServlet()
        {
        }

        // Written by Alina
        public override void DoGet(HTTPRequest request, HTTPResponse response)
        {
            string headers = request.Write();
            if (headers.IndexOf("native") == -1)
            {
                Console.WriteLine("Browser");
                string output = "<!DOCTYPE html><html><body><ul>" + GetListing("D:\\") + "</ul></body></html>";
                response.Write(output);
            }
            else
            {
                Console.WriteLine("Native");
                string output = GetListingNative("D:\\");
                response.Write(output);
            }
            Console.WriteLine(headers);

        }

        public override void DoPost(HTTPRequest request, HTTPResponse response) { }


        // Written by Alina
        private String GetListing(string path)
        {
            string[] allfiles = Directory.GetFileSystemEntries(path);

            string files = "";
            foreach (var file in allfiles)
            {
                FileInfo info = new FileInfo(file);
                if (info.Attributes.HasFlag(FileAttributes.Directory))
                {
                    files += "<li><button type=\"button\">" + info.Name + "</button></li>";
                }
                else
                {
                    files += "<li>" + info.Name + "</li>";
                }
            }
            return files;
        }

        private String GetListingNative(string path)
        {
            string[] allfiles = Directory.GetFileSystemEntries(path);

            string files = "";
            foreach (var file in allfiles)
            {
                FileInfo info = new FileInfo(file);
                if (info.Attributes.HasFlag(FileAttributes.Directory))
                {
                    files += info.Name + "\n";
                }
                else
                {
                    files += info.Name + "\n";
                }
            }
            return files;
        }
    }
}

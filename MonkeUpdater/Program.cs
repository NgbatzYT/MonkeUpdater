using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace MonkeUpdater
{
    static class Program
    {
        private static readonly string _path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MonkeUpdater");
        public static void Main()
        {
            string e = File.ReadAllText(Path.Combine(_path, "exepath.txt"));
            byte[] ee = DownloadFile("https://github.com/ngbatzyt/monkemodmanager/releases/latest/download/monkemodmanager.exe");
            File.WriteAllBytes(e, ee);
            MessageBox.Show("MonkeModManager updated successfully!", "MonkeUpdater", MessageBoxButtons.OK);
            Process.Start(e);
            Environment.Exit(0);
        }
        
        private static byte[] DownloadFile(string url)
        {
            WebClient client = new WebClient();
            client.Proxy = null;
            return client.DownloadData(url);
        }
    }
}

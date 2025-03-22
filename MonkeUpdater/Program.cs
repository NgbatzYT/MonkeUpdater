using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MonkeUpdater
{
    static class Program
    {
        private static readonly string _path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MonkeUpdater");

        public static async Task Main()
        {
            string exePath = File.ReadAllText(Path.Combine(_path, "exepath.txt"));

            string downloadUrl = "https://github.com/ngbatzyt/monkemodmanager/releases/latest/download/monkemodmanager.exe";
            await DownloadFile(downloadUrl, exePath);

            await Task.Delay(3000);

            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = exePath,
                UseShellExecute = true, 
            };

            Process.Start(psi);
            Environment.Exit(0);
        }

        private static async Task DownloadFile(string url, string savePath)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                using FileStream fs = new FileStream(savePath, FileMode.Create, FileAccess.Write, FileShare.None);
                await response.Content.CopyToAsync(fs);
            }
        }
    }
}

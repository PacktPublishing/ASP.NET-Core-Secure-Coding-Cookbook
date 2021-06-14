using System;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineBankingApp.Services
{
    public class BackupService
    {
        public async Task BackupDB(string backupname)
        {
            using (Process p = new Process())
            {
                string source = Environment.CurrentDirectory + "\\OnlineBank.db";
                string destination = Environment.CurrentDirectory + "\\backups\\" + backupname;
                p.StartInfo.Arguments = " /c copy " + source + " " + destination;
                p.StartInfo.FileName = "cmd";
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardInput = true;
                p.StartInfo.RedirectStandardOutput = true;                
                p.Start();
                await p.WaitForExitAsync();
            }   
        }
        
    }

    public static class ProcessExtensions
    {
        public static async Task WaitForExitAsync(this Process process, CancellationToken cancellationToken)
        {
            while (!process.HasExited)
            {
                await Task.Delay(100, cancellationToken);
            }
        }
    }
}
using System;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace OnlineBankingApp.Services
{
    public class BackupService
    {
        public async Task BackupDB(string backupname)
        {

            var regex = new Regex(@"^[a-zA-Z0-9]*$");                        
            if (!regex.IsMatch(backupname)) return;

            string source = Environment.CurrentDirectory + "\\OnlineBank.db";
            string destination = Environment.CurrentDirectory + "\\backups\\" + backupname;
            await FileCopyAsync(source, destination);  
        }

        public async Task FileCopyAsync(string sourceFileName, string destinationFileName, int bufferSize = 0x1000, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var sourceFile = File.OpenRead(sourceFileName))
            {
                using (var destinationFile = File.OpenWrite(destinationFileName))
                {
                    await sourceFile.CopyToAsync(destinationFile, bufferSize, cancellationToken);
                }
            }
        }        
        
    }

}
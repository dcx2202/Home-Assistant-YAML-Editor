using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAMLEditor
{
    static class SFTPManager
    {
        // Downloads files from remote host
        public static void DownloadRemoteFiles(string address, string username, string password, string origindir, string targetdir, string extension)
        {
            var sftp = new SftpClient(address, username, password);
            sftp.Connect();


            var files = sftp.ListDirectory(origindir);
            foreach (var file in files)
            {
                var fileextension = "";
                var splits = file.Name.Split('.');

                if (splits.Length < 2)
                    continue;

                fileextension = splits[splits.Length - 1];

                if (fileextension != extension)
                    continue;

                if (!Directory.Exists(targetdir))
                    Directory.CreateDirectory(targetdir);

                using (Stream filestream = File.Create(targetdir + file.Name))
                {
                    sftp.DownloadFile(origindir + file.Name, filestream);
                }
            }
        }

        public static void UploadRemoteFiles(string address, string username, string password, string origindir, string targetdir, string extension)
        {
            var sftp = new SftpClient(address, username, password);
            sftp.Connect();


            var files = Directory.EnumerateFiles(origindir, "*." + extension);

            foreach (var file in files)
            {
                var fileextension = "";
                var splits = file.Split('.');

                if (splits.Length < 2)
                    continue;

                fileextension = splits[splits.Length - 1];

                if (fileextension != extension)
                    continue;

                splits = file.Split('/');
                var filename = splits[splits.Length - 1];

                using (FileStream stream = new FileStream(file, FileMode.Open))
                {
                    sftp.UploadFile(stream, targetdir + filename);
                }
            }
        }

    }
}

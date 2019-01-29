using Renci.SshNet;
using System.IO;

namespace YAMLEditor
{
    static class SFTPManager
    {
        /// <summary>
        /// Downloads the files in a given directory in the remote machine to a given local directory
        /// </summary>
        /// <param name="address"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="origindir"></param>
        /// <param name="targetdir"></param>
        /// <param name="extension"></param>
        public static void DownloadRemoteFiles(string address, string username, string password, string origindir, string targetdir, string extension)
        {
            // Establish connection
            var sftp = new SftpClient(address, username, password);
            sftp.Connect();


            // Get the directory's files
            var files = sftp.ListDirectory(origindir);
            foreach (var file in files)
            {
                var fileextension = "";
                var splits = file.Name.Split('.');

                if (splits.Length < 2)
                    continue;

                fileextension = splits[splits.Length - 1];

                // Only download files with the correct extension
                if (fileextension != extension)
                    continue;

                if (!Directory.Exists(targetdir))
                    Directory.CreateDirectory(targetdir);

                // Download file
                using (Stream filestream = File.Create(targetdir + file.Name))
                {
                    sftp.DownloadFile(origindir + file.Name, filestream);
                }
            }
        }

        /// <summary>
        /// Uploads the files in a given directory to the remote machine
        /// </summary>
        /// <param name="address"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="origindir"></param>
        /// <param name="targetdir"></param>
        /// <param name="extension"></param>
        public static void UploadRemoteFiles(string address, string username, string password, string origindir, string targetdir, string extension)
        {
            // Establish connection
            var sftp = new SftpClient(address, username, password);
            sftp.Connect();


            // Get the directory's files
            var files = Directory.EnumerateFiles(origindir, "*." + extension);

            foreach (var file in files)
            {
                var fileextension = "";
                var splits = file.Split('.');

                if (splits.Length < 2)
                    continue;

                fileextension = splits[splits.Length - 1];

                // Only upload files with the correct extension
                if (fileextension != extension)
                    continue;

                splits = file.Split('/');
                var filename = splits[splits.Length - 1];

                // Upload file
                using (FileStream stream = new FileStream(file, FileMode.Open))
                {
                    sftp.UploadFile(stream, targetdir + filename);
                }
            }
        }

    }
}

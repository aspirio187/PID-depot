using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;

namespace Api.Depot.UIL.Static_Data
{
    public static class FilesData
    {
        public const string FILE_DIRECTORY_PATH = "Files";

        public static bool SaveFilesToFolder(IFormFile file, string folderPathTo)
        {
            if (string.IsNullOrEmpty(folderPathTo)) return false;
            if (file is null) return false;

            if (!Directory.Exists(folderPathTo))
            {
                Directory.CreateDirectory(folderPathTo);
            }

            string fileName = Path.GetFileName(file.FileName);
            if (string.IsNullOrEmpty(fileName)) return false;

            using (FileStream fileStream = new FileStream(Path.Combine(folderPathTo, fileName), FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            return true;
        }

        public static bool MoveFilesFromFolder(string folderPathFrom, string folderPathTo)
        {
            if (string.IsNullOrEmpty(folderPathFrom) || string.IsNullOrEmpty(folderPathTo)) return false;
            if (!Directory.Exists(folderPathFrom)) return false;

            if (!Directory.Exists(folderPathTo))
            {
                Directory.CreateDirectory(folderPathTo);
            }

            string[] filesToMove = Directory.GetFiles(folderPathFrom);

            for (int i = 0; i < filesToMove.Length; i++)
            {
                string fileName = Path.GetFileName(filesToMove[i]);
                if (string.IsNullOrEmpty(fileName)) continue;

                string newFile = Path.Combine(folderPathTo, fileName);
                File.Move(filesToMove[i], newFile);
                File.Delete(filesToMove[i]);
            }

            if (Directory.GetFiles(folderPathFrom).Length > 0)
            {
                return false;
            }

            Directory.Delete(folderPathFrom);

            return true;
        }
    }
}

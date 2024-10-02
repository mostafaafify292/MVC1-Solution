using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using static System.Net.WebRequestMethods;
using File = System.IO.File;

namespace MVC1.Helper
{
    public class DocumentSettings
    {
        public static string UploadFile(IFormFile file , string folderName)
        {
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", folderName);
            string fileName =$"{Guid.NewGuid()}{file.FileName}";
            string filePath = Path.Combine(folderPath, fileName);
            using var filestream = new FileStream(filePath, FileMode.Create);
            file.CopyTo(filestream);
            return fileName;
              
        }
        public static void DeleteFile(string fileName , string folderName)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(),folderName, fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}

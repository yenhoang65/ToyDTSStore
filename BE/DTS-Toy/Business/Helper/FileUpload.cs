using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;

namespace Business.Helper
{
    public class FileUpload
    {
        private readonly string _uploadFolder;

        public FileUpload(string uploadFolder)
        {
            _uploadFolder = uploadFolder;
        }

        public List<string> UploadFiles(List<IFormFile> files, string folderName, List<string> customFileNames)
        {
            var filePaths = new List<string>();
            var folderPath = Path.Combine(_uploadFolder, folderName);

            Directory.CreateDirectory(folderPath);

            for (int i = 0; i < files.Count; i++)
            {
                var file = files[i];
                if (file != null && file.Length > 0)
                {
                    var fileExtension = Path.GetExtension(file.FileName).ToLower();

                    if (fileExtension != ".jpg" && fileExtension != ".png")
                    {
                        throw new InvalidOperationException($"File '{file.FileName}' không hợp lệ. Chỉ chấp nhận file định dạng JPG và PNG.");
                    }

                    var fileName = customFileNames != null && customFileNames.Count > i
                        ? customFileNames[i]
                        : Guid.NewGuid().ToString();

                    var uniqueFileName = $"{fileName}{fileExtension}";
                    var filePath = Path.Combine(folderPath, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    filePaths.Add($"Uploads/{folderName}/{uniqueFileName}");
                }
            }

            return filePaths;
        }
    }
}

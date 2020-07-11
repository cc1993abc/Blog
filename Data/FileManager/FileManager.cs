using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.FileManager
{
    public class FileManager : IFileManager
    {
        private string _imagePath;

        public FileManager(IConfiguration config)
        {
            _imagePath = config["Path:Images"];
        }

        public FileStream ImageStream(string image)
        {
            return new FileStream(Path.Combine(_imagePath,image),FileMode.Open,FileAccess.Read);
        }

        public async Task<string> SaveImage(IFormFile image)
        {
            try
            {
            var save_path = Path.Combine(_imagePath);
            if (!Directory.Exists(save_path))
            {
                Directory.CreateDirectory(save_path);
            }

            //格式
            var mime = image.FileName.Substring(image.FileName.LastIndexOf('.'));
            var fileName = $"img_{DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss")}{mime}";
            var test = Path.Combine(save_path, fileName);
            using (var fileSteam = new FileStream(Path.Combine(save_path,fileName),FileMode.Create))
            {
                await image.CopyToAsync(fileSteam);
            }

            return fileName;

            }
            catch (Exception e )
            {

                Console.WriteLine(e.Message);
                return "error";
            }
        }
    }
}

using Book_Store.DB_Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
namespace Book_Store.BL
{
    public interface IFileServices
    {
        public string AddFile(IFormFile file, string name);
        public void DeleteFile(string file);
    }
    public class FileServices : IFileServices
    {
        public string AddFile(IFormFile file, string name)
        {
            var fileName = Path.GetFileName(file.FileName);
            var fileExtension = Path.GetExtension(fileName);
            var newFileName = String.Concat(name, fileExtension);
            var filepath = "";
            if (fileExtension == ".pdf")
            {
                filepath = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Pdf")).Root + $@"\{newFileName}";
            }
            else
            {
                filepath = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images")).Root + $@"\{newFileName}";
            }
            using (FileStream fs = System.IO.File.Create(filepath))
            {
                file.CopyTo(fs);
                fs.Flush();
            }
            return newFileName;
        }
        public void DeleteFile(string file)
        {
            var fileExtension = Path.GetExtension(file);
            var filepath = "";
            if (fileExtension == ".pdf")
            {
                filepath = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Pdf")).Root + $@"\{file}";
            }
            else
            {
                filepath = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images")).Root + $@"\{file}";
            }
            if (System.IO.File.Exists(filepath))
            {
                System.IO.File.Delete(filepath);
            }
        }

    }
}

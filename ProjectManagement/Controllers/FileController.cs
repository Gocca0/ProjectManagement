using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProjectManagement.API.RequestModels.File;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace ProjectManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileController : ControllerBase
    {
        private readonly string _uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
        private readonly string _jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "fileDB.json");


        [HttpGet("{projid}")]
        public async Task<IActionResult> Get(int projid)
        {
            var json = await System.IO.File.ReadAllTextAsync(_jsonFilePath);
            var files = JsonSerializer.Deserialize<List<FilesGet>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            files = files.AsEnumerable().Where(f => f.ProjectId == projid).ToList();

            return Ok(files);
        }

        [HttpGet("Download/{fileid}")]
        public async Task<IActionResult> Downloadfile(Guid fileid)
        {
            var json = await System.IO.File.ReadAllTextAsync(_jsonFilePath);
            var files = JsonSerializer.Deserialize<List<FilesGet>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            var file = files.AsEnumerable().Where(f => f.Id.Equals(fileid)).FirstOrDefault();
            var filePath = Path.Combine(_uploadFolder, file.Name);

            var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);

            return File(fileBytes, "application/octet-stream", file.Name);
        }

        [HttpPost("{projid}")]
        public async Task<IActionResult> Add(int projid, [FromForm] List<IFormFile> files)
        {
           

            if (files == null || files.Count == 0)
                return BadRequest("Файлы не были загружены.");

            if (!Directory.Exists(_uploadFolder))
            {
                Directory.CreateDirectory(_uploadFolder);
            }

            var json = await System.IO.File.ReadAllTextAsync(_jsonFilePath);
            List<FilesGet> fileList = JsonSerializer.Deserialize<List<FilesGet>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });


            foreach (var file in files)
            {
                var filePath = Path.Combine(_uploadFolder, file.FileName);

                var directoryFiles = Directory.GetFiles(_uploadFolder);


                if (!directoryFiles.Where(f => f.StartsWith(filePath)).Any())
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
                
                if (!fileList.Where(f => f.Name.StartsWith(file.FileName)).Any())
                {
                    fileList.Add(new FilesGet
                    {
                        Id = Guid.NewGuid(),
                        ProjectId = projid,
                        Name = file.FileName
                    });
                }
                    
            }

            json = JsonSerializer.Serialize(fileList, new JsonSerializerOptions { WriteIndented = true });
            await System.IO.File.WriteAllTextAsync(_jsonFilePath, json);

            return NoContent();
        }

        [HttpDelete("{projid}/deletefile/{fileid}")]
        public async Task<IActionResult> Delete(int projid, Guid fileid)
        {

            var json = await System.IO.File.ReadAllTextAsync(_jsonFilePath);
            var files = JsonSerializer.Deserialize<List<FilesGet>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            var file = files
                            .Where(f => f.ProjectId.Equals(projid) && f.Id.Equals(fileid) )
                            .FirstOrDefault();

            var filePath = Path.Combine(_uploadFolder, file.Name);

            var amountProjectWithFile = files
                                        .Where(f => f.Name.StartsWith(file.Name))
                                        .Select(f => f.Id).Count();


            if (amountProjectWithFile == 1)
            {
                files.Remove(file);
                System.IO.File.Delete(filePath);
            }

            if(amountProjectWithFile > 1)
            {
                files.Remove(file);
            }


            json = JsonSerializer.Serialize(files, new JsonSerializerOptions { WriteIndented = true });
            await System.IO.File.WriteAllTextAsync(_jsonFilePath, json);

            return NoContent();
        }

    }
}

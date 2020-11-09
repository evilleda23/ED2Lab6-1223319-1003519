using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Encryptors;
using System.Text;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EncryptorController : ControllerBase
    {
        readonly IWebHostEnvironment env;

        public EncryptorController(IWebHostEnvironment _env)
        {
            env = _env;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return NoContent();
        }

        [HttpGet]
        [Route("/api/rsa/keys/{p}/{q}")]
        public IActionResult GenerateKeys(int p, int q)
        {
            return NoContent();
        }

        [HttpPost]
        [Route("/api/rsa/{nombre}")]
        public IActionResult Cipher([FromForm] IFormFile file, [FromForm] IFormFile key, string nombre)
        {
            try
            {
                string path = env.ContentRootPath + "\\" + file.FileName;
                using var saver = new FileStream(path, FileMode.Create);
                file.CopyTo(saver);
                saver.Close();
                using var fileWritten = new FileStream(path, FileMode.OpenOrCreate);
                using var reader = new BinaryReader(fileWritten);
                byte[] buffer = new byte[0];
                while (fileWritten.Position < fileWritten.Length)
                {
                    int index = buffer.Length;
                    Array.Resize<byte>(ref buffer, index + 100000);
                    byte[] aux = reader.ReadBytes(100000);
                    aux.CopyTo(buffer, index);
                }
                reader.Close();
                fileWritten.Close();
                for (int i = buffer.Length - 1; i >= 0; i--)
                {
                    if (buffer[i] != 0)
                    {
                        Array.Resize<byte>(ref buffer, i + 1);
                        break;
                    }
                }
                if (buffer.Length > 0)
                {
                    using var content = new MemoryStream();
                    key.CopyTo(content);
                    var text = Encoding.ASCII.GetString(content.ToArray());
                    var encryptor = new RSAEncryptor(env.ContentRootPath);
                    path = encryptor.Cipher(buffer, new Key(text), nombre);
                    if (path != "")
                    {
                        FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate);
                        return File(fileStream, "text/plain");
                    }
                    else
                        return StatusCode(500, "La llave no es valida");
                }
                else
                    return StatusCode(500, "El archivo está vacío");
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}

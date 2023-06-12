using DW_Final_Project.Data;
using DW_Final_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DW_Final_Project.Controllers
{
    public class APIController : Controller
    {
        private readonly ApplicationDbContext _context;

        public APIController(ApplicationDbContext context)
        {
            _context = context;
        }

        ////GET: User Login
        //[HttpGet("/API/Login/{email}/{pwd}")]
        //public async Task<IActionResult> Login(string email, string pwd)
        //{
        //    if (email == null || pwd == null || email == "" || pwd == "" || _context.User == null)
        //    {
        //        return BadRequest();
        //    }
        //    pwd = EncriptarSenha(pwd);
        //    var user = await _context.User
        //        .Include(u => u.type)
        //        .FirstOrDefaultAsync(m => m.email.Equals(email) && m.password.Equals(pwd));

        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    var options = new JsonSerializerOptions
        //    {
        //        ReferenceHandler = ReferenceHandler.Preserve
        //    };

        //    var serializedUser = JsonSerializer.Serialize(user, options);

        //    return Ok(serializedUser);
        //}

        ////POST: User Register
        //[HttpPost("/API/Register")]
        //public async Task<IActionResult> Register(string email, string pwd)
        //{
        //    if (email == null || pwd == null || email == "" || pwd == "" || _context.User == null)
        //    {
        //        return BadRequest();
        //    }

        //    var user = await _context.User
        //        .Include(u => u.type)
        //        .FirstOrDefaultAsync(m => m.email.Equals(email) && m.password.Equals(pwd));

        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    var options = new JsonSerializerOptions
        //    {
        //        ReferenceHandler = ReferenceHandler.Preserve
        //    };

        //    var serializedUser = JsonSerializer.Serialize(user, options);

        //    return Ok(serializedUser);
        //}

        //GET: Get All Products
        [HttpGet("/API/Products")]
        public async Task<IActionResult> Products()
        {
            var products = await _context.Product.ToListAsync();

            if (products == null)
            {
                return Ok();
            }

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            var serializedUser = JsonSerializer.Serialize(products, options);

            return Ok(serializedUser);
        }

        //GET: Get All Products
        [HttpGet("/API/Products/{id}")]
        public async Task<IActionResult> getProductById(int id)
        {
            var product = await _context.Product.FindAsync(id);

            if (product == null)
            {
                return Ok();
            }

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            var serializedUser = JsonSerializer.Serialize(product, options);

            return Ok(serializedUser);
        }

        ////GET: Get Profile
        //[HttpGet("/API/Profile/{email}")]
        //public async Task<IActionResult> Profile(string email)
        //{
        //    if (email == null)
        //    {
        //        return BadRequest();
        //    }
        //    var person = await _context.Person
        //        .Include(u => u.user)
        //        .FirstOrDefaultAsync(m => m.user.email==email);

        //    if (person == null)
        //    {
        //        return NotFound();
        //    }

        //    var options = new JsonSerializerOptions
        //    {
        //        ReferenceHandler = ReferenceHandler.Preserve
        //    };

        //    var serializedUser = JsonSerializer.Serialize(person, options);

        //    return Ok(serializedUser);
        //}

        ////POST: User Register
        //[HttpPost("/API/updateProfile/{email}")]
        //public IActionResult UpdatePerson(string email, [FromBody] Person personUpdateModel)
        //{
        //    // Lógica para atualizar os dados da pessoa com o email fornecido

        //    // Exemplo de implementação básica para atualizar os dados da pessoa
        //    var user = _context.User.FirstOrDefault(u => u.email == email);
        //    var person = _context.Person.FirstOrDefault(p => p.userFK == user.id);

        //    if (person != null)
        //    {
        //        person.name = personUpdateModel.name;
        //        person.phoneNumber = personUpdateModel.phoneNumber;
        //        person.address = personUpdateModel.address;
        //        person.postalCode= personUpdateModel.postalCode;
        //        person.dataNasc = personUpdateModel.dataNasc;
        //        person.gender = personUpdateModel.gender;

        //        if (personUpdateModel.imagePath.StartsWith("data:image/"))
        //        {
        //            int startIndex = personUpdateModel.imagePath.IndexOf("/") + 1; // Encontra a posição do primeiro caractere após "/"
        //            int endIndex = personUpdateModel.imagePath.IndexOf(";"); // Encontra a posição do último caractere antes de ";"
        //            string extFoto = personUpdateModel.imagePath.Substring(startIndex, endIndex - startIndex);

        //            // Remova o prefixo "data:image/jpeg;base64," da string
        //            string base64String = personUpdateModel.imagePath.Substring(personUpdateModel.imagePath.IndexOf(',') + 1);

        //            // Decodifique a string base64 em um array de bytes
        //            byte[] imagemBytes = Convert.FromBase64String(base64String);
        //            //converter byte[] para ficheiro
        //            string nomeArquivo = Guid.NewGuid().ToString() + "_" + person.id+"."+extFoto;
        //            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", nomeArquivo);

        //            using (FileStream fs = new FileStream(filePath, FileMode.Create))
        //            {
        //                fs.Write(imagemBytes, 0, imagemBytes.Length);
        //            }
        //            person.imagePath = nomeArquivo;
        //        }
        //        else
        //        {
        //            //mesma imagem
        //            person.imagePath = personUpdateModel.imagePath;
        //        }

        //        _context.Update(person);
        //        _context.SaveChanges();

        //        return Ok("Dados da pessoa atualizados com sucesso.");
        //    }

        //    return NotFound("Pessoa não encontrada.");
        //}


        private string EncriptarSenha(string senha)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(senha);
                byte[] hash = sha256.ComputeHash(bytes);

                StringBuilder builder = new StringBuilder();

                for (int i = 0; i < hash.Length; i++)
                {
                    builder.Append(hash[i].ToString("x2")); // Converte o byte em uma string hexadecimal
                }

                return builder.ToString();
            }
        }


    }
}

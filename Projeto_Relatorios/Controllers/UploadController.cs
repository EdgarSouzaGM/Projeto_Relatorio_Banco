using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Projeto_Relatorios.Controllers
{
    public class UploadController : Controller
    {
        //private readonly string _caminhoDestino = @"C:\Users\Users\Desktop\Projeto_Relatorios-master\Projeto_Relatorios\bin\Debug\netcoreapp3.1\Relatorios";
        private readonly string _caminhoDestino = @"C:\Users\edgar\Desktop\documentos";

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Enviar(IFormFile arquivo)
        {
            if (arquivo != null && arquivo.Length > 0)
            {
                string Pessoa = "Enzo Gabriel";
                var caminhoArquivo = Path.Combine(_caminhoDestino, Path.GetFileName(arquivo.FileName));

                using (var stream = new FileStream(caminhoArquivo, FileMode.Create))
                {
                    await arquivo.CopyToAsync(stream);
                }
                using (var connection = new MySqlConnection("Server=localhost;Port=3306;Database=Relatorio;Uid=root;Pwd=senha123;"))
                {
                    var sql = "INSERT INTO LogRelatorio (descricao, DataRegistro) VALUES (@Descricao, @DataRegistro);";
                    var parametros = new
                    {
                        Descricao = "O Usuário " + Pessoa + " salvou o arquivo " + arquivo.Name + ".",
                        DataRegistro = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                    };

                    connection.Execute(sql, parametros);
                }
                TempData["Mensagem"] = "Arquivo enviado com sucesso!";
                return RedirectToAction("Index");
            }

            TempData["Mensagem"] = "Selecione um arquivo válido.";
            return RedirectToAction("Index");
        }
    }
}

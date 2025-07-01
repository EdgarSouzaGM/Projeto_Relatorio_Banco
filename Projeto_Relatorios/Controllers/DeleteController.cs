using Dapper;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;
using System.IO;

namespace Projeto_Relatorios.Controllers
{
    public class DeleteController : Controller
    {
        //private readonly string _caminhoDosArquivos = @"C:\Users\Users\Desktop\Projeto_Relatorios-master\Projeto_Relatorios\bin\Debug\netcoreapp3.1\Relatorios";
        private readonly string _caminhoDosArquivos = @"C:\Users\edgar\Desktop\documentos";
        [HttpPost]
        public IActionResult Deleta(string nomeArquivo)
        {
            string Pessoa = "Enzo Gabriel";
            if (string.IsNullOrEmpty(nomeArquivo))
                return BadRequest("Nome do arquivo inválido.");

            var caminhoCompleto = Path.Combine(_caminhoDosArquivos, nomeArquivo);

            if (System.IO.File.Exists(caminhoCompleto))
                System.IO.File.Delete(caminhoCompleto);

            using (var connection = new MySqlConnection("Server=localhost;Port=3306;Database=Relatorio;Uid=root;Pwd=senha123;"))
            {
                var sql = "INSERT INTO LogRelatorio (descricao, DataRegistro) VALUES (@Descricao, @DataRegistro);";
                var parametros = new
                {
                    Descricao = "O Usuário " + Pessoa + " fez a exclusão do arquivo " + nomeArquivo + ".",
                    DataRegistro = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                };

                connection.Execute(sql, parametros);
            }

            TempData["Mensagem"] = "Arquivo deletado com sucesso!";
            return RedirectToAction("Index","Home");

            
        }
    }
}

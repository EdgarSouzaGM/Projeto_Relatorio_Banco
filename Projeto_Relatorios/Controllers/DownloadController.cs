using Dapper;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;
using System.IO;

namespace Projeto_Relatorios.Controllers
{
    public class DownloadController : Controller
    {
        //private readonly string _caminhoDosArquivos = @"C:\Users\Users\Desktop\Projeto_Relatorios-master\Projeto_Relatorios\bin\Debug\netcoreapp3.1\Relatorios";
        private readonly string _caminhoDosArquivos = @"C:\Users\edgar\Desktop\documentos";

        [HttpGet]
        public IActionResult Download(string nomeArquivo)
        {
            string Pessoa = "Enzo Gabriel";
            if (string.IsNullOrEmpty(nomeArquivo))
                return BadRequest("Nome do arquivo inválido.");

            var caminhoCompleto = Path.Combine(_caminhoDosArquivos, nomeArquivo);

            if (!System.IO.File.Exists(caminhoCompleto))
                return NotFound("Arquivo não encontrado.");

            var conteudoArquivo = System.IO.File.ReadAllBytes(caminhoCompleto);
            var tipoMime = "application/octet-stream";
            using (var connection = new MySqlConnection("Server=localhost;Port=3306;Database=Relatorio;Uid=root;Pwd=senha123;"))
            {
                var sql = "INSERT INTO LogRelatorio (descricao, DataRegistro) VALUES (@Descricao, @DataRegistro);";
                var parametros = new
                {
                    Descricao = "O Usuário " + Pessoa + "fez o download do arquivo " + nomeArquivo + ".",
                    DataRegistro = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                };

                connection.Execute(sql, parametros);
            }

            return File(conteudoArquivo, tipoMime, nomeArquivo);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using System.IO;
using System;
using System.Collections.Generic;

namespace Projeto_Relatorios.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //string caminhoDaPasta = @"C:\Users\Users\Desktop\Projeto_Relatorios-master\Projeto_Relatorios\bin\Debug\netcoreapp3.1\Relatorios";
            string caminhoDaPasta = @"C:\Users\edgar\Desktop\documentos";
            List<string> listaArquivos = new List<string>();

            if (Directory.Exists(caminhoDaPasta))
            {
                string[] arquivos = Directory.GetFiles(caminhoDaPasta);

                foreach (string arquivo in arquivos)
                {
                    string nomeArquivo = Path.GetFileName(arquivo);
                    listaArquivos.Add(nomeArquivo);

                }
            }
            else
            {
                Console.WriteLine("A pasta especificada não existe.");
            }

            return View(listaArquivos);
        }

    }
}

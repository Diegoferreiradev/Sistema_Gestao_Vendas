using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SistemaDeVendas.Models;

namespace SistemaDeVendas.Controllers
{
    public class VendaController : Controller
    {

        public IActionResult Registrar()
        {
            CarregarDados();
            return View();
        }

        [HttpPost]
        public IActionResult Registrar(VendaModel venda)
        {
            venda.Inserir();
            CarregarDados();
            return View();
        }

        private void CarregarDados()
        {
            ViewBag.ListaClientes = new VendaModel().RetornarListaClientes();
            ViewBag.ListaVendedores = new VendaModel().RetornarListaVendedores();
            ViewBag.ListaProdutos = new VendaModel().RetornarListaProdutos();
        }
    }
}
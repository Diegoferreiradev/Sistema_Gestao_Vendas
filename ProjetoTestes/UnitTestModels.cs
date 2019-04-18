using System;
using Xunit;
using SistemaDeVendas.Models;
using System.Collections.Generic;

namespace ProjetoTestes
{
    public class UnitTestModels
    {
        [Fact]
        public void TestLoginOk()
        {
            LoginModel login = new LoginModel();

            login.Email = "diegoferreiradev@gmail.com";
            login.Senha = "12345";
            bool resultado = login.ValidarLogin();
            Assert.True(resultado);
        }

        [Fact]
        public void TestLoginFail()
        {
            LoginModel login = new LoginModel();

            login.Email = "diegoferreiradev@gmail.com";
            login.Senha = "testando";
            bool resultado = login.ValidarLogin();
            Assert.False(resultado);
        }

        [Fact]
        public void CheckTypeListProdutos()
        {
            ProdutoModel produto = new ProdutoModel();
            var lista = produto.ListarTodosProdutos();
            Assert.IsType<List<ProdutoModel>>(lista);
        }
    }
}

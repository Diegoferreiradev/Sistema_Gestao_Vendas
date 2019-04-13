using SistemaDeVendas.Uteis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaDeVendas.Models
{
    public class VendaModel
    {
        public string Id { get; set; }

        public string Cliente_Id { get; set; }

        public string Vendedor_Id { get; set; }

        public double Total { get; set; }

        public string ListaProdutos { get; set; }


        public List<ClienteModel> RetornarListaClientes()
        {
            return new ClienteModel().ListarTodosClientes();
        }

        public List<VendedorModel> RetornarListaVendedores()
        {
            return new VendedorModel().ListarTodosVendedores();
        }

        public List<ProdutoModel> RetornarListaProdutos()
        {
            return new ProdutoModel().ListarTodosProdutos();
        }

        public void Inserir()
        {
            DAL dal = new DAL();

            string dataVenda = DateTime.Now.Date.ToString("yyyy/MM/dd");

            string sql = $"INSERT INTO Venda(data_venda, total, vendedor_id, cliente_id) " +
                         $"VALUES ('{dataVenda}', {Total.ToString().Replace(",",".")}, {Vendedor_Id}, {Cliente_Id})";
            dal.ExecutarComandoSQL(sql);
        }
    }
}

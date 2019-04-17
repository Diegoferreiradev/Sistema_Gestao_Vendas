using Newtonsoft.Json;
using SistemaDeVendas.Uteis;
using System;
using System.Collections.Generic;
using System.Data;
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

            string dataVendas = DateTime.Now.Date.ToString("yyyy/MM/dd");

            string sql = "INSERT INTO Venda(data_venda, total, vendedor_id, cliente_id) " +
                         $"VALUES('{dataVendas}',{Total.ToString().Replace(",",".")},{Vendedor_Id},{Cliente_Id})";
            dal.ExecutarComandoSQL(sql);


            sql = $"SELECT TOP 1 id FROM Venda WHERE data_venda='{dataVendas}' AND vendedor_id={Vendedor_Id} AND cliente_id={Cliente_Id} ORDER BY id DESC";
            DataTable dt = dal.RetDataTable(sql);
            string id_venda = dt.Rows[0]["id"].ToString();

            List<ItemVendaModel> lista_produtos = JsonConvert.DeserializeObject<List<ItemVendaModel>>(ListaProdutos);

            for (int i = 0; i < lista_produtos.Count; i++)
            {
                sql = "INSERT INTO Itens_venda(venda_id, produto_id, qtd_produto, preco_produto)" +
                      $"VALUES({id_venda}, {lista_produtos[i].CodigoProduto.ToString()}," +
                      $" {lista_produtos[i].QtdProduto.ToString()}, " +
                      $"{lista_produtos[i].PrecoUnitario.ToString()})";
                dal.ExecutarComandoSQL(sql);
            }

        }
    }
}

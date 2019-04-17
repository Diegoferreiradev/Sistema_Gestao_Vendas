﻿using Newtonsoft.Json;
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

        public string Data { get; set; }

        public string Cliente_Id { get; set; }

        public string Vendedor_Id { get; set; }

        public double Total { get; set; }

        public string ListaProdutos { get; set; }


        public List<VendaModel> ListagemVendas(string DataDe, string DataAte)
        {
            return RetornarListagemVendas(DataDe, DataAte);
        }

        // Lista Geral
        public List<VendaModel> ListagemVendas()
        {
            return RetornarListagemVendas("1900/01/01","2100/01/01");
        }

        private List<VendaModel> RetornarListagemVendas(string DataDe, string DataAte)
        {
            List<VendaModel> lista = new List<VendaModel>();
            VendaModel item;
            DAL dal = new DAL();
            string sql = " SELECT v1.id, v1.data_venda, v1.total, v2.nome as vendedor, c.nome as cliente FROM" +
                         " venda v1 INNER JOIN Vendedor v2 on v1.vendedor_id = v2.id INNER JOIN cliente c " +
                         " on v1.cliente_id = c.id" +
                        $" WHERE v1.data_venda >='{DataDe}' and v1.data_venda <='{DataAte}' " +
                         " ORDER BY data_venda, total";
            DataTable dt = dal.RetDataTable(sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new VendaModel
                {
                    Id = dt.Rows[i]["Id"].ToString(),
                    Data = DateTime.Parse(dt.Rows[i]["data_venda"].ToString()).ToString("dd/MM/yyyy"),
                    Total = double.Parse(dt.Rows[i]["total"].ToString()),
                    Cliente_Id = dt.Rows[i]["cliente"].ToString(),
                    Vendedor_Id = dt.Rows[i]["vendedor"].ToString()
                };

                lista.Add(item);
            }
            return lista;
        }


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

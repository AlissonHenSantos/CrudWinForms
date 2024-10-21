using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

namespace SistemaCadastro
{
    internal class ConectaBanco
    {
        MySqlConnection conexao = new MySqlConnection("server=localhost;user id=root;password=;database=bandas");
        public string mensagem;

        public bool insereBanda(Banda newBanda)
        {
            try
            {
                conexao.Open();
                MySqlCommand cmd =
                    new MySqlCommand("sp_insertBanda", conexao);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("nome", newBanda.Nome);
                cmd.Parameters.AddWithValue("integrantes", newBanda.Integrantes);
                cmd.Parameters.AddWithValue("ranking", newBanda.Ranking);
                cmd.Parameters.AddWithValue("fk_genero", newBanda.Genero);
                cmd.ExecuteNonQuery();
                return true;

            }
            catch (MySqlException err) { 
                mensagem = err.Message;
                return false;
            }
        }
        public DataTable listaGeneros()
        {
            MySqlCommand cmd = new MySqlCommand("sp_listaGeneros", conexao);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conexao.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable tabela = new DataTable();
                da.Fill(tabela);
                return tabela;
            }
            catch (MySqlException err)
            {
                mensagem  = err.Message;  
                return null;
            }
            finally
            {
                conexao.Close();
            }
        }
    }
}

﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pi_serasa_greenloop
{
    internal class Conexao
    {
        const string host = "srv793.hstgr.io", banco = "u647084545_serasa_greenlp", usuario = "u647084545_serasa_greenlp", senha = "Serasa2023",
                     dadosConexao = $"Server={host};Database={banco};Uid={usuario};PwD={senha};";

        static MySqlConnection conexao = new MySqlConnection(dadosConexao);

        public static DataTable executaQuery(string query)
        {
            try
            {
                conexao.Open();

                MySqlCommand comando = new MySqlCommand(query, conexao);
                MySqlDataReader dados = comando.ExecuteReader();
                DataTable tabela = new DataTable();
                tabela.Load(dados);
                return tabela;
            }
            catch (Exception erro)
            {
                Console.WriteLine("BAH PIÁ DEU ERRO TCHÊ");
                Console.WriteLine(erro.Message);
                return null;
                throw;
            }
            finally { conexao.Close(); }
        }

        public static bool CPFExisteNaTabela(string cpf)
        {
            try
            {
                conexao.Open();

                string query = "SELECT COUNT(*) FROM pessoas WHERE cpf = @cpf";
                using (MySqlCommand cmd = new MySqlCommand(query, conexao))
                {
                    cmd.Parameters.AddWithValue("@cpf", cpf);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());

                    return count > 0;
                }
            }
            catch (Exception erro)
            {
                Console.WriteLine("BAH PIÁ DEU ERRO TCHÊ");
                Console.WriteLine(erro.Message);
                return false;
            }
            finally { conexao.Close(); }
        }

        public static bool EmailExisteNaTabela(string email)
        {
            try
            {
                conexao.Open();

                string query = "SELECT COUNT(*) FROM pessoas WHERE email = @email";
                using (MySqlCommand cmd = new MySqlCommand(query, conexao))
                {
                    cmd.Parameters.AddWithValue("@email", email);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());

                    return count > 0;
                }
            }
            catch (Exception erro)
            {
                Console.WriteLine("BAH PIÁ DEU ERRO TCHÊ");
                Console.WriteLine(erro.Message);
                return false;
            }
            finally { conexao.Close(); }
        }

        public static bool EmailExisteNaTabelaPolos(string email)
        {
            try
            {
                conexao.Open();

                string query = "SELECT COUNT(*) FROM polos WHERE email = @email";
                using (MySqlCommand cmd = new MySqlCommand(query, conexao))
                {
                    cmd.Parameters.AddWithValue("@email", email);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());

                    return count > 0;
                }
            }
            catch (Exception erro)
            {
                Console.WriteLine("BAH PIÁ DEU ERRO TCHÊ");
                Console.WriteLine(erro.Message);
                return false;
            }
            finally { conexao.Close(); }
        }

        public static void AdicionarPontos(string cpf, int pontos)
        {
            try
            {
                conexao.Open();

                // Recupere a pontuação atual do CPF no banco de dados
                int pontuacaoAtual = RecuperarPontuacaoAtual(cpf);

                // Soma os pontos fornecidos aos pontos atuais
                int novaPontuacao = pontuacaoAtual + pontos;

                // Atualize a pontuação no banco de dados
                AtualizarPontuacao(cpf, novaPontuacao);
            }
            catch (MySqlException erro)
            {
                Console.WriteLine("Ocorreu um erro: " + erro.Message);
            }
            finally
            {
                conexao.Close();
            }
        }

        public static int RecuperarPontuacaoAtual(string cpf)
        {
            string query = "SELECT pontos FROM pessoas WHERE CPF = @cpf";
            using (MySqlCommand cmd = new MySqlCommand(query, conexao))
            {
                cmd.Parameters.AddWithValue("@cpf", cpf);

                object result = cmd.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    return Convert.ToInt32(result);
                }
                else
                {
                    return 0;
                }
            }
        }

        private static void AtualizarPontuacao(string cpf, int novaPontuacao)
        {
            string query = "UPDATE pessoas SET pontos = @novaPontuacao WHERE CPF = @cpf";
            using (MySqlCommand cmd = new MySqlCommand(query, conexao))
            {
                cmd.Parameters.AddWithValue("@novaPontuacao", novaPontuacao);
                cmd.Parameters.AddWithValue("@cpf", cpf);
                cmd.ExecuteNonQuery();
            }
        }
        public static bool CodigoExisteNaTabelaPremios(string codigo)
        {
            try
            {
                conexao.Open();

                string query = "SELECT COUNT(*) FROM premios WHERE codigo = @codigo";
                using (MySqlCommand cmd = new MySqlCommand(query, conexao))
                {
                    cmd.Parameters.AddWithValue("@codigo", codigo);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());

                    return count > 0;
                }
            }
            catch (Exception erro)
            {
                Console.WriteLine("Ocorreu um erro: " + erro.Message);
                return false;
            }
            finally
            {
                conexao.Close();
            }
        }
    }
}

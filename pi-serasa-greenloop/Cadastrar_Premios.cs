﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pi_serasa_greenloop
{
    public partial class Cadastrar_Premios : Form
    {
        public Cadastrar_Premios()
        {
            InitializeComponent();
        }
        void cadastrarPremios()
        {
            string premio = textBoxPremios.Texts;
            string nome = txtNomeRecompensa.Texts;
            string descricao = txtDescricaoRecompensa.Texts;

            if (string.IsNullOrWhiteSpace(premio) || string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(descricao))
            {
                MessageBox.Show("Por favor, preencha todos os campos antes de cadastrar.");
                return;
            }

            if (premio.Length > 10)
            {
                MessageBox.Show("O código do prêmio deve ter no máximo 10 caracteres.");
                return;
            }

            if (!int.TryParse(txtValorRecompensa.Texts, out int valor))
            {
                MessageBox.Show("O valor inserido não é válido.");
                return;
            }

            // Verifique se o código do prêmio já existe na tabela premios
            if (Conexao.CodigoExisteNaTabelaPremios(premio))
            {
                MessageBox.Show("O código do prêmio já está em uso. Escolha outro código.");
                return;
            }

            MessageBox.Show("Prêmio Cadastrado!");
            textBoxPremios.Texts = "";
            txtNomeRecompensa.Texts = "";
            txtDescricaoRecompensa.Texts = "";
            txtValorRecompensa.Texts = "";
            textBoxPremios.Focus();
            Premios premios = new Premios(premio, nome, descricao, valor);
            premios.adicionarPremios();
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {

            cadastrarPremios();
        }

        private void Cadastrar_Premios_Load(object sender, EventArgs e)
        {
            textBoxPremios.TabIndex = 0;
            txtNomeRecompensa.TabIndex = 1;
            txtDescricaoRecompensa.TabIndex = 2;
            txtValorRecompensa.TabIndex = 3;
        }
    }
}

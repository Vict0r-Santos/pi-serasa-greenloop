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
    public partial class Local : Form
    {
        public Local()
        {
            InitializeComponent();
        }
        [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
    (
        int nLeftRect,
        int nTopRect,
        int nRightRect,
        int nBottomRect,
        int nWidthEllipse,
        int nHeightEllipse
    );
        private void CentralizarPnlCarregaPolos()
        {
            // Calcule a posição horizontal para centralizar o pnlCarregaPolos
            int xPosition = (this.ClientSize.Width - pnlCarregaPolos.Width) / 2;

            // Defina a posição do pnlCarregaPolos
            pnlCarregaPolos.Location = new Point(xPosition, pnlCarregaPolos.Location.Y);
        }

        private void PreencherPaineisDePolos()
        {
            // Consulta SQL para recuperar os polos da tabela polos
            string query = "SELECT nome, email, endereco FROM polos";

            DataTable polos = Conexao.executaQuery(query);

            if (polos != null && polos.Rows.Count > 0)
            {
                int espacamentoHorizontal = 20; // Espaçamento horizontal entre os painéis
                int espacamentoVertical = 20; // Espaçamento vertical entre os painéis
                int larguraPainel = 600; // Largura dos painéis
                int alturaPainel = 180; // Altura dos painéis
                int maxPaineisPorLinha = 2; // Máximo de painéis por linha

                int colunaAtual = 0; // Rastreia a coluna atual
                int xPosition = 20; // Posição horizontal do próximo painel
                int yPosition = 10; // Posição vertical do próximo painel

                foreach (DataRow row in polos.Rows)
                {
                    // Recupere os dados do polo a partir da linha atual
                    string nome = row["nome"].ToString();
                    string endereco = row["endereco"].ToString();
                    string email = row["email"].ToString();

                    // Crie um novo painel para o polo
                    Panel painelPolo = new Panel();
                    painelPolo.Size = new Size(larguraPainel, alturaPainel);
                    painelPolo.Location = new Point(xPosition, yPosition); // Define a posição
                    painelPolo.BackColor = Color.Orange; // Cor de fundo laranja
                    painelPolo.Padding = new Padding(10); // Margem interna
                    Cursor.Current = Cursors.Hand;

                    // Adicione bordas arredondadas ao painel
                    painelPolo.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, painelPolo.Width, painelPolo.Height, 10, 10));

                    // Crie rótulos para exibir os detalhes do polo dentro do painel
                    Label lblNome = new Label();
                    lblNome.Text = nome;
                    lblNome.Location = new Point(10, 10);
                    lblNome.Font = new Font("Segoe UI", 16, FontStyle.Bold); // Defina a fonte e o tamanho
                    lblNome.AutoSize = true; // Ajuste automático ao tamanho do texto

                    Label lblEndereco = new Label();
                    lblEndereco.Text = endereco;
                    lblEndereco.Location = new Point(10, 40);
                    lblEndereco.Font = new Font("Segoe UI", 13); // Defina a fonte e o tamanho
                    lblEndereco.AutoSize = true; // Ajuste automático ao tamanho do texto

                    Label lblEmail = new Label();
                    lblEmail.Text = $"Contato: {email}";
                    lblEmail.Location = new Point(13, 70);
                    lblEmail.Font = new Font("Segoe UI", 13); // Defina a fonte e o tamanho
                    lblEmail.AutoSize = true; // Ajuste automático ao tamanho do texto

                    // Adicione os rótulos ao painel
                    painelPolo.Controls.Add(lblNome);
                    painelPolo.Controls.Add(lblEndereco);
                    painelPolo.Controls.Add(lblEmail);

                    painelPolo.MouseEnter += (sender, e) =>
                    {
                        // Altere o cursor para a mão quando o mouse entrar no painel
                        Cursor = Cursors.Hand;
                    };
                    painelPolo.MouseLeave += (sender, e) =>
                    {
                        // Restaure o cursor padrão quando o mouse sair do painel
                        Cursor = Cursors.Default;
                    };

                    // Adicione um evento de clique ao painel
                    painelPolo.Click += (sender, e) =>
                    {
                        // Abra uma pesquisa no Google Maps com o endereço do polo
                        AbrirPesquisaGoogleMaps(endereco);
                    };

                    lblEndereco.Click += (sender, e) =>
                    {
                        // Abra uma pesquisa no Google Maps com o endereço do polo
                        AbrirPesquisaGoogleMaps(endereco);
                    };

                    lblEmail.Click += (sender, e) =>
                    {
                        // Abra uma pesquisa no Google Maps com o endereço do polo
                        AbrirPesquisaGoogleMaps(endereco);
                    };

                    lblNome.Click += (sender, e) =>
                    {
                        // Abra uma pesquisa no Google Maps com o endereço do polo
                        AbrirPesquisaGoogleMaps(endereco);
                    };

                    lblNome.MouseEnter += (sender, e) =>
                    {
                        // Altere o cursor para a mão quando o mouse entrar no painel
                        Cursor = Cursors.Hand;
                    };
                    lblNome.MouseLeave += (sender, e) =>
                    {
                        // Restaure o cursor padrão quando o mouse sair do painel
                        Cursor = Cursors.Default;
                    };

                    lblEndereco.MouseEnter += (sender, e) =>
                    {
                        // Altere o cursor para a mão quando o mouse entrar no painel
                        Cursor = Cursors.Hand;
                    };
                    lblEndereco.MouseLeave += (sender, e) =>
                    {
                        // Restaure o cursor padrão quando o mouse sair do painel
                        Cursor = Cursors.Default;
                    };

                    lblEmail.MouseEnter += (sender, e) =>
                    {
                        // Altere o cursor para a mão quando o mouse entrar no painel
                        Cursor = Cursors.Hand;
                    };
                    lblEmail.MouseLeave += (sender, e) =>
                    {
                        // Restaure o cursor padrão quando o mouse sair do painel
                        Cursor = Cursors.Default;
                    };

                    // Adicione o painel do polo ao pnlCarregaPolos
                    pnlCarregaPolos.Controls.Add(painelPolo);

                    // Atualize a posição horizontal para o próximo painel
                    xPosition += larguraPainel + espacamentoHorizontal;

                    // Verifique se atingiu o limite de painéis por linha
                    colunaAtual++;
                    if (colunaAtual >= maxPaineisPorLinha)
                    {
                        colunaAtual = 0;
                        xPosition = 20;
                        yPosition += alturaPainel + espacamentoVertical; // Mova para a próxima linha
                    }
                }
            }
            else
            {
                MessageBox.Show("Não foram encontrados polos registrados.");
            }
        }
        private void AbrirPesquisaGoogleMaps(string endereco)
        {
            try
            {
                // Use a URL do Google Maps para pesquisar o endereço
                string url = $"https://www.google.com/maps/search/?api=1&query={Uri.EscapeDataString(endereco)}";

                // Abra o navegador padrão do usuário com a URL
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Não foi possível abrir o Google Maps. Erro: {ex.Message}");
            }
        }


        private void wilBitPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Local_Load(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            CentralizarPnlCarregaPolos();
            PreencherPaineisDePolos();
        }
    }
}

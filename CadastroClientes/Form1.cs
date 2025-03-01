using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace CadastroClientes
{
    public partial class FrmCadCliente: Form
    {
        public FrmCadCliente()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void FrmCadCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
                e.SuppressKeyPress = true;

            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void SalvarClientes()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection("Server=localhost;Database=clientes_db;User=root;Password=;"))
                {
                   
                    conn.Open();
                    MessageBox.Show("ok");

                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"INSERT INTO tabClienteS 
                        (nome, documento, genero, rg, estado_civil, nasc, cep, endereco, numero, bairro, cidade, estado, celular, email, obs, situacao) 
                        VALUES 
                        (@nome, @documento, @genero, @rg, @estado_civil, @nasc, @cep, @endereco, @numero, @bairro, @cidade, @estado, @celular, @email, @obs, @situacao)";

                        // Validações para garantir que os campos não sejam nulos
                        if (campoNome == null || string.IsNullOrEmpty(campoNome.Text))
                        {
                            MessageBox.Show("Campo nome é obrigatório");
                            campoNome.Focus();
                            return;
                        }
                        cmd.Parameters.AddWithValue("@nome", campoNome.Text);
                        cmd.Parameters.AddWithValue("@documento", campoDocumento.Text);
                        if (opM.Checked == true)
                        {
                            cmd.Parameters.AddWithValue("@genero", "Masculino");
                        }
                        else if (opF.Checked == true)
                        {
                            cmd.Parameters.AddWithValue("@genero", "Feminino");
                        }
                        else if (opOutros.Checked == true)
                        {
                            cmd.Parameters.AddWithValue("@genero", "Outros");
                        }
                        else
                        {
                            MessageBox.Show("Selecione o gênero.");
                            return;
                        }
                        cmd.Parameters.AddWithValue("@rg", campoRG.Text);

                        if (cbEstadoCivil.SelectedItem == null)
                        {
                            MessageBox.Show("Selecione o estado civil.");
                            return;
                        }
                        cmd.Parameters.AddWithValue("@estado_civil", cbEstadoCivil.SelectedItem.ToString());

                        if (campoNasc.Text == "  /  /")
                        {
                            cmd.Parameters.AddWithValue("@nasc", DBNull.Value);
                        }
                        else
                        {
                            DateTime dataNascimento;
                            if (DateTime.TryParse(campoNasc.Text, out dataNascimento))
                            {
                                cmd.Parameters.AddWithValue("@nasc", dataNascimento);
                            }
                            else
                            {
                                MessageBox.Show("Data de nascimento inválida.");
                                return;
                            }
                        }

                        // Adiciona os demais campos
                        cmd.Parameters.AddWithValue("@cep", campoCEP.Text);
                        cmd.Parameters.AddWithValue("@endereco", campoEndereco.Text);
                        cmd.Parameters.AddWithValue("@numero", campoNumero.Text);
                        cmd.Parameters.AddWithValue("@bairro", campoBairro.Text);
                        cmd.Parameters.AddWithValue("@cidade", campoCidade.Text);
                        cmd.Parameters.AddWithValue("@estado", campoEstado.Text);
                        cmd.Parameters.AddWithValue("@celular", campoCelular.Text);
                        cmd.Parameters.AddWithValue("@email", campoEmail.Text);
                        cmd.Parameters.AddWithValue("@obs", campoObs.Text);

                        // Verificação de situação
                        if (ckSituacao.Checked == true)
                        {
                            cmd.Parameters.AddWithValue("@situacao", "Ativo");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@situacao", "Cancelado");
                        }

                        // Executa a inserção e recupera o ID do registro inserido
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "SELECT @@IDENTITY";
                        campoCodigo.Text = cmd.ExecuteScalar().ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro fatal: " + ex.Message);
            }
        }

        private void btSalvar_Click(object sender, EventArgs e)
        {
            if (Validacoes() == true)
            {
                return;
            }
            SalvarClientes();
        }

        private bool Validacoes()
        {
            if (campoNome.Text == "")
            {
                MessageBox.Show("Campo nome é obrigatório");
                campoNome.Focus();
                return true;
            }
            if (opCPF.Checked == false && opCNPJ.Checked == false)
            {
                MessageBox.Show("Marque o tipo de documentação\nCPF ou CNPJ");
                return true;
            }
            if (opM.Checked == false && opF.Checked == false && opOutros.Checked == false)
            {
                MessageBox.Show("Marque o tipo de gênero.");
                return true;
            }
            if (campoDocumento.Text == "")
            {
                MessageBox.Show("Campo documento é obrigatório");
                campoDocumento.Focus();
                return true;
            }
            if (campoRG.Text == "")
            {
                MessageBox.Show("Campo RG é obrigatório");
                campoRG.Focus();
                return true;
            }
            return false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (Validacoes() == true)
            {
                return;
            }
            SalvarClientes();
        }
    }
}

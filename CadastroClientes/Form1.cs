using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Globalization;

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
            campoRG.Focus();
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

                        cmd.Parameters.AddWithValue("@cep", campoCEP.Text);
                        cmd.Parameters.AddWithValue("@endereco", campoEndereco.Text);
                        cmd.Parameters.AddWithValue("@numero", campoNumero.Text);
                        cmd.Parameters.AddWithValue("@bairro", campoBairro.Text);
                        cmd.Parameters.AddWithValue("@cidade", campoCidade.Text);
                        cmd.Parameters.AddWithValue("@estado", campoEstado.Text);
                        cmd.Parameters.AddWithValue("@celular", campoCelular.Text);
                        cmd.Parameters.AddWithValue("@email", campoEmail.Text);
                        cmd.Parameters.AddWithValue("@obs", campoObs.Text);

                        if (ckSituacao.Checked == true)
                        {
                            cmd.Parameters.AddWithValue("@situacao", "Ativo");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@situacao", "Cancelado");
                        }

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

        private void button4_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Deseja limpar todos os campos?", "Y/N", MessageBoxButtons.YesNo ) == DialogResult.No)
            {
                return;
            }
            campoCEP.Text = "";
            campoEndereco.Text = "";
            campoNumero.Text = "";
            campoBairro.Text = "";
            campoCidade.Text = "";
            campoEstado.Text = "";
            campoCelular.Text = "";
            campoEmail.Text = "";
            campoObs.Text = "";
            campoNome.Text = "";
            campoNasc.Text = "";
            cbEstadoCivil.Text = "";
            campoRG.Text = "";
            campoDocumento.Text = "";
            opCNPJ.Checked = false;
            opCPF.Checked = false;
            opM.Checked = false;
            opF.Checked = false;
            opM.Checked = false;
            ckSituacao.Checked = true;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void opCPF_CheckedChanged(object sender, EventArgs e)
        {
            if (opCPF.Checked == true)
            {
                campoDocumento.Mask = "000,000,000-00";
                campoDocumento.Focus();
            }
            if (opCNPJ.Checked == true)
            {
                campoDocumento.Mask = "00,000,000/0000-00";
                campoDocumento.Focus();
            }
        }

        private void opM_CheckedChanged(object sender, EventArgs e)
        {
            campoRG.Focus();
        }

        private void opF_CheckedChanged(object sender, EventArgs e)
        {
            campoRG.Focus();
        }

        private void campoNasc_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if ( campoNasc.Text == "  /  /")
            {
                return;
            }
            try
            {
                campoNasc.Text = Convert.ToDateTime(campoNasc.Text).ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Data inválida");
                e.Cancel = true;                 
            }

        }

        private void cbEstadoCivil_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(cbEstadoCivil.Text == "")
            {
                return;
            }
            if (cbEstadoCivil.SelectedIndex == -1)
            {
                MessageBox.Show("Selecione um estado civil válido");
                e.Cancel = true;
            }
        }

        private void campoEstado_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (campoEstado.Text == "")
            {
                return;
            }
            if (campoEstado.SelectedIndex == -1)
            {
                MessageBox.Show("Selecione um estado válido");
                e.Cancel = true;
            }
        }

        private void campoNome_TextChanged(object sender, EventArgs e)
        {
            string t = campoNome.Text;

            TextInfo textInfo = new CultureInfo("pt-br", false).TextInfo;

            t = textInfo.ToTitleCase(t);

            t = t.Replace(" Das ", " das ")
                 .Replace(" Dos ", " dos ")
                 .Replace(" Da ", " da ")
                 .Replace(" Do ", " do ")
                 .Replace(" De ", " de ");

            campoNome.Text = t;

            campoNome.SelectionStart = campoNome.TextLength;
        }

        private void campoEstado_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void campoCEP_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (campoCEP.Text.Length == 0 )
            {
                return;
            }
            if (campoCEP.Text.Replace(" ", "").Length < 8 )
            {
                MessageBox.Show("CEP incompleto!");
                e.Cancel = true;
            }
        }

        private void campoDocumento_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void campoDocumento_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (campoDocumento.Text.Length == 0)
            {
                return;
            }
            if (opCPF.Checked == true && campoDocumento.Text.Replace(" ","").Length < 11 )
            {
                MessageBox.Show("CPF incompleto!");
                e.Cancel = true;
            }
            if (opCNPJ.Checked == true && campoDocumento.Text.Replace(" ", "").Length < 14)
            {
                MessageBox.Show("CNPJ incompleto!");
                e.Cancel = true;
            }
        }
    }
}

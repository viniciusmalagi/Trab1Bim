using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Trabalho1Bim
{
    public partial class Cadastro : Form
    {
        public Cadastro()
        {
            InitializeComponent();
           


        }

        SqlConnection sqlConn = null;
        private string strConn = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=TrabalhoBim;Data Source=LAPTOP-FCIGCC9H";
        private string strSql = string.Empty;
        private void Cadastro_Load(object sender, EventArgs e)
        {

        }

        private void btn_Incluir_Click(object sender, EventArgs e)
        {

            strSql = "insert into cadastroTab (Nome,Telefone,Cpf) values(@Nome,@Telefone,@Cpf)";

            sqlConn = new SqlConnection(strConn);

            SqlCommand comando = new SqlCommand(strSql, sqlConn);

            comando.Parameters.Add("@Nome", SqlDbType.VarChar).Value = txt_Nome.Text;
            comando.Parameters.Add("@Telefone", SqlDbType.VarChar).Value = msk_fone.Text;
            comando.Parameters.Add("@Cpf", SqlDbType.VarChar).Value = msk_cpf.Text;
            try
            {
                sqlConn.Open();
                comando.ExecuteNonQuery();

                MessageBox.Show("Cadastro concluído!");
            }
            catch (Exception ex)
            {
                ;
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlConn.Close();
            }

            txt_Nome.Clear();
            msk_fone.Clear();
            msk_cpf.Clear();




        }

        private void btn_pesquisar_Click(object sender, EventArgs e)
        {
            strSql ="select*from cadastroTab where Nome=@pesquisar";

            sqlConn = new SqlConnection(strConn);


            SqlCommand comando = new SqlCommand(strSql, sqlConn);

            comando.Parameters.Add("@pesquisar", SqlDbType.VarChar).Value = txt_pesquisar.Text;

            try
            {
                if (txt_pesquisar.Text == string.Empty)
                {
                    MessageBox.Show("Por favor, digite um nome.");
                }
                    sqlConn.Open();

                    SqlDataReader dr = comando.ExecuteReader();

                    if (dr.HasRows == false)
                    {
                        throw new Exception("Este nome não está cadastrado!");
                    }
                    dr.Read();

                    txt_Nome.Text = Convert.ToString(dr["Nome"]);
                    msk_fone.Text = Convert.ToString(dr["Telefone"]);
                    msk_cpf.Text = Convert.ToString(dr["Cpf"]);

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlConn.Close();
            }
            txt_pesquisar.Clear();
            btn_Excluir.Enabled = true;
        }

        private void txt_Nome_TextChanged(object sender, EventArgs e)
        {
            btn_Alterar.Enabled = true;
            btn_Excluir.Enabled = true;
            btn_Incluir.Enabled = true;
        }

        private void btn_Alterar_Click(object sender, EventArgs e)
        {
            strSql = "update cadastroTab set Nome=@Nome,Telefone=@Telefone,Cpf=@Cpf";

            sqlConn = new SqlConnection(strConn);

            SqlCommand comando = new SqlCommand(strSql, sqlConn);

            comando.Parameters.Add("@Nome", SqlDbType.VarChar).Value = txt_Nome.Text;
            comando.Parameters.Add("@Telefone", SqlDbType.VarChar).Value = msk_fone.Text;
            comando.Parameters.Add("@Cpf", SqlDbType.VarChar).Value = msk_cpf.Text;

            try
            {
                sqlConn.Open();
                comando.ExecuteNonQuery();
                MessageBox.Show("Cadastro alterado!");


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                sqlConn.Close();


            }

            txt_Nome.Clear();
            msk_fone.Clear();
            msk_cpf.Clear();
        }

        private void btn_Excluir_Click(object sender, EventArgs e)
        {
            strSql = "delete from cadastroTab where Nome=@Nome";

            sqlConn = new SqlConnection(strConn);

            SqlCommand comando = new SqlCommand(strSql, sqlConn);

            comando.Parameters.Add("@Nome", SqlDbType.VarChar).Value = txt_Nome.Text;

            try
            {
                sqlConn.Open();
                comando.ExecuteNonQuery();
                MessageBox.Show("Cadastro excluido!");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlConn.Close();
            }
            txt_Nome.Clear();
            msk_fone.Clear();
            msk_cpf.Clear();
        }

        private void btn_Sair_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(" Deseja sair de Cadastro? ", "Mensage do sistema ",

                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

            {

                Close();
            }
        }

        private void txt_pesquisar_TextChanged(object sender, EventArgs e)
        {
            btn_Alterar.Enabled = false;
            btn_Excluir.Enabled = false;
            btn_Incluir.Enabled = false;
            

        }

        private void msk_fone_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            btn_Alterar.Enabled = true;
            btn_Excluir.Enabled = true;
            btn_Incluir.Enabled = true;
        }

        private void msk_cpf_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            btn_Alterar.Enabled = true;
            btn_Excluir.Enabled = true;
            btn_Incluir.Enabled = true;
        }
    }
}











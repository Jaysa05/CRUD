using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MySql.Data.MySqlClient;

namespace GravarDadosMySQL
{
    public partial class Form1 : Form
    {
        private MySqlConnection Conexao;
        private string data_source = "datasource=localhost;username=root;password=;database=db_agenda";
        public Form1()
        {
            InitializeComponent();

            lst_contatos.View = View.Details;
            lst_contatos.LabelEdit = true;
            lst_contatos.AllowColumnReorder = true;
            lst_contatos.FullRowSelect = true;
            lst_contatos.GridLines = true;

            lst_contatos.Columns.Add("ID", 30, HorizontalAlignment.Left);
            lst_contatos.Columns.Add("Nome", 150, HorizontalAlignment.Left);
            lst_contatos.Columns.Add("Email", 150, HorizontalAlignment.Left);
            lst_contatos.Columns.Add("Telefone", 150, HorizontalAlignment.Left);

            carregar_contatos();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Criar conexão com o MySQL 
                Conexao = new MySqlConnection(data_source);
                Conexao.Open();

                // 2. Cria o comando associado à conexão aberta
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Conexao;

                cmd.CommandText = "INSERT INTO contato (nome, email, telefone)" +
                                  "VALUES" + "(@nome, @email, @telefone)";


                cmd.Parameters.AddWithValue("@nome", txtNome.Text);
                cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@telefone", txtTelefone.Text);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Contato cadastrado com sucesso!",
                                "Sucesso", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                txtNome.Text = "";
                txtEmail.Text = "";
                txtTelefone.Text = "";

            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Erro" + ex.Number + "ocorreu" + ex.Message,
                                "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu" + ex.Message,
                                "Erro", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }


            finally
            {
                Conexao.Close();
            }

            carregar_contatos();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Conexao = new MySqlConnection(data_source);

                Conexao.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Conexao;

                cmd.CommandText = "SELECT* FROM contato WHERE nome LIKE @q OR email LIKE @q";

                cmd.Parameters.AddWithValue("@q", "%" + txt_bucar.Text + "%");


                MySqlDataReader reader = cmd.ExecuteReader();

                lst_contatos.Items.Clear();

                while (reader.Read())
                {
                    string[] row =
                    {
                        //SE DER ERRADO É PQ ID É INT "reader.GetInt32(0).ToString();"  
                        reader.GetInt32(0).ToString(),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetString(3),
                };

                    var linha_listview =

                    lst_contatos.Items.Add(new ListViewItem(row));
                }




            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Erro" + ex.Number + "Ocorreu" + ex.Message,
                                 "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu" + ex.Message,
                                 "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            finally
            {
                Conexao.Close();
            }
        }

        private void carregar_contatos()
        {
            try
            {
                Conexao = new MySqlConnection(data_source);

                Conexao.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Conexao;

                cmd.CommandText = "SELECT* FROM contato ORDER BY id DESC";


                MySqlDataReader reader = cmd.ExecuteReader();

                lst_contatos.Items.Clear();

                while (reader.Read())
                {
                    string[] row =
                    {
                        //SE DER ERRADO É PQ ID É INT "reader.GetInt32(0).ToString();"  
                        reader.GetInt32(0).ToString(),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetString(3),
                };
                    lst_contatos.Items.Add(new ListViewItem(row));
                }

            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Erro" + ex.Number + "Ocorreu" + ex.Message,
                                 "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu" + ex.Message,
                                 "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            finally
            {
                Conexao.Close();
            }
        }



    }


}

    





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
            lst_contatos.Columns.Add("Email",150, HorizontalAlignment.Left);
            lst_contatos.Columns.Add("Telefone",150, HorizontalAlignment.Left);
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

                string sql = "INSERT INTO contato (nome, email, telefone)" +
                    "VALUES ('" + txtNome.Text + "' , '" + txtEmail.Text + "', '" + txtTelefone.Text + "')";
                //Executar comando Insert
                MySqlCommand comando = new MySqlCommand(sql, Conexao);

                Conexao.Open();

                comando.ExecuteReader();

                MessageBox.Show("Dado inserido com sucesso");


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Conexao.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string q = "'%" + txt_bucar.Text + "%'";

                Conexao = new MySqlConnection(data_source);

                string sql = "SELECT * " + "FROM contato " +
                    "WHERE nome LIKE " + q + "OR email LIKE " + q;

                Conexao.Open();

                MySqlCommand comando = new MySqlCommand(sql, Conexao);

                MySqlDataReader reader = comando.ExecuteReader();

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

                    var linha_listview = new ListViewItem(row);

                    lst_contatos.Items.Add(linha_listview);
            }
            



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Conexao.Close();
            }
        }
    }
}

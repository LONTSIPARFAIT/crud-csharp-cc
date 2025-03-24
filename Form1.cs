using System.Data;
using MySqlConnector;

namespace Crud_CC
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectString = "Server=localhost;User ID=root;Password='';Database=gestion_projet_cc;AllowZeroDatetime=true;";
            try
            {
                using (MySqlConnection connect = new MySqlConnection(connectString))
                {
                    connect.Open();

                    // Pr�parez la commande SQL pour l'insertion
                    MySqlCommand command = new MySqlCommand("INSERT INTO projets (appellation_projet, theme_projet, date_debut_projet) VALUES (@appellation_projet, @theme_projet, @date_debut_projet)", connect);

                    // Ajoutez les param�tres � la commande
                    //command.Parameters.AddWithValue("@id_projet", int.Parse(textBox1.Text));
                    command.Parameters.AddWithValue("@appellation_projet", textBox1.Text);
                    command.Parameters.AddWithValue("@theme_projet", textBox2.Text);
                    command.Parameters.AddWithValue("@date_debut_projet", dateTimePicker1.Value);

                    // Ex�cutez la commande
                    command.ExecuteNonQuery();

                    // Optionnel : Afficher un message de succ�s
                    MessageBox.Show("projet ajout� avec succ�s.");

                    //LoadData();
                }
            }
            catch (Exception ex)
            {
                // G�rer les erreurs
                MessageBox.Show("Une erreur est survenue : " + ex.Message);
            }
        }

        //private void LoadData()
        //{
        //    string connectString = "Server=localhost;User ID=root;Password='';Database=gestion_projet_cc;AllowZeroDatetime=true;";
        //    try
        //    {
        //        using (MySqlConnection connect = new MySqlConnection(connectString))
        //        {
        //            connect.Open();

        //            // Pr�parez la commande SQL pour r�cup�rer les donn�es
        //            MySqlCommand command = new MySqlCommand("SELECT * FROM projets", connect);
        //            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
        //            DataTable dataTable = new DataTable();

        //            // Remplissez le DataTable
        //            adapter.Fill(dataTable);

        //            // Assignez le DataTable au DataGridView
        //            dataGridView1.DataSource = dataTable;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // G�rer les erreurs
        //        MessageBox.Show("Erreur lors du chargement des donn�es : " + ex.Message);
        //    }
        //}

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            {
                textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                dateTimePicker1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string connectString = "Server=localhost;User ID=root;Password='';Database=crud-cshap;AllowZeroDatetime=true;";
            try
            {
                using (MySqlConnection connect = new MySqlConnection(connectString))
                {
                    connect.Open();

                    // Pr�parez la commande SQL pour la suppression
                    MySqlCommand command = new MySqlCommand("DELETE FROM users WHERE id = @id", connect);

                    // Ajoutez le param�tre � la commande
                    command.Parameters.AddWithValue("@id", int.Parse(textBox1.Text)); // ID depuis textBox1

                    // Ex�cutez la commande
                    int rowsAffected = command.ExecuteNonQuery();

                    // V�rifiez si une ligne a �t� supprim�e
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Utilisateur supprim� avec succ�s.");
                    }
                    else
                    {
                        MessageBox.Show("Aucun utilisateur trouv� avec cet ID.");
                    }
                }
            }
            catch (Exception ex)
            {
                // G�rer les erreurs
                MessageBox.Show("Une erreur est survenue : " + ex.Message);
            }
        }
    }
}

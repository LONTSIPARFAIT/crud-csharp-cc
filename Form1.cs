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

                    // Préparez la commande SQL pour l'insertion
                    MySqlCommand command = new MySqlCommand("INSERT INTO projets (appellation_projet, theme_projet, date_debut_projet) VALUES (@appellation_projet, @theme_projet, @date_debut_projet)", connect);

                    // Ajoutez les paramètres à la commande
                    //command.Parameters.AddWithValue("@id_projet", int.Parse(textBox1.Text));
                    command.Parameters.AddWithValue("@appellation_projet", textBox1.Text);
                    command.Parameters.AddWithValue("@theme_projet", textBox2.Text);
                    command.Parameters.AddWithValue("@date_debut_projet", dateTimePicker1.Value);

                    // Exécutez la commande
                    command.ExecuteNonQuery();

                    // Optionnel : Afficher un message de succès
                    MessageBox.Show("projet ajouté avec succès.");

                    //LoadData();
                }
            }
            catch (Exception ex)
            {
                // Gérer les erreurs
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

        //            // Préparez la commande SQL pour récupérer les données
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
        //        // Gérer les erreurs
        //        MessageBox.Show("Erreur lors du chargement des données : " + ex.Message);
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

                    // Préparez la commande SQL pour la suppression
                    MySqlCommand command = new MySqlCommand("DELETE FROM users WHERE id = @id", connect);

                    // Ajoutez le paramètre à la commande
                    command.Parameters.AddWithValue("@id", int.Parse(textBox1.Text)); // ID depuis textBox1

                    // Exécutez la commande
                    int rowsAffected = command.ExecuteNonQuery();

                    // Vérifiez si une ligne a été supprimée
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Utilisateur supprimé avec succès.");
                    }
                    else
                    {
                        MessageBox.Show("Aucun utilisateur trouvé avec cet ID.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Gérer les erreurs
                MessageBox.Show("Une erreur est survenue : " + ex.Message);
            }
        }
    }
}

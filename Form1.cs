using System.Data;
using MySqlConnector;

namespace Crud_CC
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Veuillez remplir tous les champs.");
                return;
            }

            string connectString = "Server=localhost;User ID=root;Password='';Database=gestion_projet_cc;AllowZeroDatetime=true;";
            try
            {
                using (MySqlConnection connect = new MySqlConnection(connectString))
                {
                    connect.Open();

                    // Insertion dans la base de données
                    MySqlCommand command = new MySqlCommand(
                        "INSERT INTO projets (appellation_projet, theme_projet, date_debut_projet) " +
                        "VALUES (@appellation_projet, @theme_projet, @date_debut_projet)", connect);

                    command.Parameters.AddWithValue("@appellation_projet", textBox1.Text);
                    command.Parameters.AddWithValue("@theme_projet", textBox2.Text);
                    command.Parameters.AddWithValue("@date_debut_projet", dateTimePicker1.Value);

                    command.ExecuteNonQuery();

                    // Récupérer l'ID généré par la base (si id_projet est auto-incrémenté)
                    command.CommandText = "SELECT LAST_INSERT_ID()";
                    int newId = Convert.ToInt32(command.ExecuteScalar());

                    // Ajouter la nouvelle ligne directement dans le DataGridView
                    dataGridView1.Rows.Add(newId, textBox1.Text, textBox2.Text, dateTimePicker1.Value);

                    MessageBox.Show("Projet ajouté avec succès.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de l'ajout : " + ex.Message);
            }
            LoadData();
        }

        private void LoadData()
        {
            string connectString = "Server=localhost;User ID=root;Password='';Database=gestion_projet_cc;AllowZeroDatetime=true;";
            try
            {
                using (MySqlConnection connect = new MySqlConnection(connectString))
                {
                    connect.Open();
                    MySqlCommand command = new MySqlCommand("SELECT * FROM projets", connect);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        dataGridView1.Rows.Clear(); // Effacer les lignes actuelles pour éviter les doublons
                        while (reader.Read())
                        {
                            dataGridView1.Rows.Add(
                                reader["id_projet"],
                                reader["appellation_projet"],
                                reader["theme_projet"],
                                reader["date_debut_projet"]
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors du chargement des données : " + ex.Message);
            }
        }

        private void dataGridView1_CellContentClick(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                try
                {
                    textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value?.ToString() ?? ""; // appellation_projet
                    textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value?.ToString() ?? ""; // theme_projet
                    dateTimePicker1.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[3].Value); // date_debut_projet
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erreur lors de la sélection : " + ex.Message);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Veuillez sélectionner un projet à modifier.");
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Veuillez remplir tous les champs.");
                return;
            }

            string connectString = "Server=localhost;User ID=root;Password='';Database=gestion_projet_cc;AllowZeroDatetime=true;";
            try
            {
                using (MySqlConnection connect = new MySqlConnection(connectString))
                {
                    connect.Open();

                    // Requête SQL pour mettre à jour
                    string query = "UPDATE projets SET appellation_projet = @appellation_projet, " +
                                  "theme_projet = @theme_projet, date_debut_projet = @date_debut_projet " +
                                  "WHERE id_projet = @id_projet";
                    MySqlCommand command = new MySqlCommand(query, connect);

                    // Récupérer l'ID depuis la ligne sélectionnée
                    int idProjet = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);

                    // Ajouter les paramètres
                    command.Parameters.AddWithValue("@id_projet", idProjet);
                    command.Parameters.AddWithValue("@appellation_projet", textBox1.Text);
                    command.Parameters.AddWithValue("@theme_projet", textBox2.Text);
                    command.Parameters.AddWithValue("@date_debut_projet", dateTimePicker1.Value);

                    // Exécuter la mise à jour
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        // Mise à jour réussie dans la base, mettre à jour le DataGridView
                        dataGridView1.CurrentRow.Cells[1].Value = textBox1.Text; // appellation_projet
                        dataGridView1.CurrentRow.Cells[2].Value = textBox2.Text; // theme_projet
                        dataGridView1.CurrentRow.Cells[3].Value = dateTimePicker1.Value; // date_debut_projet

                        MessageBox.Show("Projet modifié avec succès.");
                    }
                    else
                    {
                        MessageBox.Show("Aucune modification effectuée. Vérifiez l'ID du projet.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la mise à jour : " + ex.Message);
            }
        }

        // Supprimer (button3_Click)
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Veuillez sélectionner un projet à supprimer.");
                return;
            }

            if (MessageBox.Show("Voulez-vous vraiment supprimer ce projet ?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }

            string connectString = "Server=localhost;User ID=root;Password='';Database=gestion_projet_cc;AllowZeroDatetime=true;";
            try
            {
                using (MySqlConnection connect = new MySqlConnection(connectString))
                {
                    connect.Open();
                    string query = "DELETE FROM projets WHERE id_projet = @id_projet";
                    MySqlCommand command = new MySqlCommand(query, connect);

                    int idProjet = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                    command.Parameters.AddWithValue("@id_projet", idProjet);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
                        MessageBox.Show("Projet supprimé avec succès.");
                    }
                    else
                    {
                        MessageBox.Show("Aucune suppression effectuée.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la suppression : " + ex.Message);
            }
        }

    }
}

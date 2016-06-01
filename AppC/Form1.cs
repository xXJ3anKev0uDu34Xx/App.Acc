using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppC
{
    public partial class Form1 : Form
    {
        /// système de connexion
        System.Data.OleDb.OleDbConnection conn = new System.Data.OleDb.OleDbConnection();
        string Connexion = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=..\..\Plumier_data.accdb;Persist Security Info=False;";

        public Form1()
        {
            InitializeComponent();
            conn.ConnectionString = Connexion;
            conn.Open();
        }

        /// L'évenèment faisant appel à la fonction ReadData en lui passant en paramètre les infos sur la connexion. 
        public void btxgen_Click(object sender, EventArgs e)
        {
            string maCommande = "SELECT art_nom, art_reference, art_prix, art_qte_stock, art_seuil_critique FROM T_article";
            ReadData(Connexion, maCommande);
        }

        public void vider()
        {
            tbxArticle.Clear();
            tbxReference.Clear();
            tbxPrix.Clear();
            tbxStock.Clear();
            tbxSeuil.Clear();
        }

        /// Cette fonction reçois en paramètre la connexion et la requête sql
        public void ReadData(string maConnexion, string query)
        {
            OleDbCommand cmd = new OleDbCommand(query, conn);
            OleDbDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                tbxArticle.Text = reader["art_nom"].ToString();
                tbxReference.Text = reader["art_reference"].ToString();
                tbxPrix.Text = reader["art_prix"].ToString();
                tbxStock.Text = reader["art_qte_stock"].ToString();
                tbxSeuil.Text = reader["art_seuil_critique"].ToString();
            }
            reader.Close();
        }

        public void btnAjout_Click(object sender, EventArgs e)
        {
            try
            {
                // on recupère la saisie de l'utilisateur
                string strNomArt = tbxArticle.Text;
                string strRef = tbxReference.Text;
                double dblPrix = Convert.ToDouble(tbxPrix.Text);
                double dblStock = Convert.ToDouble(tbxStock.Text);
                double dblSeuil = Convert.ToDouble(tbxSeuil.Text);

                // On test si les champs de saisies ne sont pas null
                if (strNomArt != null && strRef != null)
                {
                    // Si le test est valide on vide le textbox
                    vider();

                    try
                    {
                        string strRq = "INSERT INTO T_article(art_nom,art_reference,art_prix,art_qte_stock,art_seuil_critique)VALUES('" + strNomArt + "','" + strRef + "'," + dblPrix + "," + dblStock + "," + dblSeuil + ")";
                        OleDbCommand cmd = new OleDbCommand(strRq, conn);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Enregistré");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erreur lors de l'enregistrement " + ex.Message);
                    }
                }
            }
            catch (Exception em)
            {
                MessageBox.Show(em.Message);
            }
        }

        private void btnvider_Click(object sender, EventArgs e)
        {
            vider();
        }

        private void btnSupprimer_Click(object sender, EventArgs e)
        {
            string table = "T_article";
            string column = "art_reference";

            try
            {
                string strRq = "DELETE FROM " + table + " WHERE " + column + " = " + "'" + tbxReference.Text + "'";
                OleDbCommand cmd = new OleDbCommand(strRq, conn);
                cmd.ExecuteNonQuery();
                vider();
                MessageBox.Show("supprimé");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la suppression " + ex.Message);
            }
        }

        private void btnModif_Click(object sender, EventArgs e)
        {
            try
            {
                string strRq = "Update T_article set  art_nom='" + tbxArticle.Text + "', art_prix = '" + tbxPrix.Text + "', art_qte_stock = '" + tbxStock.Text + "', art_seuil_critique = '" + tbxSeuil.Text + "' WHERE art_reference= '" + tbxReference.Text + "'";
                OleDbCommand cmd = new OleDbCommand(strRq, conn);
                cmd.ExecuteNonQuery();
                vider();
                MessageBox.Show("Table mise à jour");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.WindowsShutDown) return;

            // Confirmation de la fermeture par l'utilisateur
            switch (MessageBox.Show(this, "Souhaitez vraiment fermer le programme ?", "Fermeture", MessageBoxButtons.YesNo))
            {
                case DialogResult.No:
                    e.Cancel = true;
                    break;
                case DialogResult.Yes:
                    conn.Close();
                    break;
                default:
                    break;
            }
        }
    }
}

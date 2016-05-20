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
        public Form1()
        {
            InitializeComponent();
           
        }

        

        /// <summary>
        /// L'évenèment faisant appel à la fonction ReadData en lui passant en paramètre les infos sur la connexion. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btxgen_Click(object sender, EventArgs e)
        {
            string Connexion = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=..\..\Plumier_data.accdb;Persist Security Info=False;";
            string maCommande = "SELECT art_nom, art_reference, art_prix, art_qte_stock, art_seuil_critique FROM T_article";
           
            ReadData(Connexion, maCommande);
        }

        /// <summary>
        /// Cette fonction reçois en paramètre la connexion et la requête sql
        /// </summary>
        /// <param name="maConnexion"></param>
        /// <param name="query"></param>
        public void ReadData(string maConnexion, string query)
        {
            using (OleDbConnection connexion = new OleDbConnection(maConnexion))
            {
              
                OleDbCommand command = new OleDbCommand(query,connexion);
                connexion.Open();

                OleDbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    tbxArticle.Text = reader["art_nom"].ToString();
                    tbxReference.Text = reader["art_reference"].ToString();
                    tbxPrix.Text = reader["art_prix"] + " CHF".ToString();
                    tbxStock.Text = reader["art_qte_stock"].ToString();
                    tbxSeuil.Text = reader["art_seuil_critique"].ToString();
                }

                reader.Close();
                connexion.Close();
            }
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
                    tbxArticle.Text = "";
                    tbxReference.Text = "";
                    tbxPrix.Text = "";
                    tbxStock.Text = "";
                    tbxSeuil.Text = "";

                    System.Data.OleDb.OleDbConnection conn = new System.Data.OleDb.OleDbConnection();
                    conn.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=..\..\Plumier_data.accdb;Persist Security Info=False;";

                    try
                    {
                        conn.Open();
                        string strReq = "INSERT INTO T_article(art_nom,art_reference,art_prix,art_qte_stock,art_seuil_critique)VALUES('" + strNomArt + "','" + strRef + "'," + dblPrix + "," + dblStock + "," + dblSeuil + ")";
                        //string strReq = "INSERT INTO T_article(art_nom,art_reference,art_prix,art_qte_stock,art_seuil_critique)VALUES('aaaa', '99999', 30,10,5)";

                        OleDbCommand cmd = new OleDbCommand(strReq, conn);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Enregistré !!!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erreur lors de l'enregistrement " + ex.Message);
                    }
                    finally {

                        conn.Close();       
                    }
                }
            }
            catch (Exception)
            {    
                throw;
            }
        }

        private void btnvider_Click(object sender, EventArgs e)
        {
            tbxArticle.Text = "";
            tbxReference.Text = "";
            tbxPrix.Text = "";
            tbxStock.Text = "";
            tbxSeuil.Text = "";
        }
    }
}

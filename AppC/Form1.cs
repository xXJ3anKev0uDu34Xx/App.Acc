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

        private void btxgen_Click(object sender, EventArgs e)
        {
            string ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\mboyoiv\Desktop\AtelierBouchard\Application\AppC\Plumier_data.accdb;Persist Security Info=False;";
            OleDbConnection maConnexion = new OleDbConnection(ConnectionString);
            maConnexion.Open();

            System.Data.OleDb.OleDbCommand maCommande = new System.Data.OleDb.OleDbCommand();
            maCommande.CommandText = "SELECT art_nom, art_reference, art_prix, art_qte_stock, art_seuil_critique FROM T_article";
            OleDbDataReader monReader = maCommande.ExecuteReader();

            // test si il y a du contenu dans les table plus affichage dans les textbox
            while (monReader.Read())
            {
                tbxArticle.Text = monReader["art_nom"].ToString();
                tbxReference.Text = monReader["art_reference"].ToString();
                tbxPrix.Text = monReader["art_prix"].ToString();
                tbxStock.Text = monReader["art_qte_stock"].ToString();
                tbxSeuil.Text = monReader["art_seuil_critique"].ToString();
            }

            monReader.Close();
            maConnexion.Close();
        }
    }
}

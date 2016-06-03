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
            string Connexion = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source= ..\..\Plumier_data.accdb;Persist Security Info=False;";
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

            OleDbConnection connexion = new OleDbConnection(maConnexion);
            connexion.Open();

            OleDbCommand command = new OleDbCommand(query, connexion);
            OleDbDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
            //tbxArticle.Text = reader["SELECT art_reference from T_article"].ToString();
                   
                tbxReference.Text = reader["art_reference"].ToString();
                tbxPrix.Text = reader["art_prix"] + " CHF".ToString();
                tbxStock.Text = reader["art_qte_stock"].ToString();
                tbxSeuil.Text = reader["art_seuil_critique"].ToString();
                // SqlDataAdapter adapter = new SqlDataAdapter();
            }   
            reader.Close();
            
        }

        private void btnSuivant_Click(object sender, EventArgs e)
        {
            
        }

     
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project1
{
    public partial class Form1 : Form
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataReader reader;
        string conStr;
        string sql;
        int eId;
        string eName, eAgency, eDebut, ePrize, eSite, eImageFileName;

        private void btn_save_Click(object sender, EventArgs e)
        {
            eName = tb_name.Text;
            eAgency = tb_agency.Text;
            eDebut = tb_debut.Text;
            ePrize = tb_prize.Text;
            eSite = tb_site.Text;
            eImageFileName = tb_image.Text;

            FileStream fs = new FileStream(eImageFileName, FileMode.Open, FileAccess.Read);

            byte[] bImage = new byte[fs.Length];
            fs.Read(bImage, 0, (int)fs.Length);

            sql = "INSERT INTO enterTable (name, agency, debut, prize, site, imagefilename, image) values ('"
                + eName + "', '" + eAgency + "', '" + eDebut + "', '" + ePrize + "', '" + eSite + "', '" + eImageFileName + "', @photo);";

            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@photo", bImage);
            cmd.ExecuteNonQuery();

            fs.Close();

            listView1.Items.Clear();

            sql = "SELECT * FROM enterTable;";
            cmd.CommandText = sql;
            reader = cmd.ExecuteReader();

            ListViewItem item;

            while (reader.Read())
            {
                eId = reader.GetInt32(0);
                eName = reader.GetString(1);
                eAgency = reader.GetString(2);
                eDebut = reader.GetString(3);
                ePrize = reader.GetString(4);
                eSite = reader.GetString(5);
                eImageFileName = reader.GetString(6);

                item = new ListViewItem(eId.ToString());
                item.SubItems.Add(eName);
                item.SubItems.Add(eAgency);
                item.SubItems.Add(eDebut);
                item.SubItems.Add(ePrize);
                item.SubItems.Add(eSite);
                item.SubItems.Add(eImageFileName);

                listView1.Items.Add(item);
            }
            reader.Close();
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void btn_image_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.ShowDialog();

            pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
            tb_image.Text = openFileDialog1.FileName;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            conStr = "Server=localhost;Database=inhaDB;Trusted_Connection=True;";
            conn = new SqlConnection(conStr);
            conn.Open();

            cmd = new SqlCommand();
            cmd.Connection = conn;
            sql = "SELECT * FROM enterTable;";
            cmd.CommandText = sql;
            reader = cmd.ExecuteReader();

            ListViewItem item;

            while(reader.Read())
            {
                eId = reader.GetInt32(0);
                eName = reader.GetString(1);
                eAgency = reader.GetString(2);
                eDebut = reader.GetString(3);
                ePrize = reader.GetString(4);
                eSite = reader.GetString(5);
                eImageFileName = reader.GetString(6);

                item = new ListViewItem(eId.ToString());
                item.SubItems.Add(eName);
                item.SubItems.Add(eAgency);
                item.SubItems.Add(eDebut);
                item.SubItems.Add(ePrize);
                item.SubItems.Add(eSite);
                item.SubItems.Add(eImageFileName);

                listView1.Items.Add(item);
            }
            reader.Close();
        }
    }
}

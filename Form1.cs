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
        Image eImage;

        private void listView1_Click(object sender, EventArgs e)
        {
            eId = int.Parse(listView1.SelectedItems[0].Text);

            sql = "SELECT * FROM enterTable " + "WHERE id = " + eId + ";";

            cmd.CommandText = sql;
            reader = cmd.ExecuteReader();

            byte[] bImage = null;

            while(reader.Read())
            {
                eName = reader.GetString(1);
                eAgency = reader.GetString(2);
                eDebut = reader.GetString(3);
                ePrize = reader.GetString(4);
                eSite = reader.GetString(5);
                eImageFileName = reader.GetString(6);
                bImage = (byte[])reader[7];

                tb_name.Text = eName;
                tb_agency.Text = eAgency;
                tb_debut.Text = eDebut;
                tb_prize.Text = ePrize;
                tb_site.Text = eSite;
                tb_image.Text = eImageFileName;

                pictureBox1.Image = new Bitmap(new MemoryStream(bImage));
            }

            reader.Close();
            
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            eName = tb_name.Text;
            eAgency = tb_agency.Text;
            eDebut = tb_debut.Text;
            ePrize = tb_prize.Text;
            eSite = tb_site.Text;
            eImageFileName = tb_image.Text;
            eImage = pictureBox1.Image;

            FileStream fs = new FileStream(eImageFileName, FileMode.Open, FileAccess.Read);

            byte[] bImage = new byte[fs.Length];
            fs.Read(bImage, 0, (int)fs.Length);

            sql = "UPDATE enterTable SET " + 
                "name = '" + eName + 
                "', agency = '" + eAgency + 
                "', debut = '" + eDebut + 
                "', prize = '" + ePrize + 
                "', site = '" + eSite +
                "', imagefilename = '" + eImageFileName + 
                "', image = @image" + 
                " WHERE id = " + eId + ";";

            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@image", bImage);
            cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();

            MessageBox.Show("정보를 수정하였습니다.");

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

        private void btn_delete_Click(object sender, EventArgs e)
        {
            sql = "DELETE FROM enterTable WHERE id = " + eId + ";";
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();

            MessageBox.Show("정보를 삭제하였습니다.");

            listView1.Items.Clear();

            sql = "SELECT * FROM enterTable;";
            cmd.CommandText = sql;
            reader = cmd.ExecuteReader();

            tb_name.Text = "";
            tb_agency.Text = "";
            tb_debut.Text = "";
            tb_prize.Text = "";
            tb_site.Text = "";
            tb_image.Text = "";
            pictureBox1.Image = null;

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

        private void btn_clear_Click(object sender, EventArgs e)
        {
            tb_name.Text = "";
            tb_agency.Text = "";
            tb_debut.Text = "";
            tb_prize.Text = "";
            tb_site.Text = "";
            tb_image.Text = "";
            pictureBox1.Image = null;
        }

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
            cmd.Parameters.Clear();

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

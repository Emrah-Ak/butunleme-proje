using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace bütproje
{
    public partial class Form1 : Form
    {
        DataTable dt;

        public Form1()
        {
            InitializeComponent();
            dt = new DataTable();
            using (SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\cadd\\source\\repos\\bütproje\\bütproje\\mağaza.mdf;Integrated Security=True"))
            {
                using (SqlCommand query = new SqlCommand("select * from Ürünler", con))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(query))
                    {
                        da.Fill(dt);
                    }

                }

            }
            List<string> list = new List<string>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(row[0].ToString());
            }
            listBox1.DataSource = null;

        }




        






        SqlConnection baglantı = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\mağaza.mdf;Integrated Security = True");

        private void Form1_Load(object sender, EventArgs e)
        {
            Fill_Listbox();
            // TODO: Bu kod satırı 'mağazaDataSet.Müşteriler' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.müşterilerTableAdapter.Fill(this.mağazaDataSet.Müşteriler);

            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                DataGridViewCellStyle renk = new DataGridViewCellStyle();
                if (Convert.ToInt32(dataGridView1.Rows[i].Cells[2].Value) >= 0545)
                {
                    renk.BackColor = Color.Aqua;
                    renk.ForeColor = Color.AliceBlue;

                }

                else
                {
                    renk.BackColor = Color.Black;
                    renk.ForeColor = Color.Red;
                }

                dataGridView1.Rows[i].DefaultCellStyle = renk;

            }



        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            SqlCommand Ekle = new SqlCommand("Insert into Müşteriler  values('" + textBox5.Text + "','" + textBox6.Text + "','" + textBox7.Text + "','" + textBox8.Text + "')", baglantı);
            Ekle.ExecuteNonQuery();
            baglantı.Close();
            MessageBox.Show("kayıt başarılı");
            Form1_Load(null, null);
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            display_data();

        }
        public void display_data()

        {
            baglantı.Open();
            SqlCommand cmd = baglantı.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select * from Müşteriler";
            cmd.ExecuteNonQuery();
            DataTable dta = new DataTable();
            SqlDataAdapter dataadp = new SqlDataAdapter(cmd);
            dataadp.Fill(dta);
            dataGridView1.DataSource = dta;
            baglantı.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            display_data();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            SqlCommand Sil = baglantı.CreateCommand();
            Sil.CommandType = CommandType.Text;
            Sil.CommandText = "delete from Müşteriler where Adı='" + textBox5.Text + "'";
            Sil.ExecuteNonQuery();
            baglantı.Close();
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            display_data();
            MessageBox.Show("Başarılı");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            SqlCommand Güncelleme = baglantı.CreateCommand();
            Güncelleme.CommandType = CommandType.Text;
            Güncelleme.CommandText = "update  Müşteriler set Adı='" + textBox6.Text + "' where Adı='" + textBox5.Text + "'";
            Güncelleme.ExecuteNonQuery();
            baglantı.Close();
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            display_data();
            MessageBox.Show("Başarılı");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            SqlCommand Ara = baglantı.CreateCommand();
            Ara.CommandType = CommandType.Text;
            Ara.CommandText = "Select * from Müşteriler where Adı='" + textBox9.Text + "' ";
            Ara.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(Ara);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglantı.Close();
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";

        }







        SqlConnection baglan = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\mağaza.mdf;Integrated Security = True");


        private void button6_Click(object sender, EventArgs e)
        {
            baglan.Open();
            SqlCommand Ekle = baglan.CreateCommand();
            Ekle.CommandType = CommandType.Text;
            Ekle.CommandText = "Insert into Ürünler values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "')";
            Ekle.ExecuteNonQuery();
            baglan.Close();
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            Fill_Listbox();
            MessageBox.Show("Başarılı");
        }

        public void Fill_Listbox()
        {
            listBox1.Items.Clear();
            
            baglan.Open();
            SqlCommand Ekle = baglan.CreateCommand();
            Ekle.CommandType = CommandType.Text;
            Ekle.CommandText = "Select ÜrünID from Ürünler";
            Ekle.ExecuteNonQuery();
            DataTable Dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(Ekle);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                listBox1.Items.Add(dr["ÜrünID"].ToString());
            }
            baglan.Close();
        }


        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            baglan.Open();
            SqlCommand Ekle = baglan.CreateCommand();
            Ekle.CommandType = CommandType.Text;
            Ekle.CommandText = "Select * from Ürünler where ÜrünID='"+listBox1.SelectedItem.ToString()+"'";
            Ekle.ExecuteNonQuery();
            DataTable Dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(Ekle);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                textBox1.Text = dr["ÜrünID"].ToString();
                textBox2.Text = dr["ÜrünAd"].ToString();
                textBox3.Text = dr["Kategori"].ToString();
                textBox4.Text = dr["Fiyat"].ToString();
            }
            baglan.Close();

        }

        private void button7_Click(object sender, EventArgs e)
        {
            {
                baglan.Open();
                SqlCommand Sil = baglan.CreateCommand();
                Sil.CommandType = CommandType.Text;
                Sil.CommandText = "Delete from Ürünler where ÜrünAD='"+textBox2.Text+"'"; 
                Sil.ExecuteNonQuery();
                baglan.Close();
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                Fill_Listbox();
                MessageBox.Show("Başarılı");
            }
        }


    }
    }





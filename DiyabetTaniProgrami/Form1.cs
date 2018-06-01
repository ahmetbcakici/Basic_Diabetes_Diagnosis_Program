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

namespace DiyabetTaniProgrami
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            closeallpanels();//"closeallpanels" metotunu çalıştır
        }
        SqlConnection con = new SqlConnection(@"data source = DELL\SQLEXPRESS;initial catalog=diyabettani;integrated security=true");//sql veritabanı bağlantısı oluştur
        void savetodatabase()//veritabanına verileri göndermek için oluşturulan metot
        {
            if (con.State != ConnectionState.Open) con.Open();//veritabanı bağlantısı aç
            SqlCommand cmdinsert = new SqlCommand("insert into hastaanaliz values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "') ", con);//veritabanına veri kaydetme sorgusu
            cmdinsert.ExecuteNonQuery();//sorguyu çalıştır
            con.Close();//veritabanı bağlantısı kapat
        }
        void closeallpanels()//formdaki bütün panellerin erişilebilirliğini kapatmak için oluşturulan metot
        {
            foreach (Control item in this.Controls)//form içi nesneleri kontrol et
                if (item is Panel)//nesne "Panel" ise
                    item.Enabled = false;//erişilebilirliğini kapat
        }
        void extranotes()//ekstra panelinin erişilebilirliğini açmak için oluşturulan metot
        {
            MessageBox.Show("Eklemek istediklerinizi aşağıya yazınız.");//mesaj ver
            pnlEKSTRA.Enabled = true;//"ekstra" panelinin erişilebilirliğini aç
        }
        void ksdtaniquestion()//soru soran ve cevaba göre işlemler yapan metot
        {
            DialogResult ksdtani = MessageBox.Show("Kan şekeri değeri tanı ile uyuştu mu?", "Diyabet Tanı Programı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);//mesaj ver
            if (ksdtani.ToString() == "No")//cevap hayır ise
            {
                MessageBox.Show("Tanınızı giriniz");//mesaj ver
                pnlTANI.Enabled = true;//"tanı" panelinin erişilebilirliğini aç
            }
            else//üstteki şart veya şartlar sağlanmadıysa
                extranotes();//"extranotes" metotunu çalıştır
        }
        void riskligrupquestion()//soru soran ve cevaba göre işlemler yapan metot
        {
            DialogResult riskligrup = MessageBox.Show("Hasta tanısal olarak riskli grupta mı?", "Diyabet Tanı Programı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);//mesaj ver
            if (riskligrup.ToString() == "Yes")//cevap evet ise
            {
                MessageBox.Show("Lütfen OGTT sonucunu giriniz");//mesaj ver
                pnlOGTT.Enabled = true;//"ogtt" panelinin erişilebilirliğini aç
            }
            else//üstteki şart veya şartlar sağlanmadıysa
                ksdtaniquestion();//"ksdtaniquestion" metotunu çalıştır
        }
        private void button1_Click(object sender, EventArgs e)//buton1 e tıklanınca olacaklar
        {           
            textBox1.ReadOnly = true;//textbox1 in içeriğinin değiştirilmesini engelle
            ushort kansekeridegeri = Convert.ToUInt16(textBox1.Text);//ushort türünde kansekeridegeri değişkenini tanımla ve textbox1 in değerini uint e dönüştürüp değişkene at
            if (kansekeridegeri <= 100)//kansekeridegeri 100den küçük veya 100 e eşit ise
            {
                MessageBox.Show("Diyabet yok.");//mesaj ver
                riskligrupquestion();//"riskligrupquestion" metotunu çalıştır
            }
            else if (kansekeridegeri > 100 && kansekeridegeri <= 199)//kansekeridegeri 100 den büyük ve 199 dan küçük veya 199 a eşit ise
            {
                pnlAKSD.Enabled = true;//"aksd" panelinin erişilebilirliğini aç
                MessageBox.Show("Alttaki alana açlık kan şekeri değerinizi giriniz.");//mesaj ver
            }

            else if (kansekeridegeri >= 200)//kansekeridegeri 200 den büyük veya 200 e eşit ise
            {
                MessageBox.Show("Diyabet Tanısı!");//mesaj ver
                ksdtaniquestion();//"ksdtaniquestion" metotunu çalıştır
            }
        }
        private void button2_Click(object sender, EventArgs e)//buton2 ye tıklanınca olacaklar
        {
            textBox2.ReadOnly = true;//textbox2 nin içeriğinin değiştirilmesini engelle
            ushort aclikkansekeridegeri = Convert.ToUInt16(textBox2.Text);//ushort türünde aclikkansekeridegeri değişkenini tanımla ve textbox2 nin değerini uint e dönüştürüp değişkene at
            if (aclikkansekeridegeri <= 100)//aclikkansekeridegeri 100 den küçük veya 100 e eşit ise
            {
                MessageBox.Show("Diyabet yok.");//mesaj ver
                riskligrupquestion();//"riskligrupquestion" metotunu çalıştır
            }
            else if (aclikkansekeridegeri > 100 && aclikkansekeridegeri < 126)//aclikkansekeridegeri 100 den büyük ve 126 dan küçük ise
            {
                MessageBox.Show("Lütfen OGTT sonucunu giriniz");//mesaj ver
                pnlOGTT.Enabled = true;//"ogtt" panelinin erişilebilirliğini aç
            }
            else if (aclikkansekeridegeri >= 126)//aclikkansekeridegeri 126 dan büyük veya 126 ya eşit ise
            {
                MessageBox.Show("Diyabet Tanısı!");//mesaj ver
                ksdtaniquestion();//"ksdtaniquestion" metotunu çalıştır
            }
        }
        private void button3_Click(object sender, EventArgs e)//buton3 e tıklanınca olacaklar
        {
            textBox3.ReadOnly = true;//textbox3 ün içeriğinin değiştirilmesini engelle
            ushort ogttsonuc = Convert.ToUInt16(textBox3.Text);//ushort türünde ogttsonuc değişkenini tanımla ve textbox3 ün değerini uint e dönüştürüp değişkene at
            if (ogttsonuc >= 200)//ogttsonuc 200 den büyük veya 200 e eşit ise
            {
                MessageBox.Show("Diyabet Tanısı!");//mesaj ver
                ksdtaniquestion();//"ksdtaniquestion" metotunu çalıştır
            }
            else if (ogttsonuc > 140 && ogttsonuc < 200)//ogttsonuc 140 dan büyük ve 200 den küçük ise
            {
                MessageBox.Show("Bozulmuş Glukoz Toleransı");//mesaj ver
                ksdtaniquestion();//"ksdtaniquestion" metotunu çalıştır
            }
            else//üstteki şart veya şartlar sağlanmadıysa
            {
                MessageBox.Show("Bozulmuş Açlık Glukozu");//mesaj ver
                ksdtaniquestion();//"ksdtaniquestion" metotunu çalıştır
            }           
        }
        private void button4_Click(object sender, EventArgs e)//buton4 e tıklanınca olacaklar
        {
            textBox4.ReadOnly = true;//textbox4 ün içeriğinin değiştirilmesini engelle
            extranotes();//"extranotes" metotunu çalıştır
        }
        private void button5_Click(object sender, EventArgs e)//buton5 e tıklanınca olacaklar
        {
            textBox5.ReadOnly = true;//textbox5 in içeriğinin değiştirilmesini engelle
        }
        private void button6_Click(object sender, EventArgs e)//buton6 ya tıklanınca olacaklar
        {
            savetodatabase();//"savetodatabase" metotunu çalıştır
        }
    }
}
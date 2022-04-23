using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmithWatermanAlgorithm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int match, mismatch, gap;
        string[] FirstSequence;
        string[] SecondSequence;
        int diziboyutu, diziboyutu1;
        int counter = 0;

        public void degerleriAta()
        {
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            if (textBox3.Text == "")
            {
                match = 1;
            }
            else
            {
                match = Convert.ToInt32(textBox3.Text);
            }

            if (textBox4.Text == "")
            {
                mismatch = -1;
            }
            else
            {
                mismatch = Convert.ToInt32(textBox4.Text);
            }

            if (textBox5.Text == "")
            {
                gap = -2;

            }
            else
            {
                gap = Convert.ToInt32(textBox5.Text);
            }

            textBox3.Text = match.ToString();
            textBox4.Text = mismatch.ToString();
            textBox5.Text = gap.ToString();
        }

        private void dosyadanOkuButonu_Click(object sender, EventArgs e)
        {
            timer1.Start();
            try
            {
                if (File.Exists(@"C:\Users\Berna\Desktop\Seq1.txt") && File.Exists(@"C:\Users\Berna\Desktop\Seq2.txt"))
                {
                    FirstSequence = File.ReadAllLines(@"C:\Users\Berna\Desktop\Seq1.txt");
                    textBox1.Text = FirstSequence[1];
                    diziboyutu = Convert.ToInt32(FirstSequence[0]);

                    SecondSequence = File.ReadAllLines(@"C:\Users\Berna\Desktop\Seq2.txt");
                    textBox2.Text = SecondSequence[1];
                    diziboyutu1 = Convert.ToInt32(SecondSequence[0]);
                }
                else

                    MessageBox.Show("Dosya Bulunamadı...", "Error");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata :" + ex.ToString(), "Error");
            }
        }

        private void hizalaButonu_Click_1(object sender, EventArgs e)
        {
            string metin = textBox1.Text;
            string[] dizin1 = new string[diziboyutu];

            string metin2 = textBox2.Text;
            string[] dizin2 = new string[diziboyutu1];

            for (int i = 0; i < metin.Length; i++)
            {
                dizin1[i] = metin[i].ToString();
            }

            for (int i = 0; i < metin2.Length; i++)
            {
                dizin2[i] = metin2[i].ToString();
            }

            degerleriAta();
            gridviewDuzenle(dizin1, dizin2);
            hizala(dizin1, dizin2);
            duzenle(dizin1, dizin2);

            timer1.Stop();
        }

        void gridviewDuzenle(string[] dizin1, string[] dizin2)
        {
            DataTable tablo = new DataTable();

            string header1 = " ";
            string header2 = "";
            tablo.Columns.Add(header1);
            tablo.Columns.Add(header1 + header1);

            DataRow row1 = tablo.NewRow();
            tablo.Rows.Add(row1);
            tablo.Rows.Add(header1);

            dataGridView1.DataSource = tablo;

            for (int i = 0; i < dizin1.Length; i++)
            {
                tablo.Columns.Add(header2);
                header2 += header2;
            }

            for (int i = 0; i < dizin2.Length; i++)//aşağı doğru olanlar
            {
                DataRow row = tablo.NewRow();
                row[header1] = dizin2[i];
                tablo.Rows.Add(row);
                dataGridView1.DataSource = tablo;
            }

            for (int i = 1; i < dizin1.Length + 1; i++)
            {
                dataGridView1.Rows[0].Cells[i + 1].Value = dizin1[i - 1];
            }

            dataGridView1.Rows[1].Cells[1].Value = 0;
        }

        public int dizilimKarsilastirma(int i, int j)
        {
            int match = Convert.ToInt32(textBox3.Text);
            int mismatch = Convert.ToInt32(textBox4.Text);

            int sonuc = 0;

            if (String.Compare(dataGridView1.Rows[0].Cells[i].Value.ToString(), dataGridView1.Rows[j].Cells[0].Value.ToString()) == 0)
            {
                sonuc = match;
            }
            else
            {
                sonuc = mismatch;
            }
            return sonuc;
        }

        public int islemlerSonuc(int formul1, int formul2, int formul3)
        {
            int enbuyuk = formul1;
            int sonuc = formul1; //geçici değişken atıyoruz

            if (formul1 > formul2 && formul1 > formul3)
            {
                enbuyuk = formul1;
            }
            else if (formul2 > formul3)
            {
                enbuyuk = formul2;
            }
            else if (formul3 > formul2)
            {
                enbuyuk = formul3;
            }
            sonuc = enbuyuk;
            return sonuc;
        }

        void sifirdanDoldur(string[] dizin1, string[] dizin2)
        {
            int gap = Convert.ToInt32(textBox5.Text);
            int f1 = 0, f2 = 0, f3 = 0;
            Random rs = new Random(1);

            for (int j = 1; j < dizin1.Length + 2; j++)
            {
                int i = 1;
                if (i == 1 && j == 1)
                {

                }
                else if (i - 1 >= 1 && j - 1 >= 1)
                {
                    int karsilastirma = dizilimKarsilastirma(i - 1, j - 1);

                    int parca1 = Convert.ToInt32(dataGridView1.Rows[i - 1].Cells[j - 1].Value);
                    f1 = karsilastirma + parca1;

                    int parca2 = Convert.ToInt32(dataGridView1.Rows[i - 1].Cells[j].Value);
                    f2 = gap + parca2;

                    int parca3 = Convert.ToInt32(dataGridView1.Rows[i].Cells[j - 1].Value);
                    f3 = gap + parca3;
                }
                else if (i - 1 >= 1 && j >= 1)
                {
                    int parca2 = Convert.ToInt32(dataGridView1.Rows[i - 1].Cells[j].Value);
                    f2 = gap + parca2;
                    f1 = rs.Next(-50, f2);
                    f3 = rs.Next(-50, f2);
                }
                else if (i >= 1 && j - 1 >= 1)
                {
                    int parca3 = Convert.ToInt32(dataGridView1.Rows[i].Cells[j - 1].Value);
                    f3 = gap + parca3;
                    f1 = rs.Next(-50, f3);
                    f2 = rs.Next(-50, f3);
                }
                int sonucc = islemlerSonuc(f1, f2, f3);
                if (sonucc < 0)
                {
                    sonucc = 0;
                }
                dataGridView1.Rows[i].Cells[j].Value = sonucc;
            }

            for (int i = 2; i < dizin2.Length + 2; i++)
            {
                int j = 1;
                if (i == 1 && j == 1)
                {

                }
                else if (i - 1 >= 1 && j - 1 >= 1)
                {
                    int karsilastirma = dizilimKarsilastirma(i - 1, j - 1);

                    int parca1 = Convert.ToInt32(dataGridView1.Rows[i - 1].Cells[j - 1].Value);
                    f1 = karsilastirma + parca1;

                    int parca2 = Convert.ToInt32(dataGridView1.Rows[i - 1].Cells[j].Value);
                    f2 = gap + parca2;

                    int parca3 = Convert.ToInt32(dataGridView1.Rows[i].Cells[j - 1].Value);
                    f3 = gap + parca3;
                }
                else if (i - 1 >= 1 && j >= 1)
                {
                    int parca2 = Convert.ToInt32(dataGridView1.Rows[i - 1].Cells[j].Value);
                    f2 = gap + parca2;
                    f1 = rs.Next(-50, f2);
                    f3 = rs.Next(-50, f2);
                }
                else if (i >= 1 && j - 1 >= 1)
                {
                    int parca3 = Convert.ToInt32(dataGridView1.Rows[i].Cells[j - 1].Value);
                    f3 = gap + parca3;
                    f1 = rs.Next(-50, f3);
                    f2 = rs.Next(-50, f3);
                }
                int sonucc = islemlerSonuc(f1, f2, f3);
                if (sonucc < 0)
                {
                    sonucc = 0;
                }
                dataGridView1.Rows[i].Cells[j].Value = sonucc;
            }
        }

        void hizala(string[] dizin1, string[] dizin2)
        {
            sifirdanDoldur(dizin1, dizin2);
            int gap = Convert.ToInt32(textBox5.Text);
            int f1 = 0, f2 = 0, f3 = 0;
            Random rs = new Random(1);

            for (int j = 2; j < dizin2.Length + 2; j++)
            {
                for (int i = 2; i < dizin1.Length + 2; i++)
                {
                    int karsilastirma = dizilimKarsilastirma(i, j);

                    int parca1 = Convert.ToInt32(dataGridView1.Rows[j - 1].Cells[i - 1].Value);
                    f1 = karsilastirma + parca1;

                    int parca2 = Convert.ToInt32(dataGridView1.Rows[j].Cells[i - 1].Value);
                    f2 = gap + parca2;

                    int parca3 = Convert.ToInt32(dataGridView1.Rows[j - 1].Cells[i].Value);
                    f3 = gap + parca3;

                    if (f1 < 0)
                    {
                        f1 = 0;
                    }
                    if (f2 < 0)
                    {
                        f2 = 0;
                    }
                    if (f3 < 0)
                    {
                        f3 = 0;
                    }
                    int sonucc = islemlerSonuc(f1, f2, f3);
                    dataGridView1.Rows[j].Cells[i].Value = sonucc;
                }
            }
        }

        void duzenle(string[] dizin1, string[] dizin2)
        {
            ArrayList iDegerleri = new ArrayList();
            ArrayList jDegerleri = new ArrayList();
            ArrayList degerler = new ArrayList();

            int geciciEnBuyuk = 0;

            for (int j = dizin2.Length + 1; j >= 2; j--)
            {
                for (int i = dizin1.Length + 1; i >= 2; i--)
                {
                    if (Convert.ToInt32(dataGridView1.Rows[j].Cells[i].Value) > geciciEnBuyuk)
                    {
                        degerler.Add(Convert.ToInt32(dataGridView1.Rows[j].Cells[i].Value));
                        iDegerleri.Add(i);
                        jDegerleri.Add(j);
                    }
                }
            }

            ArrayList iDegerleriCop = new ArrayList();
            ArrayList jDegerleriCop = new ArrayList();

            for (int a = 0; a < iDegerleri.Count; a++)
            {
                for (int b = 1; b < jDegerleri.Count; b++)
                {
                    int iFark = Convert.ToInt32(iDegerleri[a]) - Convert.ToInt32(iDegerleri[b]);
                    int jFark = Convert.ToInt32(jDegerleri[a]) - Convert.ToInt32(jDegerleri[b]);

                    if (iFark == 0 && jFark == 0)
                    {

                    }
                    else if (iFark == jFark)
                    {
                        if (Convert.ToInt32(degerler[a]) > Convert.ToInt32(degerler[b]))
                        {
                            iDegerleriCop.Add(iDegerleri[b]);
                            jDegerleriCop.Add(jDegerleri[b]);
                        }
                    }
                }
            }

            for (int a = 0; a < iDegerleriCop.Count; a++)
            {
                iDegerleri.Remove(iDegerleriCop[a]);
                jDegerleri.Remove(jDegerleriCop[a]);
                degerler.Remove(a);
            }

            isaretle(iDegerleri, jDegerleri);
        }

        void isaretle(ArrayList iDegerleri, ArrayList jDegerleri)
        {
            ArrayList iKomsular = new ArrayList();
            ArrayList jKomsular = new ArrayList();
            ArrayList iDegerleriSecili = new ArrayList();
            ArrayList jDegerleriSecili = new ArrayList();
            ArrayList degerlerSecili = new ArrayList();

            int i, j;
            int skor = 0;
            int yeniSkor;

            for (int a = 0; a < iDegerleri.Count; a++)
            {
                iKomsular.Clear();
                jKomsular.Clear();
                i = Convert.ToInt32(iDegerleri[a]);
                j = Convert.ToInt32(jDegerleri[a]);
                int komsu;
                int karsilastirma = dizilimKarsilastirma(i, j);

                if (karsilastirma == 1)
                {
                    iKomsular.Add(i);
                    jKomsular.Add(j);
                    komsu = Convert.ToInt32(dataGridView1.Rows[j].Cells[i].Value);

                    while (komsu != 0)
                    {
                        komsu = Convert.ToInt32(dataGridView1.Rows[j - 1].Cells[i - 1].Value);
                        degerlerSecili.Add(komsu);
                        iKomsular.Add(i - 1);
                        jKomsular.Add(j - 1);
                        i = i - 1;
                        j = j - 1;
                    }
                    yeniSkor = skoruBul(iKomsular, jKomsular);
                    if (skor < yeniSkor)
                    {
                        skor = yeniSkor;

                        iDegerleriSecili.Clear();
                        jDegerleriSecili.Clear();

                        for (int b = 0; b < iKomsular.Count; b++)
                        {
                            iDegerleriSecili.Add(iKomsular[b]);
                            jDegerleriSecili.Add(jKomsular[b]);
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                else if (karsilastirma == -1)
                {

                }
            }
            sonSkoruBul(iDegerleriSecili, jDegerleriSecili);
        }

        public int skoruBul(ArrayList iDegerleriSecili, ArrayList jDegerleriSecili)
        {
            int skor = 0;
            ArrayList dizilim1 = new ArrayList();
            ArrayList dizilim2 = new ArrayList();
            int match = Convert.ToInt32(textBox3.Text);

            for (int a = 0; a < iDegerleriSecili.Count; a++)
            {
                int i = Convert.ToInt32(iDegerleriSecili[a]);
                int j = Convert.ToInt32(jDegerleriSecili[a]);

                if (String.Compare(dataGridView1.Rows[0].Cells[i].Value.ToString(), dataGridView1.Rows[j].Cells[0].Value.ToString()) == 0)
                {
                    skor += match;
                }
            }
            return skor;
        }

        void sonSkoruBul(ArrayList liste1, ArrayList liste2)
        {
            int skor = 0;
            ArrayList dizilim1 = new ArrayList();
            ArrayList dizilim2 = new ArrayList();
            int match = Convert.ToInt32(textBox3.Text);
            int mismatch = Convert.ToInt32(textBox4.Text);

            for (int a = liste1.Count - 1; a >= 0; a--)
            {
                int i = Convert.ToInt32(liste1[a]);
                int j = Convert.ToInt32(liste2[a]);

                dataGridView1.Rows[j].Cells[i].Style.BackColor = Color.LightSkyBlue;

                if (String.Compare(dataGridView1.Rows[0].Cells[i].Value.ToString(), dataGridView1.Rows[j].Cells[0].Value.ToString()) == 0)
                {
                    dizilim1.Add(dataGridView1.Rows[0].Cells[i].Value.ToString());
                    dizilim2.Add(dataGridView1.Rows[j].Cells[0].Value.ToString());
                    skor += match;
                }
            }
            foreach (var item in dizilim1)
            {
                textBox6.Text += item.ToString();
            }

            foreach (var item in dizilim2)
            {
                textBox7.Text += item.ToString();
            }

            textBox8.Text = skor.ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            counter++;
            label9.Text = "Run Time: " + counter.ToString();
        }
    }
}

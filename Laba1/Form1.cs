using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Laba1
{
    public partial class Form1 : Form
    {
        private List<double> X;
        private List<double> Y;
        private int N;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            X = new List<double>();
            Y = new List<double>();
            openFileDialog1.ShowDialog();
            string FileName = openFileDialog1.FileName;
            FileStream stream = File.Open(FileName, FileMode.Open, FileAccess.Read);
            if (stream != null)
            {
                StreamReader reader = new StreamReader(stream, Encoding.Default);
                for (int i = 0; !reader.EndOfStream; i++)
                {
                    string str = reader.ReadLine();
                    string str2 = str.Replace('.', ',');
                    while (str2.Contains("  "))
                    {
                        str2 = str2.Replace("  ", " ");
                    }
                    var str3 = str2.Split(' ');
                    X.Add(double.Parse(str3[0]));
                    Y.Add(double.Parse(str3[1]));
                }
                stream.Close();
            }
            N = X.Count;
            KorrPole();
            PervStAn();
            Koef();
        }

        private void KorrPole()
        {
            chart1.Series[0].Points.Clear();
            for (int i =0; i < N; i++)
            {
                chart1.Series[0].Points.AddXY(X[i], Y[i]);
            }
        }

        private void PervStAn()
        {
            dataGridView1.RowCount = 8;
            dataGridView1.Rows[0].Cells[0].Value = "Среднее";
            dataGridView1.Rows[1].Cells[0].Value = "Среднее";
            dataGridView1.Rows[2].Cells[0].Value = "Среднеквадратическое";
            dataGridView1.Rows[3].Cells[0].Value = "Среднеквадратическое";
            dataGridView1.Rows[4].Cells[0].Value = "Ассиметрия";
            dataGridView1.Rows[5].Cells[0].Value = "Ассиметрия";
            dataGridView1.Rows[6].Cells[0].Value = "Эксцесс";
            dataGridView1.Rows[7].Cells[0].Value = "Эксцесс";

            dataGridView1.Rows[0].Cells[1].Value = "Оценка";
            dataGridView1.Rows[1].Cells[1].Value = "Дов. интервал";
            dataGridView1.Rows[2].Cells[1].Value = "Оценка";
            dataGridView1.Rows[3].Cells[1].Value = "Дов. интервал";
            dataGridView1.Rows[4].Cells[1].Value = "Оценка";
            dataGridView1.Rows[5].Cells[1].Value = "Дов. интервал";
            dataGridView1.Rows[6].Cells[1].Value = "Оценка";
            dataGridView1.Rows[7].Cells[1].Value = "Дов. интервал";

            double kvantil = 1.671;

            double srednearifmX = 0;
            double srednekvX = 0;
            double srednekvsX = 0;
            double koefassX = 0;
            double koefekscX = 0;
            //double koefekacesssmeshX = 0;

            double srednearifmXSKO = 0;
            double srednekvXSKO = 0;
            double koefassXSKO = 0;
            double koefekscXSKO = 0;

            double srednearifmXTHETAV = 0;
            double srednekvXTHETAV = 0;
            double koefassXTHETAV = 0;
            double koefekscXTHETAV = 0;

            double srednearifmXTHETAN = 0;
            double srednekvXTHETAN = 0;
            double koefassXTHETAN = 0;
            double koefekscXTHETAN = 0;


            srednearifmX = X.Sum() / N;
            srednekvX = Math.Sqrt(X.Sum(x => Math.Pow((x - srednearifmX), 2)) / (N - 1));
            srednekvsX = Math.Sqrt(X.Sum(x => Math.Pow((x - srednearifmX), 2)) / N);
            koefassX = (X.Sum(x => Math.Pow((x - srednearifmX), 3)) / (N * Math.Pow(srednekvsX, 3))) * (Math.Sqrt(N * (N - 1)) / (N - 2));

            koefekscX = ((double)(N * N - 1) / ((double)(N - 2) * (double)(N - 3))) * (((X.Sum(x => Math.Pow((x - srednearifmX), 4)))) / ((double)N * Math.Pow(srednekvsX, 4))) - 3.0 + (6.0 / (N + 1));

            srednearifmXSKO = srednekvX / (Math.Sqrt(N));
            srednekvXSKO = srednekvX / (Math.Sqrt(2 * N));
            koefassXSKO = Math.Sqrt((6 * (N - 2.0)) / ((N + 1.0) * (N + 3.0)));
            koefekscXSKO = Math.Sqrt((24.0 * N * (N - 2.0) * (N - 3.0)) / ((N + 1.0) * (N + 1.0) * (N + 3.0) * (N + 5.0)));

            srednearifmXTHETAV = srednearifmX + kvantil * srednearifmXSKO;
            srednekvXTHETAV = srednekvX + kvantil * srednekvXSKO;
            koefassXTHETAV = koefassX + kvantil * koefassXSKO;
            koefekscXTHETAV = koefekscX + kvantil * koefekscXSKO;


            srednearifmXTHETAN = srednearifmX - kvantil * srednearifmXSKO;
            srednekvXTHETAN = srednekvX - kvantil * srednekvXSKO;
            koefassXTHETAN = koefassX - kvantil * koefassXSKO;
            koefekscXTHETAN = koefekscX - kvantil * koefekscXSKO;


            double srednearifmY = 0;
            double srednekvY = 0;
            double srednekvsY = 0;
            double koefassY = 0;
            double koefekscY = 0;
            //double koefekacesssmeshY = 0;

            double srednearifmYSKO = 0;
            double srednekvYSKO = 0;
            double koefassYSKO = 0;
            double koefekscYSKO = 0;

            double srednearifmYTHETAV = 0;
            double srednekvYTHETAV = 0;
            double koefassYTHETAV = 0;
            double koefekscYTHETAV = 0;

            double srednearifmYTHETAN = 0;
            double srednekvYTHETAN = 0;
            double koefassYTHETAN = 0;
            double koefekscYTHETAN = 0;


            srednearifmY = Y.Sum() / N;
            srednekvY = Math.Sqrt(Y.Sum(x => Math.Pow((x - srednearifmY), 2)) / (N - 1));
            srednekvsY = Math.Sqrt(Y.Sum(x => Math.Pow((x - srednearifmY), 2)) / N);
            koefassY = (Y.Sum(x => Math.Pow((x - srednearifmY), 3)) / (N * Math.Pow(srednekvsY, 3))) * (Math.Sqrt(N * (N - 1)) / (N - 2));

            koefekscY = ((double)(N * N - 1) / ((double)(N - 2) * (double)(N - 3))) * (((Y.Sum(x => Math.Pow((x - srednearifmY), 4)))) / ((double)N * Math.Pow(srednekvsY, 4))) - 3.0 + (6.0 / (N + 1));

            srednearifmYSKO = srednekvY / (Math.Sqrt(N));
            srednekvYSKO = srednekvY / (Math.Sqrt(2 * N));
            koefassYSKO = Math.Sqrt((6 * (N - 2.0)) / ((N + 1.0) * (N + 3.0)));
            koefekscYSKO = Math.Sqrt((24.0 * N * (N - 2.0) * (N - 3.0)) / ((N + 1.0) * (N + 1.0) * (N + 3.0) * (N + 5.0)));

            srednearifmYTHETAV = srednearifmY + kvantil * srednearifmYSKO;
            srednekvYTHETAV = srednekvY + kvantil * srednekvYSKO;
            koefassYTHETAV = koefassY + kvantil * koefassYSKO;
            koefekscYTHETAV = koefekscY + kvantil * koefekscYSKO;


            srednearifmYTHETAN = srednearifmY - kvantil * srednearifmYSKO;
            srednekvYTHETAN = srednekvY - kvantil * srednekvYSKO;
            koefassYTHETAN = koefassY - kvantil * koefassYSKO;
            koefekscYTHETAN = koefekscY - kvantil * koefekscYSKO;

            dataGridView1.Rows[0].Cells[2].Value = srednearifmX.ToString("#0.####");
            dataGridView1.Rows[0].Cells[3].Value = srednearifmY.ToString("#0.####");

            dataGridView1.Rows[2].Cells[2].Value = srednekvX.ToString("#0.####");
            dataGridView1.Rows[2].Cells[3].Value = srednekvY.ToString("#0.####");

            dataGridView1.Rows[4].Cells[2].Value = koefassX.ToString("#0.####");
            dataGridView1.Rows[4].Cells[3].Value = koefassY.ToString("#0.####");

            dataGridView1.Rows[6].Cells[2].Value = koefekscX.ToString("#0.####");
            dataGridView1.Rows[6].Cells[3].Value = koefekscY.ToString("#0.####");

            dataGridView1.Rows[1].Cells[2].Value = "[" + srednearifmXTHETAN.ToString("#0.####") + ";" + srednearifmXTHETAV.ToString("#0.####") + "]";
            dataGridView1.Rows[1].Cells[3].Value = "[" + srednearifmYTHETAN.ToString("#0.####") + ";" + srednearifmYTHETAV.ToString("#0.####") + "]";

            dataGridView1.Rows[3].Cells[2].Value = "[" + srednekvXTHETAN.ToString("#0.####") + ";" + srednekvXTHETAV.ToString("#0.####") + "]";
            dataGridView1.Rows[3].Cells[3].Value = "[" + srednekvYTHETAN.ToString("#0.####") + ";" + srednekvYTHETAV.ToString("#0.####") + "]";

            dataGridView1.Rows[5].Cells[2].Value = "[" + koefassXTHETAN.ToString("#0.####") + ";" + koefassXTHETAV.ToString("#0.####") + "]";
            dataGridView1.Rows[5].Cells[3].Value = "[" + koefassYTHETAN.ToString("#0.####") + ";" + koefassYTHETAV.ToString("#0.####") + "]";

            dataGridView1.Rows[7].Cells[2].Value = "[" + koefekscXTHETAN.ToString("#0.####") + ";" + koefekscXTHETAV.ToString("#0.####") + "]";
            dataGridView1.Rows[7].Cells[3].Value = "[" + koefekscYTHETAN.ToString("#0.####") + ";" + koefekscYTHETAV.ToString("#0.####") + "]";

        }

        private void Koef()
        {
            double alpha = double.Parse(textBox1.Text);

            dataGridView2.RowCount = 4;
            dataGridView2.Rows[0].Cells[0].Value = "Парный коэффициент корелляции";
            dataGridView2.Rows[1].Cells[0].Value = "Коррелляционное отношение";
            dataGridView2.Rows[2].Cells[0].Value = "Коэффициент Спирмена";
            dataGridView2.Rows[3].Cells[0].Value = "Коэффициент Кендела";

            #region K1
            double Xsr = X.Sum() / N;
            double Ysr = Y.Sum() / N;
            double XYsr = 0;
            for (int i = 0; i < N; XYsr += X[i] * Y[i], i++);
            XYsr /= N;
            double Xsrkv = Math.Sqrt(X.Sum(x => (x - Xsr) * (x - Xsr)) / N);
            double Ysrkv = Math.Sqrt(Y.Sum(y => (y - Ysr) * (y - Ysr)) / N);

            double K1 = (XYsr - Xsr * Ysr) / (Xsrkv * Ysrkv);

            double S1 = K1 * Math.Sqrt(N - 2) / Math.Sqrt(1 - K1 * K1);

            double Kv1 = alglib.invstudenttdistribution(N - 2, 1 - alpha * 0.5);

            double K1n = K1 + K1 * (1 - K1 * K1) / (2 * N) - alglib.invnormaldistribution(1 - alpha * 0.5) * ((1 - K1 * K1) / (Math.Sqrt(N - 1)));
            double K1v = K1 + K1 * (1 - K1 * K1) / (2 * N) + alglib.invnormaldistribution(1 - alpha * 0.5) * ((1 - K1 * K1) / (Math.Sqrt(N - 1)));

            dataGridView2.Rows[0].Cells[1].Value = K1;
            dataGridView2.Rows[0].Cells[2].Value = S1;
            dataGridView2.Rows[0].Cells[3].Value = Kv1;
            dataGridView2.Rows[0].Cells[4].Value = (Math.Abs(S1) < Kv1) ? "Значащий" : "Не значащий";
            dataGridView2.Rows[0].Cells[5].Value = "[" + K1n.ToString("#0.####") + ";" + K1v.ToString("#0.####") + "]";
            #endregion
            #region K2

            List<double> Yi = new List<double>();
            List<double> Mi = new List<double>();
            int M = (int)Math.Sqrt(N);
            List<double> X1 = new List<double>();
            foreach (double x in X) { X1.Add(x); }
            List<double> Y1 = new List<double>();
            foreach (double y in Y) { Y1.Add(y); }
            for (int i = 0; i < N; i++)
                {
                    for (int j = N - 1; j > i; j--)
                    {
                        if (X1[j] < X1[j - 1])
                        {
                            double tmp1 = X1[j];
                            X1[j] = X1[j - 1];
                            X1[j - 1] = tmp1;
                            double tmp2 = Y1[j];
                            Y1[j] = Y1[j - 1];
                            Y1[j - 1] = tmp2;
                        }
                    }
                }  

            double h = (X1[N-1] - X1[0])/M;

            for (int i = 0; i < M - 1; i++)
            {
                var tmpX = X1.FindAll(x => (x >= X1[0] + i*h)&&(x < X1[0] + (i+1)*h));
                var tmpY = Y1.FindAll(y => tmpX.Contains(X1[Y.IndexOf(y)]));
                Yi.Add(tmpY.Sum()/tmpY.Count);
                Mi.Add(tmpY.Count);
            }
            var tmppX = X1.FindAll(x => (x >= X1[0] + (M-1)*h)&&(x <= X1[0] + M*h));
            var tmppY = Y1.FindAll(y => tmppX.Contains(X1[Y.IndexOf(y)]));
            Yi.Add(tmppY.Sum()/tmppY.Count);
            Mi.Add(tmppY.Count);
              
            double YSR = Y1.Sum()/Y1.Count;

            double chisl = 0;
            //chisl = Yi.Sum(y => (y - YSR)*(y - YSR)*Mi[Yi.IndexOf(y)]);
            for (int i = 0; i < Yi.Count; i++)
            {
                chisl+= (Yi[i] - YSR)*(Yi[i] - YSR)*Mi[i];
            }
            double znam = Y1.Sum(y => (y - YSR)*(y - YSR));
            double K2 = chisl/znam;

            double S2 = (K2*K2/(M - 1))/ (  (1 - K2*K2)/(N - M) );
            double Kv2 = alglib.invfdistribution(M - 1, N - M, 1 - alpha);

            dataGridView2.Rows[1].Cells[1].Value = K2;
            dataGridView2.Rows[1].Cells[2].Value = S2;
            dataGridView2.Rows[1].Cells[3].Value = Kv2;
            dataGridView2.Rows[1].Cells[4].Value = (Math.Abs(S2) < Kv2) ? "Значащий" : "Не значащий";
           

            #endregion
            #region K3

            List<double> Rx = new List<double>();
            List<double> Ry = new List<double>();
            List<double> Ry2 = new List<double>();

            X1 = new List<double>();
            foreach (double x in X) { X1.Add(x); }
            Y1 = new List<double>();
            foreach (double y in Y) { Y1.Add(y); }
            List<double> Y2= new List<double>();
            foreach (double y in Y) { Y2.Add(y); }
            List<int> Ai = new List<int>();
            List<int> Bi = new List<int>();
            //X1.Sort();
            Y1.Sort();

            for (int i = 0; i < N; i++)
                {
                    for (int j = N - 1; j > i; j--)
                    {
                        if (X1[j] < X1[j - 1])
                        {
                            double tmp1 = X1[j];
                            X1[j] = X1[j - 1];
                            X1[j - 1] = tmp1;
                            double tmp2 = Y2[j];
                            Y2[j] = Y2[j - 1];
                            Y2[j - 1] = tmp2;
                        }
                    }
                }

            double r;
            for (int i = 0; i < N;)
            {
                int n = X1.FindAll(x => (x == X1[i])).Count;
                r = (i + i + n - 1) / (2 * n);
                for (int j = 0; j < n; j++ )
                {
                    Rx.Add(r);
                }
                i += n;
                if (n != 1) Ai.Add(n);
            }
            for (int i = 0; i < N;)
            {
                int n = Y1.FindAll(y => (y == Y2[i])).Count;
                int I = Y1.IndexOf(Y2[i]);
                r = (I + I + n - 1) / (2 * n);
                for (int j = 0; j < n; j++)
                {
                    Ry.Add(r);
                }
                i += n;
                if (n != 1) Bi.Add(n);
            }

            double A = (1.0 / 12.0) * Ai.Sum(a => a * a * a - a);
            double B = (1.0 / 12.0) * Bi.Sum(b => b * b * b - b);

            double summa = 0;
            for (int i = 0; i < N; i++)
            {
                summa+=(Rx[i] - Ry[i])*(Rx[i] - Ry[i]);
            }
            double chislitel = (1.0 / 6.0) * N * (N * N - 1) - summa - A - B;
            double znamenatel = Math.Sqrt(((1.0 / 6.0) * (N * N - 1) - 2 * A) * ((1.0 / 6.0) * (N * N - 1) - 2 * B));

            double K3 = chislitel / znamenatel;

            double S3 = (K3 * Math.Sqrt(N - 2)) / Math.Sqrt(1 - K3 * K3);

            double Kv3 = alglib.invstudenttdistribution(N - 2, 1 - alpha * 0.5);

            dataGridView2.Rows[2].Cells[1].Value = K3;
            dataGridView2.Rows[2].Cells[2].Value = S3;
            dataGridView2.Rows[2].Cells[3].Value = Kv3;
            dataGridView2.Rows[2].Cells[4].Value = (Math.Abs(S3) < Kv3) ? "Значащий" : "Не значащий";
            #endregion
            #region K4

            int S = 0;
            for (int i=0; i< N; i++)
            {
                for (int j = 0; j < N-1; j++)
                {
                    if (Ry[i] < Ry[j] && Rx[i] != Rx[j]) S += 1;
                    if (Ry[i] > Ry[j] && Rx[i] != Rx[j]) S -= 1;
                }
            }

            double C = 0.5 * Ai.Sum(a => a * (a - 1));
            double D = 0.5 * Bi.Sum(b => b * (b - 1));

            double K4 = S / Math.Sqrt((0.5*N*(N - 1) - C)*(0.5*N*(N-1) - D));

            double S4 = (3 * K4 * Math.Sqrt(N * (N - 1))) / Math.Sqrt(2 * (2 * N + 5));

            double Kv4 = alglib.invnormaldistribution(1 - alpha * 0.5);

            dataGridView2.Rows[3].Cells[1].Value = K4;
            dataGridView2.Rows[3].Cells[2].Value = S4;
            dataGridView2.Rows[3].Cells[3].Value = Kv4;
            dataGridView2.Rows[3].Cells[4].Value = (Math.Abs(S4) < Kv4) ? "Значащий" : "Не значащий";
            #endregion

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO.Ports;// necessário para ter acesso as portas 
using System.Globalization;
namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        
        string RxString;
        string otherStr;
        string RxString2;
        string otherStr2;
        bool newRead = false;
        bool newRead2 = false;
        
        public Form1()
        {
            InitializeComponent();
            timerCOM.Enabled = true;
        }

        private void atualizaListaCOMs()
        {
            int i;
            bool quantDiferente; //flag para sinalizar que a quantidade de portas mudou

            i = 0;
            quantDiferente = false;

            //se a quantidade de portas mudou
            if (comboBox1.Items.Count == SerialPort.GetPortNames().Length)
            {
                foreach (string s in SerialPort.GetPortNames())
                {
                    if (comboBox1.Items[i++].Equals(s) == false)
                    {
                        quantDiferente = true;
                    }
                }
            }
            else
            {
                quantDiferente = true;
            }

            //Se não foi detectado diferença
            if (quantDiferente == false)
            {
                return;                     
            }

            //limpa comboBox
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();

            //adiciona todas as COM diponíveis na lista
            foreach (string s in SerialPort.GetPortNames())
            {
                comboBox1.Items.Add(s);
                comboBox2.Items.Add(s);
                comboBox3.Items.Add(s);
            }
            //seleciona a primeira posição da lista
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
        }

        private void timerCOM_Tick(object sender, EventArgs e)
        {
            atualizaListaCOMs();
            
            if (newRead) {
                if (otherStr.Contains("35 73 5E BE"))
                {
                    label2.Text = "Jessica Juliana";
                    label4.Text = "Costa Weege ";
                    label6.Text = "Funcionário(a)";
                    DateTime localDate = DateTime.Now;
                    var culture = new CultureInfo("en-GB");
                    label11.Text = localDate.ToString(culture);
                    label9.Text = "";
                    
                    pictureBox1.ImageLocation = "C:/Users/umwoli1/Desktop/Visual/WindowsFormsApplication1/bin/Release/jessica.jpg";

                }
                else if (otherStr.Contains("90 28 45 1A"))
                {
                    label2.Text = "Magno";
                    label4.Text = "Weege de Oliveira";
                    label6.Text = "Funcionário(a)";
                    DateTime localDate = DateTime.Now;
                    var culture = new CultureInfo("en-GB");
                    label11.Text = localDate.ToString(culture);
                    label9.Text = "";

                    pictureBox1.ImageLocation = "C:/Users/umwoli1/Desktop/Visual/WindowsFormsApplication1/bin/Release/magno.jpg";

                }
                else
                {
                      label9.Text = "Informação incompleta!";
                }

                textBoxReceber.AppendText(otherStr);
                newRead = false;
                otherStr = "";
            }
            if (newRead2)
            {
                textBoxReceber2.AppendText(otherStr2);
                newRead2 = false;
                otherStr2 = "";
            }

        }

        private void btConectar_Click(object sender, EventArgs e)
        {
            serialPort2.Close();
            serialPort3.Close();
            comboBox2.Enabled = true;
            comboBox3.Enabled = true;
            btConectar2.Text = "Conectar";
            btConectar3.Text = "Conectar";

            if (serialPort1.IsOpen == false)
            {
                try
                {
                    serialPort1.PortName = comboBox1.Items[comboBox1.SelectedIndex].ToString();
                    serialPort1.Open();
                }
                catch
                {
                    return;
                }
                if (serialPort1.IsOpen)
                {
                    btConectar.Text = "Desconectar";
                    comboBox1.Enabled = false;
                }
            }
            else
            {
                try
                {
                    serialPort1.Close();
                    comboBox1.Enabled = true;
                    btConectar.Text = "Conectar";
                }
                catch
                {
                    return;
                }
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (serialPort1.IsOpen == true || serialPort2.IsOpen == true || serialPort3.IsOpen == true)  // se porta aberta
                serialPort1.Close();         //fecha as portas
                serialPort2.Close();
                serialPort3.Close();

        }

        private void btEnviar_Click(object sender, EventArgs e)
        {
           

        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            RxString = serialPort1.ReadExisting();              //le o dado disponível na serial
            this.Invoke(new EventHandler(trataDadoRecebido));   //chama outra thread para escrever para capturar a saida
        }
        private void trataDadoRecebido(object sender, EventArgs e)
        {
            otherStr += RxString; // A informação precisa ser concatenada pois vem em pedaços
            newRead = true;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            serialPort2.Write("1");
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            serialPort1.Close();
            serialPort3.Close();
            comboBox1.Enabled = true;
            comboBox3.Enabled = true;
            btConectar.Text = "Conectar";
            btConectar3.Text = "Conectar";

            if (serialPort2.IsOpen == false)
            {
                try
                {
                    serialPort2.PortName = comboBox2.Items[comboBox2.SelectedIndex].ToString();
                    serialPort2.Open();
                }
                catch
                {
                    return;
                }
                if (serialPort2.IsOpen)
                {
                    btConectar2.Text = "Desconectar";
                    comboBox2.Enabled = false;
                }
            }
            else
            {
                try
                {
                    serialPort2.Close();
                    comboBox2.Enabled = true;
                    btConectar2.Text = "Conectar";
                }
                catch
                {
                    return;
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void serialPort2_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            
            RxString2 = serialPort2.ReadExisting();              //le o dado disponível na serial
            this.Invoke(new EventHandler(receberDadoTag));
        }
        private void receberDadoTag(object sender, EventArgs e)
        {
            otherStr2 += RxString2; // A informação precisa ser concatenada pois vem em pedaços
            newRead2 = true;
        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (!(string.IsNullOrEmpty(textBox1.Text)) && !(string.IsNullOrEmpty(textBox2.Text)))
                serialPort2.Write("1");
            else
                MessageBox.Show("Você precisa preencher nome e sobrenome.", "Aviso!",
                MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            serialPort1.Close();
            serialPort3.Close();
            comboBox1.Enabled = true;
            comboBox3.Enabled = true;
            btConectar.Text = "Conectar";
            btConectar3.Text = "Conectar";

            if (serialPort2.IsOpen == false)
            {
                try
                {
                    serialPort2.PortName = comboBox2.Items[comboBox2.SelectedIndex].ToString();
                    serialPort2.Open();
                }
                catch
                {
                    return;
                }
                if (serialPort2.IsOpen)
                {
                    btConectar2.Text = "Desconectar";
                    comboBox2.Enabled = false;
                }
            }
            else
            {
                try
                {
                    serialPort2.Close();
                    comboBox2.Enabled = true;
                    btConectar2.Text = "Conectar";
                }
                catch
                {
                    return;
                }
            }
        }

        private void btConectar3_Click(object sender, EventArgs e)
        {
            serialPort1.Close();
            serialPort2.Close();
            comboBox1.Enabled = true;
            comboBox2.Enabled = true;
            btConectar.Text = "Conectar";
            btConectar2.Text = "Conectar";

            if (serialPort3.IsOpen == false)
            {
                try
                {
                    serialPort3.PortName = comboBox3.Items[comboBox3.SelectedIndex].ToString();
                    serialPort3.Open();
                }
                catch
                {
                    return;
                }
                if (serialPort3.IsOpen)
                {
                    btConectar3.Text = "Desconectar";
                    comboBox3.Enabled = false;
                }
            }
            else
            {
                try
                {
                    serialPort3.Close();
                    comboBox3.Enabled = true;
                    btConectar3.Text = "Conectar";
                }
                catch
                {
                    return;
                }
            }
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            String func = textBox2.Text;
            String nome = textBox1.Text;

            DataTable dt = new DataTable();
            String insSQL = "select * from usuario";
            String strConn = @"Data Source=info2.sqlite";
            SQLiteConnection conn = new SQLiteConnection(strConn);
            SQLiteDataAdapter da = new SQLiteDataAdapter(insSQL, strConn);
            da.Fill(dt);
            dataGridView1.DataSource = dt.DefaultView;

            serialPort3.Write("2");
            serialPort3.Write(func + "#");
            serialPort3.Write(nome + "#");

        }

        




    }
}

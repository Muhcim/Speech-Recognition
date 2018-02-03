using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;//Referances bölümünden ekliyoruz adfreferances freamwork ten ekliyoruz
using System.Speech.Recognition;
using System.Threading;

namespace Ses_Komutları
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SpeechSynthesizer sSynth = new SpeechSynthesizer();
        PromptBuilder pBuilder = new PromptBuilder();
        SpeechRecognitionEngine sRecognize = new SpeechRecognitionEngine();

        private void button1_Click(object sender, EventArgs e)
        {
            pBuilder.ClearContent();
            pBuilder.AppendText(textBox1.Text);
            sSynth.Speak(pBuilder);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            button3.Enabled = true;

            Choices sList = new Choices();
            sList.Add(new string[] { "merhaba", "Emre", "hasan", "nasılsın", "pop", "Hayat", "nasıl", "gidiyor", "seni ", "özledim ", "bilmiyorum ", "komador ", "exit" });
            Grammar gr = new Grammar(new GrammarBuilder(sList));

            try
            {
                sRecognize.RequestRecognizerUpdate();
                sRecognize.LoadGrammar(gr);
                sRecognize.SpeechRecognized += sRecognize_SpeechRecognized;
                sRecognize.SetInputToDefaultAudioDevice();
                sRecognize.RecognizeAsync(RecognizeMode.Multiple);

            }
            catch 
            {
                
                throw;
            }
        }

        void sRecognize_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (e.Result.Text == "exit")
            {
                Application.Exit();


                
            }
            else
            {
                textBox1.Text = textBox1.Text + " " + e.Result.Text.ToString();
            }
            if (e.Result.Text == "Emre")//Burada emre isminin icine muzik yerlestirdik 
            {
           System.Diagnostics.Process.Start("D:\\Müzikler\\klarnet.mp3");

                

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            sRecognize.RecognizeAsyncStop();
            button2.Enabled = true;
            button3.Enabled = false;
        }
    }
}
//Not Bu programi calistirmadan once bilgisiyari ayarladan ingilizce olsarak baslatmamiz gerekiyor
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;


namespace NumbersSearcher
{
    public partial class Form : System.Windows.Forms.Form
    {
        string Path;

        //int res = 0;

        /// <summary>
        /// точки изображения в чёрно-белом представлении 
        /// </summary>
        List<byte> points;


        public delegate void PrntReaction(string text);
        PrntReaction prntReaction;
        public delegate void PrntResult(string text);
        PrntReaction prntResult;
        public delegate void PrntCorrections(int count);
        PrntCorrections prntCorrections;
        public delegate Bitmap GetBitmapImg();
        GetBitmapImg getBitmapImage;

        /// <summary>
        /// Производится ли тренировка сети
        /// </summary>
        bool training = false;
        string[] images;
        Dictionary<char, List<string>> symbols = new Dictionary<char, List<string>>();
        List<char> symbs = new List<char>();//сами символы
        int lastCorrectionCount = -1;


        public void PrintReaction(string text)
        {
            rtbReaction.Text += text;
            rtbReaction.SelectionStart = rtbReaction.Text.Length;
            rtbReaction.Refresh();
        }

        public void PrintResult(string text)
        {
            Result.Text = text;
            Result.Refresh();
        }

        public void PrintCorrections(int count)
        {
            if (tbCorrectionCounts.Text != "")
            {
                string delta_s = "";
                if (lastCorrectionCount!=-1)
                {
                    int delta = 0;

                    if (lastCorrectionCount != 0)
                    {
                        delta = 100 * (lastCorrectionCount - count) / lastCorrectionCount;

                        if (delta < 0)
                            delta_s = "(+" + (-delta).ToString() + "%)";
                        else
                            delta_s = "(-" + delta.ToString() + "%)";
                    }
                    else
                    {
                        if(count!=0)
                            delta_s = "(+INF%)";
                        else
                            delta_s = "(+0%)";
                    }
                }
                tbCorrectionCounts.Text += ", " + count + delta_s;
            }
            else
                tbCorrectionCounts.Text = count.ToString();
            tbCorrectionCounts.Refresh();

            lastCorrectionCount = count;
        }

        public Bitmap GetBitmapImage()
        {
            Bitmap result = null;
            result = new Bitmap(Preview.Image, new Size(320, 240));

            return result;
        }


        Holder objects;// = Holder.GetInstance;

        void ReadFile()
        {
            //чтение файла chars.txt, который содержит список существующих символов
            if (File.Exists("chars.txt"))
            {

                StreamReader sr = null;
                try
                {
                    int num = -1, denum=0;//порядковый номер символа
                    sr = new StreamReader("chars.txt");
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] data = line.Split(' ');
                        
                        char smb = data[0][0];
                        try
                        {
                            num = Convert.ToInt32(data[1]);

                            objects.AddNeuron(smb, num - denum);
                        }
                        catch//если не удалась конвертация
                        {
                            denum++;//увеличить счётчик пропущенных символов
                        }
                    }
                    
                }
                catch
                {
        
                }
                finally
                {
                    sr.Close(); 
                }
            }
            else
            {
                StreamWriter write = new StreamWriter("chars.txt");
                write.Close();
            }

        }
        void WriteFile(char a)
        {
            StreamWriter sw;
            sw = new StreamWriter("chars.txt",true);

            sw.WriteLine(a.ToString());
            sw.Flush();
            sw.Close();
            
        }
        public Form()
        {
            InitializeComponent();
            objects = Holder.GetInstance(Preview.Width, Preview.Height);

            ReadFile();
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] StrList = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string CurrentF in StrList)
            {
                if (CurrentF.Substring(CurrentF.Length - 4) == ".bmp")
                {
                    Path = CurrentF;
                    Preview.Load(Path);
                    points = new List<byte>(Preview.Width * Preview.Height + 1);
                }
            }
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
                e.Effect = DragDropEffects.All;

        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Path = openFileDialog.FileName;
                Preview.Load(Path);
                points = new List<byte>(Preview.Width * Preview.Height + 1);

                Result.Text = "";
            }
        }

        

        private void распознатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            points.Clear();
            points.Add(1);
            
            
            Bitmap temp = new Bitmap(Preview.Image,new Size(320,240));

            for (int i = 0; i < temp.Width; i++)
            {
                for (int j = 0; j < temp.Height; j++)
                {
                    Color current = temp.GetPixel(i, j);
                    if (current.R > 127 && current.G > 127 && current.B > 127)
                        temp.SetPixel(i, j, Color.White);
                    else
                        temp.SetPixel(i, j, Color.Black);
                }
            }
            for (int i = 1; i < points.Capacity; i++)
            {
                points.Add(temp.GetPixel((i - 1) % temp.Width, (i - 1) / temp.Width).ToArgb() != -1 ? (byte)1 : (byte)0);
            }
            Result.Text = objects.Recognize(points);
        }

        private void исправитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            char smb;
            if (Result.Text == "")
                smb = '\n';
            else
                smb = Result.Text[0];
            Result.Text = objects.Correct(smb, '\0', points);
            Result.Text = "";
            
            this.Refresh();
            objects.Save();
            Result.Text = "Сохранен";
        }

        private void btTrain_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.ShowDialog();//диалоговое окно выбора папки
            string newPathAddress = folderBrowserDialog.SelectedPath;//получение выбранного имени папки

            var dir = new DirectoryInfo(newPathAddress);// папка с файлами

            images = Directory.GetFiles(@newPathAddress, "Symbol_*.png");

            if(images.Length==0)
            {
                MessageBox.Show("В выбранной папке нет подходящих для обучения изображений.");
            }
            else
                bgwTraining.RunWorkerAsync();
        }


        /// <summary>
        /// Запускает обучение по всем символам
        /// </summary>
        /// <returns>Количество исправлений</returns>
        private int Training()
        {
            int result = 0;

            for(int l=0; l<symbs.Count; l++)
            {
                char symbol = symbs[l];

                for (int k = 0; k < symbols[symbol].Count; k++)
                {
                    if (!training)
                        return result;

                    string Path = symbols[symbol][k];
                    Preview.Load(Path);
                    points = new List<byte>(Preview.Width * Preview.Height + 1);

                    points.Clear();
                    points.Add(1);

                    Bitmap temp = Preview.Invoke(getBitmapImage) as Bitmap;

                    for (int i = 0; i < temp.Width; i++)
                    {
                        for (int j = 0; j < temp.Height; j++)
                        {
                            Color current = temp.GetPixel(i, j);
                            if (current.R > 127 && current.G > 127 && current.B > 127)
                                temp.SetPixel(i, j, Color.White);
                            else
                                temp.SetPixel(i, j, Color.Black);
                        }
                    }
                    for (int i = 1; i < points.Capacity; i++)
                    {
                        points.Add(temp.GetPixel((i - 1) % temp.Width, (i - 1) / temp.Width).ToArgb() != -1 ? (byte)1 : (byte)0);
                    }

                    string trainResult = objects.Recognize(points);
                    Result.Invoke(prntResult, (trainResult));

                    if (trainResult != symbol.ToString())
                    {

                        char badSymbol = '\0';
                        try
                        {
                            badSymbol = Convert.ToChar(trainResult);
                        }
                        catch { }

                        objects.Correct(symbol, badSymbol, points);
                        
                        rtbReaction.Invoke(prntReaction, (trainResult + "->" + symbol.ToString() + " (" + Path + ")\n"));
                        objects.Save();

                        bool thisSymbTrain = false;
                        while(!thisSymbTrain)
                        {
                            result++;//отметить, что при данном проходе были ошибки
                            thisSymbTrain = Training(symbol, k);
                        }
                    }
                }
            }

            return result;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="symbol">Имя символа</param>
        /// <param name="k">Вид символа</param>
        /// <returns>Возникла ли ошибка</returns>
        private bool Training(char symbol, int k)
        {
            if (!training)
                return true;

            bool result = true;

            string Path = symbols[symbol][k];
            Preview.Load(Path);

            points.Clear();
            points.Add(1);

            Bitmap temp = Preview.Invoke(getBitmapImage) as Bitmap;

            for (int i = 0; i < temp.Width; i++)
            {
                for (int j = 0; j < temp.Height; j++)
                {
                    Color current = temp.GetPixel(i, j);
                    if (current.R > 127 && current.G > 127 && current.B > 127)
                        temp.SetPixel(i, j, Color.White);
                    else
                        temp.SetPixel(i, j, Color.Black);
                }
            }
            for (int i = 1; i < points.Capacity; i++)
            {
                points.Add(temp.GetPixel((i - 1) % temp.Width, (i - 1) / temp.Width).ToArgb() != -1 ? (byte)1 : (byte)0);
            }

            string trainResult = objects.Recognize(points);
            Result.Invoke(prntResult, (trainResult));//вызов делегата, где второй аргумент - аргумент для делегата

            if (trainResult != symbol.ToString())
            {
                char badSymbol = '\0';

                try
                {
                    badSymbol = Convert.ToChar(trainResult);
                }
                catch {}

                objects.Correct(symbol, Convert.ToChar(trainResult), points);

                rtbReaction.Invoke(prntReaction, (trainResult + "->" + symbol.ToString() + " (" + Path + ")\n"));
                objects.Save();
                
                return false;
            }

            return result;
        }


        //[STAThreadAttribute]
        private void bgwTraining_DoWork(object sender, DoWorkEventArgs e)
        {
            prntReaction = PrintReaction;//записать функцию вывода в объект делегата
            prntResult = PrintResult;
            getBitmapImage = GetBitmapImage;
            prntCorrections = PrintCorrections;

            symbols.Clear();

            foreach (string s in images) //для всех полных адресов изображений
            {
                string name = "";
                char n = '\0';

                for (int i = s.Length - 1; i > -1; i--)
                {
                    if (s[i] != '\\')
                        name = s[i].ToString() + name;
                    else break;
                }

                if (name != "")
                {
                    for (int i = 0; i < name.Length; i++)
                    {
                        if (name[i] == '_' && i < name.Length)
                        {
                            n = name[i + 1];
                            break;
                        }
                    }
                }

                if (n != '\0')
                {
                    if (symbols.ContainsKey(n))
                    {
                        symbols[n].Add(s);
                    }
                    else
                    {
                        symbols.Add(n, new List<string>());
                        symbols[n].Add(s);
                    }
                }
            }

            if (symbols.Keys.Count > 0) //если среди картинок есть подходящие для обучения
            {
                symbs=symbols.Keys.ToList();
                rtbReaction.Invoke(prntReaction, ("-! Обучение запущено\n"));//вызов делегата, где второй аргумент - аргумент для делеата
                training = true;
            }
            else return;

            ////обучение
            int r = -1; //количество исправлений при проходе
            while (r!=0 && training)
            {
                r = Training();
                tbCorrectionCounts.Invoke(prntCorrections, r);//записать количество исправлений при данном проходе
            }
            training = false;
            rtbReaction.Invoke(prntReaction, ("-! Обучение завершено\n\n"));
        }

        private void btBreakTrain_Click(object sender, EventArgs e)
        {
            if(bgwTraining.IsBusy && training == true)
            {
                rtbReaction.Text += "-! Прерывание обучения...\n";
                training = false;
            }
        }
    }
}

using System;
using System.IO;
using System.Collections.Generic;
//using System.Linq;
//using System.Text;

namespace NumbersSearcher
{
    //нейрон
    public class Neuron
    {
        //поля класса:
        StreamReader sr;//содержимое файла связей символа
        FileStream file;
        StreamWriter sw;
        
        //Метка изменения 
        bool change = false;

        //последнее выданное значение 
        int lastY = -1;

        //порядковый номер символа
        int Num=-1;

        public int LastY
        { get { return lastY; } }
        char symbol;
        public char Symbol
        { get { return symbol; } }
        public List<double> w;
        int pointCount = 0;
        public int PointCount
        { get { return pointCount; } }

        String FileName = "";
        private void SetFileName()
        { FileName = Num.ToString() + ".txt"; }  //FileName = Num.ToString() + symbol.ToString() + ".txt";

        //методы

        //создание нейрона
        //попытка прочитать данные из файла с именем (Num+".txt") //(символ+".txt")
        //при неудачном чтении из файла - заполнение матрицы весов случайными числами
        public Neuron(char RecognizingSymbol, int num, int PointCount)
        {
            symbol = RecognizingSymbol;
            Num = num;
            SetFileName();
            pointCount = PointCount + 1;
            w = new List<double>(pointCount);
            try
            {
                file = new FileStream(FileName, FileMode.Open);
                sr = new StreamReader(file);
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    double temp;
                    double.TryParse(line, out temp);
                    w.Add(temp);
                }
                sr.Close();
                file.Close();
            }
            catch //если не получилось получить данные из файла весов данного символа
            {
                w.Clear();
                change = true;
                FillW();//заполнить матрицу весов случайными величинами
            }

            if (w.Count < pointCount)
            {
                w.Clear();
                change = true;
                FillW();
            }
                
        }

        //заполнение матрицы весов случайными числами
        private void FillW()
        {
            Random random = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < w.Capacity; i++)
                w.Add(random.NextDouble() * 0.3 * (random.Next(2)==1?-1:1));
        }
        
        
        //корректировка матрицы весов
        public void Correct(List<byte> x, int delta, double speed)
        {
            change = true;
            for (int i = 0; i < w.Count; i++)
                w[i] = w[i]+ (double)speed * delta * x[i]; 
        }

        //суммирование
        double S(List<byte> x)
        {
            double result = 0;
            for (int i = 0; i < x.Count; i++)
            {
                result += w[i] * x[i];
            }
            return result;
        }

        //активационная функция
        public double Y(List<byte> x)
        {
            double Result;
            Result = S(x);
            if (Result >= 0) return Result;
            else return 0;
        }

        //сохранение значений в файл
        public void Save()
        {
            if (!change) return;

            file = File.Create(FileName);
            sw = new StreamWriter(file);
            foreach(double ww in w)    
            {
                sw.WriteLine(ww.ToString());
                sw.Flush();
            }
            sw.Close();
            file.Close();

            change = false;
        }
        
    }
}

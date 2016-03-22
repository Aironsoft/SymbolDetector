﻿//using System;
using System.Collections.Generic;

//using System.Linq;
//using System.Text;

namespace NumbersSearcher
{
    //вспомогательный класс - хранит все объекты системы и предоставляет интерфейс доступа к ним
    public class Holder
    {
        double speed = 0.5;
        char lastCorrect;

        public double Speed
        {
            get { return speed; }
            set { speed = value; }
        }
        Net net;// = new Net();

        public string Recognize(List<byte> x)
        {
            recognition = net.Recognize(x);
            if (recognition == '\n')
                return "не удалось";
            return recognition.ToString();
        }

        char recognition;
        public char Recognition
        {
            get { return recognition; }
        }

      
        private int state = 0;
        public int State
        {
            get { return state; }
            set { state = value; }
        }
        private static Holder instance;
        private Holder(int width, int height)
        {
            net = new Net(width * height);
        }
        public static Holder GetInstance(int width, int height)
        {
            if (instance == null)
            {
                instance = new Holder(width, height);
            }
            return instance;
        }
        public void AddNeuron(char symbol)
        {
            net.AddNeuron(symbol);
        }


        public string Correct(char symbol, char badSymbol, List<byte> x)
        {
            if (symbol == lastCorrect)
                speed *= 1.05;
            else if (speed > 0.1 && net.GetNeuron(symbol) != null) //если скорость > 0.1 и сеть не обучена такому символу
            {
                if (speed < 0.5)
                    speed = speed * 0.9;
                else
                    speed = 0.4;
                lastCorrect = symbol;
            }

            recognition = net.Correct(symbol, badSymbol, x, speed);
            if (recognition == '\n')
                return "не удалось";

            return recognition.ToString();
        }


        public void Save()
        {
            net.Save();
        }
        public List<double> GetNeuron(char s)
        {
            return net.GetNeuron(s);
        }
        
    }
}

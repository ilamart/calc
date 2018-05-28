using System;
using System.Collections.Generic;

namespace calc
{
    class Program
    {
        static void Main(string[] args)
            {
            ConsoleKeyInfo es;
            string input;
            try
            {
                do
                {
                    Console.Write("Введите выражение: ");
                    StringCalc rez = new StringCalc();
                    input = Console.ReadLine();
                    rez.Calculate(input);
                    Console.WriteLine(rez._result);
                    Console.WriteLine("ESC для выхода");
                    es = Console.ReadKey();
                } while (es.Key != ConsoleKey.Escape);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
            }
        }
    }

    public class Operation
    {

        public string operations = "+-/*^()";

        //public void OperationPlus
        //{
        //    result = (double.parse(mas[i - 2]) + double.parse(mas[i - 1])).tostring();
        //    mas[i - 2] = result;
        //    for (int j = i - 1; j<mas.length - 2; j++)
        //        mas[j] = mas[j + 2];
        //    array.resize(ref mas, mas.length - 2);
        //    i -= 2;
        //}
        public double Evalute(string output)
        {
            string result;
            string[] mas = output.Split(' ');
            for (int i = 0; i < mas.Length; i++)

                switch (mas[i])
                {
                    case "+":
                        result = (double.Parse(mas[i - 2]) + double.Parse(mas[i - 1])).ToString();
                        mas[i - 2] = result;
                        for (int j = i - 1; j < mas.Length - 2; j++)
                            mas[j] = mas[j + 2];
                        Array.Resize(ref mas, mas.Length - 2);
                        i -= 2;
                        break;
                    case "-":
                        result = (double.Parse(mas[i - 2]) - double.Parse(mas[i - 1])).ToString();
                        mas[i - 2] = result;
                        for (int j = i - 1; j < mas.Length - 2; j++)
                            mas[j] = mas[j + 2];
                        Array.Resize(ref mas, mas.Length - 2);
                        i -= 2;
                        break;
                    case "*":
                        result = (double.Parse(mas[i - 2]) * double.Parse(mas[i - 1])).ToString();
                        mas[i - 2] = result;
                        for (int j = i - 1; j < mas.Length - 2; j++)
                            mas[j] = mas[j + 2];
                        Array.Resize(ref mas, mas.Length - 2);
                        i -= 2;
                        break;
                    case "/":
                        result = (double.Parse(mas[i - 2]) / double.Parse(mas[i - 1])).ToString();
                        mas[i - 2] = result;
                        for (int j = i - 1; j < mas.Length - 2; j++)
                            mas[j] = mas[j + 2];
                        Array.Resize(ref mas, mas.Length - 2);
                        i -= 2;
                        break;
                }
            return double.Parse(mas[0]);
        }
    }
}
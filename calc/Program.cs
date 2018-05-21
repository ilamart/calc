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
                    RPN rez = new RPN();  
                    input = Console.ReadLine();
                    rez.Calculate(input, rez);
                    Console.WriteLine(rez.result);
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
    class RPN
    {
        Operation op = new Operation();
        public RPN()
        {

        }
        public Nullable<double> result;

        public Nullable<double> Result
        {
            set
            {
                try
                {
                result = value;
                }
                catch (Exception er)
                {
                    Console.WriteLine("Ошибка: " + er.Message);
                }
            }
            get
            {
                    return result;
            }
        }

        public void Calculate(string input, RPN abc)
        {
            string output = GetExpression(input);
            abc.result = op.Evalute(output);
        }
           
        public string GetExpression(string input)
        {
            string output = string.Empty;
            
            Stack<char> operStack = new Stack<char>();
            for (int i = 0; i < input.Length; i++)
            {
                if (Is_Delimeter(input[i]))
                    continue; 

                if (input[i] == '-' && ((i > 0 && !Char.IsDigit(input[i - 1])) || i == 0))
                {
                    i++;
                    output += "-";  
                }

                if (Char.IsDigit(input[i]))
                {
                    while (!Is_Delimeter(input[i]) && !(op.operations.IndexOf(input[i]) != -1))
                    {
                        output += input[i];
                        i++;
                        if (i == input.Length) break;
                    }
                    output += " ";
                    i--;
                }

                if (op.operations.IndexOf(input[i])!= -1)
                {
                    if (input[i] == '(')
                        operStack.Push(input[i]); 
                    else if (input[i] == ')') 
                    {
                        char s = operStack.Pop();
                        while (s != '(')
                        {
                            output += s.ToString() + ' ';
                            s = operStack.Pop();
                        }
                    }
                    else
                    {
                        if (operStack.Count > 0) 
                            if (GetPriority(input[i]) <= GetPriority(operStack.Peek()))
                                output += operStack.Pop().ToString() + " ";
                        operStack.Push(char.Parse(input[i].ToString()));
                    }
                }
            }

            while (operStack.Count > 0)
                output += operStack.Pop() + " ";
            return output;
        }

        static private byte GetPriority(char s)
        {
            switch (s)
            {
                case '(': return 0;
                case ')': return 1;
                case '+': return 2;
                case '-': return 3;
                case '*': return 4;
                case '/': return 4;
                default: return 5;
            }
        }

        static private bool Is_Delimeter(char c)
        {
            if ((" =".IndexOf(c) != -1))
                return true;
            return false;
        }
    }

    public class Operation
    {
        public string operations = "+-/*^()";

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
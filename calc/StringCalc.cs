using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calc
{
    class StringCalc
    {
        Operation op= new Operation();
        public StringCalc()
        {

        }
        public double? _result;

        private double? Result
        {
            set
            {
                try
                {
                    _result = value;
                }
                catch (Exception er)
                {
                    Console.Error.WriteLine("Ошибка: " + er.Message);
                    //Console.WriteLine("Ошибка: " + er.Message);
                }
            }
            get
            {
                return _result;
            }
        }

        public void Calculate(string input)
        {
            string output = GetExpression(input);
            _result = op.Evalute(output);
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

                if (op.operations.IndexOf(input[i]) != -1)
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
}

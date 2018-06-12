using System;
using System.Collections.Generic;

namespace calc
{
    class StringCalc
    {
        private static Operation[] _operations = { new OperationPlus(), new OperationMinus(), new OperationMultiply(), new OperationDivide(), new OperationExpone(), new OperationMyDivide() };

        private static Dictionary<string,int> Oper = new Dictionary<string,int>
        {
                { "(", 0 },
                { ")", 1 }
        };

        public StringCalc()
        {
        }
        public double _result;

        private double Result
        {
            set
            {
                    _result = value;
            }
            get
            {
                return _result;
            }
        }

        public void Calculate(string input)
        {
            string output = GetExpression(input);
            _result = Analysis(output);
        }
        
        public string GetExpression(string input)
        {
            string output = string.Empty;
            Stack<char> operStack = new Stack<char>();

            foreach (Operation elem in _operations)
                if (!Oper.ContainsKey(elem.Code))
                    Oper.Add(elem.Code, elem.Prior);

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
                    while (!Is_Delimeter(input[i]) && !(Oper.ContainsKey(input[i].ToString())))
                    {
                        output += input[i];
                        i++;
                        if (i == input.Length) break;
                    }
                    output += " ";
                    i--;
                }

                if (Oper.ContainsKey(input[i].ToString()))
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
                            if (GetPriority(input[i].ToString()) <= GetPriority(operStack.Peek().ToString()))
                                output += operStack.Pop().ToString() + " ";
                        operStack.Push(char.Parse(input[i].ToString()));
                    }
                }
            }
            while (operStack.Count > 0)
                output += operStack.Pop() + " ";
            return output;
        }

        static private int GetPriority(string s)
        {
            if (_operations != null)
            {
                foreach (Operation elem in _operations)
                    if (elem.Code == s)
                        return elem.Prior;
            }
            return 0;
        }

        static private bool Is_Delimeter(char c)
        {
            if ((" =".IndexOf(c) != -1))
                return true;
            return false;
        }

        public double Analysis(string output)
        {
            List<string> list = new List<string>(output.Split(' '));
            foreach (var s in list)
            {
                Console.WriteLine(s + " ");
            } 
            double DubResult;

            for (int i = 0; i < (list.Count); i++)
            {
                foreach (Operation item in _operations)
                {
                    if (item.Code == list[i])
                    {
                        DubResult = item.Evaluate(double.Parse(list[i - 2]), double.Parse(list[i - 1]));
                        list[i - 2] = DubResult.ToString();
                        for (int j = i - 1; j < list.Count - 2; j++)
                            list[j] = list[j + 2];
                        list.RemoveRange(list.Count - 2,2);
                        i -= 2;
                    }
                }
            }
            return double.Parse(list[0]);
        }
    }
}

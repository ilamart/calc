using System;


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
                    StringCalc rez = new StringCalc();
                    Console.Write("Введите выражение: ");
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

    public abstract class Operation
    {
        public abstract string Code { get; }

        public abstract double Evaluate(double oper1, double oper2);    
    }

    public class OperationPlus : Operation
    {
        public override string Code { get { return "+"; } }

        public override double Evaluate(double oper1, double oper2)
        {
            return (oper1 + oper2);
        }
    }

    public class OperationMinus : Operation
    {
        public override string Code { get { return "-"; } }

        public override double Evaluate(double oper1, double oper2)
        {
            return (oper1 - oper2);
        }
    }

    public class OperationMultiply : Operation
    {
        public override string Code { get { return "*"; } }

        public override double Evaluate(double oper1, double oper2)
        {
            return (oper1 * oper2);
        }
    }

    public class OperationDivide : Operation
    {
        public override string Code { get { return "/"; } }

        public override double Evaluate(double oper1, double oper2)
        {
            return (oper1 / oper2);
        }
    }

    public class OperationExpone : Operation
    {
        public override string Code { get { return "^"; } }

        public override double Evaluate(double oper1, double oper2)
        {
            for (int i=0; i<(oper2-1); i++)
            {
                oper1 *= oper1;
            }
            return (oper1);
        }
    }

    public class OperationMyDivide : Operation
    {
        public override string Code { get { return "%"; } }

        public override double Evaluate(double oper1, double oper2)
        {
            int op1 = Convert.ToInt32(oper1);
            int op2 = Convert.ToInt32(oper2);
            int rez = op1 % op2;
            return rez;
        }
    }
}

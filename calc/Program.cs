using System;

namespace calc
{
    class Program
    {
        static void Main(string[] args)
            {
            ConsoleKeyInfo es;
            try
            {
                do
                {
                    StringCalc rez = new StringCalc();
                    Console.Write("Введите выражение: ");
                    rez._result = rez.Calculation(rez.ConvertNotation(Console.ReadLine()));
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
        public abstract string OpCode { get; }

        public abstract int Priority { get; }

        public abstract double GetResult(double operand1, double operand2);    
    }

    public class OperationPlus : Operation
    {
        public override int Priority { get { return 2; } }

        public override string OpCode { get { return "+"; } }

        public override double GetResult(double operand1, double operand2)
        {
            return operand1 + operand2;
        }
    }

    public class OperationMinus : Operation
    {
        public override int Priority { get { return 3; } }

        public override string OpCode { get { return "-"; } }

        public override double GetResult(double operand1, double operand2)
        {
            return operand1 - operand2;
        }
    }

    public class OperationMultiply : Operation
    {
        public override int Priority { get { return 4; } }

        public override string OpCode { get { return "*"; } }

        public override double GetResult(double operand1, double operand2)
        {
            return operand1 * operand2;
        }
    }

    public class OperationDivide : Operation
    {
        public override int Priority { get { return 5; } }

        public override string OpCode { get { return "/"; } }

        public override double GetResult(double operand1, double operand2)
        {
            return operand1 / operand2;
        }
    }

    public class OperationExpone : Operation
    {
        public override int Priority { get { return 6; } }

        public override string OpCode { get { return "^"; } }

        public override double GetResult(double operand1, double operand2)
        {
            for (int i=0; i<(operand2 - 1); i++)
            {
                operand1 *= operand1;
            }
            return operand1;
        }
    }

    public class OperationMyDivide : Operation
    {
        public override int Priority { get { return 7; } }

        public override string OpCode { get { return "%"; } }

        public override double GetResult(double operand1, double operand2)
        {
            int oper1 = Convert.ToInt32(operand1);
            int oper2 = Convert.ToInt32(operand2);
            int rez = oper1 % oper2;
            return rez;
        }
    }
}

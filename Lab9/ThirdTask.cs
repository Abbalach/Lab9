using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9
{
    delegate double Calculation(double firstArg, double secondArg);

    class ThirdTask
    {
        Calculation Add { get; }
        Calculation Sub { get; }
        Calculation Mul { get; }
        Calculation Div { get; }

        

        public ThirdTask()
        {
            Add = (double firstArg, double secondArg) =>
            {
                return firstArg + secondArg;
            };
            Sub = (double firstArg, double secondArg) =>
            {
                return firstArg - secondArg;
            };
            Mul = (double firstArg, double secondArg) =>
            {
                return firstArg * secondArg;
            };
            Div = (double firstArg, double secondArg) =>
            {
                if (secondArg == 0)
                {
                    return double.NaN;
                }
                return firstArg / secondArg;
            };
        }

        public double Calculate(string expression)
        {

            List<char> symbols = expression.ToList();
            for (int i = 0; i < symbols.Count; i++)
            {
                if (symbols[i] == ' ' )
                {
                    symbols.RemoveAt(i);
                }
                if (symbols[i] == '.')
                {
                    symbols[i] = ',';
                }
            }
            if (symbols.Last() == '=')
            {
                symbols.RemoveAt(symbols.Count - 1);
            }
            expression = new string(symbols.ToArray());

            List<Calculation> LowerOrderOperations = new List<Calculation>();

            for (int i = 0; i < expression.Length; i++)
            {
                if (expression[i] == '+')
                {
                    LowerOrderOperations.Add(Add);
                }
                if (expression[i] == '-')
                {
                    LowerOrderOperations.Add(Sub);
                }
            }
            var LowerOrderExp = expression.Split('+','-');

            double[] mulResults = new double[LowerOrderExp.Length];

            // операции умножения и деления
            for (int i = 0; i < LowerOrderExp.Length; i++)
            {
                List<Calculation> HigherOrderOperations = new List<Calculation>();

                for (int j = 0; j < LowerOrderExp[i].Length; j++)
                {
                    if (LowerOrderExp[i][j] == '*')
                    {
                        HigherOrderOperations.Add(Mul);
                    }
                    if (LowerOrderExp[i][j] == '/')
                    {
                        HigherOrderOperations.Add(Div);
                    }
                }
                //Console.WriteLine("кол-во операций = " + HigherOrderOperations.Count);

                var HigherOrderExp = LowerOrderExp[i].Split('*', '/');

                //Console.WriteLine("кол-во операндов = " + HigherOrderExp.Length);

                double mulresultHelper = double.Parse(HigherOrderExp[0]);

                //Console.WriteLine("кол-во операндов = " + HigherOrderExp.Length);

                for (int j = 0; j < HigherOrderOperations.Count; j++)
                {
                    mulresultHelper = BasicOperationsHandler(mulresultHelper, double.Parse(HigherOrderExp[j + 1]), HigherOrderOperations[j]);
                    if (mulresultHelper == double.NaN)
                    {
                        return double.NaN;
                    }
                }
                mulResults[i] = mulresultHelper;
            }

            double finalResult = mulResults[0];

            // операции сложения и вычитания
            for (int i = 1; i < mulResults.Length; i++)
            {
                finalResult = BasicOperationsHandler(finalResult, mulResults[i], LowerOrderOperations[i - 1]);
            }

            return finalResult;
        }

        //да-да, я знаю, что хэндлерами обычно называют делегаты, но надо же как-то этот метод назвать
        double BasicOperationsHandler(double firstArg, double secondArg, Calculation operation)
        {
            return operation(firstArg, secondArg);
        }
    }
}

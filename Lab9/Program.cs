using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9
{

    class Program
    {
        delegate double Average(int firstArg, int secondArg, int thirdArg);

        delegate int RandomInt();
        delegate double AverageOfDelegates(RandomInt[] randomInt);

       

        static void Main(string[] args)
        {
            // 1
            Average average = delegate (int firstArg, int secondArg, int thirdArg)
            {
                return Convert.ToDouble(firstArg + secondArg + thirdArg) / 3;
            };



            // 2
            Random random = new Random();
            AverageOfDelegates averageOfDelegates = delegate(RandomInt[] randomInts)
            {
                double sum = 0;
                foreach (var randomInt in randomInts)
                {
                    sum += randomInt();
                }
                return sum / randomInts.Length;
            };

            RandomInt[] delegArray = new RandomInt[random.Next(1,100)];
            for (int i = 0; i < delegArray.Length; i++)
            {
                delegArray[i] = () =>
                {
                    return random.Next();
                };
            }

            Console.WriteLine(averageOfDelegates(delegArray));



            //3 : так как полноценный калькулятор уже делался и задания по сути на делегаты, то я решил, что замарачиваться с сложной проверкой строки и 
            //отрицательными числами будет лишним
            Console.WriteLine();

            Console.WriteLine("Expression:");
            string expression = Console.ReadLine();

            ThirdTask thirdTask = new ThirdTask();
            Console.WriteLine(thirdTask.Calculate(expression));


            Console.ReadKey();
        }
    }
}

using System;
using BabyStroller.SDK;

namespace Animals.Lib
{
    [Unfinished]//使用类库中的特性
    public class Cat : IAnimal
    {
        public void Voice(int times)
        {
            for (int i = 0; i < times; i++)
            {
                Console.WriteLine("Meow!");
            }
        }
    }
}

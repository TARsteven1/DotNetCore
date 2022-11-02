using System;

namespace MyUnitTest
{
    //示例类，输入电量，根据数值返回对应的字符串
    class Program
    {
        static void Main(string[] args)
        {
            var fan = new DeskFan(new PowerSupply());
            Console.WriteLine(fan.Work());
        }


    }

    public class PowerSupply:IPowerSupply
    {
        public int GetPower()
        {
            return 100;
        }
    }
    public interface IPowerSupply
    {
        int GetPower();
    }
    public class DeskFan
    {
        private IPowerSupply _powerSupply;
        public DeskFan(IPowerSupply powerSupply)
        {
            _powerSupply = powerSupply;
        }

        public string Work()
        {
            int power = _powerSupply.GetPower();
            if (power<=0)
            {
                return "fasle";
            }
            else if (power<100)
            {

                return "slow";
            }
            else if(power < 200)
            {
                return "ture";
            }
            else
            {
                return "Err";
            }
        }
    }
}

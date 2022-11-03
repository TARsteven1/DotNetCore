using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;//引用依赖注入包（NuGet）

namespace MyReflection
{
    //.Net Core和.NetFramework所引用的反射类库是不同的

    class Program
    {
        static void Main(string[] args)
        {
            //反射原理
            Console.WriteLine("反射");
            ITank tank = new HeavyTank();
            var t = tank.GetType();//获取对象静态类型的描述，所以返回Type类型（其实叫TypeInfo更为贴切）
            object o = Activator.CreateInstance(t);//使用激活器创建对象
            //重点来了，使用反射了（这是种直接使用反射的形式，但一般很少直接用，此例只用于了解反射的原理）
            MethodInfo fireMi = t.GetMethod("Fire");//得到Fire方法
            MethodInfo runMi = t.GetMethod("Run");
            fireMi.Invoke(o, null);//调用Fire方法
            runMi.Invoke(o, null);


            //依赖注入（反射的第一个用途）
            Console.WriteLine("依赖注入");
            var sc = new ServiceCollection();//实例化一个容器
            sc.AddScoped(typeof(ITank), typeof(MediumTank));//把一对类型放入容器（typeof才能拿到静态类型的动态描述）
            sc.AddScoped(typeof(IVehicle), typeof(Car));
            sc.AddScoped<Driver>();
            var sp = sc.BuildServiceProvider();//以上是一次性的依赖注入注册（可以在程序启动时注册）

            //以下是容器的使用
            ITank tank1 = sp.GetService<ITank>();
            tank1.Fire();
            tank1.Run();//依赖注入最基本的用法，从容器中创建实例，并且比传统方式更衣修改，如果想更改实例类型，只需在修改注册容器时的类型即可（27行）
            Console.WriteLine("######");
            var driver = sp.GetService<Driver>();
            driver.Drive();

            Console.WriteLine("传统方式实现");
            var driver1 = new Driver(new Car());
            driver1.Drive();//结果一致，由此可见依赖注入的强大

            //z注入 体现在：我们使用注册的类型，直接注入到目标类的构造器中
            //上述例子，将Car类型注入到Driver的构造器中来替代所需的IVehicle

        }
    }
    class Driver
    {
        private IVehicle _vehicle;
        public Driver(IVehicle vehicle)
        {
            _vehicle = vehicle;
        }
        public void Drive()
        {
            _vehicle.Run();
        }
    }
    interface IVehicle
    {
        void Run();
    }
    interface ITank : IVehicle
    {
        void Fire();
    }
    class Car : IVehicle
    {
        public void Run()
        {
            Console.WriteLine("Car Run!");

        }
    }
    class HeavyTank : ITank
    {
        public void Fire()
        {
            Console.WriteLine("HeavyTank Fire!");
        }

        public void Run()
        {
            Console.WriteLine("HeavyTank Run!");
        }
    } 
    class MediumTank : ITank
    {
        public void Fire()
        {
            Console.WriteLine("MediumTank Fire!");
        }

        public void Run()
        {
            Console.WriteLine("MediumTank Run!");
        }
    }
}

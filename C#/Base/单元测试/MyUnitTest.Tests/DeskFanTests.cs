using System;
using Xunit;
using Moq;
//一般单元测试项目的名称就是被测试项后加.Tests
namespace MyUnitTest.Tests
{
    //每个类文件就是一组TestCase
    //一个TestCase就是一个方法

    //*在测试前要添加被测试项为该项目的引用
    public class DeskFanTests
    {
        [Fact]//特征Attribute,Fact特征就表示该方法就是一个TestCase
        public void PowerLowerThanZero_OK()//电量小于0的TestCase
        {
            var fan = new DeskFan(new PowerSupplyLowerThanZero());

            var expected = "fasle"; //测试目标

            var actual = fan.Work();//测试结果

            Assert.Equal(expected, actual);//对比测试结果

            //点击测试浏览框的运行按钮进行测试
            //***最后一定注意这个测试项目的路径，决不能在被测项目之内！！！！

            //使用Moq来避免创建很多单一测试类，Nut中下载Moq
            var mock = new Mock<IPowerSupply>();
            mock.Setup(ps => ps.GetPower()).Returns(() => 0);//写到此处就不用再创建单一测试类了
            var fan2 = new DeskFan(mock.Object);//将新建的Moq测试类传入构造器中


            var expected2 = "fasle"; //测试目标

            var actual2 = fan2.Work();//测试结果

            Assert.Equal(expected2, actual2);//对比测试结果
        }
    }

    class PowerSupplyLowerThanZero : IPowerSupply//专门新建用于TestCase的类，类名很丑，之后会通过使用Moq方案代替
    {
        public int GetPower()
        {
            return 0;
        }
    }
}

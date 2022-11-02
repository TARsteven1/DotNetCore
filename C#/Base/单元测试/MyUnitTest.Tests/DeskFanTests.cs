using System;
using Xunit;
using Moq;
//һ�㵥Ԫ������Ŀ�����ƾ��Ǳ���������.Tests
namespace MyUnitTest.Tests
{
    //ÿ�����ļ�����һ��TestCase
    //һ��TestCase����һ������

    //*�ڲ���ǰҪ��ӱ�������Ϊ����Ŀ������
    public class DeskFanTests
    {
        [Fact]//����Attribute,Fact�����ͱ�ʾ�÷�������һ��TestCase
        public void PowerLowerThanZero_OK()//����С��0��TestCase
        {
            var fan = new DeskFan(new PowerSupplyLowerThanZero());

            var expected = "fasle"; //����Ŀ��

            var actual = fan.Work();//���Խ��

            Assert.Equal(expected, actual);//�ԱȲ��Խ��

            //����������������а�ť���в���
            //***���һ��ע�����������Ŀ��·�����������ڱ�����Ŀ֮�ڣ�������

            //ʹ��Moq�����ⴴ���ܶ൥һ�����࣬Nut������Moq
            var mock = new Mock<IPowerSupply>();
            mock.Setup(ps => ps.GetPower()).Returns(() => 0);//д���˴��Ͳ����ٴ�����һ��������
            var fan2 = new DeskFan(mock.Object);//���½���Moq�����ഫ�빹������


            var expected2 = "fasle"; //����Ŀ��

            var actual2 = fan2.Work();//���Խ��

            Assert.Equal(expected2, actual2);//�ԱȲ��Խ��
        }
    }

    class PowerSupplyLowerThanZero : IPowerSupply//ר���½�����TestCase���࣬�����ܳ�֮���ͨ��ʹ��Moq��������
    {
        public int GetPower()
        {
            return 0;
        }
    }
}

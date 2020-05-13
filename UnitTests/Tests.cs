using System;
using System.Collections.Generic;
using System.ComponentModel;
using NUnit.Framework;
using Lab_12;

namespace UnitTests
{
    [TestFixture]
    public class Tests
    {

        [Test]
        public void Task1()
        { 
            Program.Task1A(1);
            Program.Task1B();
            Program.Task1C();
            Assert.True(true);
        }
        [Test]
        public void Task2()
        { 
            Program.Task2();
            Assert.True(true);
        }
        
        [Test]
        public void CustomStackTest()
        { 
            CustomStack tmp = new CustomStack(5);
//            Production cur = (Production) tmp.Current;
            tmp.Reset();
            
            CustomStack tmp2 = new CustomStack();
            CustomStack tmp3 = new CustomStack(tmp);

            CustomStack tmp1Copy = tmp.Copy();
            CustomStack tmp1Clone = tmp.Clone();
            Assert.True(true);
        }
        
        [Test]
        public void PoinstTest()
        { 
            Point tmp = new Point();
            String tmpstr = tmp.ToString();
            
            PointTree tmp2 = new PointTree();
            tmpstr = tmp2.ToString();
            Assert.True(true);
        }
    }
}
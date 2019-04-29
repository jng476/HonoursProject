using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Unit_Tests
{
    [TestClass]
    public class GradeTest
    {
        string markText;
        /* Calculates Grade from mark */
        private void markWork(double mark)
        {
            mark = Math.Round((mark / 25) * 100); //Rounds Mark

            //If Mark is over 100 set to 100
            if (mark > 100)
            {
                mark = 100;
            }

            markText = ""; //Clear Mark Label

            //If Statement for grading Mark
            if (mark >= 95)
            {
                markText = "A:1";
            }
            else if (mark >= 89 && mark < 95)
            {
                markText = "A:2";
            }
            else if (mark >= 83 && mark < 89)
            {
                markText = "A:3";
            }
            else if (mark >= 76 && mark < 83)
            {
                markText = "A:4";
            }
            else if (mark >= 70 && mark < 76)
            {
                markText = "A:5";
            }
            else if (mark >= 67 && mark < 70)
            {
                markText = "B:1";
            }
            else if (mark >= 64 && mark < 67)
            {
                markText = "B:2";
            }
            else if (mark >= 60 && mark < 64)
            {
                markText = "B:3";
            }
            else if (mark >= 57 && mark < 60)
            {
                markText = "C:1";
            }
            else if (mark >= 54 && mark < 57)
            {
                markText = "C:2";
            }
            else if (mark >= 50 && mark < 54)
            {
                markText = "C:3";
            }
            else if (mark >= 47 && mark < 50)
            {
                markText = "D:1";
            }
            else if (mark >= 44 && mark < 47)
            {
                markText = "D:2";
            }
            else if (mark >= 40 && mark < 44)
            {
                markText = "D:3";
            }
            else if (mark >= 37 && mark < 40)
            {
                markText = "M:1";
            }
            else if (mark >= 34 && mark < 37)
            {
                markText = "M:2";
            }
            else if (mark >= 30 && mark < 34)
            {
                markText = "M:3";
            }
            else if (mark >= 20 && mark < 30)
            {
                markText = "CF";
            }
            else if (mark < 20)
            {
                markText = "BF";
            }

            markText += " " + mark + "%"; //Outputs Mark with grade
        }

        [TestMethod]
        public void TestMethod1()
        {
            markWork(17.5);
            Console.WriteLine(markText);
            Assert.AreEqual("A:5 70%", markText);
        }
    }
}

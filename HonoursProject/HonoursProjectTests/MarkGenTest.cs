using Microsoft.VisualStudio.TestTools.UnitTesting;
using HonoursProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HonoursProjectTests
{
    [TestClass]
    public class MarkGenTest
    {
        List<string>[] toTest = new List<string>[2];
        List<string>[] fromTest = new List<string>[2];

        /* Marks Generalizaations */
        private double markGen(List<string>[] to, List<string>[] from)
        {
            double mark = 0; //Variable to hold mark

            //Loops through Lectuers Parent Generalizations
            for (int i = 0; i < from[0].Count(); i++)
            {
                
                //Loop Through Students Parent Generalizations
                for (int j = 0; j < from[1].Count(); j++)
                {
                    string lower = from[0][i].ToLower(); //Lower Case Parent Class name
                    //Check if parents Classes match
                    if (lower.Contains(from[1][j].ToLower()))
                    {
                        lower = to[0][i].ToLower(); //Lower Case Child class From Lecturer solution
                        //Check if Child Class Match
                        if (lower.Contains(to[1][j].ToLower()))
                        {
                            mark++; //Award a Mark
                        }
                    }
                }
            }
            return mark; //Return Mark for Generalization
        }

        [TestMethod]
        public void TestMethod1()
        {
            for(int i =0; i<2; i++)
            {
                toTest[i] = new List<string>();
                fromTest[i] = new List<string>();
            }
            for (int i = 0; i < 2; i++)
            {
                toTest[i].Add("Start");
                fromTest[i].Add("End");
            }
            double newMark = markGen(toTest, fromTest);
            Assert.AreEqual(1, newMark);
        }
    }
}

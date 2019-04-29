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
    public class MarkRelationships
    {
        List<string>[] realName = new List<string>[2];
        List<string>[] realVal = new List<string>[2];

  
        /* Mark Relationships */
        private double markRel(List<string>[] classname, List<string>[] val)
        {
            double mark = 0; //Variable to store Mark
            //Loop through Lecturers Solution of Relationship Class Name
            for (int i = 0; i < classname[0].Count(); i += 2)
            {
                
                //Loop Through Students Solution of Relationship Class Names
                for (int j = 0; j < classname[1].Count(); j += 2)
                {
                    string lower = classname[0][i].ToLower(); //Lower Case The Class Name
                    //Check if one Part of Relationship Matches
                    if (lower.Contains(classname[1][j].ToLower()))
                    {
                        lower = classname[0][i + 1].ToLower(); //Lower Case the Class Name
                        //Check if other end of relationship matches
                        if (lower.Contains(classname[1][j + 1].ToLower()))
                        {
                            mark += 0.5f; //Award half a mark
                            //Check if one of the Multiplicity matches
                            if (val[0][i].Contains(val[1][j]))
                            {
                                mark += 0.25f; //Award Quarter of a Mark
                                //Check for second Multiplicty Matches
                                if (val[0][i + 1].Contains(val[1][j + 1]))
                                {
                                    mark += 0.25f; //Award Quarter of a Mark

                                }
                            }


                            break;

                        }
                    }
                }
            }
            return mark; //Return Mark
        }

        [TestMethod]
        public void TestMethod1()
        {
            for (int i = 0; i < 2; i++)
            {
                 realName[i] = new List<string>();
                 realVal[i] = new List<string>();
            }
            for (int i = 0; i < 2; i++)
            {
                realName[i].Add("Jamie");
                realName[i].Add("Daniel");
                realVal[i].Add("30");
                realVal[i].Add("8");
            }
            Assert.AreEqual(1, markRel(realName, realVal));
        }
    }
}

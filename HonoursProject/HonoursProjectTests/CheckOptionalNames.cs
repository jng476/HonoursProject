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
    public class CheckOptionalNames
    {
        List<string>[] exName = new List<string>[2];
        List<string> name = new List<string>();

        /* Checks Student Solution if optional Names are present and replaces with appropriate name */
        private void checkOptionalNames(List<string>[] exNames, List<string> names, int array)
        {
            //Loop through Studen Class Names
            for (int i = 0; i < names.Count(); i++)
            {
                //Loop through Option Names
                for (int j = 0; j < exNames[array + 1].Count(); j++)
                {
                    string lower = exNames[array][j].ToLower(); //Lower case the Option Names
                    //Check if student Classes Matches with Option name
                    if (lower.Contains(names[i].ToLower()))
                    {
                        names[i] = exNames[array + 1][j]; // Change the optional Name to the appropriate Name
                    }
                }
            }
        }
        [TestMethod]
        public void TestMethod1()
        {
            for (int i = 0; i < 2; i++)
            {
                exName[i] = new List<string>();
            }
            exName[0].Add("Pillow");
            exName[1].Add("Cushion");
            name.Add("Pillow");
            checkOptionalNames(exName, name, 0);
            Assert.AreEqual("Cushion", name[0]);
        }
    }
}

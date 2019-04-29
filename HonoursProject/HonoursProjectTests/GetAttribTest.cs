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
    public class GetAttribTest
    {
        List<string> attribNameTest = new List<string>();
        List<string> result = new List<string>();

        //Get Attributes from Class
        private List<string> getattribs(string className, List<string> attribName)
        {
            List<string> attribs = new List<string>(); //List of Attributes
            bool classVariables = false; //If attributes are in class

            //Loop through AttribNames
            for (int i = 0; i < attribName.Count(); i++)
            {
                //If Class variables are not in class
                if (classVariables == false)
                {
                    //If Attribute has tag Start
                    if (attribName[i].Contains(className + " Start"))
                    {
                        classVariables = true; //Attribute is in Class
                    }
                }
                else
                {
                    //Find End tag for End of Class
                    if (attribName[i].Contains(className + " End"))
                    {
                        classVariables = false; //Attributes Not in Class
                    }

                    //Add attributes to Attribute List if inside
                    if (classVariables == true)
                    {
                        attribs.Add(attribName[i]);
                    }
                }
            }

            return attribs; //Return List of attributes in class
        }

        [TestMethod]
        public void TestMethod1()
        {
            attribNameTest.Add("Photo Start");
            attribNameTest.Add("image");
            attribNameTest.Add("Photo End");
            result = getattribs("Photo", attribNameTest);

            Assert.AreEqual("image", result[0]);
        }
    }
}

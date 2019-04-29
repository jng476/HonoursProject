using Microsoft.VisualStudio.TestTools.UnitTesting;
using HonoursProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HonoursProject.Tests
{
    [TestClass()]
    public class UploadTests
    {
     
        List<string> names= new List<string>();
        List<string> nameID = new List<string>();
        
       
        /* Find Class Name */
        string FindName(string searchID)
        {
            //Loop throught all Class Name ID's 
            for (int i = 0; i < nameID.Count(); i++)
            {
                //If Name ID is Found
                if (nameID[i].Equals(searchID))
                {
                    return names[i]; //Return Name
                }
            }
            return null; //If nothing is found 
        }

        [TestMethod()]
        public void UploadTest()
        {
            names.Add("James");
            names.Add("Daniel");
            nameID.Add("132");
            nameID.Add("423");

            Assert.AreEqual("Daniel", FindName("423"));
            
        }
    }
}
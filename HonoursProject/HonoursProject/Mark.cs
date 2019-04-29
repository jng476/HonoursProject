using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.IO;

/* Josh Ng 08/02/2019
 * Honours Project 
 * Marking Windows Forum
*/

namespace HonoursProject
{
    public partial class Mark : Form
    {
        //Global Variables
        List<string>names = new List<string>(); //List that contains Class Names
        List<string> nameID = new List<string>(); //List that contains Class Name ID's
        double topMark = 0; //Calculates Top Mark
        string studentsMarks; //Contains all of Student Makrs

        /* Initializer for Mark */
        public Mark()
        {
            InitializeComponent();
            
        }

        /* Calculates Grade from mark */
        private void markWork(double mark)
        {
            mark = Math.Round((mark / topMark) * 100); //Rounds Mark

            //If Mark is over 100 set to 100
            if(mark > 100)
            {
                mark = 100;
            }
            
            markText.Text = ""; //Clear Mark Label

            //If Statement for grading Mark
            if (mark >= 95)
            {
                markText.Text = "A:1";
            }
            else if (mark >= 89 && mark < 95)
            {
                markText.Text = "A:2";
            }
            else if(mark >=83 && mark < 89)
            {
                markText.Text = "A:3";
            }
            else if (mark >= 76 && mark < 83)
            {
                markText.Text = "A:4";
            }
            else if (mark >= 70 && mark < 76)
            {
                markText.Text = "A:5";
            }
            else if (mark >= 67 && mark < 70)
            {
                markText.Text = "B:1";
            }
            else if (mark >= 64 && mark < 67)
            {
                markText.Text = "B:2";
            }
            else if (mark >= 60 && mark < 64)
            {
                markText.Text = "B:3";
            }
            else if (mark >= 57 && mark < 60)
            {
                markText.Text = "C:1";
            }
            else if (mark >= 54 && mark < 57)
            {
                markText.Text = "C:2";
            }
            else if (mark >= 50 && mark < 54)
            {
                markText.Text = "C:3";
            }
            else if (mark >= 47 && mark < 50)
            {
                markText.Text = "D:1";
            }
            else if (mark >= 44 && mark < 47)
            {
                markText.Text = "D:2";
            }
            else if (mark >= 40 && mark < 44)
            {
                markText.Text = "D:3";
            }
            else if (mark >= 37 && mark < 40)
            {
                markText.Text = "M:1";
            }
            else if (mark >= 34 && mark < 37)
            {
                markText.Text = "M:2";
            }
            else if (mark >= 30 && mark < 34)
            {
                markText.Text = "M:3";
            }
            else if (mark >= 20 && mark < 30)
            {
                markText.Text = "CF";
            }
            else if(mark < 20)
            {
                markText.Text = "BF";
            }

            markText.Text += " " + mark + "%"; //Outputs Mark with grade
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        /* Main Menu Button */
        private void button4_Click(object sender, EventArgs e)
        {
            Menu menu = new Menu(); //Create Menu Windows Forum
            this.Hide(); //Hides Mark Windows Forum
            menu.ShowDialog(); //Opens Menu Windows Forum
            this.Close(); //Closes Mark Windows Forum
        }

        /* Uploads a file taking in type, filepath and label to switch */
        private bool upload(string type, TextBox filePath, Label fileUpload)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog(); //Creates File Dialog for File explorer

            //Opens File Explorer and checks if user presses okay
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                filePath.Text = openFileDialog.FileName;  //Displays filepath

            }

            string path = filePath.Text; //Holds FilePath
            bool file_found = true; //Bool file being found

            //Checks if file dosn't exists and is not correct type
            if (!File.Exists(path) || !path.EndsWith(type, System.StringComparison.CurrentCultureIgnoreCase))
            {
                fileUpload.Text = "File Error"; //Display error
                fileUpload.ForeColor = Color.Red; //Change error to red
                file_found = false;
                return false;
            }
            // If file is correct
            if (file_found == true)
            {
                fileUpload.Text = "File Uploaded"; //Display upload successful
                fileUpload.ForeColor = Color.Green;
                return true;
            }
            return false;
        }

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

        /* Upload UML File */
        private void umlUpload()
        {
            names.Clear(); //Clear Class Name List
            nameID.Clear(); //Clear Class Name ID list
            //If a filepath isnt present
            if(filePath2.Text == "")
            {
                return;
            }

            XmlReader reader = XmlReader.Create(filePath2.Text); //Create Reader for XML file
            XmlReader getNames = XmlReader.Create(filePath2.Text); //Creates Reader for XML file to find Names

            //Loops through file to find names
            while (getNames.Read())
            {

                //If Element Class is found
                if ((getNames.NodeType == XmlNodeType.Element) && (getNames.Name == "Class"))
                {
                    //Checks if it has attributes
                    if (getNames.HasAttributes)
                    {
                        String Active = getNames.GetAttribute("Active");
                        //If string obtains something
                        if (!String.IsNullOrEmpty(Active))
                        {
                            names.Add(getNames.GetAttribute("Name")); //Add attribute Name to Class Name List
                            nameID.Add(getNames.GetAttribute("Id")); //Add Class Name ID to ID Lsit
                            textBox2.Text += "Class Name: " + getNames.GetAttribute("Name") + ",\r\n"; // Add line to solution with Class Name
                        }
                    }
                }
                //If Element Attribute is found
                if ((getNames.NodeType == XmlNodeType.Element) && (getNames.Name == "Attribute"))
                {
                    //Check if it has attributes
                    if (getNames.HasAttributes)
                    {
                        textBox2.Text += "Attribute Name: " + getNames.GetAttribute("Name") + ", Type: " + getNames.GetAttribute("Type"); //Add attribute Name and type to Solution textbox
                        textBox2.Text += ",\r\n"; //Add new line
                    }
                }
            }
            //Read XML file to get rest of information
            while (reader.Read())
            {
                //Checks for element DataType
                if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "DataType"))
                {
                    if (reader.HasAttributes)
                    {
                        //Finds if the IdRef Attribute is present
                        if (!String.IsNullOrEmpty(reader.GetAttribute("Idref")))
                        {
                            textBox2.Text += reader.GetAttribute("Name") + ",\r\n"; //Prints Name to student solution textbox
                        }
                    }
                }
                //Checks for element Operation
                if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "Operation"))
                {
                    if (reader.HasAttributes)
                    {

                        textBox2.Text += "Function: " + reader.GetAttribute("Name") + ",\r\n"; //Prints function to student solution

                    }
                }
                //Checks for element ModelRelationshipContainer
                if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "ModelRelationshipContainer"))
                {
                    if (reader.HasAttributes)
                    {

                        textBox2.Text += reader.GetAttribute("Name") + ",:\r\n"; //Prints Start of Relationship

                    }
                }
                //Checks for element AssociationEnd
                if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "AssociationEnd"))
                {
                    if (reader.HasAttributes)
                    {
                        if (FindName(reader.GetAttribute("EndModelElement")) != null)
                        {

                            textBox2.Text += FindName(reader.GetAttribute("EndModelElement")) + ", Value: " + reader.GetAttribute("Multiplicity") + ",\r\n"; //Prints end of a relationship with multiplicity to student solution
                        }


                    }
                }

                //Checks for element Generalization
                if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "Generalization"))
                {
                    if (reader.HasAttributes)
                    {
                        string from = FindName(reader.GetAttribute("From")); //Finds Class Name
                        string to = FindName(reader.GetAttribute("To")); //Finds Class Name

                        //Checks if any have not been found
                        if (!String.IsNullOrEmpty(from) && !String.IsNullOrEmpty(to))
                        {
                            textBox2.Text += "From: " + from + ", To: " + to + "\r\n"; //Prints relationship to student solution textbox
                        }



                    }
                }


            }
        }

        /* Gets info of a solution */
        private void getInfo(TextBox infoBox, List<string> className, List<string> attribName, List<string> type, List<string> realName, List<string> realVal, List<string> from, List<string> to, List<string>[] extraNames)
        {
            char[] splitchars = { ',', ':', '\r', '\t' }; //What characters to split a string
            string[] stringlist = (infoBox.Text).Split(splitchars); //Split Characters

            //Loops through Solution
            for (int i = 0; i < stringlist.Count(); i++)
            {
                stringlist[i] = stringlist[i].Trim(); //Trim the white spaces 
                //Checks for Class 
                if (stringlist[i].Contains("Class Name")) 
                {
                    if(className.Count() != 0)
                    {
                        attribName.Add(className[className.Count() - 1] + " End"); //Add end tag for attribute Name for class
                        type.Add(className[className.Count() - 1] + " End"); //Add end tag for Attribute type to class
                    }
                    className.Add(stringlist[i + 1].Trim()); //Trim white spaces
                    attribName.Add(className[className.Count() - 1] + " Start"); //Add tag for next class attributes
                    type.Add(className[className.Count() - 1] + " Start"); //Add tag for Next Class Attribute types

                }
                //Checks for Attributes
                if (stringlist[i].Contains("Attribute"))
                {

                    attribName.Add(stringlist[i + 1].Trim()); 
                    type.Add(stringlist[i + 3].Trim()); 
                }
                //Checks for Function
                if (stringlist[i].Contains("Function"))
                {

                    attribName.Add(stringlist[i + 1].Trim());
                    type.Add("");
                }
                //Checks for Values
                if (stringlist[i].Contains("Value"))
                {
                    realName.Add(stringlist[i - 1].Trim());
                    realVal.Add(stringlist[i + 1].Trim());
                }
                //Check From Generalizations
                if (stringlist[i].Contains("From"))
                {
                    from.Add(stringlist[i + 1].Trim());
                    to.Add(stringlist[i + 3].Trim());
                }
                //Checks for Optional Names
                if (stringlist[i].Contains("ExName"))
                {
                    extraNames[0].Add(stringlist[i + 1].Trim());
                    extraNames[1].Add(stringlist[i + 3].Trim());
                }
                //Checks for Optional Variable Names
                if (stringlist[i].Contains("ExVariable"))
                {
                    extraNames[2].Add(stringlist[i + 1].Trim());
                    extraNames[3].Add(stringlist[i + 3].Trim());
                }
                
            }

        }
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

        /* Marks a class from a UML diagram */
        private double mark_Class(List<string> variables, List<string> variables2, List<string> types, List<string> types2, double mark) {
            double newMark = 0; //Double variable with Mark

            //Loops through Lecturer List Types of attributes
            for (int i = 0; i<types.Count(); i++)
            {
                //Loops through second list of types
                for (int j = 0; j < types.Count(); j++)
                {
                    string lower = types[i].ToLower(); //Lower case for types
                    //Checks if Lecturer Solution contains Nothing
                    if (variables[i].Contains(""))
                    {
                        newMark += mark;
                        break;
                    }
                    //Checks if both solutions match
                    if (lower.Contains(types2[j].ToLower())){
                        newMark += mark; //Gives mark and removes type
                        types2.RemoveAt(j);
                        
                    }
                }
                
            }
            //Loops through Lecturer Solution List of Variables
            for (int i =0; i< variables.Count(); i++)
            {
                //Loops through student solution List of variables
                for(int j =0; j<variables2.Count(); j++)
                {
                    
                    string lower = variables[i].ToLower(); //Lower Cases the Lecturer variable
                    //Checks For match
                    if (lower.Contains(variables2[j].ToLower()))
                    {
                        newMark += mark; //Adds Mark
                    }

                }
            }

            return newMark; //Return Mark for Class
        }

        /* Marks All Classes in UML diagram */
        private double markAllClasses(List<string>[] className, List<string>[] attribs, List<string>[] types)
        {
            double fullMark = 0; //Creates variable to store full mark
            //Loops through Lecturer solution of Class Names
            for(int i =0; i<className[0].Count(); i++)
            {
                topMark++; //Adds total Available Marks for each class
                //Loops through Student Classes
                for (int j = 0; j < className[1].Count(); j++)
                {
                    string lower = className[0][i].ToLower(); //Lower case for Lecturer Name
                    //Checks for match
                    if (lower.Contains(className[0][j].ToLower()))
                    {
                        
                        List<string> variables1 = getattribs(className[0][i], attribs[0]); //Create List of variables inside Lecturer Class
                        //If Variables Are present
                        if (variables1.Count() > 0)
                        {
                            List<string> types1 = getattribs(className[0][i], types[0]); //Get types from Lecturer class
                            List<string> variables2 = getattribs(className[1][j], attribs[1]); // Get attribs from Student class
                            List<string> types2 = getattribs(className[1][j], types[1]); //Get types from Student Class
                            double val = Math.Round((1f / (float)(variables1.Count() + types1.Count() + 1f)), 2); //Calulate Marks to give for correct match
                            double mark = mark_Class(variables1, variables2, types1, types2, val); //Mark Class
                            fullMark += mark; //Add Mark to Full Mark for UML diagram
                          
                        }
                        else
                        {
                            fullMark += 1; // If no Variables Add Mark
                        }
                        
                    }
                }
            }
            return fullMark; //Return Full Mark for Classes
        }

        /* Marks Generalizaations */
        private double markGen(List<string>[] to, List<string>[] from)
        {
            double mark = 0; //Variable to hold mark

            //Loops through Lectuers Parent Generalizations
            for(int i = 0; i<from[0].Count(); i++)
            {
                topMark++; //Add to Total Available Marks
                //Loop Through Students Parent Generalizations
                for(int j = 0; j<from[1].Count(); j ++)
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
        /* Mark Relationships */
        private double markRel(List<string>[] classname, List<string>[] val)
        {
            double mark = 0; //Variable to store Mark
            //Loop through Lecturers Solution of Relationship Class Name
            for(int i = 0; i<classname[0].Count(); i += 2)
            {
                topMark++; //Add To Total Avaialble Mark
                //Loop Through Students Solution of Relationship Class Names
                for (int j=0; j<classname[1].Count(); j += 2)
                {
                    string lower = classname[0][i].ToLower(); //Lower Case The Class Name
                    //Check if one Part of Relationship Matches
                    if (lower.Contains(classname[1][j].ToLower()))
                    {
                        lower = classname[0][i+1].ToLower(); //Lower Case the Class Name
                        //Check if other end of relationship matches
                        if (lower.Contains(classname[1][j + 1].ToLower()))
                        {
                            mark += 0.5f; //Award half a mark
                            //Check if one of the Multiplicity matches
                            if (val[0][i].Contains(val[1][j])){
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
        /* Checks Student Solution if optional Names are present and replaces with appropriate name */
        private void checkOptionalNames(List<string>[] exNames, List<string> names, int array)
        {
            //Loop through Studen Class Names
            for(int i =0; i<names.Count(); i++)
            {
                //Loop through Option Names
                for(int j = 0; j<exNames[array+1].Count(); j++)
                {
                    string lower = exNames[array][j].ToLower(); //Lower case the Option Names
                    //Check if student Classes Matches with Option name
                    if (lower.Contains(names[i].ToLower()))
                    {
                        names[i] = exNames[array+1][j]; // Change the optional Name to the appropriate Name
                    }
                }
            }
        }

        /*Upload text file To Lecturer solution text box*/
        private void uploadText()
        {

            //Checks File path isnt empty and is a .txt file
            if (filePath1.Text != "") 
            {
                textBox1.Clear(); //Clears Lecturer Solution
                string[] lines = System.IO.File.ReadAllLines(filePath1.Text); //gets all Lines from txt file

                //Loops Through Each Line
                foreach (string line in lines)
                {
                    textBox1.Text += line + "\r\n"; //Prints Line
                }
            }
        }

        /* Upload Student File Button */
        private void button2_Click(object sender, EventArgs e)
        {
            if (upload(".xml", filePath2, file2))
            {
                umlUpload();
            }
        }

        /* Upload Lecturer Solution */
        private void button1_Click(object sender, EventArgs e)
        {
            if (upload(".txt", filePath1, file1))
            {
                uploadText();
            }
            
        }

        /* Mark Button */
        private void button3_Click(object sender, EventArgs e)
        {
            topMark = 0; //Total Avaialable Marks reset

            //Create Lists for Storing Data For lecturer and student Solutions
            List<string>[] className = new List<string>[2];
            List<string>[] attribName = new List<string>[2];
            List<string>[] type = new List<string>[2];
            List<string>[] realName = new List<string>[2];
            List<string>[] realVal = new List<string>[2];
            List<string>[] from = new List<string>[2];
            List<string>[] to = new List<string>[2];
            List<string>[] optionalNames = new List<string>[4];

            double mark = 0; //Reset Rewarded Marks

            //Loop to Initialize Lists
            for (int i = 0; i < 2; i++)
            {
                className[i] = new List<string>();
                attribName[i] = new List<string>();
                type[i] = new List<string>();
                realName[i] = new List<string>();
                realVal[i] = new List<string>();
                from[i] = new List<string>();
                to[i] = new List<string>();
                optionalNames[i] = new List<string>();
                optionalNames[i+2] = new List<string>();
            }
            

            //Get Info for all Data
            getInfo(textBox1, className[0], attribName[0], type[0], realName[0], realVal[0], from[0], to[0], optionalNames);
            getInfo(textBox2, className[1], attribName[1], type[1], realName[1], realVal[1], from[1], to[1], optionalNames);

            //Check if Optional names are Found
            checkOptionalNames(optionalNames, className[1], 0);
            checkOptionalNames(optionalNames, realName[1], 0);
            checkOptionalNames(optionalNames, from[1], 0);
            checkOptionalNames(optionalNames, to[1], 0);
            checkOptionalNames(optionalNames, attribName[1], 2);

            //Retrieve and add marks
            mark += markAllClasses(className, attribName, type);
            mark += markGen(to, from);
            mark += markRel(realName, realVal);
            //Grade Mark
            markWork(mark);
            
            
        }

        /* Save Button */
        private void button5_Click(object sender, EventArgs e)
        {
            //If anything is Blank
            if(textBox1.Text == "" || textBox2.Text == "" || filePath1.Text == "" || filePath2.Text == "" || markText.Text == "")
            {
                return;
            }

            AddMark m = new AddMark(filePath2.Text); //Creates Add Mark Windows Forum

            //Opens Add Mark Windows forum
            if(m.ShowDialog() == DialogResult.OK)
            {
                studentsMarks += m.Student.Replace(",", System.Environment.NewLine); //Adds Mark to student Marks string
                filePath2.Clear();
                textBox2.Clear();
            }
           

        }
        /* Save button */
        private void button6_Click(object sender, EventArgs e)
        {
           
                SaveFileDialog save = new SaveFileDialog(); //Create File Dialog

                //Opens File Explorer And checks if okay has been pressed.
                if (save.ShowDialog() == DialogResult.OK)
                {
                    using (Stream s = File.Open(save.FileName, FileMode.CreateNew))
                    using (StreamWriter sw = new StreamWriter(s))
                    {
                        sw.Write(studentsMarks); //Write Marks
                        
                    }
                    studentsMarks = ""; //Reset Saved Marks
                }
            
        }

        private void Mark_Load(object sender, EventArgs e)
        {

        }
    }
}

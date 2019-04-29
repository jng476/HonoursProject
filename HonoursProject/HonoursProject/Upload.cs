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
 * Main Menu
*/
namespace HonoursProject
{
    public partial class Upload : Form
    {
        //Global Variables
        List<string> names = new List<string>(); //Listof ClassNames
        List<string> varNames = new List<string>(); //List of Variable Names
        List<string> nameID = new List<string>(); // List of Class Name ID
        bool opAdded = false; // Checks if optional Names have been added

        /* Iniliazier for upload */
        public Upload()
        {
            InitializeComponent();
            
        }

        /* Main Menu Button */
        private void button1_Click(object sender, EventArgs e)
        {
            Menu menu = new Menu(); //Create Menu
            this.Hide(); //Hide Upload Windows Forum
            menu.ShowDialog(); //Open Menu Windows Forum
            this.Close(); //Close Upload Forum
        }

        /* Checks when text has been changed in textBox */
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        /* Function to fine a name when given Class Name ID */
        string FindName(string searchID)
        {
            // Loops Through all name ID
            for (int i = 0; i < nameID.Count(); i++)
            {
                //If the Name ID is found Return the name relevant to ID if not return nothing.
                if (nameID[i].Equals(searchID))
                {
                    return names[i];
                }
            }
            return null; 
        }
        
        /* Upload Button */
        private void button3_Click(object sender, EventArgs e)
        {
            TextBox.Text = ""; //Renew Text box used for creating solution
            string filePath = FilePath.Text; //Get filepath.
            bool file_found = true; //creates bull if its correct file
            names.Clear(); //Clear Class Names List
            nameID.Clear(); //Clear Class name ID List

            //Check to see if the file path exists and if the file ends with .XML
            if (!File.Exists(filePath) || !filePath.EndsWith(".xml", System.StringComparison.CurrentCultureIgnoreCase)) 
            {
                Filetext.Text = "File Error"; //Show a file error
                Filetext.ForeColor = Color.Red; //Make the text Red
                file_found = false; //Turn File Found False
            }
                
            
            
            //If File is valid
            if(file_found == true)
            {
                XmlReader reader = XmlReader.Create(filePath); //Create a XML reader for the file 
                XmlReader getNames = XmlReader.Create(filePath); //Create a second XML reader to read names
                //Loop to obtain Names
                while (getNames.Read())
                {
                    //If Element Class is found
                    if ((getNames.NodeType == XmlNodeType.Element) && (getNames.Name == "Class"))
                    {
                        //Checks if it has attributes
                        if (getNames.HasAttributes)
                        {
                            String Active = getNames.GetAttribute("Active");
                            //If String obtains something
                            if (!String.IsNullOrEmpty(Active))
                            {
                                names.Add(getNames.GetAttribute("Name")); //Add Name to Class Name List
                                nameID.Add(getNames.GetAttribute("Id")); //Add Class Name ID to ID list
                                TextBox.Text += "Class Name: " + getNames.GetAttribute("Name") + ",\r\n"; // Add line to solution with Class name 
                            }
                        }
                    }
                    //If Element Attribute is found
                    if ((getNames.NodeType == XmlNodeType.Element) && (getNames.Name == "Attribute"))
                    {
                        //Checks if it has attributes
                        if (getNames.HasAttributes)
                        {
                            varNames.Add(getNames.GetAttribute("Name")); //Adds Variable Name to Variable Name List
                            TextBox.Text += "Attribute Name: " + getNames.GetAttribute("Name") + ", Type: " + getNames.GetAttribute("Type"); //Add Attribute Name and type to Solution textbox
                            TextBox.Text += ",\r\n";// New Line
                        }
                    }
                }
                //Read XML file to get rest of information
                while (reader.Read())
                {
                    //Finds the element Datatype 
                    if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "DataType"))
                    {
                        if (reader.HasAttributes)
                        {
                            //Finds if the Idref attribute is present 
                            if (!String.IsNullOrEmpty(reader.GetAttribute("Idref")))
                            {
                                TextBox.Text += reader.GetAttribute("Name") + ",\r\n"; // Adds Attribute to string to solution 
                            }
                        }
                    }
                    // Finds ELement Operation is found
                    if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "Operation"))
                    {
                        //Finds Attributes
                        if (reader.HasAttributes)
                        {

                            TextBox.Text += "Function: " + reader.GetAttribute("Name") + ",\r\n"; // Adds Functions To class

                        }
                    }
                    // Finds Start of Relationships
                    if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "ModelRelationshipContainer"))
                    {
                        // Get Relationship Naeme
                        if (reader.HasAttributes)
                        {

                            TextBox.Text += reader.GetAttribute("Name") + ",:\r\n"; // Prints start of Relationship

                        }
                    }
                    // Finds AssociationEnd (Relationship)
                    if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "AssociationEnd"))
                    {
                        //Gets Attributes
                        if (reader.HasAttributes)
                        {
                            //Finds the Class name 
                            if(FindName(reader.GetAttribute("EndModelElement")) != null) { 
                               
                               TextBox.Text += FindName(reader.GetAttribute("EndModelElement")) + ", Value: " + reader.GetAttribute("Multiplicity") + ",\r\n"; // Prints End of Relationship with Multiplicity
                            }
                            

                        }
                    }
                    // Find Generalization Relationships
                    if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "Generalization"))
                    {
                        //Gets Attributes
                        if (reader.HasAttributes)
                        {
                            string from = FindName(reader.GetAttribute("From")); //Gets Parent Name
                            string to = FindName(reader.GetAttribute("To")); //Gets Child Name
                            
                            //Chekcs if names are found
                            if (!String.IsNullOrEmpty(from) && !String.IsNullOrEmpty(to))
                            {
                                TextBox.Text += "From: " + from + ", To: " + to + ",\r\n"; //Prints Generalization Relationship to solution textbox
                            }
                            


                        }
                    }


                }
               
                Filetext.Text = "File Uploaded"; //Anounces Upload successful
                Filetext.ForeColor = Color.Green; //Change text to green
                opAdded = false; //Turns optional names added to false.
            }


        }
        
        /* on Load for Uplaod Windows Forum */
        private void Upload_Load(object sender, EventArgs e)
        {

        }

        /*Search button*/
        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog(); // Creates file Explorers to search for files

            //Opens File Explorer and checks if okay button is pressed
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                FilePath.Text = openFileDialog.FileName; //Enters FilePath to FilePath text

            }
        }
        /* Save Button */ 
        private void button4_Click(object sender, EventArgs e)
        {
            //Checks if Class diagram is large enough
            if (TextBox.Text.Length < 100)
            {
                Filetext.Text = "Invalid Text to Save"; //error message
                Filetext.ForeColor = Color.Red; //Change message to red
            }
            else
            {
                SaveFileDialog save = new SaveFileDialog(); //Creates File explorer for saving
                //Opens File Expllorer and checks if user presses okay button.
                if (save.ShowDialog() == DialogResult.OK)
                {
                    using (Stream s = File.Open(save.FileName, FileMode.CreateNew)) //Uses file stream to create new file
                    using (StreamWriter sw = new StreamWriter(s)) //Creates writer to write to new file
                    {
                        sw.Write(TextBox.Text + ".txt"); //Creates text file
                        Filetext.Text = save.FileName + " Saved"; //Outputs comment showing user that file has been saved
                        Filetext.ForeColor = Color.Green; //change comment colour to green.
                    }
                }
                
            }
        }

        /* Optional Names Button */
        private void button5_Click(object sender, EventArgs e)
        {
            //Checks if a file has been uploaded
            if(TextBox.Text == "")
            {
                return;
            }
            //Creates AddExtraName windows Forum
            AddExtraName add = new AddExtraName(names, varNames, opAdded);
           
            //Opens Windows Forum and checks when users presses okay
            if (add.ShowDialog() == DialogResult.OK)
            {
                TextBox.Text += add.ExtraNames.Replace(".", System.Environment.NewLine); //Replaces , with new lines to seperate optional names
                opAdded = true; //Changes optiona names to true
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/* Josh Ng 08/02/2019
 * Honours Project 
 * Add Extra names
*/

namespace HonoursProject
{
    public partial class AddExtraName : Form
    {

        private string extraNames; //Stores extra names

        /*Getter and Setter for storing extra names*/ 
        public string ExtraNames{

            get { return extraNames; }
            set { extraNames = value; }

        }

        /* Gets Class Names Variable Names from UML diagram and displays them to be added checks if optional names have been previously added*/
        public AddExtraName(List<string> names, List<string> varNames, bool opAdded)
        {
            InitializeComponent();
            textBox1.Text = ""; //Input Optional name textbox cleared
            //Checks if optional names have been added before
            if (opAdded == false)
            {
                extraNames = "Optional Names ,.";
            }
            //Loops through Class Names and adds to combo Box
            for(int i = 0; i<names.Count(); i++)
            {
                comboBox1.Items.Add(names[i]);
            }
            //Loops Through VarNames and adds to comboBox
            for (int i = 0; i < varNames.Count(); i++)
            {
                comboBox1.Items.Add("Var: "+ varNames[i]);
            }
        }

        /*Next Button */
        private void Next_Click(object sender, EventArgs e)
        {
            //Checks if selection in combobox hasnt been selected
            if(comboBox1.Text == "")
            {
                return;
            }
            //Checks if a variable has been selected or class Name
            if (comboBox1.Text.Contains("Var:"))
            {
                extraNames += "ExVariable: " + textBox1.Text + ", For: " + comboBox1.Text.Remove(0, 5) + ",.\n"; // Adds Variable Optional Name
            }
            else
            {
                extraNames += "ExName: " + textBox1.Text + ", For: " + comboBox1.Text + ",.\n"; //Adds Class Option Name
            }
            textBox1.Text = ""; //clear Input Box
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void AddExtraName_Load(object sender, EventArgs e)
        {
            
        }
    }
}

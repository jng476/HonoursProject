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
    public partial class AddMark : Form
    {
        private string student; //holds Student Mark

        //Getter and Setter For student Mark
        public string Student
        {
            get { return student; }
            set { student = value;  }
        }

        /* Obtains the Grade from the mark */
        private void markWork(int mark)
        {
            //Checks if mark is above 100 sets to 100 incase
            if (mark > 100)
            {
                mark = 100;
            }
            MLabel.Text = ""; //Clears Mark Label

            //If Statement for grading Mark
            if (mark >= 95)
            {
                MLabel.Text = "A:1";
            }
            else if (mark >= 89 && mark < 95)
            {
                MLabel.Text = "A:2";
            }
            else if (mark >= 83 && mark < 89)
            {
                MLabel.Text = "A:3";
            }
            else if (mark >= 76 && mark < 83)
            {
                MLabel.Text = "A:4";
            }
            else if (mark >= 70 && mark < 76)
            {
                MLabel.Text = "A:5";
            }
            else if (mark >= 67 && mark < 70)
            {
                MLabel.Text = "B:1";
            }
            else if (mark >= 64 && mark < 67)
            {
                MLabel.Text = "B:2";
            }
            else if (mark >= 60 && mark < 64)
            {
                MLabel.Text = "B:3";
            }
            else if (mark >= 57 && mark < 60)
            {
                MLabel.Text = "C:1";
            }
            else if (mark >= 54 && mark < 57)
            {
                MLabel.Text = "C:2";
            }
            else if (mark >= 50 && mark < 54)
            {
                MLabel.Text = "C:3";
            }
            else if(mark >=47 && mark < 50)
            {
                MLabel.Text = "D:1";
            }
            else if (mark >= 44 && mark < 47)
            {
                MLabel.Text = "D:2";
            }
            else if (mark >= 40 && mark < 44)
            {
                MLabel.Text = "D:3";
            }
            else if (mark >= 37 && mark < 40)
            {
                MLabel.Text = "M:1";
            }
            else if (mark >= 34 && mark < 37)
            {
                MLabel.Text = "M:2";
            }
            else if (mark >= 30 && mark < 34)
            {
                MLabel.Text = "M:3";
            }
            else if (mark >= 20 && mark < 30)
            {
                MLabel.Text = "CF";
            }
            else if (mark < 20)
            {
                MLabel.Text = "BF";
            }
            
        }

        /* Initializes Windows Forum */
        public AddMark(string studentName)
        {
            InitializeComponent();
            name.Text = studentName.Split('\\').Last(); //Splits Files Path to Get File Name
        }
        /* When Key is Pressed in Mark Textbox */
        private void mark_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Checks if Key pressed is digit and is not more than 3 digits
            if(!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.')  || mark.Text.Length > 2 && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;          
            }
        }
        /* Inputs Student Mark with Name, Mark, and Comment For Student */
        private void button1_Click(object sender, EventArgs e)
        {
            Student = "Name: " + name.Text + ",\n Mark: " + mark.Text + ",\n Comment: " + description.Text + ",\n\r";
        }
        /* Checks When text Changes */
        private void mark_TextChanged(object sender, EventArgs e)
        {
            //Checks if the text Box is Empty
            if (mark.Text != "")
            {
                //Checks if the number inside is above 100
                if (Int32.Parse(mark.Text) > 100)
                {
                    mark.Text = mark.Text.Substring(0, mark.Text.Length - 1); //Deletes last Digit
                }
                markWork(Int32.Parse(mark.Text)); //Grades Mark
            }
        }

        private void AddMark_Load(object sender, EventArgs e)
        {

        }
    }
}

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
 * Main Menu
*/
namespace HonoursProject
{
    public partial class Menu : Form
    {
        /* Initialize When Start */
        public Menu()
        {
            InitializeComponent(); 
        }
        /* When Menu Loads */
        private void Menu_Load(object sender, EventArgs e)
        {

        }
        /* Upload Button */
        private void button1_Click(object sender, EventArgs e)
        {
            Upload upload = new Upload(); //Creates Upload Windows
            this.Hide(); //Hides Current Program
            upload.ShowDialog(); //Upload Windows Forum Shown
            this.Close(); // Closes Menu
        }

        /* Close Button */
        private void button3_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(1); //Closes Program
        }

        /*Mark Button */
        private void button2_Click(object sender, EventArgs e)
        {
            Mark mark = new Mark(); //Create Mark WindowsForum
            this.Hide(); //Hides Main Menu
            mark.ShowDialog(); //Opens Mark Windows Forum
            this.Close(); //Close Main Menu
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}

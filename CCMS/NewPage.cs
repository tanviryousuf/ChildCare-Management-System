using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CCMS
{
    public partial class NewPage : Form
    {
        public NewPage()
        {
            InitializeComponent();
        }

        private void NewPage_Load(object sender, EventArgs e)
        {
            regpanel.Hide();
            pnlNanny.Hide();
            pnlBabysitter.Hide();
            PnlAbout.Show();
        }

        private void bunifuFlatButton8_Click(object sender, EventArgs e)
        {
            Log lg = new Log();
            lg.Show();
        }

        private void bunifuFlatButton9_Click(object sender, EventArgs e)
        {
            regpanel.Show();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_regFamily_Click(object sender, EventArgs e)
        {
            regpanel.Hide();
            Reg1 r1 = new Reg1();
            r1.Show();
        }

        private void btn_regNanny_Click(object sender, EventArgs e)
        {
            regpanel.Hide();
            Reg2 r2 = new Reg2();
            r2.Show();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {

        }

        private void bunifuFlatButton7_Click(object sender, EventArgs e)
        {
            
            PnlAbout.Show();
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            pnlNanny.Show();
            
            
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            
            pnlBabysitter.Show();
            
        }
    }
}

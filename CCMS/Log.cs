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
    public partial class Log : Form
    {
        login lg = new login();
        public static string username;
        public static string recby
        {
            get{ return username; }
            set { username = value; }
        }
        public Log()
        {
            InitializeComponent();
        }

        private void Log_Load(object sender, EventArgs e)
        {


           


        }

        private void txtuser_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtuser.Text))
            {
               
                MessageBox.Show("Please Enter Username and Password", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtuser.Focus();
                return;
            }
            try
            {
                using (CMSEntities db = new CMSEntities())
                {

                    var query = from u in db.logins
                                where u.Username == txtuser.Text && u.Password == txtpass.Text && u.Type == comboBox1.Text
                                select u;
                    if (query.SingleOrDefault() != null)
                    {
                        
                        recby = txtuser.Text;
                        MessageBox.Show("Welcome! ", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                          lg.Username = recby;
                        lg = db.logins.Where(x => x.Username == lg.Username).FirstOrDefault();
                       if(lg.Type == "Family")
                       {
                           this.Hide();
                           FamilyView fv = new FamilyView();
                           fv.Show();

                       }
                         if(lg.Type == "Nanny")
                       {
                           this.Hide();
                           NannyView fv = new NannyView();
                           fv.Show();

                       }
                         if (lg.Type == "Admin")
                         {
                             this.Hide();
                             AdminView fv = new AdminView();
                             fv.Show();

                         }
                         
                        



                    }
                    else
                    {
                        MessageBox.Show("Please enter correct username and password ", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtuser.Text = "";
            txtpass.Text = "";
        }
    }
}

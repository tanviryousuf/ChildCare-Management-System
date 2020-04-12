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
    public partial class AdminView : MetroFramework.Forms.MetroForm
    {
        WorkTable model = new WorkTable();
        Notifi model1 = new Notifi();
        AdmintoNannyReq model3 = new AdmintoNannyReq();
        String FamilyUser;
        String NannyUser;
        int row;
        public AdminView()
        {
            InitializeComponent();
        }

        void populateDataGridView()
        {

            using (CMSEntities db = new CMSEntities())
            {
                dgvnoti.AutoGenerateColumns = false;

                dgvnoti.DataSource = db.WorkTables.ToList<WorkTable>();

                row = dgvnoti.RowCount;
                lblrow.Text = Convert.ToString( row);
            }

        }
        private void button2_Click(object sender, EventArgs e)
        {
            populateDataGridView();
            dgvnoti.Show();
        }

        private void dgvnoti_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            model3.FUsername = FamilyUser;
            model3.NUsername = NannyUser;
            using (CMSEntities db = new CMSEntities())
            {
                if (model3.UserID == 0)
                {

                    db.AdmintoNannyReqs.Add(model3);
                    MessageBox.Show("request accepted!");

                }
                db.SaveChanges();


            }
        }

        private void dgvnoti_Click(object sender, EventArgs e)
        {
            try {
                if (dgvnoti.CurrentRow.Index != -1)
                {
                    FamilyUser = Convert.ToString(dgvnoti.CurrentRow.Cells["dgvFuser"].Value);
                    NannyUser = Convert.ToString(dgvnoti.CurrentRow.Cells["dgvNanny"].Value);
                    model.FUsername = Convert.ToString(dgvnoti.CurrentRow.Cells["dgvFuser"].Value);
                    using(CMSEntities db = new CMSEntities())
                    {
                        model = db.WorkTables.Where(x => x.FUsername == model.FUsername).FirstOrDefault(x => x.NUsername == NannyUser);
                    }
                   
                }
            }
            catch
            {
                MessageBox.Show("Invalid");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure to delete ?", "EF CRUID Operation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    using (CMSEntities db = new CMSEntities())
                    {
                        var entry = db.Entry(model);
                        if (entry.State == System.Data.Entity.EntityState.Detached)
                        {
                            db.WorkTables.Attach(model);
                            db.WorkTables.Remove(model);
                            db.SaveChanges();
                            populateDataGridView();

                            MessageBox.Show("Item Deleted");

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
            Log lg = new Log();
            lg.Show();
        }

        private void AdminView_Load(object sender, EventArgs e)
        {
            populateDataGridView();
            dgvnoti.Hide();
        }
    }
}

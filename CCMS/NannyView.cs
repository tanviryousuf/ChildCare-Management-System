using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace CCMS
{
    public partial class NannyView : MetroFramework.Forms.MetroForm
    {
        NannyData model = new NannyData();
        WorkChart model1 = new WorkChart();
        AdmintoNannyReq model3 = new AdmintoNannyReq();
        FamilyDB model4 = new FamilyDB();
        WorkTable model5 = new WorkTable();
        String type;
        String userName;
        String free;
        string requF;
        int row1;
        public NannyView()
        {
            InitializeComponent();
        }
        private byte[] ConvertFiltoByte(string sPath)
        {
            byte[] data = null;
            FileInfo fInfo = new FileInfo(sPath);
            long numBytes = fInfo.Length;
            FileStream fileStream = new FileStream(sPath, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fileStream);
            data = br.ReadBytes((int)numBytes);

            return data;
        }
        private Image ConvertBytetoImage(byte[] photo)
        {
            Image newImage;
            using (MemoryStream ms = new MemoryStream(photo, 0, photo.Length))
            {
                ms.Write(photo, 0, photo.Length);
                newImage = Image.FromStream(ms, true);
            }
            return newImage;
        }

        private void NannyView_Load(object sender, EventArgs e)
        {
            model.Username = Log.recby;
            model1.Username = Log.recby;
            preq.Hide();
            using (CMSEntities db = new CMSEntities())
            {
                model = db.NannyDatas.Where(x => x.Username == model.Username).FirstOrDefault();
                model1 = db.WorkCharts.Where(x => x.Username == model1.Username).FirstOrDefault();
                txtname.Text = model.Name; ;
                txtNID.Text = model.NationalID;
                txtAddress.Text = model.Address;
                txtemail.Text = model.Email;
                userpic.Image = ConvertBytetoImage(model.UserPic);
                picbox.Image = ConvertBytetoImage(model.NIDPic);
                txtexsal.Text = model.ExSalary;
                txtexperience.Text = model.Experience;
                txtedu.Text = model.Education;
                txtLabel.Text = model.Username;
                txtt.Text = model.Type;
                txtmobile.Text = model.Mobile;
                txtname.Enabled = false;
                txtNID.Enabled = false;
                txtAddress.Enabled = false;
                txtemail.Enabled = false;
                userpic.Enabled = false;
                picbox.Enabled = false;
                txtexsal.Enabled = false;
                txtexperience.Enabled = false;
                txtedu.Enabled = false;
                txtLabel.Enabled = false;
                txtt.Enabled = false;
                checkBox3.Enabled = false;
                checkBox4.Enabled = false;
                checkBox5.Enabled = false;
                checkBox6.Enabled = false;
                btn_sat.Enabled = false;
                btn_sun.Enabled = false;
                btn_mon.Enabled = false;
                btn_tues.Enabled = false;
                btn_wed.Enabled = false;
                btn_thurs.Enabled = false;
                btn_fri.Enabled = false;
                if (model1.Saturday == "Free")
                {
                    
                    free = " Saturday";
                }
                if (model1.Sunday == "Free")
                {
                    free = free + " Sunday";
                }
                if (model1.Monday == "Free")
                {
                    free = free + " Monday";
                }
                if (model1.Tuesday == "Free")
                {
                    free = free + " Tuesday";
                }
                if (model1.Wednesday == "Free")
                {
                    free = free + " Wednesday";
                }
                if (model1.Thursday == "Free")
                {
                    free = free + " Thursday";
                }
                if (model1.Friday == "Free")
                {
                    free = free + " Friday";
                }
                txtFree.Text = free;
               

            }

            row1 = dgvrequest.RowCount;
            row.Text = Convert.ToString(row1);
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Please select a photo";
            ofd.Filter = "JPG|*.jpg|PNG|*|GIF|*gif";
            ofd.Multiselect = false;
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.userpic.ImageLocation = ofd.FileName;

            }
        }

        private void userpicbox_Click(object sender, EventArgs e)
        {

        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            txtname.Enabled = true;
            txtNID.Enabled = true;
            txtAddress.Enabled = true;
            txtemail.Enabled = true;
            userpic.Enabled = true;
            picbox.Enabled = true;
            txtexsal.Enabled = true;
            txtexperience.Enabled = true;
            txtedu.Enabled = true;
            txtLabel.Enabled = true;
            txtt.Enabled = true;
            checkBox3.Enabled = true;
            checkBox4.Enabled = true;
            checkBox5.Enabled = true;
            checkBox6.Enabled = true;
            btn_sat.Enabled = true;
            btn_sun.Enabled = true;
            btn_mon.Enabled = true;
            btn_tues.Enabled = true;
            btn_wed.Enabled = true;
            btn_thurs.Enabled = true;
            btn_fri.Enabled = true;
                
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            type = "Daily Nanny";
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            type = "Temporary";
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            type = "Live in Nanny";
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            type = "Baby sitter";
        }

        

       

        private void button6_Click(object sender, EventArgs e)
        {
            checkBox3.Enabled = false;
            checkBox4.Enabled = false;
            checkBox5.Enabled = false;
            checkBox6.Enabled = false;
            try
            {


                using (CMSEntities db = new CMSEntities())
                {
                    model.Name = txtname.Text.Trim();
                    model.NationalID = txtNID.Text.Trim();
                    model.NIDPic = ConvertFiltoByte(this.picbox.ImageLocation);

                    model.Email = txtNID.Text.Trim();
                    model.Address = txtAddress.Text.Trim();


                    model.UserPic = ConvertFiltoByte(this.userpic.ImageLocation);
                    model.Type = type;
                    model.Experience = txtexperience.Text.Trim();
                    model.ExSalary = txtexsal.Text.Trim();
                    model.Education = txtedu.Text.Trim();
                    model.Mobile = txtmobile.Text.Trim();
                    model.BirthDate = datePicker.Value;
                    model.Status = "Free";

                    model1.Username = Log.recby;


                    model1 = db.WorkCharts.Where(x => x.Username == model1.Username).FirstOrDefault();


                    db.Entry(model1).State = System.Data.Entity.EntityState.Modified;
                    db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    MessageBox.Show("your profile is successfully updated");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please change once more");
                throw;
            }
        }

        private void btn_sun_CheckedChanged_1(object sender, EventArgs e)
        {
            model1.Username = Log.recby;
              using (CMSEntities db = new CMSEntities())
              {
                  model1 = db.WorkCharts.Where(x => x.Username == model1.Username).FirstOrDefault();
                  if(btn_sun.Checked)
                  {
                      model1.Sunday = "Booked";
                  }
                  else
                  {
                      model1.Sunday = "Free";
                  }
                  
                  db.Entry(model1).State = System.Data.Entity.EntityState.Modified;
                  db.SaveChanges();
              }
        }

        private void picbox_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Please select a photo";
            ofd.Filter = "JPG|*.jpg|PNG|*|GIF|*gif";
            ofd.Multiselect = false;
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.picbox.ImageLocation = ofd.FileName;

            }
        }

        private void btn_mon_CheckedChanged(object sender, EventArgs e)
        {
            model1.Username = Log.recby;
            using (CMSEntities db = new CMSEntities())
            {
                model1 = db.WorkCharts.Where(x => x.Username == model1.Username).FirstOrDefault();
                if (btn_sun.Checked)
                {
                    model1.Monday = "Booked";
                }
                else
                {
                    model1.Monday = "Free";
                }

                db.Entry(model1).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        private void btn_tues_CheckedChanged(object sender, EventArgs e)
        {
            model1.Username = Log.recby;
            using (CMSEntities db = new CMSEntities())
            {
                model1 = db.WorkCharts.Where(x => x.Username == model1.Username).FirstOrDefault();
                if (btn_sun.Checked)
                {
                    model1.Tuesday = "Booked";
                }
                else
                {
                    model1.Tuesday = "Free";
                }

                db.Entry(model1).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        private void btn_wed_CheckedChanged(object sender, EventArgs e)
        {
            model1.Username = Log.recby;
            using (CMSEntities db = new CMSEntities())
            {
                model1 = db.WorkCharts.Where(x => x.Username == model1.Username).FirstOrDefault();
                if (btn_sun.Checked)
                {
                    model1.Wednesday = "Booked";
                }
                else
                {
                    model1.Wednesday = "Free";
                }

                db.Entry(model1).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        private void btn_sat_CheckedChanged(object sender, EventArgs e)
        {
            model1.Username = Log.recby;
            using (CMSEntities db = new CMSEntities())
            {
                model1 = db.WorkCharts.Where(x => x.Username == model1.Username).FirstOrDefault();
                if (btn_sun.Checked)
                {
                    model1.Saturday = "Booked";
                }
                else
                {
                    model1.Saturday = "Free";
                }

                db.Entry(model1).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        private void btn_thurs_CheckedChanged(object sender, EventArgs e)
        {
            model1.Username = Log.recby;
            using (CMSEntities db = new CMSEntities())
            {
                model1 = db.WorkCharts.Where(x => x.Username == model1.Username).FirstOrDefault();
                if (btn_sun.Checked)
                {
                    model1.Thursday = "Booked";
                }
                else
                {
                    model1.Thursday = "Free";
                }

                db.Entry(model1).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        private void btn_fri_CheckedChanged(object sender, EventArgs e)
        {
            model1.Username = Log.recby;
            using (CMSEntities db = new CMSEntities())
            {
                model1 = db.WorkCharts.Where(x => x.Username == model1.Username).FirstOrDefault();
                if (btn_sun.Checked)
                {
                    model1.Friday = "Booked";
                }
                else
                {
                    model1.Friday = "Free";
                }

                db.Entry(model1).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            preq.Hide();
        }

        private void button11_Click(object sender, EventArgs e)
        {

        }

        private void txtFree_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                model3.NUsername = Log.recby;
                using (CMSEntities db = new CMSEntities())
                {
                    model3 = db.AdmintoNannyReqs.Where(x => x.NUsername == model3.NUsername).FirstOrDefault();
                    dgvrequest.AutoGenerateColumns = false;

                    dgvrequest.DataSource = db.AdmintoNannyReqs.Where(x => x.NUsername == model3.NUsername).ToList<AdmintoNannyReq>();

                    row1 = dgvrequest.RowCount;
                    row.Text = Convert.ToString(row1);

                }


                preq.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }


        }

        private void dgvrequest_Click(object sender, EventArgs e)
        {
            
        
        }

        private void dgvrequest_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvrequest.CurrentRow.Index != -1)
                {
                    requF = Convert.ToString(dgvrequest.CurrentRow.Cells["gdvreq"].Value);
                    model4.Username = requF;
                    using (CMSEntities db = new CMSEntities())
                    {
                        model4 = db.FamilyDBs.Where(x => x.Username == model4.Username).FirstOrDefault();
                        txtnm.Text = model4.Name;
                        txtid.Text = model4.NationalID;
                        txtad.Text = model4.Address;
                        txtema.Text = model4.Email;
                        txtga.Text = model4.Gander;
                        pcbox.Image = ConvertBytetoImage(model4.NIDPic);

                    }
                }
            }
            catch
            {
                MessageBox.Show("Invalid");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                model5.NUsername = Log.recby;
                using (CMSEntities db = new CMSEntities())
                {
                    model5 = db.WorkTables.Where(x => x.NUsername == model5.NUsername).FirstOrDefault(x => x.FUsername == requF);
                    model5.WorkStatus = "Booked";
                    db.SaveChanges();
                    MessageBox.Show("Congratulation ! you are haired!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
           
        }

        private void panel22_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button11_Click_1(object sender, EventArgs e)
        {
            this.Close();
            Log lg = new Log();
            lg.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }


             
        
    }
}

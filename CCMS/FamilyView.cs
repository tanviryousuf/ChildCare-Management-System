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
    public partial class FamilyView : MetroFramework.Forms.MetroForm
    {
        FamilyDB model = new FamilyDB();
        NannyData model1 = new NannyData();
        WorkChart model2 = new WorkChart();
        Notifi model3 = new Notifi();
        WorkTable model4 = new WorkTable();
        string fuser = Log.recby;
        string gen;
        string free = " ";
        string userName=" ";
        string search;
        public FamilyView()
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
        void populateDataGridView()
        {                           

            using (CMSEntities db = new CMSEntities())
            {
                dgvNannies.AutoGenerateColumns = false;

                dgvNannies.DataSource = db.NannyDatas.ToList<NannyData>();

            }
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

        private void FamilyView_Load(object sender, EventArgs e)
        {

            model.Username = Log.recby;

            using (CMSEntities db = new CMSEntities())
            {
                model = db.FamilyDBs.Where(x => x.Username == model.Username).FirstOrDefault();
                txtName.Text = model.Name;
                txtNID.Text = model.NationalID;
                txtAddress.Text = model.Address;
                txtEmail.Text = model.Email;
                txtG.Text = model.Gander;
                picbox.Image = ConvertBytetoImage(model.NIDPic);
                btn_haire.Enabled = false;
                panelpro.Hide();
                txtName.Enabled = false;
                txtNID.Enabled = false;
                txtEmail.Enabled = false;  
                txtG.Enabled = false;
                txtAddress.Enabled = false;
                picbox.Enabled = false;
                txtG.Show();
                btn_save.Hide();
                redioFemale.Hide();
                redioMale.Hide();
            }
            populateDataGridView();
         }
       
        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgvNannies_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void dgvNannies_Click(object sender, EventArgs e)
        {
            panelpro.Show();
            btn_haire.Enabled = true;
            try
            {
                if (dgvNannies.CurrentRow.Index != -1)
                {
                    userName = Convert.ToString(dgvNannies.CurrentRow.Cells["dgvUser"].Value);

                    using (CMSEntities db = new CMSEntities())
                    {
                        model1 = db.NannyDatas.Where(x => x.Username == userName).FirstOrDefault();
                        model2 =db.WorkCharts.Where(x => x.Username == userName).FirstOrDefault();
                        txt_n.Text = model1.Name;
                        txtt.Text = model1.Username;
                        txtBirth.Text = Convert.ToString( model1.BirthDate);
                        txtedu.Text = model1.Education;
                        txtem.Text = model1.Email;
                        txtexperience.Text = model1.Experience;
                        txtexsal.Text = model1.ExSalary;
                        NidPic.Image = ConvertBytetoImage(model1.NIDPic);
                        userpic.Image = ConvertBytetoImage(model1.UserPic);
                        txtmobile.Text = model1.Mobile;
                        txtGen.Text = model1.Gander;
                        if(model2.Saturday == "Free")
                        {
                            free =" Saturday";
                        }
                        if (model2.Sunday == "Free")
                        {
                            free = free + " Sunday";
                        }
                        if (model2.Monday == "Free")
                        {
                            free = free + " Monday";
                        }
                        if (model2.Tuesday == "Free")
                        {
                            free = free + " Tuesday";
                        }
                        if (model2.Wednesday == "Free")
                        {
                            free = free + " Wednesday";
                        }
                        if (model2.Thursday == "Free")
                        {
                            free = free + " Thursday";
                        }
                        if (model2.Friday == "Free")
                        {
                            free = free + " Friday";
                        }
                        txtFree.Text = free;
                        

                    }

                }
            }
            catch
            {
                MessageBox.Show("Something wrong happend");
            }
        }

        private void btn_haire_Click(object sender, EventArgs e)
        {
            using (CMSEntities db = new CMSEntities())
            {
                
                model4.FUsername = fuser;
                model4.NUsername = userName;
                model4.WorkStatus = "proccessing";
                if (model3.UserID == 0)
                {
                    
                    db.WorkTables.Add(model4);

                    MessageBox.Show("Haire ruquest sent");
                }
                db.SaveChanges();
            }
        }

        private void panelpro_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            panelpro.Hide();
        }

        private void NidPic_Click(object sender, EventArgs e)
        {

        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            txtName.Enabled = true;
            txtNID.Enabled = true;
            txtEmail.Enabled = true;
            txtG.Enabled = true;
            txtAddress.Enabled = true;
            picbox.Enabled = true;
            btn_save.Show();
            redioMale.Show();
            redioFemale.Show();
            txtG.Hide();
           
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            gen = "Male";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            gen = "Female";
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

        private void button9_Click(object sender, EventArgs e)
        {
            model.Username = Log.recby;
            using (CMSEntities db = new CMSEntities())
            {
            model = db.FamilyDBs.Where(x => x.Username == model.Username).FirstOrDefault();
            model.Name = txtName.Text.Trim();
            model.NationalID = txtNID.Text.Trim();
            model.NIDPic = ConvertFiltoByte(this.picbox.ImageLocation);
            model.Gander = gen;
            model.Email = txtEmail.Text.Trim();
            model.Address = txtAddress.Text.Trim();
            
                db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                MessageBox.Show("Changes saved successfully");
            }
            redioFemale.Hide();
            redioMale.Hide();
            this.Hide();
            FamilyView fv = new FamilyView();
            fv.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            panelpro.Show();
            btn_haire.Enabled = true;
            btn_save.Show();
            try
            {
                if (dgvNannies.CurrentRow.Index != -1)
                {
                    search = txtSearch1.Text;
                    using (CMSEntities db = new CMSEntities())
                    {
                        model1 = db.NannyDatas.Where(x => x.Username == search).FirstOrDefault();
                        model2 = db.WorkCharts.Where(x => x.Username == search).FirstOrDefault();
                        txt_n.Text = model1.Name;
                        txtt.Text = model1.Username;
                        txtBirth.Text = Convert.ToString(model1.BirthDate);
                        txtedu.Text = model1.Education;
                        txtem.Text = model1.Email;
                        txtexperience.Text = model1.Experience;
                        txtexsal.Text = model1.ExSalary;
                        NidPic.Image = ConvertBytetoImage(model1.NIDPic);
                        userpic.Image = ConvertBytetoImage(model1.UserPic);
                        txtmobile.Text = model1.Mobile;
                        txtGen.Text = model1.Gander;
                        if (model2.Saturday == "Free")
                        {
                            free = " Saturday";
                        }
                        if (model2.Sunday == "Free")
                        {
                            free = free + " Sunday";
                        }
                        if (model2.Monday == "Free")
                        {
                            free = free + " Monday";
                        }
                        if (model2.Tuesday == "Free")
                        {
                            free = free + " Tuesday";
                        }
                        if (model2.Wednesday == "Free")
                        {
                            free = free + " Wednesday";
                        }
                        if (model2.Thursday == "Free")
                        {
                            free = free + " Thursday";
                        }
                        if (model2.Friday == "Free")
                        {
                            free = free + " Friday";
                        }
                        txtFree.Text = free;


                    }
                    

                }
            }
            catch
            {
                MessageBox.Show("Something wrong happend");
            }
               
            
           
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            this.Close();
            Log lg = new Log();
            lg.Show();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        
    }
}

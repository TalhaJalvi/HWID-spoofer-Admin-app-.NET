using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FireSharp.Config;
using FireSharp.Response;
using FireSharp.Interfaces;

namespace adminApplication
{
    public partial class adminSigninForm : Form
    {
        DataTable dt = new DataTable();
        public adminSigninForm()
        {
            InitializeComponent();
            //For darkmode
            darkbar.UseImmersiveDarkMode(Handle, true);
        }

        //Method for firebase connection
        IFirebaseConfig fcon = new FirebaseConfig()
        {
            AuthSecret = "NuHfD8AJJ8xOod70Au0bYtf4zjxVt7ySeGv5HBI9",
            BasePath = "https://spoofer-af6ea-default-rtdb.firebaseio.com/"
        };

        IFirebaseClient client;


        private void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.BackColor = Color.White;
            button1.ForeColor = Color.Black;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.BackColor = Color.Transparent;
            button1.ForeColor = Color.White;
        }



        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            this.subKeyTextBox1.MaxLength = 8;

        }


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void adminSigninForm_Load(object sender, EventArgs e)
        {

            //this.FormBorderStyle = FormBorderStyle.FixedSingle;
            try
            {
                client = new FireSharp.FirebaseClient(fcon);
            }
            catch
            {
                MessageBox.Show("There was an error connecting to Internet!");
            }
            //Adding columns 
            dt.Columns.Add("ID");
            dt.Columns.Add("Name");
            dt.Columns.Add("Subscription Key");
            dt.Columns.Add("Expiry Date");


            dataGridView1.DataSource = dt;


            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            dataGridView1.Columns.Add(btn);
            btn.HeaderText = "UPDATE";
            btn.Text = "Update";
            btn.Name = "updbtn";
            btn.UseColumnTextForButtonValue = true;

            DataGridViewButtonColumn btn2 = new DataGridViewButtonColumn();
            dataGridView1.Columns.Add(btn2);
            btn2.HeaderText = "DELETE";
            btn2.Text = "Delete";
            btn2.Name = "delbtn";
            btn2.UseColumnTextForButtonValue = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            customer cst = new customer()
            {
                Name = customerNametextbox.Text,
                date = dateKeyValid.Text,
                subk1 = subKeyTextBox1.Text,
            };
            string id = subKeyTextBox1.Text;
            var setter = client.Set("customerList/" + id, cst);
            MessageBox.Show("Subscription Added Successfully!");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void customerNametextbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        // Random Key
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        private void keyGeneratorButton_Click(object sender, EventArgs e)
        {
            subKeyTextBox1.Text = RandomString(8);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string id = subKeyTextBox1.Text;
            MessageBox.Show(id);
            //Fething data from database
            FirebaseResponse response = client.Get("customerList/" + id);
            customer responsecst = response.ResultAs<customer>();  //result from database
            //Now value from textboxes of current login

            customer curcst = new customer()
            {
                subk1 = subKeyTextBox1.Text,
            };

            if (customer.IsEqual(responsecst, curcst))
            {
                MessageBox.Show("This key already exists");
            }
            else
            {
                MessageBox.Show("Key is unique and does not exist for any other client!!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            export();
        }



        private async void export()
        {
            dt.Rows.Clear();
            var response = await client.GetAsync("customerList");
            var result = response.ResultAs<Dictionary<string, customer>>();
            var i = 1;
            foreach (var item in result)
            {
                var value = item.Value;
                DataRow row = dt.NewRow();
                row["ID"] = i++;
                // or item.Key
                row["Name"] = value.Name;
                row["Subscription Key"] = value.subk1;
                row["Expiry Date"] = value.date;

                dt.Rows.Add(row);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var i = 0;
            // Ignore clicks that are not in our 
            if (e.ColumnIndex == dataGridView1.Columns["delbtn"].Index && e.RowIndex >= 0)
            {
                //MessageBox.Show("Button on row {0} clicked" + e.RowIndex);
                i = e.RowIndex + 1;
                //MessageBox.Show("And index is:" + i);
                DataRow[] dr = dt.Select("ID=" + i);
                // MessageBox.Show(i.ToString());
                foreach (DataRow row in dr)
                {
                    var subscriptionKey = row["Subscription Key"].ToString();

                    var set = client.Delete("customerList/" + subscriptionKey);
                    MessageBox.Show("Data was deleted Successfully!!");
                    export();
                }
            }
            //Here update code lies
            if (e.ColumnIndex == dataGridView1.Columns["updbtn"].Index && e.RowIndex >= 0)
            {
                this.Hide();
                update up = new update();
                up.Show ();



            }
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            version svn = new version()
            {
                sName = sName.Text,
                sdate = sdate.Text,
                sversion = sversion.Text,
            };
            string svid = sversion.Text;
            var setter = client.Set("versionList/"+svid, svn);
            MessageBox.Show("Version Added Successfully!");
        }
    }

}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace adminApplication
{
    public partial class update : Form
    {
        public update()
        {
            InitializeComponent();
            darkbar.UseImmersiveDarkMode(Handle,true);
        }

        private void update_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }
    }
}

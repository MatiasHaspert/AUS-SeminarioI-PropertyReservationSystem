using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsClient;

namespace WinFormsApp
{
    public partial class PropertyListForm : Form
    {
        public PropertyListForm()
        {
            InitializeComponent();
            dgvProperties.AutoGenerateColumns = false;
        }

        private async void PropertyListForm_Load(object sender, EventArgs e)
        {
            var properties = await Program.PropertyClient.GetPropertiesAsync();
            dgvProperties.DataSource = properties?.ToList();
        }

        private void dgvProperties_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            var form = new PropertyForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                var properties = await Program.PropertyClient.GetPropertiesAsync();
                dgvProperties.DataSource = properties?.ToList();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }
    }
}

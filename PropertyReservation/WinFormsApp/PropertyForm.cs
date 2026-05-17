using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Application.DTOs.Property;

namespace WinFormsApp
{
    public partial class PropertyForm : Form
    {
        public PropertyForm()
        {
            InitializeComponent();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text) ||
                string.IsNullOrWhiteSpace(txtCountry.Text) ||
                string.IsNullOrWhiteSpace(txtState.Text) ||
                string.IsNullOrWhiteSpace(txtCity.Text) ||
                string.IsNullOrWhiteSpace(txtStreetAddress.Text) ||
                string.IsNullOrWhiteSpace(txtPostalCode.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos obligatorios.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtPostalCode.Text, out int postalCode) || postalCode <= 0)
            {
                MessageBox.Show("El código postal debe ser un número entero positivo.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var dto = new PropertyRequestDTO
            {
                Title = txtTitle.Text.Trim(),
                Description = txtDescription.Text.Trim(),
                NightlyPrice = nudNightlyPrice.Value,
                MaxGuests = (int)numMaxGuests.Value,
                Bedrooms = (int)nudBedrooms.Value,
                Bathrooms = (int)nudBathrooms.Value,
                Country = txtCountry.Text.Trim(),
                State = txtState.Text.Trim(),
                City = txtCity.Text.Trim(),
                StreetAddress = txtStreetAddress.Text.Trim(),
                PostalCode = postalCode
            };

            try
            {
                var result = await Program.PropertyClient.CreatePropertyAsync(dto);
                if (result != null)
                {
                    MessageBox.Show("Propiedad creada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al crear la propiedad: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

    }
}

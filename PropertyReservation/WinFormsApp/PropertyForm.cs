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
        private readonly int? _editingId;

        public PropertyForm()
        {
            InitializeComponent();
            lblFormTitle.Text = "Nueva propiedad";
            lblFormSubtitle.Text = "Complete los campos obligatorios (marcados con *) y presione Guardar";
            Text = "Nueva propiedad";
        }

        // CU-04: modo edición — precarga los datos.
        public PropertyForm(int propertyId) : this()
        {
            _editingId = propertyId;
            lblFormTitle.Text = "Editar propiedad";
            lblFormSubtitle.Text = $"Modificando la propiedad #{propertyId}";
            Text = "Editar propiedad";
            btnSave.Text = "Actualizar";
            Load += async (_, _) => await LoadForEditAsync();
        }

        private async Task LoadForEditAsync()
        {
            if (_editingId is null) return;
            try
            {
                var detail = await Program.PropertyClient.GetPropertyByIdAsync(_editingId.Value);
                if (detail == null) return;

                txtTitle.Text = detail.Title;
                txtDescription.Text = detail.Description;
                nudNightlyPrice.Value = detail.NightlyPrice;
                numMaxGuests.Value = detail.MaxGuests;
                nudBedrooms.Value = detail.Bedrooms;
                nudBathrooms.Value = detail.Bathrooms;
                txtCountry.Text = detail.Country;
                txtState.Text = detail.State;
                txtCity.Text = detail.City;
                txtStreetAddress.Text = detail.StreetAddress;
                txtPostalCode.Text = detail.PostalCode.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo cargar la propiedad: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                if (_editingId.HasValue)
                {
                    await Program.PropertyClient.UpdatePropertyAsync(_editingId.Value, dto);
                    MessageBox.Show("Propiedad actualizada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    var result = await Program.PropertyClient.CreatePropertyAsync(dto);
                    if (result == null) return;
                    MessageBox.Show("Propiedad creada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar la propiedad: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

    }
}

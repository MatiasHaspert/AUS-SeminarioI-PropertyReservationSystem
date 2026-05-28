namespace WinFormsApp
{
    partial class PropertyForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            pnlHeader = new Panel();
            lblFormTitle = new Label();
            lblFormSubtitle = new Label();

            pnlBody = new Panel();

            lblTitle = new Label();
            txtTitle = new TextBox();

            lblDescription = new Label();
            txtDescription = new TextBox();

            lblSectionLocation = new Label();

            lblCountry = new Label();
            txtCountry = new TextBox();

            lblState = new Label();
            txtState = new TextBox();

            lblCity = new Label();
            txtCity = new TextBox();

            lblPostalCode = new Label();
            txtPostalCode = new TextBox();

            lblStreetAddress = new Label();
            txtStreetAddress = new TextBox();

            lblSectionPricing = new Label();

            lblNightlyPrice = new Label();
            nudNightlyPrice = new NumericUpDown();

            lblMaxGuests = new Label();
            numMaxGuests = new NumericUpDown();

            lblBedrooms = new Label();
            nudBedrooms = new NumericUpDown();

            lblBathrooms = new Label();
            nudBathrooms = new NumericUpDown();

            pnlActions = new Panel();
            btnSave = new Button();
            btnCancel = new Button();

            pnlHeader.SuspendLayout();
            pnlBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudNightlyPrice).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numMaxGuests).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudBedrooms).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudBathrooms).BeginInit();
            pnlActions.SuspendLayout();
            SuspendLayout();

            // pnlHeader
            pnlHeader.BackColor = Color.FromArgb(44, 62, 80);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Height = 70;
            pnlHeader.Controls.Add(lblFormTitle);
            pnlHeader.Controls.Add(lblFormSubtitle);

            lblFormTitle.Text = "Propiedad";
            lblFormTitle.ForeColor = Color.White;
            lblFormTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblFormTitle.AutoSize = true;
            lblFormTitle.Location = new Point(22, 12);

            lblFormSubtitle.Text = "Complete los campos obligatorios y presione Guardar";
            lblFormSubtitle.ForeColor = Color.FromArgb(189, 195, 199);
            lblFormSubtitle.Font = new Font("Segoe UI", 9F);
            lblFormSubtitle.AutoSize = true;
            lblFormSubtitle.Location = new Point(24, 43);

            // pnlBody
            pnlBody.AutoScroll = true;
            pnlBody.BackColor = Color.White;
            pnlBody.Dock = DockStyle.Fill;
            pnlBody.Padding = new Padding(24, 18, 24, 18);
            pnlBody.Controls.AddRange(new Control[]
            {
                lblTitle, txtTitle,
                lblDescription, txtDescription,
                lblSectionLocation,
                lblCountry, txtCountry,
                lblState, txtState,
                lblCity, txtCity,
                lblPostalCode, txtPostalCode,
                lblStreetAddress, txtStreetAddress,
                lblSectionPricing,
                lblNightlyPrice, nudNightlyPrice,
                lblMaxGuests, numMaxGuests,
                lblBedrooms, nudBedrooms,
                lblBathrooms, nudBathrooms
            });

            //
            // Título
            //
            lblTitle.Text = "Título *";
            lblTitle.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(52, 73, 94);
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(24, 16);

            txtTitle.Location = new Point(24, 38);
            txtTitle.Size = new Size(700, 26);
            txtTitle.Font = new Font("Segoe UI", 9.75F);
            txtTitle.BorderStyle = BorderStyle.FixedSingle;

            //
            // Descripción
            //
            lblDescription.Text = "Descripción";
            lblDescription.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            lblDescription.ForeColor = Color.FromArgb(52, 73, 94);
            lblDescription.AutoSize = true;
            lblDescription.Location = new Point(24, 78);

            txtDescription.Location = new Point(24, 100);
            txtDescription.Size = new Size(700, 90);
            txtDescription.Multiline = true;
            txtDescription.Font = new Font("Segoe UI", 9.75F);
            txtDescription.BorderStyle = BorderStyle.FixedSingle;
            txtDescription.ScrollBars = ScrollBars.Vertical;

            //
            // Sección Ubicación
            //
            lblSectionLocation.Text = "Ubicación";
            lblSectionLocation.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold);
            lblSectionLocation.ForeColor = Color.FromArgb(41, 128, 185);
            lblSectionLocation.AutoSize = true;
            lblSectionLocation.Location = new Point(24, 210);

            lblCountry.Text = "País *";
            lblCountry.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            lblCountry.ForeColor = Color.FromArgb(52, 73, 94);
            lblCountry.AutoSize = true;
            lblCountry.Location = new Point(24, 244);

            txtCountry.Location = new Point(24, 266);
            txtCountry.Size = new Size(340, 26);
            txtCountry.Font = new Font("Segoe UI", 9.75F);
            txtCountry.BorderStyle = BorderStyle.FixedSingle;

            lblState.Text = "Provincia *";
            lblState.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            lblState.ForeColor = Color.FromArgb(52, 73, 94);
            lblState.AutoSize = true;
            lblState.Location = new Point(384, 244);

            txtState.Location = new Point(384, 266);
            txtState.Size = new Size(340, 26);
            txtState.Font = new Font("Segoe UI", 9.75F);
            txtState.BorderStyle = BorderStyle.FixedSingle;

            lblCity.Text = "Ciudad *";
            lblCity.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            lblCity.ForeColor = Color.FromArgb(52, 73, 94);
            lblCity.AutoSize = true;
            lblCity.Location = new Point(24, 306);

            txtCity.Location = new Point(24, 328);
            txtCity.Size = new Size(340, 26);
            txtCity.Font = new Font("Segoe UI", 9.75F);
            txtCity.BorderStyle = BorderStyle.FixedSingle;

            lblPostalCode.Text = "Código Postal *";
            lblPostalCode.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            lblPostalCode.ForeColor = Color.FromArgb(52, 73, 94);
            lblPostalCode.AutoSize = true;
            lblPostalCode.Location = new Point(384, 306);

            txtPostalCode.Location = new Point(384, 328);
            txtPostalCode.Size = new Size(340, 26);
            txtPostalCode.Font = new Font("Segoe UI", 9.75F);
            txtPostalCode.BorderStyle = BorderStyle.FixedSingle;

            lblStreetAddress.Text = "Dirección *";
            lblStreetAddress.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            lblStreetAddress.ForeColor = Color.FromArgb(52, 73, 94);
            lblStreetAddress.AutoSize = true;
            lblStreetAddress.Location = new Point(24, 368);

            txtStreetAddress.Location = new Point(24, 390);
            txtStreetAddress.Size = new Size(700, 26);
            txtStreetAddress.Font = new Font("Segoe UI", 9.75F);
            txtStreetAddress.BorderStyle = BorderStyle.FixedSingle;

            //
            // Sección Precio y capacidad
            //
            lblSectionPricing.Text = "Precio y capacidad";
            lblSectionPricing.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold);
            lblSectionPricing.ForeColor = Color.FromArgb(41, 128, 185);
            lblSectionPricing.AutoSize = true;
            lblSectionPricing.Location = new Point(24, 438);

            lblNightlyPrice.Text = "Precio por noche *";
            lblNightlyPrice.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            lblNightlyPrice.ForeColor = Color.FromArgb(52, 73, 94);
            lblNightlyPrice.AutoSize = true;
            lblNightlyPrice.Location = new Point(24, 472);

            nudNightlyPrice.DecimalPlaces = 2;
            nudNightlyPrice.Increment = new decimal(new int[] { 100, 0, 0, 0 });
            nudNightlyPrice.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            nudNightlyPrice.Location = new Point(24, 494);
            nudNightlyPrice.Size = new Size(340, 26);
            nudNightlyPrice.Font = new Font("Segoe UI", 9.75F);

            lblMaxGuests.Text = "Huéspedes máximos";
            lblMaxGuests.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            lblMaxGuests.ForeColor = Color.FromArgb(52, 73, 94);
            lblMaxGuests.AutoSize = true;
            lblMaxGuests.Location = new Point(384, 472);

            numMaxGuests.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            numMaxGuests.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numMaxGuests.Value = new decimal(new int[] { 1, 0, 0, 0 });
            numMaxGuests.Location = new Point(384, 494);
            numMaxGuests.Size = new Size(340, 26);
            numMaxGuests.Font = new Font("Segoe UI", 9.75F);

            lblBedrooms.Text = "Habitaciones";
            lblBedrooms.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            lblBedrooms.ForeColor = Color.FromArgb(52, 73, 94);
            lblBedrooms.AutoSize = true;
            lblBedrooms.Location = new Point(24, 534);

            nudBedrooms.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            nudBedrooms.Location = new Point(24, 556);
            nudBedrooms.Size = new Size(340, 26);
            nudBedrooms.Font = new Font("Segoe UI", 9.75F);

            lblBathrooms.Text = "Baños";
            lblBathrooms.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            lblBathrooms.ForeColor = Color.FromArgb(52, 73, 94);
            lblBathrooms.AutoSize = true;
            lblBathrooms.Location = new Point(384, 534);

            nudBathrooms.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            nudBathrooms.Location = new Point(384, 556);
            nudBathrooms.Size = new Size(340, 26);
            nudBathrooms.Font = new Font("Segoe UI", 9.75F);

            // pnlActions
            pnlActions.BackColor = Color.FromArgb(236, 240, 241);
            pnlActions.Dock = DockStyle.Bottom;
            pnlActions.Height = 64;
            pnlActions.Padding = new Padding(24, 14, 24, 14);
            pnlActions.Controls.Add(btnSave);
            pnlActions.Controls.Add(btnCancel);

            btnSave.Text = "Guardar";
            btnSave.Size = new Size(120, 36);
            btnSave.Location = new Point(640, 14);
            btnSave.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.BackColor = Color.FromArgb(41, 128, 185);
            btnSave.ForeColor = Color.White;
            btnSave.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            btnSave.Cursor = Cursors.Hand;
            btnSave.Click += btnSave_Click;

            btnCancel.Text = "Cancelar";
            btnCancel.Size = new Size(110, 36);
            btnCancel.Location = new Point(524, 14);
            btnCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.FlatAppearance.BorderSize = 1;
            btnCancel.FlatAppearance.BorderColor = Color.FromArgb(189, 195, 199);
            btnCancel.BackColor = Color.White;
            btnCancel.ForeColor = Color.FromArgb(52, 73, 94);
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.Click += btnCancel_Click;

            // PropertyForm
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(770, 720);
            Controls.Add(pnlBody);
            Controls.Add(pnlActions);
            Controls.Add(pnlHeader);
            Font = new Font("Segoe UI", 9F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterParent;
            AcceptButton = btnSave;
            CancelButton = btnCancel;
            Name = "PropertyForm";
            Text = "Propiedad";

            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlBody.ResumeLayout(false);
            pnlBody.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudNightlyPrice).EndInit();
            ((System.ComponentModel.ISupportInitialize)numMaxGuests).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudBedrooms).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudBathrooms).EndInit();
            pnlActions.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlHeader;
        private Label lblFormTitle;
        private Label lblFormSubtitle;

        private Panel pnlBody;

        private Label lblTitle;
        private TextBox txtTitle;
        private Label lblDescription;
        private TextBox txtDescription;

        private Label lblSectionLocation;

        private Label lblCountry;
        private TextBox txtCountry;
        private Label lblState;
        private TextBox txtState;
        private Label lblCity;
        private TextBox txtCity;
        private Label lblPostalCode;
        private TextBox txtPostalCode;
        private Label lblStreetAddress;
        private TextBox txtStreetAddress;

        private Label lblSectionPricing;

        private Label lblNightlyPrice;
        private NumericUpDown nudNightlyPrice;
        private Label lblMaxGuests;
        private NumericUpDown numMaxGuests;
        private Label lblBedrooms;
        private NumericUpDown nudBedrooms;
        private Label lblBathrooms;
        private NumericUpDown nudBathrooms;

        private Panel pnlActions;
        private Button btnSave;
        private Button btnCancel;
    }
}

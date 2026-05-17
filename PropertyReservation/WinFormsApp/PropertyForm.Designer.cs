namespace WinFormsApp
{
    partial class PropertyForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tableLayoutPanel1 = new TableLayoutPanel();
            label1 = new Label();
            txtTitle = new TextBox();
            label2 = new Label();
            txtDescription = new TextBox();
            label3 = new Label();
            numPrice = new NumericUpDown();
            label4 = new Label();
            numMaxGuests = new NumericUpDown();
            label5 = new Label();
            txtCountry = new TextBox();
            label6 = new Label();
            txtState = new TextBox();
            label7 = new Label();
            txtCity = new TextBox();
            label12 = new Label();
            txtPostalCode = new TextBox();
            label11 = new Label();
            txtStreetAddress = new TextBox();
            label8 = new Label();
            nudNightlyPrice = new NumericUpDown();
            label9 = new Label();
            nudBedrooms = new NumericUpDown();
            label10 = new Label();
            nudBathrooms = new NumericUpDown();
            flowLayoutPanel1 = new FlowLayoutPanel();
            btnSave = new Button();
            btnCancel = new Button();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numPrice).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numMaxGuests).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudNightlyPrice).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudBedrooms).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudBathrooms).BeginInit();
            flowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(txtTitle, 1, 0);
            tableLayoutPanel1.Controls.Add(label2, 0, 1);
            tableLayoutPanel1.Controls.Add(txtDescription, 1, 1);
            tableLayoutPanel1.Controls.Add(label3, 0, 2);
            tableLayoutPanel1.Controls.Add(numPrice, 1, 2);
            tableLayoutPanel1.Controls.Add(label4, 0, 3);
            tableLayoutPanel1.Controls.Add(numMaxGuests, 1, 3);
            tableLayoutPanel1.Controls.Add(label5, 0, 4);
            tableLayoutPanel1.Controls.Add(txtCountry, 1, 4);
            tableLayoutPanel1.Controls.Add(label6, 0, 5);
            tableLayoutPanel1.Controls.Add(txtState, 1, 5);
            tableLayoutPanel1.Controls.Add(label7, 0, 6);
            tableLayoutPanel1.Controls.Add(txtCity, 1, 6);
            tableLayoutPanel1.Controls.Add(label12, 0, 7);
            tableLayoutPanel1.Controls.Add(txtPostalCode, 1, 7);
            tableLayoutPanel1.Controls.Add(label11, 0, 8);
            tableLayoutPanel1.Controls.Add(txtStreetAddress, 1, 8);
            tableLayoutPanel1.Controls.Add(label8, 0, 9);
            tableLayoutPanel1.Controls.Add(nudNightlyPrice, 1, 9);
            tableLayoutPanel1.Controls.Add(label9, 0, 10);
            tableLayoutPanel1.Controls.Add(nudBedrooms, 1, 10);
            tableLayoutPanel1.Controls.Add(label10, 0, 11);
            tableLayoutPanel1.Controls.Add(nudBathrooms, 1, 11);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 13;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 80F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 33F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 31F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 39F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 74F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 41F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 106F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(800, 632);
            tableLayoutPanel1.TabIndex = 0;
            tableLayoutPanel1.Paint += tableLayoutPanel1_Paint;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(38, 15);
            label1.TabIndex = 0;
            label1.Text = "Título";
            // 
            // txtTitle
            // 
            txtTitle.Dock = DockStyle.Fill;
            txtTitle.Location = new Point(108, 3);
            txtTitle.Name = "txtTitle";
            txtTitle.Size = new Size(689, 23);
            txtTitle.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(3, 32);
            label2.Name = "label2";
            label2.Size = new Size(69, 15);
            label2.TabIndex = 2;
            label2.Text = "Descripción";
            // 
            // txtDescription
            // 
            txtDescription.Dock = DockStyle.Fill;
            txtDescription.Location = new Point(108, 35);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(689, 122);
            txtDescription.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(3, 160);
            label3.Name = "label3";
            label3.Size = new Size(40, 15);
            label3.TabIndex = 4;
            label3.Text = "Precio";
            // 
            // numPrice
            // 
            numPrice.DecimalPlaces = 2;
            numPrice.Dock = DockStyle.Fill;
            numPrice.Location = new Point(108, 163);
            numPrice.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            numPrice.Name = "numPrice";
            numPrice.Size = new Size(689, 23);
            numPrice.TabIndex = 5;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(3, 190);
            label4.Name = "label4";
            label4.Size = new Size(65, 15);
            label4.TabIndex = 6;
            label4.Text = "Huéspedes";
            // 
            // numMaxGuests
            // 
            numMaxGuests.Dock = DockStyle.Fill;
            numMaxGuests.Location = new Point(108, 193);
            numMaxGuests.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            numMaxGuests.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numMaxGuests.Name = "numMaxGuests";
            numMaxGuests.Size = new Size(689, 23);
            numMaxGuests.TabIndex = 7;
            numMaxGuests.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(3, 223);
            label5.Name = "label5";
            label5.Size = new Size(28, 15);
            label5.TabIndex = 8;
            label5.Text = "País";
            // 
            // txtCountry
            // 
            txtCountry.Dock = DockStyle.Fill;
            txtCountry.Location = new Point(108, 226);
            txtCountry.Name = "txtCountry";
            txtCountry.Size = new Size(689, 23);
            txtCountry.TabIndex = 9;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(3, 258);
            label6.Name = "label6";
            label6.Size = new Size(56, 15);
            label6.TabIndex = 10;
            label6.Text = "Provincia";
            // 
            // txtState
            // 
            txtState.Dock = DockStyle.Fill;
            txtState.Location = new Point(108, 261);
            txtState.Name = "txtState";
            txtState.Size = new Size(689, 23);
            txtState.TabIndex = 11;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(3, 289);
            label7.Name = "label7";
            label7.Size = new Size(45, 15);
            label7.TabIndex = 12;
            label7.Text = "Ciudad";
            // 
            // txtCity
            // 
            txtCity.Dock = DockStyle.Fill;
            txtCity.Location = new Point(108, 292);
            txtCity.Name = "txtCity";
            txtCity.Size = new Size(689, 23);
            txtCity.TabIndex = 13;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(3, 328);
            label12.Name = "label12";
            label12.Size = new Size(81, 15);
            label12.TabIndex = 14;
            label12.Text = "Código Postal";
            // 
            // txtPostalCode
            // 
            txtPostalCode.Dock = DockStyle.Fill;
            txtPostalCode.Location = new Point(108, 331);
            txtPostalCode.Name = "txtPostalCode";
            txtPostalCode.Size = new Size(689, 23);
            txtPostalCode.TabIndex = 15;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(3, 402);
            label11.Name = "label11";
            label11.Size = new Size(57, 15);
            label11.TabIndex = 16;
            label11.Text = "Dirección";
            // 
            // txtStreetAddress
            // 
            txtStreetAddress.Dock = DockStyle.Fill;
            txtStreetAddress.Location = new Point(108, 405);
            txtStreetAddress.Name = "txtStreetAddress";
            txtStreetAddress.Size = new Size(689, 23);
            txtStreetAddress.TabIndex = 17;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(3, 443);
            label8.Name = "label8";
            label8.Size = new Size(99, 15);
            label8.TabIndex = 18;
            label8.Text = "Precio por Noche";
            // 
            // nudNightlyPrice
            // 
            nudNightlyPrice.DecimalPlaces = 2;
            nudNightlyPrice.Dock = DockStyle.Fill;
            nudNightlyPrice.Increment = new decimal(new int[] { 100, 0, 0, 0 });
            nudNightlyPrice.Location = new Point(108, 446);
            nudNightlyPrice.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            nudNightlyPrice.Name = "nudNightlyPrice";
            nudNightlyPrice.Size = new Size(689, 23);
            nudNightlyPrice.TabIndex = 19;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(3, 475);
            label9.Name = "label9";
            label9.Size = new Size(76, 15);
            label9.TabIndex = 20;
            label9.Text = "Habitaciones";
            // 
            // nudBedrooms
            // 
            nudBedrooms.Dock = DockStyle.Fill;
            nudBedrooms.Location = new Point(108, 478);
            nudBedrooms.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            nudBedrooms.Name = "nudBedrooms";
            nudBedrooms.Size = new Size(689, 23);
            nudBedrooms.TabIndex = 21;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(3, 505);
            label10.Name = "label10";
            label10.Size = new Size(39, 15);
            label10.TabIndex = 22;
            label10.Text = "Baños";
            // 
            // nudBathrooms
            // 
            nudBathrooms.Dock = DockStyle.Fill;
            nudBathrooms.Location = new Point(108, 508);
            nudBathrooms.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            nudBathrooms.Name = "nudBathrooms";
            nudBathrooms.Size = new Size(689, 23);
            nudBathrooms.TabIndex = 23;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(btnSave);
            flowLayoutPanel1.Controls.Add(btnCancel);
            flowLayoutPanel1.Dock = DockStyle.Bottom;
            flowLayoutPanel1.FlowDirection = FlowDirection.RightToLeft;
            flowLayoutPanel1.Location = new Point(0, 582);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Padding = new Padding(10);
            flowLayoutPanel1.Size = new Size(800, 50);
            flowLayoutPanel1.TabIndex = 1;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(702, 13);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 23);
            btnSave.TabIndex = 0;
            btnSave.Text = "Guardar";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(621, 13);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 1;
            btnCancel.Text = "Cancelar";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // PropertyForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 632);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(tableLayoutPanel1);
            Name = "PropertyForm";
            Text = "PropertyForm";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numPrice).EndInit();
            ((System.ComponentModel.ISupportInitialize)numMaxGuests).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudNightlyPrice).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudBedrooms).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudBathrooms).EndInit();
            flowLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Label label1;
        private TextBox txtTitle;
        private Label label2;
        private TextBox txtDescription;
        private Label label3;
        private NumericUpDown numPrice;
        private Label label4;
        private NumericUpDown numMaxGuests;
        private Label label5;
        private TextBox txtCountry;
        private Label label6;
        private TextBox txtState;
        private Label label7;
        private TextBox txtCity;
        private FlowLayoutPanel flowLayoutPanel1;
        private Button btnSave;
        private Button btnCancel;
        private Label label8;
        private NumericUpDown nudNightlyPrice;
        private Label label9;
        private NumericUpDown nudBedrooms;
        private Label label10;
        private NumericUpDown nudBathrooms;
        private Label label11;
        private TextBox txtStreetAddress;
        private Label label12;
        private TextBox txtPostalCode;
    }
}
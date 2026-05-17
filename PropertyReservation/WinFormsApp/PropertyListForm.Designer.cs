namespace WinFormsApp
{
    partial class PropertyListForm
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
            dgvProperties = new DataGridView();
            colId = new DataGridViewTextBoxColumn();
            colTitle = new DataGridViewTextBoxColumn();
            colNightlyPrice = new DataGridViewTextBoxColumn();
            colMaxGuests = new DataGridViewTextBoxColumn();
            colBedrooms = new DataGridViewTextBoxColumn();
            colBathrooms = new DataGridViewTextBoxColumn();
            colCountry = new DataGridViewTextBoxColumn();
            colState = new DataGridViewTextBoxColumn();
            colCity = new DataGridViewTextBoxColumn();
            btnAdd = new Button();
            btnEdit = new Button();
            btnDelete = new Button();
            pnlActions = new Panel();
            ((System.ComponentModel.ISupportInitialize)dgvProperties).BeginInit();
            pnlActions.SuspendLayout();
            SuspendLayout();
            // 
            // dgvProperties
            // 
            dgvProperties.AllowUserToAddRows = false;
            dgvProperties.AllowUserToDeleteRows = false;
            dgvProperties.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProperties.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProperties.Columns.AddRange(new DataGridViewColumn[] { colId, colTitle, colNightlyPrice, colMaxGuests, colBedrooms, colBathrooms, colCountry, colState, colCity });
            dgvProperties.Dock = DockStyle.Fill;
            dgvProperties.Location = new Point(0, 60);
            dgvProperties.MultiSelect = false;
            dgvProperties.Name = "dgvProperties";
            dgvProperties.ReadOnly = true;
            dgvProperties.RowHeadersVisible = false;
            dgvProperties.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProperties.Size = new Size(800, 390);
            dgvProperties.TabIndex = 0;
            dgvProperties.CellContentClick += dgvProperties_CellContentClick;
            // 
            // colId
            // 
            colId.DataPropertyName = "id";
            colId.HeaderText = "Id";
            colId.Name = "colId";
            colId.ReadOnly = true;
            colId.Visible = false;
            // 
            // colTitle
            // 
            colTitle.DataPropertyName = "title";
            colTitle.HeaderText = "Title";
            colTitle.Name = "colTitle";
            colTitle.ReadOnly = true;
            // 
            // colNightlyPrice
            // 
            colNightlyPrice.DataPropertyName = "nigthlyPrice";
            colNightlyPrice.HeaderText = "Precio por Noche";
            colNightlyPrice.Name = "colNightlyPrice";
            colNightlyPrice.ReadOnly = true;
            // 
            // colMaxGuests
            // 
            colMaxGuests.DataPropertyName = "maxGuests";
            colMaxGuests.HeaderText = "Huéspedes máx.";
            colMaxGuests.Name = "colMaxGuests";
            colMaxGuests.ReadOnly = true;
            // 
            // colBedrooms
            // 
            colBedrooms.DataPropertyName = "bedrooms";
            colBedrooms.HeaderText = "Habitaciones";
            colBedrooms.Name = "colBedrooms";
            colBedrooms.ReadOnly = true;
            // 
            // colBathrooms
            // 
            colBathrooms.DataPropertyName = "bathrooms";
            colBathrooms.HeaderText = "Baños";
            colBathrooms.Name = "colBathrooms";
            colBathrooms.ReadOnly = true;
            // 
            // colCountry
            // 
            colCountry.DataPropertyName = "country";
            colCountry.HeaderText = "País";
            colCountry.Name = "colCountry";
            colCountry.ReadOnly = true;
            // 
            // colState
            // 
            colState.DataPropertyName = "state";
            colState.HeaderText = "Provincia";
            colState.Name = "colState";
            colState.ReadOnly = true;
            // 
            // colCity
            // 
            colCity.DataPropertyName = "city";
            colCity.HeaderText = "Ciudad";
            colCity.Name = "colCity";
            colCity.ReadOnly = true;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(35, 19);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(75, 23);
            btnAdd.TabIndex = 1;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnEdit
            // 
            btnEdit.Location = new Point(346, 19);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(75, 23);
            btnEdit.TabIndex = 2;
            btnEdit.Text = "Edit";
            btnEdit.UseVisualStyleBackColor = true;
            btnEdit.Click += btnEdit_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(677, 19);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(75, 23);
            btnDelete.TabIndex = 3;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // pnlActions
            // 
            pnlActions.Controls.Add(btnDelete);
            pnlActions.Controls.Add(btnEdit);
            pnlActions.Controls.Add(btnAdd);
            pnlActions.Dock = DockStyle.Top;
            pnlActions.Location = new Point(0, 0);
            pnlActions.Name = "pnlActions";
            pnlActions.Size = new Size(800, 60);
            pnlActions.TabIndex = 4;
            // 
            // PropertyListForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dgvProperties);
            Controls.Add(pnlActions);
            Name = "PropertyListForm";
            Text = "PropertyListForm";
            Load += PropertyListForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvProperties).EndInit();
            pnlActions.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlActions;
        private DataGridView dgvProperties;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnDelete;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colTitle;
        private DataGridViewTextBoxColumn colNightlyPrice;
        private DataGridViewTextBoxColumn colMaxGuests;
        private DataGridViewTextBoxColumn colBedrooms;
        private DataGridViewTextBoxColumn colBathrooms;
        private DataGridViewTextBoxColumn colCountry;
        private DataGridViewTextBoxColumn colState;
        private DataGridViewTextBoxColumn colCity;
    }
}
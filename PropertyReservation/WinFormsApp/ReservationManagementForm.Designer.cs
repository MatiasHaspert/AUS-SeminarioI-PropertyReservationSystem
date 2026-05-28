namespace WinFormsApp
{
    partial class ReservationManagementForm
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
            components = new System.ComponentModel.Container();

            pnlHeader = new Panel();
            lblFormTitle = new Label();
            lblFormSubtitle = new Label();

            pnlFilters = new Panel();
            lblStatusFilter = new Label(); cboStatusFilter = new ComboBox();
            lblPropertyFilter = new Label(); numPropertyFilter = new NumericUpDown();
            lblGuestFilter = new Label(); numGuestFilter = new NumericUpDown();
            lblFrom = new Label(); dtpFrom = new DateTimePicker();
            lblTo = new Label(); dtpTo = new DateTimePicker();
            btnApplyFilter = new Button(); btnClearFilter = new Button();

            dgvReservations = new DataGridView();
            colId = new DataGridViewTextBoxColumn();
            colProperty = new DataGridViewTextBoxColumn();
            colGuest = new DataGridViewTextBoxColumn();
            colStart = new DataGridViewTextBoxColumn();
            colEnd = new DataGridViewTextBoxColumn();
            colTotal = new DataGridViewTextBoxColumn();
            colStatus = new DataGridViewTextBoxColumn();

            pnlActions = new Panel();
            btnDetail = new Button();
            btnChangeStatus = new Button();
            btnCancel = new Button();
            btnRefresh = new Button();

            lblStatus = new Label();

            pnlHeader.SuspendLayout();
            pnlFilters.SuspendLayout();
            pnlActions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvReservations).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numPropertyFilter).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numGuestFilter).BeginInit();
            SuspendLayout();

            // pnlHeader
            pnlHeader.BackColor = Color.FromArgb(44, 62, 80);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Height = 70;
            pnlHeader.Controls.Add(lblFormTitle);
            pnlHeader.Controls.Add(lblFormSubtitle);

            lblFormTitle.Text = "Gestionar Reservas";
            lblFormTitle.ForeColor = Color.White;
            lblFormTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblFormTitle.AutoSize = true;
            lblFormTitle.Location = new Point(22, 12);

            lblFormSubtitle.Text = "Ver detalle, cambiar estado (transiciones válidas) y cancelar reservas";
            lblFormSubtitle.ForeColor = Color.FromArgb(189, 195, 199);
            lblFormSubtitle.Font = new Font("Segoe UI", 9F);
            lblFormSubtitle.AutoSize = true;
            lblFormSubtitle.Location = new Point(24, 43);

            // pnlFilters
            pnlFilters.Dock = DockStyle.Top;
            pnlFilters.Height = 100;
            pnlFilters.Padding = new Padding(18, 12, 18, 12);
            pnlFilters.BackColor = Color.FromArgb(236, 240, 241);
            pnlFilters.Controls.AddRange(new Control[] { lblStatusFilter, cboStatusFilter, lblPropertyFilter, numPropertyFilter, lblGuestFilter, numGuestFilter, lblFrom, dtpFrom, lblTo, dtpTo, btnApplyFilter, btnClearFilter });

            lblStatusFilter.Text = "Estado:";
            lblStatusFilter.AutoSize = true;
            lblStatusFilter.Location = new Point(22, 22);
            lblStatusFilter.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            lblStatusFilter.ForeColor = Color.FromArgb(52, 73, 94);

            cboStatusFilter.Location = new Point(85, 18);
            cboStatusFilter.Width = 150;
            cboStatusFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cboStatusFilter.FlatStyle = FlatStyle.Flat;
            cboStatusFilter.Items.AddRange(new object[] { "(Todos)", "PendingPayment", "PaymentUploaded", "Confirmed", "Rejected", "Expired", "Cancelled", "Completed" });
            cboStatusFilter.SelectedIndex = 0;

            lblPropertyFilter.Text = "Propiedad Id:";
            lblPropertyFilter.AutoSize = true;
            lblPropertyFilter.Location = new Point(255, 22);
            lblPropertyFilter.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            lblPropertyFilter.ForeColor = Color.FromArgb(52, 73, 94);

            numPropertyFilter.Location = new Point(345, 18);
            numPropertyFilter.Width = 90;
            numPropertyFilter.Minimum = 0;
            numPropertyFilter.Maximum = 999999;
            numPropertyFilter.Font = new Font("Segoe UI", 9.5F);

            lblGuestFilter.Text = "Huésped Id:";
            lblGuestFilter.AutoSize = true;
            lblGuestFilter.Location = new Point(455, 22);
            lblGuestFilter.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            lblGuestFilter.ForeColor = Color.FromArgb(52, 73, 94);

            numGuestFilter.Location = new Point(535, 18);
            numGuestFilter.Width = 90;
            numGuestFilter.Minimum = 0;
            numGuestFilter.Maximum = 999999;
            numGuestFilter.Font = new Font("Segoe UI", 9.5F);

            lblFrom.Text = "Desde:";
            lblFrom.AutoSize = true;
            lblFrom.Location = new Point(22, 62);
            lblFrom.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            lblFrom.ForeColor = Color.FromArgb(52, 73, 94);

            dtpFrom.Location = new Point(85, 58);
            dtpFrom.Width = 150;
            dtpFrom.Format = DateTimePickerFormat.Short;
            dtpFrom.ShowCheckBox = true;
            dtpFrom.Checked = false;
            dtpFrom.Font = new Font("Segoe UI", 9.5F);

            lblTo.Text = "Hasta:";
            lblTo.AutoSize = true;
            lblTo.Location = new Point(255, 62);
            lblTo.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            lblTo.ForeColor = Color.FromArgb(52, 73, 94);

            dtpTo.Location = new Point(345, 58);
            dtpTo.Width = 150;
            dtpTo.Format = DateTimePickerFormat.Short;
            dtpTo.ShowCheckBox = true;
            dtpTo.Checked = false;
            dtpTo.Font = new Font("Segoe UI", 9.5F);

            btnApplyFilter.Text = "Filtrar";
            btnApplyFilter.Location = new Point(650, 16);
            btnApplyFilter.Size = new Size(95, 30);
            btnApplyFilter.FlatStyle = FlatStyle.Flat;
            btnApplyFilter.FlatAppearance.BorderSize = 0;
            btnApplyFilter.BackColor = Color.FromArgb(41, 128, 185);
            btnApplyFilter.ForeColor = Color.White;
            btnApplyFilter.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            btnApplyFilter.Cursor = Cursors.Hand;
            btnApplyFilter.Click += btnApplyFilter_Click;

            btnClearFilter.Text = "Limpiar";
            btnClearFilter.Location = new Point(650, 56);
            btnClearFilter.Size = new Size(95, 30);
            btnClearFilter.FlatStyle = FlatStyle.Flat;
            btnClearFilter.FlatAppearance.BorderSize = 1;
            btnClearFilter.FlatAppearance.BorderColor = Color.FromArgb(189, 195, 199);
            btnClearFilter.BackColor = Color.White;
            btnClearFilter.ForeColor = Color.FromArgb(52, 73, 94);
            btnClearFilter.Cursor = Cursors.Hand;
            btnClearFilter.Click += btnClearFilter_Click;

            // dgvReservations
            dgvReservations.Dock = DockStyle.Fill;
            dgvReservations.AutoGenerateColumns = false;
            dgvReservations.AllowUserToAddRows = false;
            dgvReservations.AllowUserToDeleteRows = false;
            dgvReservations.ReadOnly = true;
            dgvReservations.RowHeadersVisible = false;
            dgvReservations.MultiSelect = false;
            dgvReservations.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvReservations.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvReservations.BackgroundColor = Color.White;
            dgvReservations.BorderStyle = BorderStyle.None;
            dgvReservations.Font = new Font("Segoe UI", 9F);
            dgvReservations.Columns.AddRange(new DataGridViewColumn[] { colId, colProperty, colGuest, colStart, colEnd, colTotal, colStatus });

            colId.HeaderText = "Id"; colId.DataPropertyName = "id"; colId.FillWeight = 40;
            colProperty.HeaderText = "Propiedad"; colProperty.DataPropertyName = "propertyTitle"; colProperty.FillWeight = 160;
            colGuest.HeaderText = "Huésped"; colGuest.DataPropertyName = "guestName"; colGuest.FillWeight = 140;
            colStart.HeaderText = "Desde"; colStart.DataPropertyName = "startDate"; colStart.FillWeight = 80; colStart.DefaultCellStyle.Format = "dd/MM/yyyy";
            colEnd.HeaderText = "Hasta"; colEnd.DataPropertyName = "endDate"; colEnd.FillWeight = 80; colEnd.DefaultCellStyle.Format = "dd/MM/yyyy";
            colTotal.HeaderText = "Total"; colTotal.DataPropertyName = "totalPrice"; colTotal.FillWeight = 80; colTotal.DefaultCellStyle.Format = "C2";
            colStatus.HeaderText = "Estado"; colStatus.DataPropertyName = "status"; colStatus.FillWeight = 110;

            // pnlActions
            pnlActions.Dock = DockStyle.Bottom;
            pnlActions.Height = 64;
            pnlActions.BackColor = Color.White;
            pnlActions.Padding = new Padding(18, 14, 18, 14);
            pnlActions.Controls.AddRange(new Control[] { btnDetail, btnChangeStatus, btnCancel, btnRefresh });

            btnDetail.Text = "Ver detalle";
            btnDetail.Location = new Point(18, 17);
            btnDetail.Size = new Size(115, 34);
            btnDetail.FlatStyle = FlatStyle.Flat;
            btnDetail.FlatAppearance.BorderSize = 1;
            btnDetail.FlatAppearance.BorderColor = Color.FromArgb(189, 195, 199);
            btnDetail.BackColor = Color.White;
            btnDetail.ForeColor = Color.FromArgb(52, 73, 94);
            btnDetail.Cursor = Cursors.Hand;
            btnDetail.Click += btnDetail_Click;

            btnChangeStatus.Text = "Cambiar estado";
            btnChangeStatus.Location = new Point(140, 17);
            btnChangeStatus.Size = new Size(140, 34);
            btnChangeStatus.FlatStyle = FlatStyle.Flat;
            btnChangeStatus.FlatAppearance.BorderSize = 0;
            btnChangeStatus.BackColor = Color.FromArgb(41, 128, 185);
            btnChangeStatus.ForeColor = Color.White;
            btnChangeStatus.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            btnChangeStatus.Cursor = Cursors.Hand;
            btnChangeStatus.Click += btnChangeStatus_Click;

            btnCancel.Text = "Cancelar reserva";
            btnCancel.Location = new Point(287, 17);
            btnCancel.Size = new Size(150, 34);
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.BackColor = Color.FromArgb(192, 57, 43);
            btnCancel.ForeColor = Color.White;
            btnCancel.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.Click += btnCancel_Click;

            btnRefresh.Text = "Refrescar";
            btnRefresh.Location = new Point(950, 17);
            btnRefresh.Size = new Size(110, 34);
            btnRefresh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.FlatAppearance.BorderSize = 1;
            btnRefresh.FlatAppearance.BorderColor = Color.FromArgb(189, 195, 199);
            btnRefresh.BackColor = Color.White;
            btnRefresh.ForeColor = Color.FromArgb(52, 73, 94);
            btnRefresh.Cursor = Cursors.Hand;
            btnRefresh.Click += btnRefresh_Click;

            // lblStatus
            lblStatus.Dock = DockStyle.Bottom;
            lblStatus.Height = 26;
            lblStatus.TextAlign = ContentAlignment.MiddleLeft;
            lblStatus.Padding = new Padding(20, 0, 20, 0);
            lblStatus.BackColor = Color.FromArgb(236, 240, 241);
            lblStatus.ForeColor = Color.FromArgb(52, 73, 94);
            lblStatus.Font = new Font("Segoe UI", 8.5F);

            // ReservationManagementForm
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1080, 620);
            Controls.Add(dgvReservations);
            Controls.Add(pnlActions);
            Controls.Add(lblStatus);
            Controls.Add(pnlFilters);
            Controls.Add(pnlHeader);
            Font = new Font("Segoe UI", 9F);
            Name = "ReservationManagementForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Gestionar Reservas";
            Load += ReservationManagementForm_Load;

            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlFilters.ResumeLayout(false);
            pnlFilters.PerformLayout();
            pnlActions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvReservations).EndInit();
            ((System.ComponentModel.ISupportInitialize)numPropertyFilter).EndInit();
            ((System.ComponentModel.ISupportInitialize)numGuestFilter).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlHeader;
        private Label lblFormTitle;
        private Label lblFormSubtitle;

        private Panel pnlFilters;
        private Label lblStatusFilter; private ComboBox cboStatusFilter;
        private Label lblPropertyFilter; private NumericUpDown numPropertyFilter;
        private Label lblGuestFilter; private NumericUpDown numGuestFilter;
        private Label lblFrom; private DateTimePicker dtpFrom;
        private Label lblTo; private DateTimePicker dtpTo;
        private Button btnApplyFilter; private Button btnClearFilter;

        private DataGridView dgvReservations;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colProperty;
        private DataGridViewTextBoxColumn colGuest;
        private DataGridViewTextBoxColumn colStart;
        private DataGridViewTextBoxColumn colEnd;
        private DataGridViewTextBoxColumn colTotal;
        private DataGridViewTextBoxColumn colStatus;

        private Panel pnlActions;
        private Button btnDetail;
        private Button btnChangeStatus;
        private Button btnCancel;
        private Button btnRefresh;

        private Label lblStatus;
    }
}

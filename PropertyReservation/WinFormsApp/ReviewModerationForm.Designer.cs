namespace WinFormsApp
{
    partial class ReviewModerationForm
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

            pnlPropertySelector = new Panel();
            lblPropertySelector = new Label();
            cboProperty = new ComboBox();
            txtPropertySearch = new TextBox();
            btnLoadReviews = new Button();

            dgvReviews = new DataGridView();
            colId = new DataGridViewTextBoxColumn();
            colUser = new DataGridViewTextBoxColumn();
            colRating = new DataGridViewTextBoxColumn();
            colDate = new DataGridViewTextBoxColumn();
            colComment = new DataGridViewTextBoxColumn();

            pnlActions = new Panel();
            btnDetail = new Button();
            btnDelete = new Button();
            btnRefresh = new Button();

            lblStatus = new Label();

            pnlPropertySelector.SuspendLayout();
            pnlActions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvReviews).BeginInit();
            SuspendLayout();

            // pnlPropertySelector
            pnlPropertySelector.Dock = DockStyle.Top;
            pnlPropertySelector.Height = 65;
            pnlPropertySelector.Padding = new Padding(10);
            pnlPropertySelector.BackColor = SystemColors.ControlLight;
            pnlPropertySelector.Controls.AddRange(new Control[] { lblPropertySelector, txtPropertySearch, cboProperty, btnLoadReviews });

            lblPropertySelector.Text = "Propiedad:"; lblPropertySelector.AutoSize = true; lblPropertySelector.Location = new Point(15, 22);
            txtPropertySearch.Location = new Point(95, 18); txtPropertySearch.Width = 200;
            txtPropertySearch.PlaceholderText = "Filtrar por título...";
            txtPropertySearch.TextChanged += txtPropertySearch_TextChanged;

            cboProperty.Location = new Point(305, 18); cboProperty.Width = 380;
            cboProperty.DropDownStyle = ComboBoxStyle.DropDownList;
            cboProperty.DisplayMember = "Title";
            cboProperty.ValueMember = "Id";

            btnLoadReviews.Text = "Cargar reseñas"; btnLoadReviews.Location = new Point(695, 17); btnLoadReviews.Size = new Size(130, 28); btnLoadReviews.Click += btnLoadReviews_Click;

            // dgvReviews
            dgvReviews.Dock = DockStyle.Fill;
            dgvReviews.AutoGenerateColumns = false;
            dgvReviews.AllowUserToAddRows = false;
            dgvReviews.AllowUserToDeleteRows = false;
            dgvReviews.ReadOnly = true;
            dgvReviews.RowHeadersVisible = false;
            dgvReviews.MultiSelect = false;
            dgvReviews.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvReviews.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvReviews.Columns.AddRange(new DataGridViewColumn[] { colId, colUser, colRating, colDate, colComment });

            colId.HeaderText = "Id"; colId.DataPropertyName = "id"; colId.FillWeight = 30;
            colUser.HeaderText = "Autor"; colUser.DataPropertyName = "userName"; colUser.FillWeight = 90;
            colRating.HeaderText = "Rating"; colRating.DataPropertyName = "rating"; colRating.FillWeight = 50;
            colDate.HeaderText = "Fecha"; colDate.DataPropertyName = "date"; colDate.FillWeight = 80; colDate.DefaultCellStyle.Format = "dd/MM/yyyy";
            colComment.HeaderText = "Comentario"; colComment.DataPropertyName = "comment"; colComment.FillWeight = 250;

            // pnlActions
            pnlActions.Dock = DockStyle.Bottom;
            pnlActions.Height = 55;
            pnlActions.Padding = new Padding(10);
            pnlActions.Controls.AddRange(new Control[] { btnDetail, btnDelete, btnRefresh });

            btnDetail.Text = "Ver detalle"; btnDetail.Location = new Point(10, 12); btnDetail.Size = new Size(110, 30); btnDetail.Enabled = false; btnDetail.Click += btnDetail_Click;
            btnDelete.Text = "Eliminar reseña"; btnDelete.Location = new Point(130, 12); btnDelete.Size = new Size(140, 30); btnDelete.Enabled = false; btnDelete.Click += btnDelete_Click;
            btnRefresh.Text = "Refrescar"; btnRefresh.Location = new Point(740, 12); btnRefresh.Size = new Size(100, 30); btnRefresh.Anchor = AnchorStyles.Top | AnchorStyles.Right; btnRefresh.Click += btnRefresh_Click;

            // lblStatus
            lblStatus.Dock = DockStyle.Bottom;
            lblStatus.Height = 24;
            lblStatus.TextAlign = ContentAlignment.MiddleLeft;
            lblStatus.Padding = new Padding(15, 0, 15, 0);
            lblStatus.BackColor = SystemColors.ControlLight;

            // ReviewModerationForm
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(870, 540);
            Controls.Add(dgvReviews);
            Controls.Add(pnlActions);
            Controls.Add(lblStatus);
            Controls.Add(pnlPropertySelector);
            Name = "ReviewModerationForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Moderar Reseñas";
            Load += ReviewModerationForm_Load;

            pnlPropertySelector.ResumeLayout(false);
            pnlPropertySelector.PerformLayout();
            pnlActions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvReviews).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlPropertySelector;
        private Label lblPropertySelector;
        private TextBox txtPropertySearch;
        private ComboBox cboProperty;
        private Button btnLoadReviews;

        private DataGridView dgvReviews;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colUser;
        private DataGridViewTextBoxColumn colRating;
        private DataGridViewTextBoxColumn colDate;
        private DataGridViewTextBoxColumn colComment;

        private Panel pnlActions;
        private Button btnDetail;
        private Button btnDelete;
        private Button btnRefresh;

        private Label lblStatus;
    }
}

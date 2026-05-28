using Application.DTOs.Reservation;
using Domain.Enums;

namespace WinFormsApp
{
    public partial class ReservationManagementForm : Form
    {
        // Transiciones válidas hacia las que un Admin puede mover una reserva desde un estado origen.
        private static readonly Dictionary<ReservationStatus, ReservationStatus[]> ValidTransitions = new()
        {
            [ReservationStatus.PendingPayment]  = new[] { ReservationStatus.Cancelled, ReservationStatus.Expired },
            [ReservationStatus.PaymentUploaded] = new[] { ReservationStatus.Confirmed, ReservationStatus.Rejected, ReservationStatus.Cancelled },
            [ReservationStatus.Confirmed]       = new[] { ReservationStatus.Completed, ReservationStatus.Cancelled },
            [ReservationStatus.Rejected]        = new[] { ReservationStatus.PendingPayment },
        };

        public ReservationManagementForm()
        {
            InitializeComponent();
            UiTheme.ApplyGridStyle(dgvReservations);
        }

        private async void ReservationManagementForm_Load(object sender, EventArgs e)
        {
            await LoadReservationsAsync();
        }

        private async Task LoadReservationsAsync()
        {
            lblStatus.Text = "Cargando reservas...";
            try
            {
                string? statusFilter = cboStatusFilter.SelectedIndex > 0 ? cboStatusFilter.SelectedItem?.ToString() : null;
                int? propertyId = numPropertyFilter.Value > 0 ? (int)numPropertyFilter.Value : null;
                int? guestId = numGuestFilter.Value > 0 ? (int)numGuestFilter.Value : null;
                DateOnly? from = dtpFrom.Checked ? DateOnly.FromDateTime(dtpFrom.Value.Date) : null;
                DateOnly? to = dtpTo.Checked ? DateOnly.FromDateTime(dtpTo.Value.Date) : null;

                var reservations = await Program.ReservationClient.GetAllForAdminAsync(statusFilter, propertyId, guestId, from, to);
                var list = reservations?.ToList();
                dgvReservations.DataSource = list;

                lblStatus.Text = list == null || list.Count == 0
                    ? "No hay reservas que coincidan con los criterios."
                    : $"{list.Count} reserva(s).";

                var hasAny = list != null && list.Count > 0;
                btnDetail.Enabled = btnChangeStatus.Enabled = btnCancel.Enabled = hasAny;
            }
            catch (Exception ex)
            {
                lblStatus.Text = $"Error: {ex.Message}";
            }
        }

        private ReservationResponseDTO? GetSelected()
        {
            if (dgvReservations.CurrentRow?.DataBoundItem is ReservationResponseDTO r) return r;
            MessageBox.Show("Seleccione una reserva primero.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return null;
        }

        private async void btnApplyFilter_Click(object sender, EventArgs e) => await LoadReservationsAsync();
        private async void btnRefresh_Click(object sender, EventArgs e) => await LoadReservationsAsync();

        private async void btnClearFilter_Click(object sender, EventArgs e)
        {
            cboStatusFilter.SelectedIndex = 0;
            numPropertyFilter.Value = 0;
            numGuestFilter.Value = 0;
            dtpFrom.Checked = false;
            dtpTo.Checked = false;
            await LoadReservationsAsync();
        }

        private void btnDetail_Click(object sender, EventArgs e)
        {
            var r = GetSelected();
            if (r == null) return;

            var msg = $"Id: {r.Id}\r\n" +
                      $"Propiedad ({r.PropertyId}): {r.PropertyTitle}\r\n" +
                      $"Huésped ({r.GuestId}): {r.GuestName}\r\n" +
                      $"Fechas: {r.StartDate:dd/MM/yyyy} → {r.EndDate:dd/MM/yyyy}\r\n" +
                      $"Total: {r.TotalPrice:C}\r\n" +
                      $"Estado: {r.Status}\r\n" +
                      $"Tiene reseña: {(r.HasReview ? "Sí" : "No")}";

            MessageBox.Show(msg, "Detalle de reserva", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void btnChangeStatus_Click(object sender, EventArgs e)
        {
            var r = GetSelected();
            if (r == null) return;

            if (!Enum.TryParse<ReservationStatus>(r.Status, out var currentStatus))
            {
                MessageBox.Show($"Estado origen desconocido: {r.Status}", "Aviso");
                return;
            }

            if (!ValidTransitions.TryGetValue(currentStatus, out var targets) || targets.Length == 0)
            {
                MessageBox.Show($"No hay transiciones válidas desde el estado {currentStatus}.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var dialog = new ChangeReservationStatusDialog(currentStatus, targets);
            if (dialog.ShowDialog(this) != DialogResult.OK) return;

            if (MessageBox.Show(
                    $"¿Confirma transicionar la reserva #{r.Id} de {currentStatus} a {dialog.SelectedStatus}?",
                    "Confirmar transición", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                await Program.ReservationClient.ChangeReservationStatusAsync(r.Id,
                    new ChangeReservationStatusDTO { Status = dialog.SelectedStatus });
                await LoadReservationsAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnCancel_Click(object sender, EventArgs e)
        {
            var r = GetSelected();
            if (r == null) return;

            if (!Enum.TryParse<ReservationStatus>(r.Status, out var currentStatus)) return;

            var cancellable = new[] { ReservationStatus.PendingPayment, ReservationStatus.PaymentUploaded, ReservationStatus.Confirmed };
            if (!cancellable.Contains(currentStatus))
            {
                MessageBox.Show($"Solo se pueden cancelar reservas en estado PendingPayment, PaymentUploaded o Confirmed (actual: {currentStatus}).",
                    "No permitido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show($"¿Cancelar la reserva #{r.Id}?", "Confirmar",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            try
            {
                await Program.ReservationClient.ChangeReservationStatusAsync(r.Id,
                    new ChangeReservationStatusDTO { Status = ReservationStatus.Cancelled });
                await LoadReservationsAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    internal class ChangeReservationStatusDialog : Form
    {
        private readonly ComboBox _cboStatus;
        public ReservationStatus SelectedStatus { get; private set; }

        public ChangeReservationStatusDialog(ReservationStatus current, ReservationStatus[] targets)
        {
            Text = "Cambiar estado";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MinimizeBox = false;
            MaximizeBox = false;
            ClientSize = new Size(360, 150);

            var lbl = new Label { Text = $"Estado actual: {current}", Location = new Point(15, 15), AutoSize = true };
            _cboStatus = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Location = new Point(15, 50), Width = 320 };
            foreach (var t in targets) _cboStatus.Items.Add(t);
            if (_cboStatus.Items.Count > 0) _cboStatus.SelectedIndex = 0;

            var btnOk = new Button { Text = "Aceptar", Location = new Point(175, 105), Size = new Size(75, 28), DialogResult = DialogResult.OK };
            var btnCancel = new Button { Text = "Cancelar", Location = new Point(260, 105), Size = new Size(75, 28), DialogResult = DialogResult.Cancel };

            btnOk.Click += (_, _) =>
            {
                if (_cboStatus.SelectedItem is ReservationStatus status)
                    SelectedStatus = status;
            };

            Controls.AddRange(new Control[] { lbl, _cboStatus, btnOk, btnCancel });
            AcceptButton = btnOk;
            CancelButton = btnCancel;
        }
    }
}

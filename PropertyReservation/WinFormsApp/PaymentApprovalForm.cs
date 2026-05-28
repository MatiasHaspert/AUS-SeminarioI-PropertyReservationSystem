using System.Diagnostics;
using Application.DTOs.Payments;
using Domain.Enums;

namespace WinFormsApp
{
    public partial class PaymentApprovalForm : Form
    {
        // Comprobante actualmente cargado en el visor.
        private byte[]? _proofBytes;
        private string? _proofContentType;
        private string? _proofTempPath;

        public PaymentApprovalForm()
        {
            InitializeComponent();
            UiTheme.ApplyGridStyle(dgvPayments);
        }

        private async void PaymentApprovalForm_Load(object sender, EventArgs e)
        {
            await LoadPaymentsAsync();
        }

        private async Task LoadPaymentsAsync()
        {
            lblStatus.Text = "Cargando pagos pendientes...";
            ClearProofViewer();
            try
            {
                var payments = await Program.PaymentsClient.GetPaymentsUnderReviewAsync();
                var list = payments?.OrderBy(p => p.UploadedAt).ToList();
                dgvPayments.DataSource = list;

                if (list == null || list.Count == 0)
                {
                    lblStatus.Text = "No hay pagos pendientes de revisión.";
                    btnLoadProof.Enabled = btnApprove.Enabled = btnReject.Enabled = false;
                }
                else
                {
                    lblStatus.Text = $"{list.Count} pago(s) bajo revisión (ordenados por antigüedad).";
                    btnLoadProof.Enabled = btnApprove.Enabled = btnReject.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = $"Error: {ex.Message}";
            }
        }

        private PendingPaymentListDTO? GetSelected()
        {
            if (dgvPayments.CurrentRow?.DataBoundItem is PendingPaymentListDTO p) return p;
            MessageBox.Show("Seleccione un pago primero.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return null;
        }

        private void ClearProofViewer()
        {
            if (pbProof.Image != null)
            {
                var oldImage = pbProof.Image;
                pbProof.Image = null;
                oldImage.Dispose();
            }
            _proofBytes = null;
            _proofContentType = null;
            _proofTempPath = null;
            btnOpenExternal.Enabled = false;
            lblProofInfo.Text = "Seleccione un pago y presione \"Ver comprobante\".";
        }

        private async void btnLoadProof_Click(object sender, EventArgs e)
        {
            var p = GetSelected();
            if (p == null) return;

            ClearProofViewer();
            lblProofInfo.Text = "Descargando comprobante...";

            try
            {
                var result = await Program.PaymentsClient.GetPaymentProofAsync(p.PaymentId);
                if (result == null)
                {
                    lblProofInfo.Text = "No se pudo cargar el comprobante. Reintente o rechace el pago.";
                    return;
                }

                _proofBytes = result.Value.Content;
                _proofContentType = result.Value.ContentType;

                if (_proofContentType.StartsWith("image/", StringComparison.OrdinalIgnoreCase))
                {
                    using var ms = new MemoryStream(_proofBytes);
                    pbProof.Image = Image.FromStream(ms);
                    lblProofInfo.Text = $"Imagen ({_proofContentType}) — {_proofBytes.Length / 1024} KB";
                    btnOpenExternal.Enabled = true;
                }
                else
                {
                    lblProofInfo.Text = $"Archivo {_proofContentType} ({_proofBytes.Length / 1024} KB). No es imagen — usar visor externo.";
                    btnOpenExternal.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                lblProofInfo.Text = $"Error al cargar el comprobante: {ex.Message}";
            }
        }

        private void btnOpenExternal_Click(object sender, EventArgs e)
        {
            if (_proofBytes == null) return;
            try
            {
                var ext = _proofContentType switch
                {
                    "application/pdf" => ".pdf",
                    var c when c?.StartsWith("image/png") == true => ".png",
                    var c when c?.StartsWith("image/jpeg") == true => ".jpg",
                    _ => ".bin"
                };

                _proofTempPath = Path.Combine(Path.GetTempPath(), $"proof_{Guid.NewGuid():N}{ext}");
                File.WriteAllBytes(_proofTempPath, _proofBytes);

                Process.Start(new ProcessStartInfo
                {
                    FileName = _proofTempPath,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo abrir el archivo: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnRefresh_Click(object sender, EventArgs e) => await LoadPaymentsAsync();

        private async void btnApprove_Click(object sender, EventArgs e)
        {
            var p = GetSelected();
            if (p == null) return;

            // Validación adicional CU-06 7a: monto vs total esperado — pendiente de exponer en el DTO.
            if (MessageBox.Show(
                    $"¿Aprobar el pago de {p.Amount:C} de {p.GuestEmail} por la reserva #{p.ReservationId}?\r\n" +
                    "La reserva pasará a estado Confirmed.",
                    "Confirmar aprobación",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            await ChangeStatus(p.PaymentId, PaymentStatus.Approved);
        }

        private async void btnReject_Click(object sender, EventArgs e)
        {
            var p = GetSelected();
            if (p == null) return;

            using var input = new RejectReasonDialog();
            if (input.ShowDialog(this) != DialogResult.OK) return;

            if (MessageBox.Show(
                    $"¿Rechazar el pago de {p.GuestEmail}?\r\n" +
                    "La reserva volverá al estado PendingPayment.\r\n\r\n" +
                    $"Motivo: {(string.IsNullOrWhiteSpace(input.Reason) ? "(sin motivo)" : input.Reason)}",
                    "Confirmar rechazo",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

            await ChangeStatus(p.PaymentId, PaymentStatus.Rejected);
        }

        private async Task ChangeStatus(Guid paymentId, PaymentStatus newStatus)
        {
            try
            {
                await Program.PaymentsClient.ChangePaymentStatusAsync(paymentId,
                    new ChangePaymentStatusDTO { PaymentStatus = newStatus });
                MessageBox.Show("Pago actualizado correctamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                await LoadPaymentsAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    internal class RejectReasonDialog : Form
    {
        private readonly TextBox _txtReason;
        public string Reason => _txtReason.Text;

        public RejectReasonDialog()
        {
            Text = "Motivo del rechazo (opcional)";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MinimizeBox = false;
            MaximizeBox = false;
            ClientSize = new Size(420, 200);

            var lbl = new Label { Text = "Motivo:", Location = new Point(15, 15), AutoSize = true };
            _txtReason = new TextBox
            {
                Location = new Point(15, 40),
                Size = new Size(390, 100),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical
            };

            var btnOk = new Button { Text = "Aceptar", Location = new Point(235, 155), Size = new Size(85, 30), DialogResult = DialogResult.OK };
            var btnCancel = new Button { Text = "Cancelar", Location = new Point(325, 155), Size = new Size(85, 30), DialogResult = DialogResult.Cancel };

            Controls.AddRange(new Control[] { lbl, _txtReason, btnOk, btnCancel });
            AcceptButton = btnOk;
            CancelButton = btnCancel;
        }
    }
}

using Application.DTOs.Property;
using Application.DTOs.Review;

namespace WinFormsApp
{
    public partial class ReviewModerationForm : Form
    {
        private List<PropertyListResponseDTO> _allProperties = new();

        public ReviewModerationForm()
        {
            InitializeComponent();
        }

        private async void ReviewModerationForm_Load(object sender, EventArgs e)
        {
            await LoadPropertiesAsync();
        }

        private async Task LoadPropertiesAsync()
        {
            lblStatus.Text = "Cargando propiedades...";
            try
            {
                var properties = await Program.PropertyClient.GetPropertiesAsync(includeDeleted: true);
                _allProperties = properties?.OrderBy(p => p.Title).ToList() ?? new List<PropertyListResponseDTO>();
                FilterPropertyCombo(string.Empty);
                lblStatus.Text = "Elija una propiedad para listar sus reseñas.";
            }
            catch (Exception ex)
            {
                lblStatus.Text = $"Error al cargar propiedades: {ex.Message}";
            }
        }

        private void FilterPropertyCombo(string filter)
        {
            var filtered = string.IsNullOrWhiteSpace(filter)
                ? _allProperties
                : _allProperties.Where(p => p.Title.Contains(filter, StringComparison.OrdinalIgnoreCase)).ToList();

            cboProperty.DataSource = null;
            cboProperty.DisplayMember = nameof(PropertyListResponseDTO.Title);
            cboProperty.ValueMember = nameof(PropertyListResponseDTO.Id);
            cboProperty.DataSource = filtered;
            btnLoadReviews.Enabled = filtered.Count > 0;
        }

        private void txtPropertySearch_TextChanged(object sender, EventArgs e)
        {
            FilterPropertyCombo(txtPropertySearch.Text);
        }

        private async void btnLoadReviews_Click(object sender, EventArgs e)
        {
            if (cboProperty.SelectedItem is not PropertyListResponseDTO property)
            {
                MessageBox.Show("Seleccione una propiedad primero.", "Aviso");
                return;
            }

            lblStatus.Text = $"Cargando reseñas de '{property.Title}'...";
            try
            {
                var reviews = await Program.ReviewClient.GetPropertyReviewsAsync(property.Id);
                var list = reviews?.OrderByDescending(r => r.Date).ToList();
                dgvReviews.DataSource = list;

                if (list == null || list.Count == 0)
                {
                    lblStatus.Text = "Esta propiedad aún no tiene reseñas.";
                    btnDetail.Enabled = btnDelete.Enabled = false;
                }
                else
                {
                    lblStatus.Text = $"{list.Count} reseña(s).";
                    btnDetail.Enabled = btnDelete.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = $"Error: {ex.Message}";
            }
        }

        private ReviewResponseDTO? GetSelected()
        {
            if (dgvReviews.CurrentRow?.DataBoundItem is ReviewResponseDTO r) return r;
            MessageBox.Show("Seleccione una reseña primero.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return null;
        }

        private void btnDetail_Click(object sender, EventArgs e)
        {
            var r = GetSelected();
            if (r == null) return;

            var msg = $"Id: {r.Id}\r\n" +
                      $"Autor: {r.UserName} (Id {r.UserId})\r\n" +
                      $"Rating: {r.Rating}/5\r\n" +
                      $"Fecha: {r.Date:dd/MM/yyyy HH:mm}\r\n\r\n" +
                      $"Comentario:\r\n{r.Comment}";

            MessageBox.Show(msg, "Detalle de reseña", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            var r = GetSelected();
            if (r == null) return;

            var preview = r.Comment.Length > 120 ? r.Comment.Substring(0, 120) + "..." : r.Comment;
            if (MessageBox.Show(
                    $"¿Eliminar la reseña #{r.Id} de {r.UserName} (rating {r.Rating}/5)?\r\n\r\n\"{preview}\"\r\n\r\nEsta acción es irreversible.",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

            try
            {
                var ok = await Program.ReviewClient.DeleteReviewAsync(r.Id);
                if (!ok)
                {
                    MessageBox.Show("No se pudo eliminar la reseña.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                btnLoadReviews_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnRefresh_Click(object sender, EventArgs e) => await LoadPropertiesAsync();
    }
}

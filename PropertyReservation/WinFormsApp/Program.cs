using WinFormsClient;

namespace WinFormsApp
{
    internal static class Program
    {
        private static readonly HttpClient httpClient = new HttpClient(new AuthenticationHandler
        {
            InnerHandler = new HttpClientHandler()
        })
        {
            BaseAddress = new Uri("https://localhost:7099")
        };

        public static AuthApiClient AuthClient { get; private set; } = null!;
        public static PropertyApiClient PropertyClient { get; private set; } = null!;
        public static ReservationApiClient ReservationClient { get; private set; } = null!;
        public static AmenityApiClient AmenityClient { get; private set; } = null!;
        public static ReviewApiClient ReviewClient { get; private set; } = null!;
        public static PropertyAvailabilityApiClient PropertyAvailabilityClient { get; private set; } = null!;
        public static PropertyImageApiClient PropertyImageClient { get; private set; } = null!;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            
            InitializeApiClients();

            using var loginForm = new LoginForm();
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                System.Windows.Forms.Application.Run(new PropertyListForm());
            }
        }

        private static void InitializeApiClients()
        {
            // Crear los clientes API usando el mismo HttpClient
            AuthClient = new AuthApiClient(httpClient);
            PropertyClient = new PropertyApiClient(httpClient);
            ReservationClient = new ReservationApiClient(httpClient);
            AmenityClient = new AmenityApiClient(httpClient);
            ReviewClient = new ReviewApiClient(httpClient);
            PropertyAvailabilityClient = new PropertyAvailabilityApiClient(httpClient);
            PropertyImageClient = new PropertyImageApiClient(httpClient);
        }
    }
}
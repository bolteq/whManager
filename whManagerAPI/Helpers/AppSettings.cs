
namespace whManagerAPI.Helpers
{
    /// <summary>
    /// Klasa pomocnicza zawierająca ustawienia zdefinioawne w appsettings.json
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// Sekret służący do podpisywania tokenów dostępowych do API
        /// </summary>
        public string Secret { get; set; }
    }
}

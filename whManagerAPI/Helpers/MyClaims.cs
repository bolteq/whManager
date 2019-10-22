using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace whManagerAPI.Helpers
{
    /// <summary>
    /// Klasa pomocnicza dla dostępu do nadanych uprawnień
    /// </summary>
    public static class MyClaims
    {
        /// <summary>
        /// Pomocnicza zmienna statyczna pozwalająca na uniknięcie wpisywania bezpośrednio nazwy uprawnienia
        /// </summary>
        public static string CompanyId = "CompanyId";
    }
}

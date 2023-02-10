using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace AntroStop.Domain
{

    /// <summary>
    /// Класс парсер для прав JWT
    /// </summary>
    public class JwtParser
    {
        /// <summary>
        /// Метод возвращающий данные из JWT
        /// </summary>
        /// <param name="jwt">JWT</param>
        /// <returns>Список параметров Claim</returns>
        public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            var payload = jwt.Split('.')[1];

            var jsonBytes = ParseBase64WithoutPadding(payload);

            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));
            return claims;
        }
        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}

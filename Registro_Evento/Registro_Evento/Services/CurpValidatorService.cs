using System.Text.RegularExpressions;
using System.Linq;

namespace Registro_Evento.Services
{
    public class CurpValidatorService
    {
        private readonly Dictionary<string, string> _estados = new Dictionary<string, string>
        {
            {"AS", "Aguascalientes"}, {"BC", "Baja California"}, {"BS", "Baja California Sur"},
            {"CC", "Campeche"}, {"CS", "Chiapas"}, {"CH", "Chihuahua"}, {"CL", "Coahuila"},
            {"CM", "Colima"}, {"DF", "Ciudad de México"}, {"DG", "Durango"}, {"GT", "Guanajuato"},
            {"GR", "Guerrero"}, {"HG", "Hidalgo"}, {"JC", "Jalisco"}, {"MC", "Estado de México"},
            {"MN", "Michoacán"}, {"MS", "Morelos"}, {"NT", "Nayarit"}, {"NL", "Nuevo León"},
            {"OC", "Oaxaca"}, {"PL", "Puebla"}, {"QT", "Querétaro"}, {"QR", "Quintana Roo"},
            {"SP", "San Luis Potosí"}, {"SL", "Sinaloa"}, {"SR", "Sonora"}, {"TC", "Tabasco"},
            {"TS", "Tamaulipas"}, {"TL", "Tlaxcala"}, {"VZ", "Veracruz"}, {"YN", "Yucatán"},
            {"ZS", "Zacatecas"}, {"NE", "Nacido en el Extranjero"}
        };

        private readonly string[] _palabrasInconvenientes = {
            "BACA", "BAKA", "BUEI", "BUEY", "CACA", "CACO", "CAGA", "CAGO", "CAKA", "CAKO",
            "COGE", "COGI", "COJA", "COJE", "COJI", "COJO", "COLA", "CULO", "FALO", "FETO",
            "GETA", "GUEI", "GUEY", "JETA", "JOTO", "KACA", "KACO", "KAGA", "KAGO", "KAKA",
            "KAKO", "KOGE", "KOGI", "KOJA", "KOJE", "KOJI", "KOJO", "KOLA", "KULO", "LILO",
            "LOCO", "LOKA", "LOKO", "MAME", "MAMO", "MEAR", "MEAS", "MEON", "MIAR", "MION",
            "MOCO", "MOKO", "MULA", "MULO", "NACA", "NACO", "PEDA", "PEDO", "PENE", "PIPI",
            "PITO", "POPO", "PUTA", "PUTO", "QULO", "RATA", "ROBA", "ROBE", "ROBO", "RUIN",
            "SENO", "TETA", "VACA", "VAGA", "VAGO", "VAKA", "VUEI", "VUEY", "WUEI", "WUEY"
        };

        public (bool esValida, string mensaje, Dictionary<string, string> datos) ValidarCurp(string curp)
        {
            if (string.IsNullOrEmpty(curp))
                return (false, "La CURP no puede estar vacía", new Dictionary<string, string>());

            curp = curp.ToUpper().Trim();

            // Limpiar caracteres problemáticos
            curp = curp.Replace("Ø", "0").Replace("ø", "0");

            // Validar longitud
            if (curp.Length != 18)
                return (false, $"La CURP debe tener exactamente 18 caracteres (tiene {curp.Length})", new Dictionary<string, string>());

            // Validar formato básico sin restricciones muy estrictas
            if (!curp.All(c => char.IsLetterOrDigit(c)))
                return (false, "La CURP solo puede contener letras y números", new Dictionary<string, string>());

            // Verificar estructura básica: 4 letras + 6 números + 1 letra + 7 caracteres alfanuméricos
            if (!Regex.IsMatch(curp.Substring(0, 4), @"^[A-Z]{4}$"))
                return (false, "Los primeros 4 caracteres deben ser letras", new Dictionary<string, string>());

            if (!Regex.IsMatch(curp.Substring(4, 6), @"^[0-9]{6}$"))
                return (false, "Los caracteres 5-10 deben ser números (fecha de nacimiento)", new Dictionary<string, string>());

            if (!Regex.IsMatch(curp.Substring(10, 1), @"^[HM]$"))
                return (false, "El carácter 11 debe ser H o M (sexo)", new Dictionary<string, string>());

            if (!Regex.IsMatch(curp.Substring(11, 2), @"^[A-Z]{2}$"))
                return (false, "Los caracteres 12-13 deben ser letras (estado)", new Dictionary<string, string>());

            if (!Regex.IsMatch(curp.Substring(13, 3), @"^[A-Z]{3}$"))
                return (false, "Los caracteres 14-16 deben ser letras (consonantes)", new Dictionary<string, string>());

            if (!Regex.IsMatch(curp.Substring(16, 2), @"^[A-Z0-9]{2}$"))
                return (false, "Los últimos 2 caracteres deben ser letras o números", new Dictionary<string, string>());

            // Validar palabras inconvenientes
            string primeras4 = curp.Substring(0, 4);
            if (_palabrasInconvenientes.Contains(primeras4))
                return (false, "La CURP contiene una combinación de letras no permitida", new Dictionary<string, string>());

            // Extraer información
            var datos = ExtraerDatosCurp(curp);

            // Validar fecha
            if (!ValidarFecha(datos["año"], datos["mes"], datos["dia"]))
                return (false, $"La fecha de nacimiento en la CURP no es válida ({datos["dia"]}/{datos["mes"]}/{datos["año"]})", datos);

            // Validar estado
            if (!_estados.ContainsKey(datos["estado"]))
                return (false, $"El código de estado '{datos["estado"]}' en la CURP no es válido", datos);

            datos["estadoNombre"] = _estados[datos["estado"]];

            return (true, "CURP válida", datos);
        }

        private Dictionary<string, string> ExtraerDatosCurp(string curp)
        {
            return new Dictionary<string, string>
            {
                ["apellidoPaterno"] = curp.Substring(0, 2),
                ["apellidoMaterno"] = curp.Substring(2, 1),
                ["nombre"] = curp.Substring(3, 1),
                ["año"] = curp.Substring(4, 2),
                ["mes"] = curp.Substring(6, 2),
                ["dia"] = curp.Substring(8, 2),
                ["sexo"] = curp.Substring(10, 1) == "H" ? "Hombre" : "Mujer",
                ["estado"] = curp.Substring(11, 2),
                ["consonantes"] = curp.Substring(13, 3),
                ["digito"] = curp.Substring(17, 1)
            };
        }

        private bool ValidarFecha(string año, string mes, string dia)
        {
            if (!int.TryParse(año, out int añoInt) ||
                !int.TryParse(mes, out int mesInt) ||
                !int.TryParse(dia, out int diaInt))
                return false;

            // Validar rangos básicos
            if (mesInt < 1 || mesInt > 12 || diaInt < 1 || diaInt > 31)
                return false;

            // Determinar si es año 19XX o 20XX
            int añoCompleto = añoInt <= DateTime.Now.Year - 2000 ? 2000 + añoInt : 1900 + añoInt;

            // Validar que el año esté en un rango razonable (1900-2025)
            if (añoCompleto < 1900 || añoCompleto > 2025)
                return false;

            try
            {
                var fecha = new DateTime(añoCompleto, mesInt, diaInt);
                return fecha <= DateTime.Now;
            }
            catch
            {
                return false;
            }
        }
    }
}
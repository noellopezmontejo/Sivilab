

using System.Text.RegularExpressions;

namespace Sivilab.Eventos.Services
{
    public class CurpValidatorService
    {
        private readonly Dictionary<string, string> _estados = new()
        {
            {"AS","Aguascalientes"},{"BC","Baja California"},{"BS","Baja California Sur"},
            {"CC","Campeche"},{"CS","Chiapas"},{"CH","Chihuahua"},{"CL","Coahuila"},
            {"CM","Colima"},{"DF","Ciudad de México"},{"DG","Durango"},{"GT","Guanajuato"},
            {"GR","Guerrero"},{"HG","Hidalgo"},{"JC","Jalisco"},{"MC","México"},
            {"MN","Michoacán"},{"MS","Morelos"},{"NT","Nayarit"},{"NL","Nuevo León"},
            {"OC","Oaxaca"},{"PL","Puebla"},{"QT","Querétaro"},{"QR","Quintana Roo"},
            {"SP","San Luis Potosí"},{"SL","Sinaloa"},{"SR","Sonora"},{"TC","Tabasco"},
            {"TS","Tamaulipas"},{"TL","Tlaxcala"},{"VZ","Veracruz"},{"YN","Yucatán"},
            {"ZS","Zacatecas"},{"NE","Nacido en el Extranjero"}
        };

        private readonly HashSet<string> _palabrasInconvenientes = new(StringComparer.OrdinalIgnoreCase)
        {
            "BACA","BAKA","BUEI","BUEY","CACA","CACO","CAGA","CAGO","CAKA","CAKO",
            "COGE","COGI","COJA","COJE","COJI","COJO","COLA","CULO","FALO","FETO",
            "GETA","GUEI","GUEY","JETA","JOTO","KACA","KACO","KAGA","KAGO","KAKA",
            "KAKO","KOGE","KOGI","KOJA","KOJE","KOJI","KOJO","KOLA","KULO","LILO",
            "LOCO","LOKA","LOKO","MAME","MAMO","MEAR","MEAS","MEON","MIAR","MION",
            "MOCO","MOKO","MULA","MULO","NACA","NACO","PEDA","PEDO","PENE","PIPI",
            "PITO","POPO","PUTA","PUTO","QULO","RATA","ROBA","ROBE","ROBO","RUIN",
            "SENO","TETA","VACA","VAGA","VAGO","VAKA","VUEI","VUEY","WUEI","WUEY"
        };

        public (bool EsValida, string Mensaje, Dictionary<string,string> Datos) ValidarCurp(string? curp)
        {
            var datos = new Dictionary<string, string>();
            if (string.IsNullOrWhiteSpace(curp))
                return (false, "La CURP no puede estar vacía", datos);

            curp = curp.ToUpper().Trim().Replace("Ø", "0").Replace("ø", "0");

            if (curp.Length != 18)
                return (false, $"La CURP debe tener exactamente 18 caracteres (tiene {curp.Length})", datos);

            if (!Regex.IsMatch(curp, @"^[A-Z0-9]{18}$"))
                return (false, "La CURP sólo debe contener letras y números", datos);

            // Validación estructural
            if (!Regex.IsMatch(curp.Substring(0, 4), @"^[A-Z]{4}$"))
                return (false, "Los primeros 4 caracteres deben ser letras", datos);

            if (!Regex.IsMatch(curp.Substring(4, 6), @"^[0-9]{6}$"))
                return (false, "La fecha de nacimiento en la CURP no tiene el formato correcto", datos);

            if (!Regex.IsMatch(curp.Substring(10, 1), @"^[HM]$"))
                return (false, "El carácter 11 debe ser H o M", datos);

            if (!Regex.IsMatch(curp.Substring(11, 2), @"^[A-Z]{2}$"))
                return (false, "El código de estado en la CURP no es válido", datos);

            if (!Regex.IsMatch(curp.Substring(13, 3), @"^[A-Z]{3}$"))
                return (false, "La sección de consonantes en la CURP no es válida", datos);

            if (!Regex.IsMatch(curp.Substring(16, 2), @"^[A-Z0-9]{2}$"))
                return (false, "Los últimos 2 caracteres deben ser letras o números", datos);

            var primeras4 = curp.Substring(0, 4);
            if (_palabrasInconvenientes.Contains(primeras4))
                return (false, "La CURP contiene una combinación de letras no permitida", datos);

            // Extraer datos
            datos["apellidoPaterno"] = curp.Substring(0, 2);
            datos["apellidoMaterno"] = curp.Substring(2, 1);
            datos["nombre"] = curp.Substring(3, 1);
            datos["año"] = curp.Substring(4, 2);
            datos["mes"] = curp.Substring(6, 2);
            datos["dia"] = curp.Substring(8, 2);
            datos["sexo"] = curp.Substring(10, 1);
            datos["estado"] = curp.Substring(11, 2);
            datos["consonantes"] = curp.Substring(13, 3);
            datos["digito"] = curp.Substring(17, 1);

            // Validar fecha
            if (!int.TryParse(datos["año"], out var año) ||
                !int.TryParse(datos["mes"], out var mes) ||
                !int.TryParse(datos["dia"], out var dia))
            {
                return (false, "Fecha inválida en la CURP", datos);
            }

            int añoCompleto = (año > DateTime.Now.Year % 100) ? 1900 + año : 2000 + año;
            try
            {
                var fecha = new DateTime(añoCompleto, mes, dia);
                if (fecha > DateTime.Now) return (false, "La fecha de nacimiento en la CURP es mayor a la fecha actual", datos);
                datos["fechaNacimiento"] = fecha.ToString("yyyy-MM-dd");
            }
            catch
            {
                return (false, "Fecha inválida en la CURP", datos);
            }

            // Validar estado
            if (!_estados.TryGetValue(datos["estado"], out var estadoNombre))
                return (false, $"El código de estado '{datos["estado"]}' no es válido", datos);

            datos["estadoNombre"] = estadoNombre;
            datos["sexoNombre"] = datos["sexo"] == "H" ? "Masculino" : "Femenino";

            return (true, "CURP válida", datos);
        }
    }
}
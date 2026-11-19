using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace BagproWebAPI.Service
{
    /// <summary>
    /// Reglas compartidas para la creación de items en ClientesOT_Item: normalización del nombre,
    /// presentaciones válidas y lectura de los indicadores de texto ('1', 'True', 'S', 'SI').
    /// </summary>
    public static class NombreItemNormalizador
    {
        private static readonly Regex EspaciosRegex = new(@"\s+", RegexOptions.Compiled);
        private static readonly Regex PorEntreNumerosRegex = new(@"(?<=\d)\s*X\s*(?=\d)", RegexOptions.Compiled);
        private static readonly Regex CentimetrosRegex = new(@"(?:\b|(?<=\d))CM\b", RegexOptions.Compiled);
        private static readonly Regex GramosRegex = new(@"(?<=\d)(\s*)G\b", RegexOptions.Compiled);
        // Para comparar, "10 CMS" y "10CMS" (o "500 GR" y "500GR") cuentan como iguales.
        private static readonly Regex NumeroUnidadRegex = new(@"(\d)\s+(CMS|GR)\b", RegexOptions.Compiled);

        /// <summary>
        /// Presentaciones de BagPro (PtPresentacionNom) con su código (PtPresentacion), según los datos de ClientesOT_Item (Kilo 2, Unidad 4, Paquete 5, MTS 7).
        /// </summary>
        private static readonly Dictionary<string, (string Nombre, int Codigo)> Presentaciones = new(StringComparer.OrdinalIgnoreCase)
        {
            ["Kilo"] = ("Kilo", 2), ["Kg"] = ("Kilo", 2), ["KLS"] = ("Kilo", 2),
            ["Unidad"] = ("Unidad", 4), ["Und"] = ("Unidad", 4),
            ["Paquete"] = ("Paquete", 5), ["PAQ"] = ("Paquete", 5),
            // En ClientesOT_Item, MTS tiene código 7 (el 3 es Rollo), según los datos de plasticaribe2.
            ["MTS"] = ("MTS", 7), ["Metro"] = ("MTS", 7),
        };

        /// <summary>
        /// Normaliza el nombre de un item: mayúsculas, sin tildes (conserva la Ñ), espacios colapsados,
        /// sin punto final, CM → CMS, G → GR después de un número y " X " entre números.
        /// </summary>
        /// <param name="nombre">Nombre escrito por el usuario o guardado en BagPro.</param>
        /// <returns>Nombre normalizado; cadena vacía si el nombre es nulo o vacío.</returns>
        public static string Normalizar(string? nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre)) return string.Empty;

            string texto = QuitarTildes(nombre.ToUpperInvariant());
            texto = PorEntreNumerosRegex.Replace(texto, " X ");
            texto = CentimetrosRegex.Replace(texto, "CMS");
            texto = GramosRegex.Replace(texto, "$1GR");
            texto = NumeroUnidadRegex.Replace(texto, "$1$2");
            texto = EspaciosRegex.Replace(texto, " ").Trim();
            texto = texto.TrimEnd('.').TrimEnd();
            return texto;
        }

        /// <summary>
        /// Limpia el nombre que se guarda en BagPro: mayúsculas, espacios colapsados y sin espacios a los lados.
        /// No aplica las equivalencias de unidades; esas solo se usan para comparar con <see cref="Normalizar"/>.
        /// </summary>
        /// <param name="nombre">Nombre escrito por el usuario.</param>
        /// <returns>Nombre listo para guardar; cadena vacía si es nulo o vacío.</returns>
        public static string LimpiarParaGuardar(string? nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre)) return string.Empty;
            return EspaciosRegex.Replace(nombre.ToUpperInvariant(), " ").Trim();
        }

        /// <summary>
        /// Convierte una presentación recibida (Kilo/Kg/KLS, Unidad/Und, Paquete/PAQ, MTS) al nombre y código de BagPro.
        /// </summary>
        /// <param name="presentacion">Presentación recibida.</param>
        /// <param name="nombre">Nombre en BagPro (Kilo, Unidad, Paquete, MTS).</param>
        /// <param name="codigo">Código de BagPro para PtPresentacion.</param>
        /// <returns>true si la presentación es válida.</returns>
        public static bool TryPresentacion(string? presentacion, out string nombre, out int codigo)
        {
            nombre = string.Empty;
            codigo = 0;
            if (string.IsNullOrWhiteSpace(presentacion) || !Presentaciones.TryGetValue(presentacion.Trim(), out var valor)) return false;
            nombre = valor.Nombre;
            codigo = valor.Codigo;
            return true;
        }

        /// <summary>
        /// Interpreta los indicadores de texto de ClientesOT_Item (Impresion, Extrusion, etc.).
        /// </summary>
        /// <param name="valor">Valor guardado en BagPro.</param>
        /// <returns>true si el valor es '1', 'True', 'S', 'SI' o 'SÍ', sin distinguir mayúsculas.</returns>
        public static bool EsSi(string? valor)
        {
            string v = (valor ?? string.Empty).Trim().ToUpperInvariant();
            return v is "1" or "TRUE" or "S" or "SI" or "SÍ";
        }

        /// <summary>
        /// Quita las marcas diacríticas de un texto en mayúsculas, salvo la virgulilla de la Ñ.
        /// </summary>
        /// <param name="texto">Texto en mayúsculas.</param>
        /// <returns>Texto sin tildes.</returns>
        private static string QuitarTildes(string texto)
        {
            string descompuesto = texto.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder(descompuesto.Length);
            for (int i = 0; i < descompuesto.Length; i++)
            {
                char c = descompuesto[i];
                if (CharUnicodeInfo.GetUnicodeCategory(c) == UnicodeCategory.NonSpacingMark)
                {
                    bool esEnie = c == '̃' && i > 0 && descompuesto[i - 1] == 'N';
                    if (!esEnie) continue;
                }
                sb.Append(c);
            }
            return sb.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}

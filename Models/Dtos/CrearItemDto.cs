using System.ComponentModel.DataAnnotations;
using BagproWebAPI.Service;

namespace BagproWebAPI.Models.Dtos
{
    /// <summary>
    /// Ficha técnica para crear un item en ClientesOT_Item desde Plasticaribe (una fila = item + presentación).
    /// Los códigos de catálogo son los de BagPro; los nombres se resuelven en el servidor.
    /// </summary>
    public class CrearItemDto : IValidatableObject
    {
        /// <summary>Código BagPro del cliente (ClientesOT_Item.Cliente).</summary>
        [Range(1, int.MaxValue)]
        public int Cliente { get; set; }

        /// <summary>Nombre del cliente (ClienteNom).</summary>
        [Required, StringLength(200)]
        public string ClienteNom { get; set; } = null!;

        /// <summary>Nombre del item; se guarda normalizado.</summary>
        [Required, StringLength(200, MinimumLength = 3)]
        public string Nombre { get; set; } = null!;

        /// <summary>Formato de producto terminado (PtFormatopt, tabla FormatoPT).</summary>
        [Required, Range(1, int.MaxValue)]
        public int? Formato { get; set; }

        /// <summary>Formato de extrusión (ExtFormato, tabla Formato). Opcional.</summary>
        [Range(1, int.MaxValue)]
        public int? FormatoExtrusion { get; set; }

        /// <summary>Material (ExtMaterial).</summary>
        [Required, Range(1, int.MaxValue)]
        public int? Material { get; set; }

        /// <summary>Mezcla predefinida (bpMezclasPreDef.Item); llena MezModo y los componentes de la mezcla.</summary>
        [Required, Range(1, int.MaxValue)]
        public int? Mezcla { get; set; }

        /// <summary>Pigmento (ExtPigmento). Opcional.</summary>
        [Range(1, int.MaxValue)]
        public int? Pigmento { get; set; }

        /// <summary>Tratado (ExtTratado). Opcional.</summary>
        [Range(1, int.MaxValue)]
        public int? Tratado { get; set; }

        /// <summary>Unidad de las medidas y del calibre (ExtUnidades).</summary>
        [Required, Range(1, int.MaxValue)]
        public int? Unidad { get; set; }

        /// <summary>Ancho del producto terminado (PtAnchopt). Mayor que 0.</summary>
        [Range(0.01, 999999.99)]
        public decimal Ancho { get; set; }

        /// <summary>Largo del producto terminado (PtLargopt).</summary>
        [Range(0, 999999.99)]
        public decimal Largo { get; set; }

        /// <summary>Fuelle del producto terminado (PtFuelle).</summary>
        [Range(0, 999999.99)]
        public decimal Fuelle { get; set; }

        /// <summary>Calibre (ExtCalibre). Mayor que 0 cuando el item lleva extrusión.</summary>
        [Range(0, 999999.99)]
        public decimal Calibre { get; set; }

        /// <summary>Anchos de extrusión (ExtAcho1..3). Opcionales.</summary>
        [Range(0, 999999.99)] public decimal? AnchoExtrusion1 { get; set; }
        /// <summary>Ancho de extrusión 2 (ExtAcho2).</summary>
        [Range(0, 999999.99)] public decimal? AnchoExtrusion2 { get; set; }
        /// <summary>Ancho de extrusión 3 (ExtAcho3).</summary>
        [Range(0, 999999.99)] public decimal? AnchoExtrusion3 { get; set; }

        /// <summary>Procesos del item; se guardan como '1'/'0'.</summary>
        public bool Extrusion { get; set; } = true;
        /// <summary>Impresion.</summary>
        public bool Impresion { get; set; }
        /// <summary>Laminado (columna Lamiando, como en SPCrearOTDesdePlasticaribeBDD).</summary>
        public bool Laminado { get; set; }
        /// <summary>Rotograbado (columna Laminado2, como en SPCrearOTDesdePlasticaribeBDD).</summary>
        public bool Rotograbado { get; set; }
        /// <summary>Corte.</summary>
        public bool Corte { get; set; }
        /// <summary>Sellado (columna Pterminado).</summary>
        public bool Sellado { get; set; }
        /// <summary>Cyrel.</summary>
        public bool Cyrel { get; set; }

        /// <summary>Tipo de impresión (ImpFlexo). Solo si Impresion.</summary>
        [Range(1, int.MaxValue)] public int? TipoImpresion { get; set; }
        /// <summary>Rodillo (ImpRodillo).</summary>
        [Range(0, int.MaxValue)] public int? Rodillo { get; set; }
        /// <summary>Pistas (ImpPista).</summary>
        [Range(0, int.MaxValue)] public int? Pista { get; set; }
        /// <summary>Códigos de tintas (ImpTinta1..8), en orden. Máximo 8.</summary>
        public List<int>? Tintas { get; set; }

        /// <summary>Capas de laminado (LamCapa1..3). Máximo 3. Solo si Laminado.</summary>
        public List<CapaLaminadoDto>? CapasLaminado { get; set; }

        /// <summary>Tipo de sellado (PtSelladoPt). Opcional.</summary>
        [Range(1, int.MaxValue)] public int? TipoSellado { get; set; }
        /// <summary>Unidades por paquete (PtQtyPquete). Obligatoria para la presentación Paquete.</summary>
        [Range(0, int.MaxValue)] public int? CantidadPaquete { get; set; }
        /// <summary>Unidades o paquetes por bulto (PtQtyBulto).</summary>
        [Range(0, int.MaxValue)] public int? CantidadBulto { get; set; }
        /// <summary>Peso millar (PtPesoMillar).</summary>
        [Range(0, 999999.99)] public decimal? PesoMillar { get; set; }
        /// <summary>Peso del producto terminado (PtPesopt).</summary>
        [Range(0, 999999.99)] public decimal? Peso { get; set; }

        /// <summary>Presentación: Kilo/Kg/KLS, Unidad/Und, Paquete/PAQ o MTS.</summary>
        [Required]
        public string Presentacion { get; set; } = null!;

        /// <summary>Precio de venta de la presentación (DatosvalorBolsa; también DatosValorKg si es Kilo). Por defecto 0.</summary>
        [Range(0, 9999999999999999.99)]
        public decimal? Precio { get; set; }

        /// <summary>Código del vendedor (UsrModifica, tabla Vendedores). Opcional.</summary>
        [StringLength(50)]
        public string? Vendedor { get; set; }

        /// <summary>Observación. Opcional.</summary>
        [StringLength(4000)]
        public string? Observacion { get; set; }

        /// <summary>
        /// Reglas entre campos: presentación válida, calibre con extrusión, unidades por paquete,
        /// máximo de tintas y de capas.
        /// </summary>
        /// <param name="validationContext">Contexto de validación.</param>
        /// <returns>Errores encontrados.</returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!NombreItemNormalizador.TryPresentacion(Presentacion, out string presentacion, out _))
                yield return new ValidationResult("Presentación no válida. Use Kilo, Unidad, Paquete o MTS.", new[] { nameof(Presentacion) });

            if (string.IsNullOrEmpty(NombreItemNormalizador.Normalizar(Nombre)))
                yield return new ValidationResult("El nombre es obligatorio.", new[] { nameof(Nombre) });

            if (Extrusion && Calibre <= 0)
                yield return new ValidationResult("El calibre debe ser mayor que 0 cuando el item lleva extrusión.", new[] { nameof(Calibre) });

            if (presentacion == "Paquete" && (CantidadPaquete ?? 0) <= 0)
                yield return new ValidationResult("La presentación Paquete requiere la cantidad de unidades por paquete.", new[] { nameof(CantidadPaquete) });

            if (Tintas != null && Tintas.Count > 8)
                yield return new ValidationResult("Máximo 8 tintas.", new[] { nameof(Tintas) });

            if (Tintas != null && Tintas.Any(t => t <= 0))
                yield return new ValidationResult("Código de tinta no válido.", new[] { nameof(Tintas) });

            if (CapasLaminado != null && CapasLaminado.Count > 3)
                yield return new ValidationResult("Máximo 3 capas de laminado.", new[] { nameof(CapasLaminado) });
        }
    }

    /// <summary>
    /// Capa de laminado de la ficha (LamCapaN, LamCalibreN, cantN).
    /// </summary>
    public class CapaLaminadoDto
    {
        /// <summary>Código de la capa (tabla LaminadoCapa).</summary>
        [Range(1, int.MaxValue)]
        public int Capa { get; set; }

        /// <summary>Calibre de la capa.</summary>
        [Range(0, 999999.99)]
        public decimal? Calibre { get; set; }

        /// <summary>Cantidad de la capa.</summary>
        [Range(0, int.MaxValue)]
        public int? Cantidad { get; set; }
    }

    /// <summary>
    /// Cuerpo de POST api/ClientesOtItems/{codigo}/presentaciones.
    /// </summary>
    public class AgregarPresentacionDto
    {
        /// <summary>Presentación nueva: Kilo/Kg/KLS, Unidad/Und, Paquete/PAQ o MTS.</summary>
        [Required]
        public string Presentacion { get; set; } = null!;

        /// <summary>Precio de venta de la presentación. Por defecto 0.</summary>
        [Range(0, 9999999999999999.99)]
        public decimal? Precio { get; set; }
    }
}

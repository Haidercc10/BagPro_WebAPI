using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BagproWebAPI.Models
{
    public partial class plasticaribeContext : DbContext
    {
        public plasticaribeContext()
        {
        }

        public plasticaribeContext(DbContextOptions<plasticaribeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Anoestadistica> Anoestadisticas { get; set; } = null!;
        public virtual DbSet<ArticuloMateriaprima> ArticuloMateriaprimas { get; set; } = null!;
        public virtual DbSet<Bagproduccion> Bagproduccions { get; set; } = null!;
        public virtual DbSet<Bagproduccion2> Bagproduccion2s { get; set; } = null!;
        public virtual DbSet<BagtemNomina> BagtemNominas { get; set; } = null!;
        public virtual DbSet<Bodega> Bodegas { get; set; } = null!;
        public virtual DbSet<BodegaTransicion> BodegaTransicions { get; set; } = null!;
        public virtual DbSet<BpClientesItem> BpClientesItems { get; set; } = null!;
        public virtual DbSet<BpMezclasPreDef> BpMezclasPreDefs { get; set; } = null!;
        public virtual DbSet<BpMezclasPreDef2> BpMezclasPreDef2s { get; set; } = null!;
        public virtual DbSet<Calibre> Calibres { get; set; } = null!;
        public virtual DbSet<Cargue> Cargues { get; set; } = null!;
        public virtual DbSet<Cartera> Carteras { get; set; } = null!;
        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<ClientesOt> ClientesOts { get; set; } = null!;
        public virtual DbSet<ClientesOtItem> ClientesOtItems { get; set; } = null!;
        public virtual DbSet<ConosCalibre> ConosCalibres { get; set; } = null!;
        public virtual DbSet<EntregaMp> EntregaMps { get; set; } = null!;
        public virtual DbSet<Estadoordene> Estadoordenes { get; set; } = null!;
        public virtual DbSet<Existencia> Existencias { get; set; } = null!;
        public virtual DbSet<Familium> Familia { get; set; } = null!;
        public virtual DbSet<Formato> Formatos { get; set; } = null!;
        public virtual DbSet<FormatoPt> FormatoPts { get; set; } = null!;
        public virtual DbSet<GrupoMaterialPrima> GrupoMaterialPrimas { get; set; } = null!;
        public virtual DbSet<HistorialArticuloMateriaprima> HistorialArticuloMateriaprimas { get; set; } = null!;
        public virtual DbSet<HistorialEntregaMp> HistorialEntregaMps { get; set; } = null!;
        public virtual DbSet<HistorialPrima> HistorialPrimas { get; set; } = null!;
        public virtual DbSet<Horario> Horarios { get; set; } = null!;
        public virtual DbSet<ImpresionFlexo> ImpresionFlexos { get; set; } = null!;
        public virtual DbSet<ImpresionTintum> ImpresionTinta { get; set; } = null!;
        public virtual DbSet<ImprimirProgramacion> ImprimirProgramacions { get; set; } = null!;
        public virtual DbSet<InvBopp> InvBopps { get; set; } = null!;
        public virtual DbSet<InvMp> InvMps { get; set; } = null!;
        public virtual DbSet<Inventario> Inventarios { get; set; } = null!;
        public virtual DbSet<Item> Items { get; set; } = null!;
        public virtual DbSet<ItemPeso> ItemPesos { get; set; } = null!;
        public virtual DbSet<LaminadoCapa> LaminadoCapas { get; set; } = null!;
        public virtual DbSet<Linea> Lineas { get; set; } = null!;
        public virtual DbSet<LiquidacionVendedor> LiquidacionVendedors { get; set; } = null!;
        public virtual DbSet<LogBagpro> LogBagpros { get; set; } = null!;
        public virtual DbSet<Material> Materials { get; set; } = null!;
        public virtual DbSet<MaterialMp> MaterialMps { get; set; } = null!;
        public virtual DbSet<MaterialProduccionMateriaPrima> MaterialProduccionMateriaPrimas { get; set; } = null!;
        public virtual DbSet<MaterialProgramado> MaterialProgramados { get; set; } = null!;
        public virtual DbSet<MaterialesExtrusion> MaterialesExtrusions { get; set; } = null!;
        public virtual DbSet<Mezcla> Mezclas { get; set; } = null!;
        public virtual DbSet<MezclaPigmento> MezclaPigmentos { get; set; } = null!;
        public virtual DbSet<MezclaProducto> MezclaProductos { get; set; } = null!;
        public virtual DbSet<NominaVendedor> NominaVendedors { get; set; } = null!;
        public virtual DbSet<OficialMaterialProduccionMateriaPrima> OficialMaterialProduccionMateriaPrimas { get; set; } = null!;
        public virtual DbSet<OficialTemporalMaterialProduccionMateriaPrima> OficialTemporalMaterialProduccionMateriaPrimas { get; set; } = null!;
        public virtual DbSet<Operario> Operarios { get; set; } = null!;
        public virtual DbSet<OperarioImpresion> OperarioImpresions { get; set; } = null!;
        public virtual DbSet<OperariosProceso> OperariosProcesos { get; set; } = null!;
        public virtual DbSet<Permiso> Permisos { get; set; } = null!;
        public virtual DbSet<Pigmento> Pigmentos { get; set; } = null!;
        public virtual DbSet<Planilla> Planillas { get; set; } = null!;
        public virtual DbSet<Porcentajevendedorcliente> Porcentajevendedorclientes { get; set; } = null!;
        public virtual DbSet<PresentacionPt> PresentacionPts { get; set; } = null!;
        public virtual DbSet<ProcBopp> ProcBopps { get; set; } = null!;
        public virtual DbSet<ProcCorte> ProcCortes { get; set; } = null!;
        public virtual DbSet<ProcExtrusion> ProcExtrusions { get; set; } = null!;
        public virtual DbSet<ProcImpresion> ProcImpresions { get; set; } = null!;
        public virtual DbSet<ProcImpresionRollosBopp> ProcImpresionRollosBopps { get; set; } = null!;
        public virtual DbSet<ProcImpresionTblImpresion> ProcImpresionTblImpresions { get; set; } = null!;
        public virtual DbSet<ProcImpresionTblImpresion2> ProcImpresionTblImpresion2s { get; set; } = null!;
        public virtual DbSet<ProcImpresionTblRollosImp> ProcImpresionTblRollosImps { get; set; } = null!;
        public virtual DbSet<ProcSellado> ProcSellados { get; set; } = null!;
        public virtual DbSet<ProcSelladoNomina> ProcSelladoNominas { get; set; } = null!;
        public virtual DbSet<Procdesperdicio> Procdesperdicios { get; set; } = null!;
        public virtual DbSet<ProcdesperdicioDetalle> ProcdesperdicioDetalles { get; set; } = null!;
        public virtual DbSet<ProcesoImpresion> ProcesoImpresions { get; set; } = null!;
        public virtual DbSet<Programacion> Programacions { get; set; } = null!;
        public virtual DbSet<ProvedoresMateriaprima> ProvedoresMateriaprimas { get; set; } = null!;
        public virtual DbSet<Puertovascula> Puertovasculas { get; set; } = null!;
        public virtual DbSet<Referencia> Referencias { get; set; } = null!;
        public virtual DbSet<Riquisicion> Riquisicions { get; set; } = null!;
        public virtual DbSet<SelladoPt> SelladoPts { get; set; } = null!;
        public virtual DbSet<SubprocesoImpresion> SubprocesoImpresions { get; set; } = null!;
        public virtual DbSet<Table1> Table1s { get; set; } = null!;
        public virtual DbSet<TemporalMaterialProduccionMateriaPrima> TemporalMaterialProduccionMateriaPrimas { get; set; } = null!;
        public virtual DbSet<TipoDesperdicio> TipoDesperdicios { get; set; } = null!;
        public virtual DbSet<TipoProceso> TipoProcesos { get; set; } = null!;
        public virtual DbSet<TiposMateriale> TiposMateriales { get; set; } = null!;
        public virtual DbSet<Tratado> Tratados { get; set; } = null!;
        public virtual DbSet<Unidade> Unidades { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UsuarioPermiso> UsuarioPermisos { get; set; } = null!;
        public virtual DbSet<Vendedore> Vendedores { get; set; } = null!;
        public virtual DbSet<ViewCliente> ViewClientes { get; set; } = null!;
        public virtual DbSet<ViewProcExtrusionReporte> ViewProcExtrusionReportes { get; set; } = null!;
        public virtual DbSet<ViewProcSellado> ViewProcSellados { get; set; } = null!;
        public virtual DbSet<Wiketiando> Wiketiandos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=192.168.0.250; Database=plasticaribe; User ID= sa; pwd= 123581321;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Anoestadistica>(entity =>
            {
                entity.HasKey(e => e.Ano);

                entity.ToTable("anoestadistica");

                entity.Property(e => e.Ano)
                    .HasMaxLength(50)
                    .HasColumnName("ano");
            });

            modelBuilder.Entity<ArticuloMateriaprima>(entity =>
            {
                entity.HasKey(e => e.Codigo);

                entity.ToTable("ArticuloMateriaprima");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CostoArticulo).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.CuentaIvaventas)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CuentaIVAVentas")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Cuentaiva)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("cuentaiva")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.DescripcionOtroIdioma)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Grupo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Peso)
                    .HasColumnType("decimal(18, 4)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.PorcentajeIva)
                    .HasColumnType("decimal(18, 6)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.PrecioVenta).HasColumnType("numeric(18, 4)");

                entity.Property(e => e.Presentacion)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Tipo)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<Bagproduccion>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("BAGproduccion");

                entity.Property(e => e.Calibre).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Cliente)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ClienteItem)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ClienteItemNombre)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ClienteNombre)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Extancho)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("extancho");

                entity.Property(e => e.Extfuelle)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("extfuelle");

                entity.Property(e => e.Extlargo)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("extlargo");

                entity.Property(e => e.Extnetokg)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("extnetokg");

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha");

                entity.Property(e => e.Hora)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Material)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .HasColumnName("material")
                    .IsFixedLength();

                entity.Property(e => e.NomStatus)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Operador)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Ot)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .HasColumnName("OT")
                    .IsFixedLength();

                entity.Property(e => e.Rollo)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Bagproduccion2>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("BAGproduccion2");

                entity.Property(e => e.Calibre).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Cliente)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ClienteItem)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ClienteItemNombre)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ClienteNombre)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Extancho)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("extancho");

                entity.Property(e => e.Extfuelle)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("extfuelle");

                entity.Property(e => e.Extlargo)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("extlargo");

                entity.Property(e => e.Extnetokg)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("extnetokg");

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha");

                entity.Property(e => e.Hora)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Material)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .HasColumnName("material")
                    .IsFixedLength();

                entity.Property(e => e.NomStatus)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Operador)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Ot)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .HasColumnName("OT")
                    .IsFixedLength();

                entity.Property(e => e.Rollo)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<BagtemNomina>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("BAGTemNomina");

                entity.Property(e => e.Acomulado).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Cedula)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Cedula2)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Cedula3)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Cedula4)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Cedula5)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Cliente)
                    .HasMaxLength(180)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.CodCliente)
                    .HasMaxLength(180)
                    .IsUnicode(false)
                    .HasColumnName("Cod_Cliente")
                    .IsFixedLength();

                entity.Property(e => e.Desv).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.EnvioZeus)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.FechaEntrada)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .HasColumnName("Fecha_Entrada")
                    .IsFixedLength();

                entity.Property(e => e.Hora)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Item)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Maquina)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .HasColumnName("maquina")
                    .IsFixedLength();

                entity.Property(e => e.NomReferencia)
                    .HasMaxLength(180)
                    .IsUnicode(false)
                    .HasColumnName("Nom_Referencia")
                    .IsFixedLength();

                entity.Property(e => e.NomStatus)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Operario)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Operario2)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Operario3)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Operario4)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Operario5)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Ot)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .HasColumnName("OT")
                    .IsFixedLength();

                entity.Property(e => e.Peso)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("peso");

                entity.Property(e => e.PesoMillar).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Pesot).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Qty)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("qty");

                entity.Property(e => e.Referencia)
                    .HasMaxLength(180)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Turnos)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Unidad)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Bodega>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("PK__BODEGAS__CC87E1277399DEE3");

                entity.ToTable("BODEGAS");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("CODIGO");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPCION");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");

                entity.Property(e => e.Responsable)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("RESPONSABLE");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("TELEFONO");

                entity.Property(e => e.Ubicacion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("UBICACION");
            });

            modelBuilder.Entity<BodegaTransicion>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Bodega_Transicion");

                entity.Property(e => e.Bulto)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Cliente)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Estado)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FechaEntrada)
                    .HasColumnType("date")
                    .HasColumnName("Fecha_Entrada");

                entity.Property(e => e.Maq)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NomReferencia)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Nom_Referencia");

                entity.Property(e => e.Operario)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Peeso).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Qty).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<BpClientesItem>(entity =>
            {
                entity.HasKey(e => e.Codigo);

                entity.ToTable("bpClientesItems");

                entity.Property(e => e.Cliente)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ClienteIdentNro)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ClienteNombre)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ContabItem)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ExtAncho1).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.ExtAncho2).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.ExtAncho3).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.ExtCalibre).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.ExtFormato)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ExtIndicaciones)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ExtPesoMetroL).HasColumnType("numeric(8, 3)");

                entity.Property(e => e.ExtPigmento)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ExtUnidad)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.FechaCrea)
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaModifica)
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ImpCyrelRoto)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('CYREL')");

                entity.Property(e => e.ImpTinta1)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ImpTinta2)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ImpTinta3)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ImpTinta4)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ImpTinta5)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ImpTinta6)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ImpTinta7)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ImpTinta8)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ItemBarCode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.LamCalibre1).HasColumnType("numeric(5, 2)");

                entity.Property(e => e.LamCalibre2).HasColumnType("numeric(5, 2)");

                entity.Property(e => e.LamCalibre3).HasColumnType("numeric(5, 2)");

                entity.Property(e => e.LamCantidad1).HasColumnType("numeric(8, 2)");

                entity.Property(e => e.LamCantidad2).HasColumnType("numeric(8, 2)");

                entity.Property(e => e.LamCantidad3).HasColumnType("numeric(8, 2)");

                entity.Property(e => e.LamCapa1)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.LamCapa2)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.LamCapa3)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.M1p1id).HasColumnName("M1P1Id");

                entity.Property(e => e.M1p1nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("M1P1Nombre")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.M1p1x100)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("M1P1X100");

                entity.Property(e => e.M1p2id).HasColumnName("M1P2Id");

                entity.Property(e => e.M1p2nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("M1P2Nombre")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.M1p2x100)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("M1P2X100");

                entity.Property(e => e.M1p3id).HasColumnName("M1P3Id");

                entity.Property(e => e.M1p3nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("M1P3Nombre")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.M1p3x100)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("M1P3X100");

                entity.Property(e => e.M1p4id).HasColumnName("M1P4Id");

                entity.Property(e => e.M1p4nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("M1P4Nombre")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.M1p4x100)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("M1P4X100");

                entity.Property(e => e.M1pig1Id).HasColumnName("M1Pig1Id");

                entity.Property(e => e.M1pig1Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("M1Pig1Nombre")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.M1pig1X100)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("M1Pig1X100");

                entity.Property(e => e.M1pig2Id).HasColumnName("M1Pig2Id");

                entity.Property(e => e.M1pig2Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("M1Pig2Nombre")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.M1pig2X100)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("M1Pig2X100");

                entity.Property(e => e.M2p1id).HasColumnName("M2P1Id");

                entity.Property(e => e.M2p1nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("M2P1Nombre")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.M2p1x100)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("M2P1X100");

                entity.Property(e => e.M2p2id).HasColumnName("M2P2Id");

                entity.Property(e => e.M2p2nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("M2P2Nombre")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.M2p2x100)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("M2P2X100");

                entity.Property(e => e.M2p3id).HasColumnName("M2P3Id");

                entity.Property(e => e.M2p3nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("M2P3Nombre")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.M2p3x100)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("M2P3X100");

                entity.Property(e => e.M2p4id).HasColumnName("M2P4Id");

                entity.Property(e => e.M2p4nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("M2P4Nombre")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.M2p4x100)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("M2P4X100");

                entity.Property(e => e.M2pig1Id).HasColumnName("M2Pig1Id");

                entity.Property(e => e.M2pig1Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("M2Pig1Nombre")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.M2pig1X100)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("M2Pig1X100");

                entity.Property(e => e.M2pig2Id).HasColumnName("M2Pig2Id");

                entity.Property(e => e.M2pig2Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("M2Pig2Nombre")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.M2pig2X100)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("M2Pig2X100");

                entity.Property(e => e.M3p1id).HasColumnName("M3P1Id");

                entity.Property(e => e.M3p1nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("M3P1Nombre")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.M3p1x100)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("M3P1X100");

                entity.Property(e => e.M3p2id).HasColumnName("M3P2Id");

                entity.Property(e => e.M3p2nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("M3P2Nombre")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.M3p2x100)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("M3P2X100");

                entity.Property(e => e.M3p3id).HasColumnName("M3P3Id");

                entity.Property(e => e.M3p3nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("M3P3Nombre")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.M3p3x100)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("M3P3X100");

                entity.Property(e => e.M3p4id).HasColumnName("M3P4Id");

                entity.Property(e => e.M3p4nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("M3P4Nombre")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.M3p4x100)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("M3P4X100");

                entity.Property(e => e.M3pig1Id).HasColumnName("M3Pig1Id");

                entity.Property(e => e.M3pig1Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("M3Pig1Nombre")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.M3pig1X100)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("M3Pig1X100");

                entity.Property(e => e.M3pig2Id).HasColumnName("M3Pig2Id");

                entity.Property(e => e.M3pig2Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("M3Pig2Nombre")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.M3pig2X100)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("M3Pig2X100");

                entity.Property(e => e.Material)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Material1X100).HasColumnType("numeric(5, 2)");

                entity.Property(e => e.Material2X100).HasColumnType("numeric(5, 2)");

                entity.Property(e => e.Material3X100).HasColumnType("numeric(5, 2)");

                entity.Property(e => e.MezclaPreDef)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('CUSTOM')");

                entity.Property(e => e.MezclasNro).HasDefaultValueSql("((1))");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.OtIndicaciones)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.PtAncho).HasColumnType("numeric(8, 2)");

                entity.Property(e => e.PtCantidadXbulto).HasColumnName("PtCantidadXBulto");

                entity.Property(e => e.PtCantidadXpaquete).HasColumnName("PtCantidadXPaquete");

                entity.Property(e => e.PtFormato)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.PtFuelle).HasColumnType("numeric(8, 2)");

                entity.Property(e => e.PtLargo).HasColumnType("numeric(8, 2)");

                entity.Property(e => e.PtMargenSeg).HasColumnType("numeric(8, 2)");

                entity.Property(e => e.PtPesoMillar).HasColumnType("numeric(18, 4)");

                entity.Property(e => e.PtPesoRolloKg).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PtPresentacion)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('ROLLO')");

                entity.Property(e => e.PtSellado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ptSellado")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Snext).HasColumnName("SNExt");

                entity.Property(e => e.Snimp).HasColumnName("SNImp");

                entity.Property(e => e.Snlam).HasColumnName("SNLam");

                entity.Property(e => e.Snpt).HasColumnName("SNPT");

                entity.Property(e => e.UsrCrea)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UsrModifica)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<BpMezclasPreDef>(entity =>
            {
                entity.HasKey(e => e.Nombre);

                entity.ToTable("bpMezclasPreDef");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.FechaCrea)
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaModifica)
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Item).ValueGeneratedOnAdd();

                entity.Property(e => e.M1p1id)
                    .HasColumnName("M1P1Id")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.M1p1nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("M1P1Nombre")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.M1p1x100)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("M1P1X100")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.M1p2id)
                    .HasColumnName("M1P2Id")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.M1p2nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("M1P2Nombre")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.M1p2x100)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("M1P2X100")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.M1p3id)
                    .HasColumnName("M1P3Id")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.M1p3nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("M1P3Nombre")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.M1p3x100)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("M1P3X100")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.M1p4id)
                    .HasColumnName("M1P4Id")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.M1p4nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("M1P4Nombre")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.M1p4x100)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("M1P4X100")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.M1pig1Id)
                    .HasColumnName("M1Pig1Id")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.M1pig1Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("M1Pig1Nombre")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.M1pig1X100)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("M1Pig1X100")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.M1pig2Id)
                    .HasColumnName("M1Pig2Id")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.M1pig2Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("M1Pig2Nombre")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.M1pig2X100)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("M1Pig2X100")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.M2p1id)
                    .HasColumnName("M2P1Id")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.M2p1nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("M2P1Nombre")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.M2p1x100)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("M2P1X100")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.M2p2id)
                    .HasColumnName("M2P2Id")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.M2p2nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("M2P2Nombre")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.M2p2x100)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("M2P2X100")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.M2p3id)
                    .HasColumnName("M2P3Id")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.M2p3nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("M2P3Nombre")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.M2p3x100)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("M2P3X100")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.M2p4id)
                    .HasColumnName("M2P4Id")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.M2p4nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("M2P4Nombre")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.M2p4x100)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("M2P4X100")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.M2pig1Id)
                    .HasColumnName("M2Pig1Id")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.M2pig1Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("M2Pig1Nombre")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.M2pig1X100)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("M2Pig1X100")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.M2pig2Id)
                    .HasColumnName("M2Pig2Id")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.M2pig2Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("M2Pig2Nombre")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.M2pig2X100)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("M2Pig2X100")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.M3p1id)
                    .HasColumnName("M3P1Id")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.M3p1nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("M3P1Nombre")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.M3p1x100)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("M3P1X100")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.M3p2id)
                    .HasColumnName("M3P2Id")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.M3p2nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("M3P2Nombre")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.M3p2x100)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("M3P2X100")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.M3p3id)
                    .HasColumnName("M3P3Id")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.M3p3nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("M3P3Nombre")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.M3p3x100)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("M3P3X100")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.M3p4id)
                    .HasColumnName("M3P4Id")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.M3p4nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("M3P4Nombre")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.M3p4x100)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("M3P4X100")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.M3pig1Id)
                    .HasColumnName("M3Pig1Id")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.M3pig1Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("M3Pig1Nombre")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.M3pig1X100)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("M3Pig1X100")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.M3pig2Id)
                    .HasColumnName("M3Pig2Id")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.M3pig2Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("M3Pig2Nombre")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.M3pig2X100)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("M3Pig2X100")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Material)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Material1X100)
                    .HasColumnType("numeric(5, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Material2X100)
                    .HasColumnType("numeric(5, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Material3X100)
                    .HasColumnType("numeric(5, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.MezclasNro).HasDefaultValueSql("((1))");

                entity.Property(e => e.UsrCrea)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UsrModifica)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<BpMezclasPreDef2>(entity =>
            {
                entity.HasKey(e => e.Nombre);

                entity.ToTable("bpMezclasPreDef2");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.FechaCrea)
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaModifica)
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Item).ValueGeneratedOnAdd();

                entity.Property(e => e.M1p1id)
                    .HasColumnName("M1P1Id")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.M1p1nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("M1P1Nombre")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.M1p1x100)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("M1P1X100")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.M1p2id)
                    .HasColumnName("M1P2Id")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.M1p2nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("M1P2Nombre")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.M1p2x100)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("M1P2X100")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.M1p3id)
                    .HasColumnName("M1P3Id")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.M1p3nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("M1P3Nombre")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.M1p3x100)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("M1P3X100")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.M1p4id)
                    .HasColumnName("M1P4Id")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.M1p4nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("M1P4Nombre")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.M1p4x100)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("M1P4X100")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.M1pig1Id)
                    .HasColumnName("M1Pig1Id")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.M1pig1Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("M1Pig1Nombre")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.M1pig1X100)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("M1Pig1X100")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.M1pig2Id)
                    .HasColumnName("M1Pig2Id")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.M1pig2Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("M1Pig2Nombre")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.M1pig2X100)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("M1Pig2X100")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.M2p1id)
                    .HasColumnName("M2P1Id")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.M2p1nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("M2P1Nombre")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.M2p1x100)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("M2P1X100")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.M2p2id)
                    .HasColumnName("M2P2Id")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.M2p2nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("M2P2Nombre")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.M2p2x100)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("M2P2X100")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.M2p3id)
                    .HasColumnName("M2P3Id")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.M2p3nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("M2P3Nombre")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.M2p3x100)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("M2P3X100")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.M2p4id)
                    .HasColumnName("M2P4Id")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.M2p4nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("M2P4Nombre")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.M2p4x100)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("M2P4X100")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.M2pig1Id)
                    .HasColumnName("M2Pig1Id")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.M2pig1Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("M2Pig1Nombre")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.M2pig1X100)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("M2Pig1X100")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.M2pig2Id)
                    .HasColumnName("M2Pig2Id")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.M2pig2Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("M2Pig2Nombre")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.M2pig2X100)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("M2Pig2X100")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.M3p1id)
                    .HasColumnName("M3P1Id")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.M3p1nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("M3P1Nombre")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.M3p1x100)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("M3P1X100")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.M3p2id)
                    .HasColumnName("M3P2Id")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.M3p2nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("M3P2Nombre")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.M3p2x100)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("M3P2X100")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.M3p3id)
                    .HasColumnName("M3P3Id")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.M3p3nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("M3P3Nombre")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.M3p3x100)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("M3P3X100")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.M3p4id)
                    .HasColumnName("M3P4Id")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.M3p4nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("M3P4Nombre")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.M3p4x100)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("M3P4X100")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.M3pig1Id)
                    .HasColumnName("M3Pig1Id")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.M3pig1Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("M3Pig1Nombre")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.M3pig1X100)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("M3Pig1X100")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.M3pig2Id)
                    .HasColumnName("M3Pig2Id")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.M3pig2Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("M3Pig2Nombre")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.M3pig2X100)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("M3Pig2X100")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Material)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Material1X100)
                    .HasColumnType("numeric(5, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Material2X100)
                    .HasColumnType("numeric(5, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Material3X100)
                    .HasColumnType("numeric(5, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.MezclasNro).HasDefaultValueSql("((1))");

                entity.Property(e => e.UsrCrea)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UsrModifica)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<Calibre>(entity =>
            {
                entity.Property(e => e.Calibre1)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Calibre");

                entity.Property(e => e.CodigoZeus)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Codigo_Zeus");
            });

            modelBuilder.Entity<Cargue>(entity =>
            {
                entity.HasKey(e => e.Rollo);

                entity.ToTable("Cargue");

                entity.Property(e => e.Rollo).ValueGeneratedNever();

                entity.Property(e => e.Cliente)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.ClienteItem)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Estado)
                    .HasMaxLength(50)
                    .HasColumnName("estado");

                entity.Property(e => e.ExtCono2).HasMaxLength(50);

                entity.Property(e => e.Extancho)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("extancho");

                entity.Property(e => e.Extnetokg)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("extnetokg");

                entity.Property(e => e.Fecha)
                    .HasColumnType("date")
                    .HasColumnName("fecha")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Ot)
                    .HasMaxLength(50)
                    .HasColumnName("OT");
            });

            modelBuilder.Entity<Cartera>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Cartera");

                entity.Property(e => e.Descripcion).HasColumnName("DESCRIPCION");

                entity.Property(e => e.Direccion1).HasColumnName("DIRECCION_1");

                entity.Property(e => e.FechaGen).HasColumnName("fecha_gen");

                entity.Property(e => e.IdDiasVcto).HasColumnName("ID_DIAS_VCTO");

                entity.Property(e => e.IdFecha).HasColumnName("ID_FECHA");

                entity.Property(e => e.IdFechaVcto).HasColumnName("ID_FECHA_VCTO");

                entity.Property(e => e.IdNroCru).HasColumnName("ID_NRO_CRU");

                entity.Property(e => e.IdRango).HasColumnName("id_rango");

                entity.Property(e => e.IdTerc).HasColumnName("ID_TERC");

                entity.Property(e => e.IdTipoCru).HasColumnName("ID_TIPO_CRU");

                entity.Property(e => e.IdVendedor).HasColumnName("ID_VENDEDOR");

                entity.Property(e => e.LapsoDoc).HasColumnName("LAPSO_DOC");

                entity.Property(e => e.NombreCliente).HasColumnName("Nombre_Cliente");

                entity.Property(e => e.SaldosTotCartera).HasColumnName("SALDOS_TOT_CARTERA");

                entity.Property(e => e.Telefono1).HasColumnName("TELEFONO_1");
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.IdentNro);

                entity.Property(e => e.IdentNro).HasMaxLength(50);

                entity.Property(e => e.CodBagpro)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Cod_Bagpro");

                entity.Property(e => e.Codigo).ValueGeneratedOnAdd();

                entity.Property(e => e.Estado)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.FechaCrea).HasColumnType("smalldatetime");

                entity.Property(e => e.FechaModifica).HasColumnType("smalldatetime");

                entity.Property(e => e.IdentTipo)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.UsrCrea).HasMaxLength(50);

                entity.Property(e => e.UsrModifica).HasMaxLength(50);
            });

            modelBuilder.Entity<ClientesOt>(entity =>
            {
                entity.HasKey(e => e.Item);

                entity.ToTable("ClientesOT");

                entity.Property(e => e.Item).ValueGeneratedNever();

                entity.Property(e => e.Cant1).HasColumnName("cant1");

                entity.Property(e => e.Cant2).HasColumnName("cant2");

                entity.Property(e => e.Cant3).HasColumnName("cant3");

                entity.Property(e => e.ClienteItemsNom).IsUnicode(false);

                entity.Property(e => e.Corte)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Cyrel).HasMaxLength(50);

                entity.Property(e => e.DatosFechaDespachar).HasColumnType("smalldatetime");

                entity.Property(e => e.DatosOt)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("DatosOT");

                entity.Property(e => e.DatosValorKg).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.DatoscantKg).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.DatosmargenKg).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.DatosotKg).HasColumnType("numeric(18, 0)");

                entity.Property(e => e.DatosvalorBolsa).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.DatosvalorOt)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("DatosvalorOT");

                entity.Property(e => e.Estado).HasMaxLength(50);

                entity.Property(e => e.EtiquetaFuelle).HasMaxLength(50);

                entity.Property(e => e.EtiquetaLargo).HasMaxLength(50);

                entity.Property(e => e.ExtAcho1).HasColumnType("numeric(8, 2)");

                entity.Property(e => e.ExtAcho2).HasColumnType("numeric(8, 2)");

                entity.Property(e => e.ExtAcho3).HasColumnType("numeric(8, 2)");

                entity.Property(e => e.ExtCalibre).HasColumnType("numeric(8, 2)");

                entity.Property(e => e.ExtFormato)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.ExtFormatoNom)
                    .HasMaxLength(30)
                    .IsFixedLength();

                entity.Property(e => e.ExtMaterial)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.ExtMaterialNom)
                    .HasMaxLength(30)
                    .IsFixedLength();

                entity.Property(e => e.ExtPeso).HasColumnType("numeric(8, 2)");

                entity.Property(e => e.ExtPigmento)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.ExtPigmentoNom)
                    .HasMaxLength(30)
                    .IsFixedLength();

                entity.Property(e => e.ExtTratado)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.ExtTratadoNom)
                    .HasMaxLength(30)
                    .IsFixedLength();

                entity.Property(e => e.ExtUnidades)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.ExtUnidadesNom)
                    .HasMaxLength(30)
                    .IsFixedLength();

                entity.Property(e => e.Extrusion).HasMaxLength(50);

                entity.Property(e => e.FechaCrea)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaModifica)
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaTerminada).HasMaxLength(50);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.ImpFlexo)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.ImpFlexoNom)
                    .HasMaxLength(30)
                    .IsFixedLength();

                entity.Property(e => e.ImpTinta1)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.ImpTinta1Nom)
                    .HasMaxLength(30)
                    .IsFixedLength();

                entity.Property(e => e.ImpTinta2)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.ImpTinta2Nom)
                    .HasMaxLength(30)
                    .IsFixedLength();

                entity.Property(e => e.ImpTinta3)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.ImpTinta3Nom)
                    .HasMaxLength(30)
                    .IsFixedLength();

                entity.Property(e => e.ImpTinta4)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.ImpTinta4Nom)
                    .HasMaxLength(30)
                    .IsFixedLength();

                entity.Property(e => e.ImpTinta5)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.ImpTinta5Nom)
                    .HasMaxLength(30)
                    .IsFixedLength();

                entity.Property(e => e.ImpTinta6)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.ImpTinta6Nom)
                    .HasMaxLength(30)
                    .IsFixedLength();

                entity.Property(e => e.ImpTinta7)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.ImpTinta7Nom)
                    .HasMaxLength(30)
                    .IsFixedLength();

                entity.Property(e => e.ImpTinta8)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.ImpTinta8Nom)
                    .HasMaxLength(30)
                    .IsFixedLength();

                entity.Property(e => e.Impresion)
                    .HasMaxLength(20)
                    .IsFixedLength();

                entity.Property(e => e.LamCalibre1).HasColumnType("numeric(8, 2)");

                entity.Property(e => e.LamCalibre2).HasColumnType("numeric(8, 2)");

                entity.Property(e => e.LamCalibre3).HasColumnType("numeric(8, 2)");

                entity.Property(e => e.LamCapa1)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.LamCapa1Nom)
                    .HasMaxLength(30)
                    .IsFixedLength();

                entity.Property(e => e.LamCapa2)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.LamCapa2Nom)
                    .HasMaxLength(30)
                    .IsFixedLength();

                entity.Property(e => e.LamCapa3)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.LamCapa3Nom)
                    .HasMaxLength(30)
                    .IsFixedLength();

                entity.Property(e => e.Lamiando)
                    .HasMaxLength(30)
                    .IsFixedLength();

                entity.Property(e => e.Laminado2)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.MezModo)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.MezModoNom)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Mezcla)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Mezpigm1cap1)
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.Mezpigm1cap1Nom)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Mezpigm1cap2)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Mezpigm1cap2Nom)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Mezpigm1cap3)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Mezpigm1cap3Nom)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Mezpigm2cap1)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Mezpigm2cap1Nom)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Mezpigm2cap2)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Mezpigm2cap2Nom)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Mezpigm2cap3)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Mezpigm2cap3Nom)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Mezporc1).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Mezporc2).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Mezporc3).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Mezprod1cap1)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod1cap1Nom)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod1cap2)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod1cap2Nom)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod1cap3)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod1cap3Nom)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod2cap1)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod2cap1Nom)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod2cap2)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod2cap2Nom)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod2cap3)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod2cap3Nom)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod3cap1)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod3cap1Nom)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod3cap2)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod3cap2Nom)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod3cap3)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod3cap3Nom)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod4cap1)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod4cap1Nom)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod4cap2)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod4cap2Nom)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod4cap3)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod4cap3Nom)
                    .HasMaxLength(30)
                    .IsFixedLength();

                entity.Property(e => e.Pedido).HasMaxLength(50);

                entity.Property(e => e.PesoBulto).HasMaxLength(50);

                entity.Property(e => e.Pesopaquete).HasMaxLength(50);

                entity.Property(e => e.Porc1cap1).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Porc1cap2).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Porc1cap3).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Porc2cap1).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Porc2cap2).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Porc2cap3).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Porc3cap1).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Porc3cap2).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Porc3cap3).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Porc4cap1).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Porc4cap2).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Porc4cap3).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Porcpig1cap1).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Porcpig1cap2).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Porcpig1cap3).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Porcpig2cap1).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Porcpig2cap2).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Porcpig2cap3).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PtAnchopt).HasColumnType("numeric(8, 2)");

                entity.Property(e => e.PtFormatopt).HasMaxLength(50);

                entity.Property(e => e.PtFormatoptNom).HasMaxLength(50);

                entity.Property(e => e.PtFuelle).HasColumnType("numeric(8, 2)");

                entity.Property(e => e.PtLargopt).HasColumnType("numeric(8, 2)");

                entity.Property(e => e.PtMargen).HasColumnType("numeric(8, 2)");

                entity.Property(e => e.PtPesoMillar).HasColumnType("numeric(8, 2)");

                entity.Property(e => e.PtPesoRollo).HasColumnType("numeric(8, 2)");

                entity.Property(e => e.PtPesopt).HasColumnType("numeric(8, 2)");

                entity.Property(e => e.PtPresentacion).HasMaxLength(50);

                entity.Property(e => e.PtPresentacionNom).HasMaxLength(50);

                entity.Property(e => e.PtSelladoPt).HasMaxLength(50);

                entity.Property(e => e.PtSelladoPtNom).HasMaxLength(50);

                entity.Property(e => e.Pterminado).HasMaxLength(50);

                entity.Property(e => e.UsrCrea).HasMaxLength(50);

                entity.Property(e => e.UsrModifica)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<ClientesOtItem>(entity =>
            {
                entity.ToTable("ClientesOT_Item");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cant1).HasColumnName("cant1");

                entity.Property(e => e.Cant2).HasColumnName("cant2");

                entity.Property(e => e.Cant3).HasColumnName("cant3");

                entity.Property(e => e.ClienteItemsNom).IsUnicode(false);

                entity.Property(e => e.Corte)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Cyrel).HasMaxLength(50);

                entity.Property(e => e.DatosFechaDespachar).HasColumnType("smalldatetime");

                entity.Property(e => e.DatosOt)
                    .HasMaxLength(10)
                    .HasColumnName("DatosOT")
                    .IsFixedLength();

                entity.Property(e => e.DatosValorKg).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.DatoscantKg).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.DatosmargenKg).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.DatosotKg).HasColumnType("numeric(18, 0)");

                entity.Property(e => e.DatosvalorBolsa).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.DatosvalorOt)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("DatosvalorOT");

                entity.Property(e => e.Dia).HasMaxLength(50);

                entity.Property(e => e.DiaC)
                    .HasColumnType("numeric(6, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.EtiquetaFuelle).HasMaxLength(50);

                entity.Property(e => e.EtiquetaLargo).HasMaxLength(50);

                entity.Property(e => e.ExtAcho1).HasColumnType("numeric(8, 2)");

                entity.Property(e => e.ExtAcho2).HasColumnType("numeric(8, 2)");

                entity.Property(e => e.ExtAcho3).HasColumnType("numeric(8, 2)");

                entity.Property(e => e.ExtCalibre).HasColumnType("numeric(8, 2)");

                entity.Property(e => e.ExtFormato)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.ExtFormatoNom)
                    .HasMaxLength(30)
                    .IsFixedLength();

                entity.Property(e => e.ExtMaterial)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.ExtMaterialNom)
                    .HasMaxLength(30)
                    .IsFixedLength();

                entity.Property(e => e.ExtPeso).HasColumnType("numeric(8, 2)");

                entity.Property(e => e.ExtPigmento)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.ExtPigmentoNom)
                    .HasMaxLength(30)
                    .IsFixedLength();

                entity.Property(e => e.ExtTratado)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.ExtTratadoNom)
                    .HasMaxLength(30)
                    .IsFixedLength();

                entity.Property(e => e.ExtUnidades)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.ExtUnidadesNom)
                    .HasMaxLength(30)
                    .IsFixedLength();

                entity.Property(e => e.Extrusion).HasMaxLength(50);

                entity.Property(e => e.FechaCrea)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaModifica)
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ImpFlexo)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.ImpFlexoNom)
                    .HasMaxLength(30)
                    .IsFixedLength();

                entity.Property(e => e.ImpTinta1)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.ImpTinta1Nom)
                    .HasMaxLength(30)
                    .IsFixedLength();

                entity.Property(e => e.ImpTinta2)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.ImpTinta2Nom)
                    .HasMaxLength(30)
                    .IsFixedLength();

                entity.Property(e => e.ImpTinta3)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.ImpTinta3Nom)
                    .HasMaxLength(30)
                    .IsFixedLength();

                entity.Property(e => e.ImpTinta4)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.ImpTinta4Nom)
                    .HasMaxLength(30)
                    .IsFixedLength();

                entity.Property(e => e.ImpTinta5)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.ImpTinta5Nom)
                    .HasMaxLength(30)
                    .IsFixedLength();

                entity.Property(e => e.ImpTinta6)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.ImpTinta6Nom)
                    .HasMaxLength(30)
                    .IsFixedLength();

                entity.Property(e => e.ImpTinta7)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.ImpTinta7Nom)
                    .HasMaxLength(30)
                    .IsFixedLength();

                entity.Property(e => e.ImpTinta8)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.ImpTinta8Nom)
                    .HasMaxLength(30)
                    .IsFixedLength();

                entity.Property(e => e.Impresion)
                    .HasMaxLength(20)
                    .IsFixedLength();

                entity.Property(e => e.LamCalibre1).HasColumnType("numeric(8, 2)");

                entity.Property(e => e.LamCalibre2).HasColumnType("numeric(8, 2)");

                entity.Property(e => e.LamCalibre3).HasColumnType("numeric(8, 2)");

                entity.Property(e => e.LamCapa1)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.LamCapa1Nom)
                    .HasMaxLength(30)
                    .IsFixedLength();

                entity.Property(e => e.LamCapa2)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.LamCapa2Nom)
                    .HasMaxLength(30)
                    .IsFixedLength();

                entity.Property(e => e.LamCapa3)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.LamCapa3Nom)
                    .HasMaxLength(30)
                    .IsFixedLength();

                entity.Property(e => e.Lamiando)
                    .HasMaxLength(30)
                    .IsFixedLength();

                entity.Property(e => e.Laminado2)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.MezModo)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.MezModoNom)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Mezcla)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Mezpigm1cap1)
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.Mezpigm1cap1Nom)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Mezpigm1cap2)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Mezpigm1cap2Nom)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Mezpigm1cap3)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Mezpigm1cap3Nom)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Mezpigm2cap1)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Mezpigm2cap1Nom)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Mezpigm2cap2)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Mezpigm2cap2Nom)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Mezpigm2cap3)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Mezpigm2cap3Nom)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Mezporc1).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Mezporc2).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Mezporc3).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Mezprod1cap1)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod1cap1Nom)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod1cap2)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod1cap2Nom)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod1cap3)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod1cap3Nom)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod2cap1)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod2cap1Nom)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod2cap2)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod2cap2Nom)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod2cap3)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod2cap3Nom)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod3cap1)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod3cap1Nom)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod3cap2)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod3cap2Nom)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod3cap3)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod3cap3Nom)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod4cap1)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod4cap1Nom)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod4cap2)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod4cap2Nom)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod4cap3)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod4cap3Nom)
                    .HasMaxLength(30)
                    .IsFixedLength();

                entity.Property(e => e.Noche).HasMaxLength(50);

                entity.Property(e => e.NocheC)
                    .HasColumnType("numeric(6, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Pedido).HasMaxLength(50);

                entity.Property(e => e.PesoBulto).HasMaxLength(50);

                entity.Property(e => e.Pesopaquete).HasMaxLength(50);

                entity.Property(e => e.Porc1cap1).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Porc1cap2).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Porc1cap3).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Porc2cap1).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Porc2cap2).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Porc2cap3).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Porc3cap1).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Porc3cap2).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Porc3cap3).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Porc4cap1).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Porc4cap2).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Porc4cap3).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Porcpig1cap1).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Porcpig1cap2).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Porcpig1cap3).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Porcpig2cap1).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Porcpig2cap2).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Porcpig2cap3).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PtAnchopt).HasColumnType("numeric(8, 2)");

                entity.Property(e => e.PtFormatopt).HasMaxLength(50);

                entity.Property(e => e.PtFormatoptNom).HasMaxLength(50);

                entity.Property(e => e.PtFuelle).HasColumnType("numeric(8, 2)");

                entity.Property(e => e.PtLargopt).HasColumnType("numeric(8, 2)");

                entity.Property(e => e.PtMargen).HasColumnType("numeric(8, 2)");

                entity.Property(e => e.PtPesoMillar).HasColumnType("numeric(8, 2)");

                entity.Property(e => e.PtPesoRollo).HasColumnType("numeric(8, 2)");

                entity.Property(e => e.PtPesopt).HasColumnType("numeric(8, 2)");

                entity.Property(e => e.PtPresentacion).HasMaxLength(50);

                entity.Property(e => e.PtPresentacionNom).HasMaxLength(50);

                entity.Property(e => e.PtSelladoPt).HasMaxLength(50);

                entity.Property(e => e.PtSelladoPtNom).HasMaxLength(50);

                entity.Property(e => e.Pterminado).HasMaxLength(50);

                entity.Property(e => e.UsrCrea).HasMaxLength(50);

                entity.Property(e => e.UsrModifica)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<ConosCalibre>(entity =>
            {
                entity.Property(e => e.KgCmAncho).HasColumnType("numeric(18, 4)");

                entity.Property(e => e.Referencia)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EntregaMp>(entity =>
            {
                entity.HasKey(e => e.Item);

                entity.ToTable("EntregaMP");

                entity.Property(e => e.Item).ValueGeneratedNever();

                entity.Property(e => e.Cliente)
                    .IsUnicode(false)
                    .HasColumnName("cliente");

                entity.Property(e => e.CodEstado).HasColumnName("Cod_Estado");

                entity.Property(e => e.CodMaterial).HasColumnName("Cod_Material");

                entity.Property(e => e.Estado)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.FechaCrea)
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaModifica)
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.ItemCliente)
                    .IsUnicode(false)
                    .HasColumnName("Item_Cliente");

                entity.Property(e => e.Material).IsUnicode(false);

                entity.Property(e => e.Ot).HasColumnName("OT");

                entity.Property(e => e.QtyEntregada)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("Qty_Entregada");

                entity.Property(e => e.QtySolicitada)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("Qty_Solicitada");

                entity.Property(e => e.UsrCrea).HasMaxLength(50);

                entity.Property(e => e.UsrModifica)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<Estadoordene>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.NombreUnidades).HasMaxLength(50);
            });

            modelBuilder.Entity<Existencia>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.FechaIngreso)
                    .HasColumnType("date")
                    .HasColumnName("FECHA_INGRESO");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Item)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ITEM");

                entity.Property(e => e.Peso)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("PESO");

                entity.Property(e => e.Proceso)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PROCESO");

                entity.Property(e => e.Qty)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("QTY");
            });

            modelBuilder.Entity<Familium>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Item)
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Formato>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Formato");

                entity.Property(e => e.FechaCrea)
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaModifica)
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.NombreFormato).HasMaxLength(50);

                entity.Property(e => e.UsrCrea).HasMaxLength(50);

                entity.Property(e => e.UsrModifica)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<FormatoPt>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("FormatoPT");

                entity.Property(e => e.FechaCrea)
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaModifica)
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.NombreFormato).HasMaxLength(50);

                entity.Property(e => e.UsrCrea).HasMaxLength(50);

                entity.Property(e => e.UsrModifica)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<GrupoMaterialPrima>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Grupo_Material_Prima");

                entity.Property(e => e.CodigoMaterial).HasMaxLength(50);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.NombreMaterial).HasMaxLength(50);
            });

            modelBuilder.Entity<HistorialArticuloMateriaprima>(entity =>
            {
                entity.HasKey(e => e.IdArticulo);

                entity.ToTable("HistorialArticuloMateriaprima");

                entity.Property(e => e.Ancho)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CostoArticulo).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Doc)
                    .HasMaxLength(30)
                    .IsFixedLength();

                entity.Property(e => e.Fecha)
                    .HasMaxLength(30)
                    .IsFixedLength();

                entity.Property(e => e.Fuente)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Grupo)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Micra)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Nit)
                    .HasMaxLength(30)
                    .IsFixedLength();

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Ot)
                    .HasMaxLength(10)
                    .HasColumnName("OT")
                    .IsFixedLength();

                entity.Property(e => e.Peso)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.PrecioVenta)
                    .HasColumnType("numeric(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Proveedor)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.Tipo)
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            modelBuilder.Entity<HistorialEntregaMp>(entity =>
            {
                entity.HasKey(e => e.Item);

                entity.ToTable("HistorialEntregaMP");

                entity.Property(e => e.Item).ValueGeneratedNever();

                entity.Property(e => e.Cliente)
                    .IsUnicode(false)
                    .HasColumnName("cliente");

                entity.Property(e => e.CodEstado).HasColumnName("Cod_Estado");

                entity.Property(e => e.CodMaterial).HasColumnName("Cod_Material");

                entity.Property(e => e.Estado)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.FechaEntrega)
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaModifica)
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.ItemCliente)
                    .IsUnicode(false)
                    .HasColumnName("Item_Cliente");

                entity.Property(e => e.Material).IsUnicode(false);

                entity.Property(e => e.Ot).HasColumnName("OT");

                entity.Property(e => e.QtyEntregada)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("Qty_Entregada");

                entity.Property(e => e.QtySolicitada)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("Qty_Solicitada");

                entity.Property(e => e.UsrDespacho)
                    .HasMaxLength(50)
                    .HasColumnName("Usr_Despacho");

                entity.Property(e => e.UsrModifica)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UsrRecibio)
                    .HasMaxLength(50)
                    .HasColumnName("Usr_Recibio");

                entity.Property(e => e.UsrSistemas)
                    .HasMaxLength(50)
                    .HasColumnName("Usr_Sistemas");
            });

            modelBuilder.Entity<HistorialPrima>(entity =>
            {
                entity.ToTable("HistorialPrima");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Compra).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.IdProveedor)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Item)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Tipo)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Valor).HasColumnType("numeric(18, 2)");
            });

            modelBuilder.Entity<Horario>(entity =>
            {
                entity.HasKey(e => e.Item);

                entity.Property(e => e.Item).ValueGeneratedNever();

                entity.Property(e => e.Activo)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Fecha)
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Hora)
                    .HasMaxLength(50)
                    .HasColumnName("HORA");

                entity.Property(e => e.Hora2)
                    .HasMaxLength(50)
                    .HasColumnName("HORA2");

                entity.Property(e => e.HoraF)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.HoraI)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Minuto)
                    .HasMaxLength(50)
                    .HasColumnName("MINUTO");

                entity.Property(e => e.Minuto2)
                    .HasMaxLength(50)
                    .HasColumnName("MINUTO2");

                entity.Property(e => e.Proceso).HasMaxLength(50);

                entity.Property(e => e.Procesoplanta)
                    .HasMaxLength(80)
                    .HasColumnName("PROCESOPLANTA");

                entity.Property(e => e.Turno)
                    .HasMaxLength(50)
                    .HasColumnName("TURNO");

                entity.Property(e => e.Usuario)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("USUARIO");
            });

            modelBuilder.Entity<ImpresionFlexo>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ImpresionFlexo");

                entity.Property(e => e.FechaCrea)
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaModifica)
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.NombreMaterial).HasMaxLength(50);

                entity.Property(e => e.UsrCrea).HasMaxLength(50);

                entity.Property(e => e.UsrModifica)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<ImpresionTintum>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.FechaCrea)
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaModifica)
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.NombreMaterial).HasMaxLength(50);

                entity.Property(e => e.Referencia).HasMaxLength(50);

                entity.Property(e => e.UsrCrea)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UsrModifica)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<ImprimirProgramacion>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Imprimir_Programacion");

                entity.Property(e => e.Cliente)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ClienteNom)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Fecha)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.IdMaterial)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .HasColumnName("id_material")
                    .IsFixedLength();

                entity.Property(e => e.Item)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Kilos).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Maq)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .HasColumnName("MAQ")
                    .IsFixedLength();

                entity.Property(e => e.Material)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Ot)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .HasColumnName("OT")
                    .IsFixedLength();

                entity.Property(e => e.Referencia)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Solicitud).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Tipo)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<InvBopp>(entity =>
            {
                entity.HasKey(e => e.Item);

                entity.ToTable("InvBopp");

                entity.Property(e => e.AncPadre)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("Anc_Padre");

                entity.Property(e => e.Ancho).HasColumnType("numeric(5, 2)");

                entity.Property(e => e.Calibre).HasMaxLength(50);

                entity.Property(e => e.Fecha)
                    .HasMaxLength(50)
                    .HasColumnName("fecha");

                entity.Property(e => e.Hora).HasMaxLength(50);

                entity.Property(e => e.Kilos).HasColumnType("numeric(5, 2)");

                entity.Property(e => e.PesoPadre)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("Peso_Padre");

                entity.Property(e => e.Sku).HasMaxLength(50);

                entity.Property(e => e.SkuPadre)
                    .HasMaxLength(50)
                    .HasColumnName("Sku_Padre");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<InvMp>(entity =>
            {
                entity.HasKey(e => e.Item);

                entity.ToTable("Inv_MP");

                entity.Property(e => e.Item).ValueGeneratedNever();

                entity.Property(e => e.CodMaterialMp1)
                    .HasMaxLength(50)
                    .HasColumnName("Cod_MaterialMP1");

                entity.Property(e => e.Codigo).ValueGeneratedOnAdd();

                entity.Property(e => e.FechaCrea)
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaModifica)
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.MaterialMp1)
                    .HasMaxLength(50)
                    .HasColumnName("MaterialMP1");

                entity.Property(e => e.QtyInv)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("qty_inv");

                entity.Property(e => e.QtySal)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("qty_sal");

                entity.Property(e => e.UsrCrea).HasMaxLength(50);

                entity.Property(e => e.UsrModifica)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<Inventario>(entity =>
            {
                entity.ToTable("INVENTARIO");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Anchomm).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CalibreMicras).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Fecha)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("fecha")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Pesoneto).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Tipo).HasMaxLength(50);

                entity.Property(e => e.UsrCrea).HasMaxLength(50);
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CodigoCliente)
                    .HasMaxLength(50)
                    .HasColumnName("Codigo_Cliente");

                entity.Property(e => e.CodigoItem).HasColumnName("Codigo_Item");

                entity.Property(e => e.DespItem)
                    .HasMaxLength(100)
                    .HasColumnName("Desp_Item");

                entity.Property(e => e.FechaCreaItem)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("FechaCrea_Item");

                entity.Property(e => e.FechaModificaItem)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("FechaModifica_Item");

                entity.Property(e => e.UsrCreaItem)
                    .HasMaxLength(50)
                    .HasColumnName("UsrCrea_Item");

                entity.Property(e => e.UsrModificaItem)
                    .HasMaxLength(50)
                    .HasColumnName("UsrModifica_Item");
            });

            modelBuilder.Entity<ItemPeso>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ItemPeso");

                entity.Property(e => e.Id)
                    .HasMaxLength(10)
                    .HasColumnName("ID")
                    .IsFixedLength();
            });

            modelBuilder.Entity<LaminadoCapa>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("LaminadoCapa");

                entity.Property(e => e.FechaCrea)
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaModifica)
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.NombreMaterial).HasMaxLength(50);

                entity.Property(e => e.UsrCrea)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UsrModifica)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<Linea>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("LINEAS");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LiquidacionVendedor>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("LiquidacionVendedor");

                entity.Property(e => e.Base)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("BASE");

                entity.Property(e => e.Basenc)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("BASENC");

                entity.Property(e => e.Cliente)
                    .HasMaxLength(50)
                    .HasColumnName("CLIENTE");

                entity.Property(e => e.Comision)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("COMISION");

                entity.Property(e => e.Doc)
                    .HasMaxLength(50)
                    .HasColumnName("DOC");

                entity.Property(e => e.Factura)
                    .HasMaxLength(50)
                    .HasColumnName("FACTURA");

                entity.Property(e => e.Fechafact)
                    .HasMaxLength(50)
                    .HasColumnName("FECHAFACT");

                entity.Property(e => e.Fechapago)
                    .HasMaxLength(50)
                    .HasColumnName("FECHAPAGO");

                entity.Property(e => e.Liq)
                    .HasMaxLength(50)
                    .HasColumnName("LIQ");

                entity.Property(e => e.Mes)
                    .HasMaxLength(50)
                    .HasColumnName("MES");

                entity.Property(e => e.Nc)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("NC");

                entity.Property(e => e.Periodo)
                    .HasMaxLength(50)
                    .HasColumnName("PERIODO");

                entity.Property(e => e.Rc)
                    .HasMaxLength(50)
                    .HasColumnName("RC");

                entity.Property(e => e.Sw)
                    .HasMaxLength(50)
                    .HasColumnName("SW");

                entity.Property(e => e.Valorrc)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("VALORRC");

                entity.Property(e => e.Vendedor)
                    .HasMaxLength(50)
                    .HasColumnName("VENDEDOR");
            });

            modelBuilder.Entity<LogBagpro>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("log_bagpro");

                entity.Property(e => e.Bulto).HasMaxLength(50);

                entity.Property(e => e.Fecha).HasMaxLength(50);

                entity.Property(e => e.Hora)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Metodo).HasMaxLength(50);

                entity.Property(e => e.Operario).HasMaxLength(50);

                entity.Property(e => e.Ot)
                    .HasMaxLength(10)
                    .HasColumnName("OT")
                    .IsFixedLength();

                entity.Property(e => e.Proceso).HasMaxLength(50);
            });

            modelBuilder.Entity<Material>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Material");

                entity.Property(e => e.FechaCrea)
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaModifica)
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.NombreMaterial).HasMaxLength(50);

                entity.Property(e => e.UsrCrea).HasMaxLength(50);

                entity.Property(e => e.UsrModifica)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<MaterialMp>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("MaterialMP");

                entity.Property(e => e.CodGrupo)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Codigo).ValueGeneratedOnAdd();

                entity.Property(e => e.Grupo).HasMaxLength(50);

                entity.Property(e => e.ItemDescripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ItemGrupo)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("(getdate())")
                    .IsFixedLength();
            });

            modelBuilder.Entity<MaterialProduccionMateriaPrima>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Material_produccion_materia_prima");

                entity.Property(e => e.Mezpigm1cap1)
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.Mezpigm1cap1Nom)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Mezpigm1cap2)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Mezpigm1cap2Nom)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Mezpigm1cap3)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Mezpigm1cap3Nom)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Mezpigm2cap1)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Mezpigm2cap1Nom)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Mezpigm2cap2)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Mezpigm2cap2Nom)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Mezpigm2cap3)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Mezpigm2cap3Nom)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Mezporc1).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Mezporc2).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Mezporc3).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Mezprod1cap1)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod1cap1Nom)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod1cap2)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod1cap2Nom)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod1cap3)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod1cap3Nom)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod2cap1)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod2cap1Nom)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod2cap2)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod2cap2Nom)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod2cap3)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod2cap3Nom)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod3cap1)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod3cap1Nom)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod3cap2)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod3cap2Nom)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod3cap3)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod3cap3Nom)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod4cap1)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod4cap1Nom)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod4cap2)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod4cap2Nom)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod4cap3)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod4cap3Nom)
                    .HasMaxLength(30)
                    .IsFixedLength();

                entity.Property(e => e.Ot)
                    .HasMaxLength(10)
                    .HasColumnName("OT")
                    .IsFixedLength();

                entity.Property(e => e.Porc1cap1).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Porc1cap2).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Porc1cap3).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Porc2cap1).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Porc2cap2).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Porc2cap3).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Porc3cap1).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Porc3cap2).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Porc3cap3).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Porc4cap1).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Porc4cap2).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Porc4cap3).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Porcpig1cap1).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Porcpig1cap2).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Porcpig1cap3).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Porcpig2cap1).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Porcpig2cap2).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Porcpig2cap3).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.QtyCap1).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.QtyCap2).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.QtyCap3).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.QtyMat1)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("QtyMAT1");

                entity.Property(e => e.QtyMat10)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("QtyMAT10");

                entity.Property(e => e.QtyMat11)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("QtyMAT11");

                entity.Property(e => e.QtyMat12)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("QtyMAT12");

                entity.Property(e => e.QtyMat13)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("QtyMAT13");

                entity.Property(e => e.QtyMat14)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("QtyMAT14");

                entity.Property(e => e.QtyMat15)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("QtyMAT15");

                entity.Property(e => e.QtyMat16)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("QtyMAT16");

                entity.Property(e => e.QtyMat17)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("QtyMAT17");

                entity.Property(e => e.QtyMat18)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("QtyMAT18");

                entity.Property(e => e.QtyMat2)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("QtyMAT2");

                entity.Property(e => e.QtyMat3)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("QtyMAT3");

                entity.Property(e => e.QtyMat4)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("QtyMAT4");

                entity.Property(e => e.QtyMat5)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("QtyMAT5");

                entity.Property(e => e.QtyMat6)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("QtyMAT6");

                entity.Property(e => e.QtyMat7)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("QtyMAT7");

                entity.Property(e => e.QtyMat8)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("QtyMAT8");

                entity.Property(e => e.QtyMat9)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("QtyMAT9");
            });

            modelBuilder.Entity<MaterialProgramado>(entity =>
            {
                entity.ToTable("MaterialProgramado");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Ot).HasColumnName("OT");

                entity.Property(e => e.Solicitud).HasColumnType("numeric(18, 2)");
            });

            modelBuilder.Entity<MaterialesExtrusion>(entity =>
            {
                entity.ToTable("MaterialesExtrusion");

                entity.Property(e => e.MatAsignado)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .HasColumnName("Mat_asignado");

                entity.Property(e => e.Material)
                    .HasMaxLength(80)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Mezcla>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Mezcla");

                entity.Property(e => e.FechaCrea)
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaModifica)
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.NombreMecla).HasMaxLength(50);

                entity.Property(e => e.UsrCrea).HasMaxLength(50);

                entity.Property(e => e.UsrModifica)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<MezclaPigmento>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.FechaCrea)
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaModifica)
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.NombreMaterial).HasMaxLength(50);

                entity.Property(e => e.UsrCrea).HasMaxLength(50);

                entity.Property(e => e.UsrModifica)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<MezclaProducto>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.FechaCrea)
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaModifica)
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.NombreMaterial).HasMaxLength(50);

                entity.Property(e => e.UsrCrea).HasMaxLength(50);

                entity.Property(e => e.UsrModifica)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<NominaVendedor>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("NominaVendedor");

                entity.Property(e => e.Base)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("BASE");

                entity.Property(e => e.Basenc)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("BASENC");

                entity.Property(e => e.Cartera)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("CARTERA");

                entity.Property(e => e.Cliente)
                    .HasMaxLength(50)
                    .HasColumnName("CLIENTE");

                entity.Property(e => e.Comision)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("COMISION");

                entity.Property(e => e.Dias)
                    .HasMaxLength(50)
                    .HasColumnName("DIAS");

                entity.Property(e => e.Factura)
                    .HasMaxLength(50)
                    .HasColumnName("FACTURA");

                entity.Property(e => e.FechaNomina).HasMaxLength(50);

                entity.Property(e => e.Fechafact)
                    .HasMaxLength(50)
                    .HasColumnName("FECHAFACT");

                entity.Property(e => e.Fechapago)
                    .HasMaxLength(50)
                    .HasColumnName("FECHAPAGO");

                entity.Property(e => e.Idvende)
                    .HasMaxLength(50)
                    .HasColumnName("IDVENDE");

                entity.Property(e => e.Liq)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("LIQ");

                entity.Property(e => e.Mes)
                    .HasMaxLength(50)
                    .HasColumnName("MES");

                entity.Property(e => e.Nc)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("NC");

                entity.Property(e => e.Periodo).HasColumnName("PERIODO");

                entity.Property(e => e.Rc)
                    .HasMaxLength(50)
                    .HasColumnName("RC");

                entity.Property(e => e.Sw).HasColumnName("SW");

                entity.Property(e => e.Valorrc)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("VALORRC");

                entity.Property(e => e.Vendedor).HasColumnName("VENDEDOR");
            });

            modelBuilder.Entity<OficialMaterialProduccionMateriaPrima>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Oficial_Material_produccion_materia_prima");

                entity.Property(e => e.Mezpigm1cap1)
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.Mezpigm1cap1Nom)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Mezpigm1cap2)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Mezpigm1cap2Nom)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Mezpigm1cap3)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Mezpigm1cap3Nom)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Mezpigm2cap1)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Mezpigm2cap1Nom)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Mezpigm2cap2)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Mezpigm2cap2Nom)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Mezpigm2cap3)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Mezpigm2cap3Nom)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Mezporc1).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Mezporc2).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Mezporc3).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Mezprod1cap1)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod1cap1Nom)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod1cap2)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod1cap2Nom)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod1cap3)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod1cap3Nom)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod2cap1)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod2cap1Nom)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod2cap2)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod2cap2Nom)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod2cap3)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod2cap3Nom)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod3cap1)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod3cap1Nom)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod3cap2)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod3cap2Nom)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod3cap3)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod3cap3Nom)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod4cap1)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod4cap1Nom)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod4cap2)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod4cap2Nom)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod4cap3)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Mezprod4cap3Nom)
                    .HasMaxLength(30)
                    .IsFixedLength();

                entity.Property(e => e.Ot)
                    .HasMaxLength(10)
                    .HasColumnName("OT")
                    .IsFixedLength();

                entity.Property(e => e.Porc1cap1).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Porc1cap2).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Porc1cap3).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Porc2cap1).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Porc2cap2).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Porc2cap3).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Porc3cap1).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Porc3cap2).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Porc3cap3).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Porc4cap1).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Porc4cap2).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Porc4cap3).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Porcpig1cap1).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Porcpig1cap2).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Porcpig1cap3).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Porcpig2cap1).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Porcpig2cap2).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Porcpig2cap3).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.QtyCap1).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.QtyCap2).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.QtyCap3).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.QtyMat1)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("QtyMAT1");

                entity.Property(e => e.QtyMat10)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("QtyMAT10");

                entity.Property(e => e.QtyMat11)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("QtyMAT11");

                entity.Property(e => e.QtyMat12)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("QtyMAT12");

                entity.Property(e => e.QtyMat13)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("QtyMAT13");

                entity.Property(e => e.QtyMat14)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("QtyMAT14");

                entity.Property(e => e.QtyMat15)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("QtyMAT15");

                entity.Property(e => e.QtyMat16)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("QtyMAT16");

                entity.Property(e => e.QtyMat17)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("QtyMAT17");

                entity.Property(e => e.QtyMat18)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("QtyMAT18");

                entity.Property(e => e.QtyMat2)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("QtyMAT2");

                entity.Property(e => e.QtyMat3)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("QtyMAT3");

                entity.Property(e => e.QtyMat4)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("QtyMAT4");

                entity.Property(e => e.QtyMat5)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("QtyMAT5");

                entity.Property(e => e.QtyMat6)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("QtyMAT6");

                entity.Property(e => e.QtyMat7)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("QtyMAT7");

                entity.Property(e => e.QtyMat8)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("QtyMAT8");

                entity.Property(e => e.QtyMat9)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("QtyMAT9");
            });

            modelBuilder.Entity<OficialTemporalMaterialProduccionMateriaPrima>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Oficial_temporal_Material_produccion_materia_prima");

                entity.Property(e => e.Mezprod1cap1).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Ot)
                    .HasMaxLength(10)
                    .HasColumnName("OT")
                    .IsFixedLength();
            });

            modelBuilder.Entity<Operario>(entity =>
            {
                entity.HasKey(e => e.NombreCompleto);

                entity.Property(e => e.NombreCompleto)
                    .HasMaxLength(70)
                    .HasColumnName("Nombre_Completo");

                entity.Property(e => e.Cedula).HasMaxLength(50);
            });

            modelBuilder.Entity<OperarioImpresion>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("OperarioImpresion");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Maquina)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.Property(e => e.Tipo).HasMaxLength(50);
            });

            modelBuilder.Entity<OperariosProceso>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Cedula)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("CEDULA");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .HasColumnName("NOMBRE");

                entity.Property(e => e.Planta)
                    .HasMaxLength(50)
                    .HasColumnName("PLANTA");
            });

            modelBuilder.Entity<Permiso>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Operario).HasMaxLength(50);

                entity.Property(e => e.Permiso1).HasColumnName("Permiso");
            });

            modelBuilder.Entity<Pigmento>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.FechaCrea)
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaModifica)
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.NombreMaterial).HasMaxLength(50);

                entity.Property(e => e.UsrCrea).HasMaxLength(50);

                entity.Property(e => e.UsrModifica)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<Planilla>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Planilla");
            });

            modelBuilder.Entity<Porcentajevendedorcliente>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("porcentajevendedorcliente");

                entity.Property(e => e.Nit)
                    .HasMaxLength(50)
                    .HasColumnName("nit");

                entity.Property(e => e.Porcentaje)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("porcentaje");
            });

            modelBuilder.Entity<PresentacionPt>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("PresentacionPT");

                entity.Property(e => e.FechaCrea)
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaModifica)
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.NombreFormato).HasMaxLength(50);

                entity.Property(e => e.UsrCrea).HasMaxLength(50);

                entity.Property(e => e.UsrModifica)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<ProcBopp>(entity =>
            {
                entity.HasKey(e => e.Item);

                entity.ToTable("ProcBopp");

                entity.Property(e => e.Calibre).HasColumnType("numeric(5, 2)");

                entity.Property(e => e.Cliente)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.ClienteItem)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Estado)
                    .HasMaxLength(50)
                    .HasColumnName("estado");

                entity.Property(e => e.ExtBruto).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.ExtCono2).HasMaxLength(50);

                entity.Property(e => e.ExtConoC)
                    .HasColumnType("numeric(18, 3)")
                    .HasColumnName("extConoC");

                entity.Property(e => e.ExtTara).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Extancho)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("extancho");

                entity.Property(e => e.Extfuelle)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("extfuelle");

                entity.Property(e => e.Extlargo)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("extlargo");

                entity.Property(e => e.Extnetokg)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("extnetokg");

                entity.Property(e => e.Extrend)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("extrend");

                entity.Property(e => e.Exttotal)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("exttotal");

                entity.Property(e => e.Exttotaldesp)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("exttotaldesp");

                entity.Property(e => e.Exttotalextruir)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("exttotalextruir");

                entity.Property(e => e.Exttotalrollos)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("exttotalrollos");

                entity.Property(e => e.Extunidad)
                    .HasMaxLength(50)
                    .HasColumnName("extunidad")
                    .IsFixedLength();

                entity.Property(e => e.Fecha)
                    .HasColumnType("date")
                    .HasColumnName("fecha")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Hora)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Material)
                    .HasMaxLength(50)
                    .HasColumnName("material");

                entity.Property(e => e.NomStatus).HasMaxLength(50);

                entity.Property(e => e.Operador)
                    .HasMaxLength(50)
                    .HasColumnName("operador");

                entity.Property(e => e.Ot)
                    .HasMaxLength(50)
                    .HasColumnName("OT");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Turno)
                    .HasMaxLength(80)
                    .IsFixedLength();
            });

            modelBuilder.Entity<ProcCorte>(entity =>
            {
                entity.HasKey(e => e.Item);

                entity.ToTable("ProcCorte");

                entity.Property(e => e.Calibre).HasColumnType("numeric(18, 1)");

                entity.Property(e => e.Extancho)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("extancho");

                entity.Property(e => e.Fecha)
                    .HasColumnType("date")
                    .HasColumnName("fecha")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Hora)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Material)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.NomStatus).HasMaxLength(50);

                entity.Property(e => e.Peso).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Rollo).HasMaxLength(50);

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<ProcExtrusion>(entity =>
            {
                entity.HasKey(e => e.Item);

                entity.ToTable("ProcExtrusion");

                entity.Property(e => e.Ac)
                    .HasMaxLength(10)
                    .HasColumnName("AC")
                    .IsFixedLength();

                entity.Property(e => e.Calibre).HasColumnType("numeric(5, 2)");

                entity.Property(e => e.Cliente)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.ClienteItem)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.EnvioZeus)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Estado)
                    .HasMaxLength(50)
                    .HasColumnName("estado");

                entity.Property(e => e.EstadoInventario)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Estado_Inventario")
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.ExtBruto).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.ExtCono2).HasMaxLength(50);

                entity.Property(e => e.ExtConoC)
                    .HasColumnType("numeric(18, 3)")
                    .HasColumnName("extConoC");

                entity.Property(e => e.ExtTara).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Extancho)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("extancho");

                entity.Property(e => e.Extfuelle)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("extfuelle");

                entity.Property(e => e.Extlargo)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("extlargo");

                entity.Property(e => e.Extnetokg)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("extnetokg");

                entity.Property(e => e.Extrend)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("extrend");

                entity.Property(e => e.Exttotal)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("exttotal");

                entity.Property(e => e.Exttotaldesp)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("exttotaldesp");

                entity.Property(e => e.Exttotalextruir)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("exttotalextruir");

                entity.Property(e => e.Exttotalrollos)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("exttotalrollos");

                entity.Property(e => e.Extunidad)
                    .HasMaxLength(10)
                    .HasColumnName("extunidad")
                    .IsFixedLength();

                entity.Property(e => e.Facturado)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Fecha)
                    .HasColumnType("date")
                    .HasColumnName("fecha")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaFacturado)
                    .HasColumnType("date")
                    .HasColumnName("Fecha_Facturado");

                entity.Property(e => e.FechaRecibidoDespacho)
                    .HasColumnType("date")
                    .HasColumnName("Fecha_Recibido_Despacho");

                entity.Property(e => e.FechaSalida)
                    .HasColumnType("date")
                    .HasColumnName("Fecha_Salida");

                entity.Property(e => e.Hora)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Material)
                    .HasMaxLength(50)
                    .HasColumnName("material");

                entity.Property(e => e.NomStatus).HasMaxLength(50);

                entity.Property(e => e.Observaciones)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Operador)
                    .HasMaxLength(50)
                    .HasColumnName("operador");

                entity.Property(e => e.Ot)
                    .HasMaxLength(50)
                    .HasColumnName("OT");

                entity.Property(e => e.QtyDisponible).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.QtyNoDisponible).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.TipoDesperdicio).HasMaxLength(50);

                entity.Property(e => e.Turno)
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            modelBuilder.Entity<ProcImpresion>(entity =>
            {
                entity.ToTable("ProcImpresion");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Ancho).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.Fecha)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("fecha")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Ots1).HasColumnName("OTS1");

                entity.Property(e => e.Ots2).HasColumnName("OTS2");

                entity.Property(e => e.Ots3).HasColumnName("OTS3");

                entity.Property(e => e.Ots4).HasColumnName("OTS4");

                entity.Property(e => e.Ots5).HasColumnName("OTS5");

                entity.Property(e => e.Peso).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Tirilla).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.UsrCrea).HasMaxLength(50);
            });

            modelBuilder.Entity<ProcImpresionRollosBopp>(entity =>
            {
                entity.ToTable("ProcImpresion_RollosBopp");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Adj).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Ancho).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Cliente).HasColumnName("cliente");

                entity.Property(e => e.CodCliente)
                    .HasMaxLength(10)
                    .HasColumnName("Cod_Cliente")
                    .IsFixedLength();

                entity.Property(e => e.CodItem).HasColumnName("Cod_item");

                entity.Property(e => e.Estado).HasMaxLength(5);

                entity.Property(e => e.Fecha)
                    .HasColumnType("date")
                    .HasColumnName("fecha")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Kls).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Ot)
                    .HasMaxLength(10)
                    .HasColumnName("OT")
                    .IsFixedLength();

                entity.Property(e => e.OtImp)
                    .HasMaxLength(10)
                    .HasColumnName("OT_Imp")
                    .IsFixedLength();

                entity.Property(e => e.RAncho)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("R_ancho");

                entity.Property(e => e.RCalibre)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("R_Calibre");

                entity.Property(e => e.RPeso)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("R_Peso");

                entity.Property(e => e.Rodillo).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Rollos)
                    .HasMaxLength(20)
                    .IsFixedLength();

                entity.Property(e => e.SAncho)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("S_ancho");

                entity.Property(e => e.STirilla)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("S_Tirilla");

                entity.Property(e => e.Tipo)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Tirilla).HasColumnType("numeric(18, 2)");
            });

            modelBuilder.Entity<ProcImpresionTblImpresion>(entity =>
            {
                entity.ToTable("ProcImpresion_Tbl_impresion");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Ancho).HasColumnType("numeric(18, 1)");

                entity.Property(e => e.AnchoMed).HasColumnType("numeric(18, 1)");

                entity.Property(e => e.Cliente).HasColumnName("cliente");

                entity.Property(e => e.CodCliente)
                    .HasMaxLength(10)
                    .HasColumnName("Cod_Cliente")
                    .IsFixedLength();

                entity.Property(e => e.CodItem).HasColumnName("Cod_item");

                entity.Property(e => e.DatosOtKg).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Fecha)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("fecha")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Impresion)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Ots1).HasColumnName("OTS1");

                entity.Property(e => e.Ots2).HasColumnName("OTS2");

                entity.Property(e => e.Ots3).HasColumnName("OTS3");

                entity.Property(e => e.Ots4).HasColumnName("OTS4");

                entity.Property(e => e.Ots5).HasColumnName("OTS5");

                entity.Property(e => e.Rodillo).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.UsrCrea).HasMaxLength(50);
            });

            modelBuilder.Entity<ProcImpresionTblImpresion2>(entity =>
            {
                entity.ToTable("ProcImpresion_Tbl_impresion2");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Ancho).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Cliente).HasColumnName("cliente");

                entity.Property(e => e.CodCliente)
                    .HasMaxLength(10)
                    .HasColumnName("Cod_Cliente")
                    .IsFixedLength();

                entity.Property(e => e.CodItem).HasColumnName("Cod_item");

                entity.Property(e => e.Fecha)
                    .HasColumnType("date")
                    .HasColumnName("fecha")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Impresion)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Ots).HasColumnName("OTS");

                entity.Property(e => e.UsrCrea).HasMaxLength(50);
            });

            modelBuilder.Entity<ProcImpresionTblRollosImp>(entity =>
            {
                entity.ToTable("ProcImpresion_TblRollosImp");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Ancho).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Calibre).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Fecha)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("fecha")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdMat).HasColumnName("ID_Mat");

                entity.Property(e => e.Impresion)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.P1kg).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.P2kg).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.P3kg).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.P4kg).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.P5kg).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PesoNeto).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Pista1).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Pista2).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Pista3).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Pista4).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Pista5).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Tirilla).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.UsrCrea).HasMaxLength(50);
            });

            modelBuilder.Entity<ProcSellado>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ProcSellado");

                entity.Property(e => e.Ac)
                    .HasMaxLength(10)
                    .HasColumnName("AC")
                    .IsFixedLength();

                entity.Property(e => e.Cedula).HasMaxLength(50);

                entity.Property(e => e.Cedula2).HasMaxLength(50);

                entity.Property(e => e.Cedula3).HasMaxLength(50);

                entity.Property(e => e.Cedula4).HasMaxLength(50);

                entity.Property(e => e.Cedula5).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CodCliente)
                    .HasMaxLength(10)
                    .HasColumnName("Cod_Cliente")
                    .IsFixedLength();

                entity.Property(e => e.DatoEntrada).HasColumnName("Dato_Entrada");

                entity.Property(e => e.DatoSalida).HasColumnName("Dato_Salida");

                entity.Property(e => e.Desv).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.EnvioZeus)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.EstadoInventario)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Estado_Inventario")
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.Facturado)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.FechaCambio).HasColumnName("Fecha_Cambio");

                entity.Property(e => e.FechaEntrada)
                    .HasColumnType("date")
                    .HasColumnName("Fecha_Entrada")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaFacturado)
                    .HasColumnType("date")
                    .HasColumnName("Fecha_Facturado");

                entity.Property(e => e.FechaRecibidoDespacho)
                    .HasColumnType("date")
                    .HasColumnName("Fecha_Recibido_Despacho");

                entity.Property(e => e.FechaSalida)
                    .HasColumnType("date")
                    .HasColumnName("Fecha_Salida");

                entity.Property(e => e.Item).ValueGeneratedOnAdd();

                entity.Property(e => e.Maquina)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.NomReferencia).HasColumnName("Nom_Referencia");

                entity.Property(e => e.NomStatus).HasMaxLength(50);

                entity.Property(e => e.Observaciones)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Operario5).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Ot)
                    .HasMaxLength(10)
                    .HasColumnName("OT")
                    .IsFixedLength();

                entity.Property(e => e.Peso).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PesoMillar).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Pesot).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Qty).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.QtyDisponible).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.QtyNoDisponible).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Referencia)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.RemplazoItem).HasMaxLength(50);

                entity.Property(e => e.Turnos)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Unidad).HasMaxLength(50);
            });

            modelBuilder.Entity<ProcSelladoNomina>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ProcSelladoNomina");

                entity.Property(e => e.Cedula).HasMaxLength(50);

                entity.Property(e => e.Cedula2).HasMaxLength(50);

                entity.Property(e => e.Cedula3).HasMaxLength(50);

                entity.Property(e => e.Cedula4).HasMaxLength(50);

                entity.Property(e => e.Cedula5).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CodCliente)
                    .HasMaxLength(10)
                    .HasColumnName("Cod_Cliente")
                    .IsFixedLength();

                entity.Property(e => e.DatoEntrada).HasColumnName("Dato_Entrada");

                entity.Property(e => e.DatoSalida).HasColumnName("Dato_Salida");

                entity.Property(e => e.Desv).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.EnvioZeus)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.FechaCambio).HasColumnName("Fecha_Cambio");

                entity.Property(e => e.FechaEntrada)
                    .HasColumnType("date")
                    .HasColumnName("Fecha_Entrada")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Item).ValueGeneratedOnAdd();

                entity.Property(e => e.Maquina)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.NomReferencia).HasColumnName("Nom_Referencia");

                entity.Property(e => e.NomStatus).HasMaxLength(50);

                entity.Property(e => e.Operario5).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Ot)
                    .HasMaxLength(10)
                    .HasColumnName("OT")
                    .IsFixedLength();

                entity.Property(e => e.Peso).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PesoMillar).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Pesot).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Qty).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Referencia)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Rollo)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Turnos)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Unidad).HasMaxLength(50);
            });

            modelBuilder.Entity<Procdesperdicio>(entity =>
            {
                entity.HasKey(e => e.Item);

                entity.ToTable("Procdesperdicio");

                entity.Property(e => e.ExtBruto).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.ExtTara).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Extnetokg)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("extnetokg");

                entity.Property(e => e.Exttotaldesp)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("exttotaldesp");

                entity.Property(e => e.Exttotalextruir)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("exttotalextruir");

                entity.Property(e => e.Exttotalrollos)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("exttotalrollos");

                entity.Property(e => e.Fecha)
                    .HasColumnType("date")
                    .HasColumnName("fecha")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Material)
                    .HasMaxLength(50)
                    .HasColumnName("material");

                entity.Property(e => e.Operador)
                    .HasMaxLength(50)
                    .HasColumnName("operador");

                entity.Property(e => e.Ot)
                    .HasMaxLength(50)
                    .HasColumnName("OT");
            });

            modelBuilder.Entity<ProcdesperdicioDetalle>(entity =>
            {
                entity.HasKey(e => e.Cons);

                entity.ToTable("Procdesperdicio_detalle");

                entity.Property(e => e.Calibre).HasColumnType("numeric(5, 2)");

                entity.Property(e => e.Extancho)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("extancho");

                entity.Property(e => e.Extnetokg)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("extnetokg");

                entity.Property(e => e.Exttotalrollos)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("exttotalrollos");

                entity.Property(e => e.Extunidad)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("extunidad");

                entity.Property(e => e.Fecha)
                    .HasColumnType("date")
                    .HasColumnName("fecha")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Material)
                    .HasMaxLength(50)
                    .HasColumnName("material");

                entity.Property(e => e.Ot)
                    .HasMaxLength(50)
                    .HasColumnName("OT");

                entity.Property(e => e.Turno)
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            modelBuilder.Entity<ProcesoImpresion>(entity =>
            {
                entity.ToTable("Proceso_Impresion");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Adj1).HasColumnType("numeric(18, 1)");

                entity.Property(e => e.Adj2).HasColumnType("numeric(18, 1)");

                entity.Property(e => e.Adj3).HasColumnType("numeric(18, 1)");

                entity.Property(e => e.Adj4).HasColumnType("numeric(18, 1)");

                entity.Property(e => e.Adj5).HasColumnType("numeric(18, 1)");

                entity.Property(e => e.Ancho1).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Ancho2).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Ancho3).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Ancho4).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Ancho5).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.AnchoM1).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.AnchoM10).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.AnchoM11).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.AnchoM12).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.AnchoM13).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.AnchoM14).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.AnchoM15).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.AnchoM16).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.AnchoM17).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.AnchoM18).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.AnchoM19).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.AnchoM2).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.AnchoM20).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.AnchoM21).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.AnchoM22).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.AnchoM23).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.AnchoM24).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.AnchoM25).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.AnchoM26).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.AnchoM27).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.AnchoM28).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.AnchoM29).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.AnchoM3).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.AnchoM30).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.AnchoM31).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.AnchoM32).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.AnchoM33).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.AnchoM34).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.AnchoM35).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.AnchoM36).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.AnchoM37).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.AnchoM38).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.AnchoM39).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.AnchoM4).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.AnchoM40).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.AnchoM41).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.AnchoM42).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.AnchoM43).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.AnchoM44).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.AnchoM45).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.AnchoM46).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.AnchoM47).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.AnchoM48).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.AnchoM49).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.AnchoM5).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.AnchoM50).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.AnchoM6).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.AnchoM7).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.AnchoM8).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.AnchoM9).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.AnchoPista).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CalibreM1).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CalibreM10).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CalibreM11).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CalibreM12).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CalibreM13).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CalibreM14).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CalibreM15).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CalibreM16).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CalibreM17).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CalibreM18).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CalibreM19).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CalibreM2).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CalibreM20).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CalibreM21).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CalibreM22).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CalibreM23).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CalibreM24).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CalibreM25).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CalibreM26).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CalibreM27).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CalibreM28).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CalibreM29).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CalibreM3).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CalibreM30).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CalibreM31).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CalibreM32).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CalibreM33).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CalibreM34).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CalibreM35).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CalibreM36).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CalibreM37).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CalibreM38).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CalibreM39).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CalibreM4).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CalibreM40).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CalibreM41).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CalibreM42).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CalibreM43).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CalibreM44).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CalibreM45).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CalibreM46).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CalibreM47).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CalibreM48).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CalibreM49).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CalibreM5).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CalibreM50).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CalibreM6).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CalibreM7).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CalibreM8).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CalibreM9).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Cliente1).HasColumnName("cliente1");

                entity.Property(e => e.Cliente2).HasColumnName("cliente2");

                entity.Property(e => e.Cliente3).HasColumnName("cliente3");

                entity.Property(e => e.Cliente4).HasColumnName("cliente4");

                entity.Property(e => e.Cliente5).HasColumnName("cliente5");

                entity.Property(e => e.CodCliente1)
                    .HasMaxLength(10)
                    .HasColumnName("Cod_Cliente1")
                    .IsFixedLength();

                entity.Property(e => e.CodCliente2)
                    .HasMaxLength(10)
                    .HasColumnName("Cod_Cliente2")
                    .IsFixedLength();

                entity.Property(e => e.CodCliente3)
                    .HasMaxLength(10)
                    .HasColumnName("Cod_Cliente3")
                    .IsFixedLength();

                entity.Property(e => e.CodCliente4)
                    .HasMaxLength(10)
                    .HasColumnName("Cod_Cliente4")
                    .IsFixedLength();

                entity.Property(e => e.CodCliente5)
                    .HasMaxLength(10)
                    .HasColumnName("Cod_Cliente5")
                    .IsFixedLength();

                entity.Property(e => e.CodItem1).HasColumnName("Cod_item1");

                entity.Property(e => e.CodItem2).HasColumnName("Cod_item2");

                entity.Property(e => e.CodItem3).HasColumnName("Cod_item3");

                entity.Property(e => e.CodItem4).HasColumnName("Cod_item4");

                entity.Property(e => e.CodItem5).HasColumnName("Cod_item5");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.Fecha)
                    .HasColumnType("date")
                    .HasColumnName("fecha")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Fuelle1)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("fuelle1");

                entity.Property(e => e.Fuelle2).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Fuelle3).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Fuelle4).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Fuelle5).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Largo1).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Largo2).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Largo3).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Largo4).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Largo5).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Ots1).HasColumnName("OTS1");

                entity.Property(e => e.Ots2).HasColumnName("OTS2");

                entity.Property(e => e.Ots3).HasColumnName("OTS3");

                entity.Property(e => e.Ots4).HasColumnName("OTS4");

                entity.Property(e => e.Ots5).HasColumnName("OTS5");

                entity.Property(e => e.PesoM1).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PesoM10).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PesoM11).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PesoM12).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PesoM13).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PesoM14).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PesoM15).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PesoM16).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PesoM17).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PesoM18).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PesoM19).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PesoM2).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PesoM20).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PesoM21).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PesoM22).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PesoM23).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PesoM24).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PesoM25).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PesoM26).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PesoM27).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PesoM28).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PesoM29).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PesoM3).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PesoM30).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PesoM31).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PesoM32).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PesoM33).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PesoM34).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PesoM35).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PesoM36).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PesoM37).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PesoM38).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PesoM39).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PesoM4).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PesoM40).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PesoM41).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PesoM42).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PesoM43).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PesoM44).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PesoM45).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PesoM46).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PesoM47).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PesoM48).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PesoM49).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PesoM5)
                    .HasColumnType("numeric(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.PesoM50).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PesoM6).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PesoM7).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PesoM8).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PesoM9).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Qty1)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("qty1");

                entity.Property(e => e.Qty2)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("qty2");

                entity.Property(e => e.Qty3)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("qty3");

                entity.Property(e => e.Qty4)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("qty4");

                entity.Property(e => e.Qty5)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("qty5");

                entity.Property(e => e.Rodillo1).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Rodillo2).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Rodillo3).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Rodillo4).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Rodillo5).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Tirilla).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.TirillaKg).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.UsrCrea).HasMaxLength(50);
            });

            modelBuilder.Entity<Programacion>(entity =>
            {
                entity.ToTable("Programacion");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdMaterial).HasColumnName("id_material");

                entity.Property(e => e.Kilos).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Maq)
                    .HasMaxLength(10)
                    .HasColumnName("MAQ")
                    .IsFixedLength();

                entity.Property(e => e.Ot)
                    .HasMaxLength(10)
                    .HasColumnName("OT")
                    .IsFixedLength();

                entity.Property(e => e.Solicitud).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Tipo)
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            modelBuilder.Entity<ProvedoresMateriaprima>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ProvedoresMateriaprima");

                entity.Property(e => e.Grupo)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Nit)
                    .HasMaxLength(100)
                    .HasColumnName("nit");

                entity.Property(e => e.Nombre).HasMaxLength(100);
            });

            modelBuilder.Entity<Puertovascula>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("puertovascula");

                entity.Property(e => e.Proceso)
                    .HasMaxLength(50)
                    .HasColumnName("proceso");

                entity.Property(e => e.Puerto)
                    .HasMaxLength(10)
                    .HasColumnName("puerto")
                    .IsFixedLength();
            });

            modelBuilder.Entity<Referencia>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("PK__REFERNCI__CC87E127B6D7FBA5");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("CODIGO");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPCION");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");
            });

            modelBuilder.Entity<Riquisicion>(entity =>
            {
                entity.HasKey(e => e.Item);

                entity.ToTable("Riquisicion");

                entity.Property(e => e.Item).ValueGeneratedNever();

                entity.Property(e => e.Cap1pedido1)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("cap1pedido1");

                entity.Property(e => e.Cap1pedido2)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("cap1pedido2");

                entity.Property(e => e.Cap1pedido3)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("cap1pedido3");

                entity.Property(e => e.Cap1pedido4)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("cap1pedido4");

                entity.Property(e => e.Cap1pedido5)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("cap1pedido5");

                entity.Property(e => e.Cap1pedido6)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("cap1pedido6");

                entity.Property(e => e.Cap1pedir1)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("cap1pedir1");

                entity.Property(e => e.Cap1pedir2)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("cap1pedir2");

                entity.Property(e => e.Cap1pedir3)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("cap1pedir3");

                entity.Property(e => e.Cap1pedir4)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("cap1pedir4");

                entity.Property(e => e.Cap1pedir5)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("cap1pedir5");

                entity.Property(e => e.Cap1pedir6)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("cap1pedir6");

                entity.Property(e => e.Cap1porc1)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("cap1porc1");

                entity.Property(e => e.Cap1porc2)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("cap1porc2");

                entity.Property(e => e.Cap1porc3)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("cap1porc3");

                entity.Property(e => e.Cap1porc4)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("cap1porc4");

                entity.Property(e => e.Cap1porc5)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("cap1porc5");

                entity.Property(e => e.Cap1porc6)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("cap1porc6");

                entity.Property(e => e.Cap1t1)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("cap1t1");

                entity.Property(e => e.Cap1t2)
                    .IsUnicode(false)
                    .HasColumnName("cap1t2");

                entity.Property(e => e.Cap1t3)
                    .IsUnicode(false)
                    .HasColumnName("cap1t3");

                entity.Property(e => e.Cap1t4)
                    .IsUnicode(false)
                    .HasColumnName("cap1t4");

                entity.Property(e => e.Cap1t5)
                    .IsUnicode(false)
                    .HasColumnName("cap1t5");

                entity.Property(e => e.Cap1t6)
                    .IsUnicode(false)
                    .HasColumnName("cap1t6");

                entity.Property(e => e.Cap1t7)
                    .IsUnicode(false)
                    .HasColumnName("cap1t7");

                entity.Property(e => e.Cap2pedido1)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("cap2pedido1");

                entity.Property(e => e.Cap2pedido2)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("cap2pedido2");

                entity.Property(e => e.Cap2pedido3)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("cap2pedido3");

                entity.Property(e => e.Cap2pedido4)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("cap2pedido4");

                entity.Property(e => e.Cap2pedido5)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("cap2pedido5");

                entity.Property(e => e.Cap2pedido6)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("cap2pedido6");

                entity.Property(e => e.Cap2pedir1)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("cap2pedir1");

                entity.Property(e => e.Cap2pedir2)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("cap2pedir2");

                entity.Property(e => e.Cap2pedir3)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("cap2pedir3");

                entity.Property(e => e.Cap2pedir4)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("cap2pedir4");

                entity.Property(e => e.Cap2pedir5)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("cap2pedir5");

                entity.Property(e => e.Cap2pedir6)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("cap2pedir6");

                entity.Property(e => e.Cap2porc1)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("cap2porc1");

                entity.Property(e => e.Cap2porc2)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("cap2porc2");

                entity.Property(e => e.Cap2porc3)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("cap2porc3");

                entity.Property(e => e.Cap2porc4)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("cap2porc4");

                entity.Property(e => e.Cap2porc5)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("cap2porc5");

                entity.Property(e => e.Cap2porc6)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("cap2porc6");

                entity.Property(e => e.Cap2t1)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("cap2t1");

                entity.Property(e => e.Cap2t2)
                    .IsUnicode(false)
                    .HasColumnName("cap2t2");

                entity.Property(e => e.Cap2t3)
                    .IsUnicode(false)
                    .HasColumnName("cap2t3");

                entity.Property(e => e.Cap2t4)
                    .IsUnicode(false)
                    .HasColumnName("cap2t4");

                entity.Property(e => e.Cap2t5)
                    .IsUnicode(false)
                    .HasColumnName("cap2t5");

                entity.Property(e => e.Cap2t6)
                    .IsUnicode(false)
                    .HasColumnName("cap2t6");

                entity.Property(e => e.Cap2t7)
                    .IsUnicode(false)
                    .HasColumnName("cap2t7");

                entity.Property(e => e.Cap3pedido1)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("cap3pedido1");

                entity.Property(e => e.Cap3pedido2)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("cap3pedido2");

                entity.Property(e => e.Cap3pedido3)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("cap3pedido3");

                entity.Property(e => e.Cap3pedido4)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("cap3pedido4");

                entity.Property(e => e.Cap3pedido5)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("cap3pedido5");

                entity.Property(e => e.Cap3pedido6)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("cap3pedido6");

                entity.Property(e => e.Cap3pedir1)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("cap3pedir1");

                entity.Property(e => e.Cap3pedir2)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("cap3pedir2");

                entity.Property(e => e.Cap3pedir3)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("cap3pedir3");

                entity.Property(e => e.Cap3pedir4)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("cap3pedir4");

                entity.Property(e => e.Cap3pedir5)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("cap3pedir5");

                entity.Property(e => e.Cap3pedir6)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("cap3pedir6");

                entity.Property(e => e.Cap3porc1)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("cap3porc1");

                entity.Property(e => e.Cap3porc2)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("cap3porc2");

                entity.Property(e => e.Cap3porc3)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("cap3porc3");

                entity.Property(e => e.Cap3porc4)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("cap3porc4");

                entity.Property(e => e.Cap3porc5)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("cap3porc5");

                entity.Property(e => e.Cap3porc6)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("cap3porc6");

                entity.Property(e => e.Cap3t1)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("cap3t1");

                entity.Property(e => e.Cap3t2)
                    .IsUnicode(false)
                    .HasColumnName("cap3t2");

                entity.Property(e => e.Cap3t3)
                    .IsUnicode(false)
                    .HasColumnName("cap3t3");

                entity.Property(e => e.Cap3t4)
                    .IsUnicode(false)
                    .HasColumnName("cap3t4");

                entity.Property(e => e.Cap3t5)
                    .IsUnicode(false)
                    .HasColumnName("cap3t5");

                entity.Property(e => e.Cap3t6)
                    .IsUnicode(false)
                    .HasColumnName("cap3t6");

                entity.Property(e => e.Cap3t7)
                    .IsUnicode(false)
                    .HasColumnName("cap3t7");

                entity.Property(e => e.Cliente)
                    .IsUnicode(false)
                    .HasColumnName("cliente");

                entity.Property(e => e.CodCliente).HasColumnName("Cod_cliente");

                entity.Property(e => e.CodEstado).HasColumnName("Cod_Estado");

                entity.Property(e => e.CodItem).HasColumnName("Cod_item");

                entity.Property(e => e.Estado)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.FechaCrea)
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaModifica)
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.ItemCliente)
                    .IsUnicode(false)
                    .HasColumnName("Item_Cliente");

                entity.Property(e => e.Ot).HasColumnName("OT");

                entity.Property(e => e.OtMaquinaMp).HasColumnName("OT_MAQUINA_MP");

                entity.Property(e => e.OtPedidoMp)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("OT_PEDIDO_MP");

                entity.Property(e => e.TotalOt)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("Total_OT");

                entity.Property(e => e.UsrCrea).HasMaxLength(50);

                entity.Property(e => e.UsrModifica)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<SelladoPt>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("SelladoPT");

                entity.Property(e => e.FechaCrea)
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaModifica)
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.NombreFormato).HasMaxLength(50);

                entity.Property(e => e.UsrCrea).HasMaxLength(50);

                entity.Property(e => e.UsrModifica)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<SubprocesoImpresion>(entity =>
            {
                entity.HasKey(e => e.Item);

                entity.ToTable("subprocesoImpresion");

                entity.Property(e => e.Calibre).HasColumnType("numeric(5, 2)");

                entity.Property(e => e.Cliente)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.ClienteItem)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Estado)
                    .HasMaxLength(50)
                    .HasColumnName("estado");

                entity.Property(e => e.ExtBruto).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.ExtCono2).HasMaxLength(50);

                entity.Property(e => e.ExtConoC)
                    .HasColumnType("numeric(18, 3)")
                    .HasColumnName("extConoC");

                entity.Property(e => e.ExtTara).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Extancho)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("extancho");

                entity.Property(e => e.Extfuelle)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("extfuelle");

                entity.Property(e => e.Extlargo)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("extlargo");

                entity.Property(e => e.Extnetokg)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("extnetokg");

                entity.Property(e => e.Exttotal)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("exttotal");

                entity.Property(e => e.Extunidad)
                    .HasMaxLength(10)
                    .HasColumnName("extunidad")
                    .IsFixedLength();

                entity.Property(e => e.Fecha)
                    .HasColumnType("date")
                    .HasColumnName("fecha")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Material)
                    .HasMaxLength(50)
                    .HasColumnName("material");

                entity.Property(e => e.Operador)
                    .HasMaxLength(50)
                    .HasColumnName("operador");

                entity.Property(e => e.Ot)
                    .HasMaxLength(50)
                    .HasColumnName("OT");

                entity.Property(e => e.Tipo).HasMaxLength(50);
            });

            modelBuilder.Entity<Table1>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Table_1");

                entity.Property(e => e.Prueba)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("PRUEBA")
                    .IsFixedLength();
            });

            modelBuilder.Entity<TemporalMaterialProduccionMateriaPrima>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("temporal_Material_produccion_materia_prima");

                entity.Property(e => e.Mezprod1cap1).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Ot)
                    .HasMaxLength(10)
                    .HasColumnName("OT")
                    .IsFixedLength();
            });

            modelBuilder.Entity<TipoDesperdicio>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<TipoProceso>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TipoProceso");

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.Property(e => e.Tipo)
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            modelBuilder.Entity<TiposMateriale>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("PK__TIPOS_MA__CC87E127EA5EBD90");

                entity.ToTable("TIPOS_MATERIALES");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("CODIGO");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPCION");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");
            });

            modelBuilder.Entity<Tratado>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Tratado");

                entity.Property(e => e.FechaCrea)
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaModifica)
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.NombreTratado).HasMaxLength(50);

                entity.Property(e => e.UsrCrea).HasMaxLength(50);

                entity.Property(e => e.UsrModifica)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<Unidade>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.FechaCrea)
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaModifica)
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.NombreUnidades).HasMaxLength(50);

                entity.Property(e => e.UsrCrea).HasMaxLength(50);

                entity.Property(e => e.UsrModifica)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.CampoZeus)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.FechaCrea)
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaModifica)
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Nomina)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.Reportes)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UsrCrea)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UsrModifica)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<UsuarioPermiso>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("Usuario_permisos");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.CampoZeus)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.FechaCrea)
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaModifica)
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.Reportes)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UsrCrea)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UsrModifica)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<Vendedore>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(10)
                    .HasColumnName("ID")
                    .IsFixedLength();

                entity.Property(e => e.IdBagpro)
                    .HasMaxLength(10)
                    .HasColumnName("ID_bagpro")
                    .IsFixedLength();

                entity.Property(e => e.Permiso)
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            modelBuilder.Entity<ViewCliente>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("ViewClientes");

                entity.Property(e => e.CodBagpro)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Cod_Bagpro");

                entity.Property(e => e.Codigo).ValueGeneratedOnAdd();

                entity.Property(e => e.Estado)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.FechaCrea).HasColumnType("smalldatetime");

                entity.Property(e => e.FechaModifica).HasColumnType("smalldatetime");

                entity.Property(e => e.IdentNro).HasMaxLength(50);

                entity.Property(e => e.IdentTipo)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.UsrCrea).HasMaxLength(50);

                entity.Property(e => e.UsrModifica).HasMaxLength(50);
            });

            modelBuilder.Entity<ViewProcExtrusionReporte>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("ViewProcExtrusionReporte");

                entity.Property(e => e.Cliente).HasColumnName("CLIENTE");

                entity.Property(e => e.EstadoInventario)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ESTADO_INVENTARIO");

                entity.Property(e => e.FechaEntrada)
                    .HasColumnType("date")
                    .HasColumnName("FECHA_ENTRADA");

                entity.Property(e => e.Hora)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Item)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ITEM");

                entity.Property(e => e.Maquina).HasColumnName("MAQUINA");

                entity.Property(e => e.Ot)
                    .HasMaxLength(50)
                    .HasColumnName("OT");

                entity.Property(e => e.Peso)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("PESO");

                entity.Property(e => e.Qty)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("QTY");

                entity.Property(e => e.QtyDisponible).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.QtyNoDisponible).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Referencia)
                    .HasMaxLength(50)
                    .HasColumnName("REFERENCIA")
                    .IsFixedLength();
            });

            modelBuilder.Entity<ViewProcSellado>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("ViewProcSellado");

                entity.Property(e => e.Cliente).HasColumnName("CLIENTE");

                entity.Property(e => e.EstadoInventario)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("ESTADO_INVENTARIO");

                entity.Property(e => e.FechaEntrada)
                    .HasColumnType("date")
                    .HasColumnName("FECHA_ENTRADA");

                entity.Property(e => e.Hora).HasColumnName("HORA");

                entity.Property(e => e.Item)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ITEM");

                entity.Property(e => e.Maquina)
                    .HasMaxLength(10)
                    .HasColumnName("MAQUINA")
                    .IsFixedLength();

                entity.Property(e => e.Ot)
                    .HasMaxLength(10)
                    .HasColumnName("OT")
                    .IsFixedLength();

                entity.Property(e => e.Peso)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("PESO");

                entity.Property(e => e.Qty)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("QTY");

                entity.Property(e => e.QtyDisponible).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.QtyNoDisponible).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Referencia)
                    .HasMaxLength(10)
                    .HasColumnName("REFERENCIA")
                    .IsFixedLength();
            });

            modelBuilder.Entity<Wiketiando>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Wiketiando");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Dia).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Id)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Mq).HasColumnName("MQ");

                entity.Property(e => e.Noche).HasColumnType("numeric(18, 2)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

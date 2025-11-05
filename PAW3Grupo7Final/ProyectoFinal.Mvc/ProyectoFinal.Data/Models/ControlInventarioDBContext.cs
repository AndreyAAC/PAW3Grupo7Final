using Microsoft.EntityFrameworkCore;

namespace ProyectoFinal.Data.Models;

public partial class ControlInventarioDBContext : DbContext
{
    public ControlInventarioDBContext() { }

    public ControlInventarioDBContext(DbContextOptions<ControlInventarioDBContext> options)
        : base(options) { }

    public virtual DbSet<Usuario> Usuarios { get; set; }
    public virtual DbSet<Role> Roles { get; set; }
    public virtual DbSet<Producto> Productos { get; set; }
    public virtual DbSet<TipoProducto> TiposProducto { get; set; }
    public virtual DbSet<Inventario> Inventarios { get; set; }
    public virtual DbSet<Cliente> Clientes { get; set; }
    public virtual DbSet<Cita> Citas { get; set; }
    public virtual DbSet<Pedido> Pedidos { get; set; }
    public virtual DbSet<PedidoDetalle> PedidoDetalles { get; set; }
    public virtual DbSet<EstadoPedido> EstadosPedido { get; set; }
    public virtual DbSet<Gasto> Gastos { get; set; }
    public virtual DbSet<CategoriaGasto> CategoriasGasto { get; set; }
    public virtual DbSet<CuentasPagar> CuentasPorPagar { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // ===== Usuario =====
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK_Usuario");
            entity.ToTable("Usuario");

            entity.HasIndex(e => e.Cedula, "UQ_Usuario_cedula").IsUnique();
            entity.HasIndex(e => e.Correo, "UQ_Usuario_correo").IsUnique();

            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.Cedula).HasColumnName("cedula");
            entity.Property(e => e.Contrasenia).HasMaxLength(200).HasColumnName("contrasenia");
            entity.Property(e => e.Correo).HasMaxLength(256).HasColumnName("correo");
            entity.Property(e => e.NombreApellido).HasMaxLength(150).HasColumnName("nombreApellido");
            entity.Property(e => e.NombreUsuario).HasMaxLength(100).HasColumnName("nombreUsuario");
            entity.Property(e => e.Role).HasColumnName("role");
            entity.Property(e => e.Telefono).HasMaxLength(20).HasColumnName("telefono");

            // FK a Role
            entity.HasOne(d => d.RoleNavigation)
                  .WithMany(p => p.Usuarios)
                  .HasForeignKey(d => d.Role)
                  .OnDelete(DeleteBehavior.Restrict)
                  .HasConstraintName("FK_Usuario_Role");
        });

        // ===== Role =====
        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRole).HasName("PK_Role");
            entity.ToTable("Role");
            entity.Property(e => e.IdRole).HasColumnName("idRole");
            entity.Property(e => e.NombreRole).HasMaxLength(100).HasColumnName("NombreRole");
        });

        // ===== TipoProducto =====
        modelBuilder.Entity<TipoProducto>(entity =>
        {
            entity.HasKey(e => e.IdTipoProducto).HasName("PK_TipoProducto");
            entity.ToTable("TipoProducto");
            entity.Property(e => e.IdTipoProducto).HasColumnName("idTipoProducto");
            entity.Property(e => e.NombreTipo).HasMaxLength(120).HasColumnName("NombreTipo");
        });

        // ===== Producto =====
        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK_Producto");
            entity.ToTable("Producto");

            entity.Property(e => e.IdProducto).HasColumnName("idProducto");
            entity.Property(e => e.Nombre).HasMaxLength(200).HasColumnName("nombre");
            entity.Property(e => e.Imagen).HasMaxLength(500).HasColumnName("imagen");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.Precio).HasPrecision(18, 2).HasColumnName("precio");
            entity.Property(e => e.IdTipoProducto).HasColumnName("idTipoProducto");

            entity.HasOne(d => d.TipoProducto)
                  .WithMany(p => p.Productos)
                  .HasForeignKey(d => d.IdTipoProducto)
                  .OnDelete(DeleteBehavior.SetNull)
                  .HasConstraintName("FK_Producto_TipoProducto");
        });

        // ===== Inventario =====
        modelBuilder.Entity<Inventario>(entity =>
        {
            entity.HasKey(e => e.IdInventario).HasName("PK_Inventario");
            entity.ToTable("Inventario");
            entity.Property(e => e.IdInventario).HasColumnName("idInventario");
            entity.Property(e => e.IdProducto).HasColumnName("idProducto");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");

            entity.HasOne(d => d.Producto)
                  .WithMany(p => p.Inventarios)
                  .HasForeignKey(d => d.IdProducto)
                  .OnDelete(DeleteBehavior.Cascade)
                  .HasConstraintName("FK_Inventario_Producto");
        });

        // ===== Cliente =====
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PK_Cliente");
            entity.ToTable("Cliente");
            entity.Property(e => e.IdCliente).HasColumnName("idCliente");
            entity.Property(e => e.Nombre).HasMaxLength(150).HasColumnName("nombre");
            entity.Property(e => e.Telefono).HasMaxLength(20).HasColumnName("telefono");
            entity.Property(e => e.Correo).HasMaxLength(256).HasColumnName("correo");
        });

        // ===== Cita =====
        modelBuilder.Entity<Cita>(entity =>
        {
            entity.HasKey(e => e.IdCita).HasName("PK_Cita");
            entity.ToTable("Cita");

            entity.Property(e => e.IdCita).HasColumnName("idCita");
            entity.Property(e => e.IdCliente).HasColumnName("idCliente");
            entity.Property(e => e.Motivo).HasMaxLength(300).HasColumnName("motivo");
            entity.Property(e => e.Producto).HasColumnName("producto");
            entity.Property(e => e.Detalle).HasColumnName("detalle");
            entity.Property(e => e.FechaCita).HasColumnType("date").HasColumnName("FechaCita");
            entity.Property(e => e.HoraCita).HasColumnType("time").HasColumnName("HoraCita");

            entity.HasOne(d => d.Cliente)
                  .WithMany(p => p.Citas)
                  .HasForeignKey(d => d.IdCliente)
                  .OnDelete(DeleteBehavior.Cascade)
                  .HasConstraintName("FK_Cita_Cliente");

            entity.HasOne(d => d.ProductoNavigation)
                  .WithMany(p => p.Citas)
                  .HasForeignKey(d => d.Producto)
                  .OnDelete(DeleteBehavior.SetNull)
                  .HasConstraintName("FK_Cita_Producto");
        });

        // ===== EstadoPedido =====
        modelBuilder.Entity<EstadoPedido>(entity =>
        {
            entity.HasKey(e => e.IdEstadoPedido).HasName("PK_EstadoPedido");
            entity.ToTable("EstadoPedido");
            entity.Property(e => e.IdEstadoPedido).HasColumnName("idEstadoPedido");
            entity.Property(e => e.NombreEstado).HasMaxLength(100).HasColumnName("NombreEstado");
        });

        // ===== Pedido =====
        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasKey(e => e.IdPedido).HasName("PK_Pedido");
            entity.ToTable("Pedido");

            entity.Property(e => e.IdPedido).HasColumnName("idPedido");
            entity.Property(e => e.FechaDeInicio).HasColumnType("date").HasColumnName("fechaDeInicio");
            entity.Property(e => e.FechaDeEntrega).HasColumnType("date").HasColumnName("fechaDeEntrega");
            entity.Property(e => e.IdEstadoPedido).HasColumnName("idEstadoPedido");

            entity.HasOne(d => d.EstadoPedido)
                  .WithMany(p => p.Pedidos)
                  .HasForeignKey(d => d.IdEstadoPedido)
                  .OnDelete(DeleteBehavior.Restrict)
                  .HasConstraintName("FK_Pedido_EstadoPedido");
        });

        // ===== PedidoDetalle =====
        modelBuilder.Entity<PedidoDetalle>(entity =>
        {
            entity.ToTable("PedidoDetalle");
            entity.HasKey(e => new { e.IdPedido, e.IdProducto }).HasName("PK_PedidoDetalle");

            entity.Property(e => e.IdPedido).HasColumnName("idPedido");
            entity.Property(e => e.IdProducto).HasColumnName("idProducto");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.PrecioUnitario).HasPrecision(18, 2).HasColumnName("precioUnitario");

            entity.HasOne(d => d.Pedido)
                  .WithMany(p => p.PedidoDetalles)
                  .HasForeignKey(d => d.IdPedido)
                  .OnDelete(DeleteBehavior.Cascade)
                  .HasConstraintName("FK_PedidoDetalle_Pedido");

            entity.HasOne(d => d.Producto)
                  .WithMany(p => p.PedidoDetalles)
                  .HasForeignKey(d => d.IdProducto)
                  .OnDelete(DeleteBehavior.Restrict)
                  .HasConstraintName("FK_PedidoDetalle_Producto");
        });

        // ===== CategoriaGasto =====
        modelBuilder.Entity<CategoriaGasto>(entity =>
        {
            entity.HasKey(e => e.IdCategoriaGasto).HasName("PK_CategoriaGasto");
            entity.ToTable("CategoriaGasto");
            entity.Property(e => e.IdCategoriaGasto).HasColumnName("idCategoriaGasto");
            entity.Property(e => e.NombreCategoria).HasMaxLength(120).HasColumnName("NombreCategoria");
        });

        // ===== Gasto =====
        modelBuilder.Entity<Gasto>(entity =>
        {
            entity.HasKey(e => e.IdGasto).HasName("PK_Gasto");
            entity.ToTable("Gasto");

            entity.Property(e => e.IdGasto).HasColumnName("idGasto");
            entity.Property(e => e.Motivo).HasMaxLength(300).HasColumnName("motivo");
            entity.Property(e => e.FechaGasto).HasColumnType("date").HasColumnName("fechaGasto");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.Monto).HasPrecision(18, 2).HasColumnName("monto");
            entity.Property(e => e.IdCategoriaGasto).HasColumnName("idCategoriaGasto");

            entity.HasOne(d => d.CategoriaGasto)
                  .WithMany(p => p.Gastos)
                  .HasForeignKey(d => d.IdCategoriaGasto)
                  .OnDelete(DeleteBehavior.SetNull)
                  .HasConstraintName("FK_Gasto_CategoriaGasto");
        });

        // ===== CuentasPagar =====
        modelBuilder.Entity<CuentasPagar>(entity =>
        {
            entity.HasKey(e => e.IdCuentaPagar).HasName("PK_CuentasPagar");
            entity.ToTable("CuentasPagar");

            entity.Property(e => e.IdCuentaPagar).HasColumnName("idCuentaPagar");
            entity.Property(e => e.Motivo).HasMaxLength(300).HasColumnName("motivo");
            entity.Property(e => e.FechaCuentaPagar).HasColumnType("date").HasColumnName("fechaCuentaPagar");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.Monto).HasPrecision(18, 2).HasColumnName("monto");
            entity.Property(e => e.PlazoPagar).HasMaxLength(100).HasColumnName("plazoPagar");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
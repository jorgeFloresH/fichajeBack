using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace apiServices.Models
{
    public partial class siscolasgamcContext : DbContext
    {
        public siscolasgamcContext()
        {
        }

        public siscolasgamcContext(DbContextOptions<siscolasgamcContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Agencium> Agencia { get; set; } = null!;
        public virtual DbSet<Contacto> Contactos { get; set; } = null!;
        public virtual DbSet<DetalleObservacion> DetalleObservacions { get; set; } = null!;
        public virtual DbSet<Historial> Historials { get; set; } = null!;
        public virtual DbSet<HistorialDerivacion> HistorialDerivacions { get; set; } = null!;
        public virtual DbSet<Multimedium> Multimedia { get; set; } = null!;
        public virtual DbSet<Observacione> Observaciones { get; set; } = null!;
        public virtual DbSet<PantallaMul> PantallaMuls { get; set; } = null!;
        public virtual DbSet<Prioridad> Prioridads { get; set; } = null!;
        public virtual DbSet<Requisito> Requisitos { get; set; } = null!;
        public virtual DbSet<RequisitoTramite> RequisitoTramites { get; set; } = null!;
        public virtual DbSet<Ticket> Tickets { get; set; } = null!;
        public virtual DbSet<TipoPantalla> TipoPantallas { get; set; } = null!;
        public virtual DbSet<TipoPerfil> TipoPerfils { get; set; } = null!;
        public virtual DbSet<TraVen> TraVens { get; set; } = null!;
        public virtual DbSet<Tramite> Tramites { get; set; } = null!;
        public virtual DbSet<UserTicket> UserTickets { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;
        public virtual DbSet<UtTramite> UtTramites { get; set; } = null!;
        public virtual DbSet<VenUsuario> VenUsuarios { get; set; } = null!;
        public virtual DbSet<Ventanilla> Ventanillas { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
/*warningTo protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=172.16.66.103; Port=5433 ;Database=siscolasgamc;Username=postgres;Password=4tmt1k3t$");*/
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Agencium>(entity =>
            {
                entity.HasKey(e => e.IdAgencia)
                    .HasName("agencia_pkey");

                entity.ToTable("agencia");

                entity.Property(e => e.IdAgencia)
                    .HasColumnName("id_agencia")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Acdes).HasColumnName("acdes");

                entity.Property(e => e.Consulta).HasColumnName("consulta");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.Mapa).HasColumnName("mapa");

                entity.Property(e => e.Multimedia).HasColumnName("multimedia");

                entity.Property(e => e.NomAgencia)
                    .HasMaxLength(50)
                    .HasColumnName("nom_agencia");
            });

            modelBuilder.Entity<Contacto>(entity =>
            {
                entity.HasKey(e => e.IdContacto)
                    .HasName("contacto_pkey");

                entity.ToTable("contacto");

                entity.Property(e => e.IdContacto)
                    .HasColumnName("id_contacto")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Correo).HasColumnName("correo");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.NCelular).HasColumnName("n_celular");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Contactos)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("id_usuario");
            });

            modelBuilder.Entity<DetalleObservacion>(entity =>
            {
                entity.HasKey(e => e.IdDetalleObs)
                    .HasName("detalle_observacion_pkey");

                entity.ToTable("detalle_observacion");

                entity.HasIndex(e => e.IdObservaciones, "fki_id_observaciones");

                entity.Property(e => e.IdDetalleObs)
                    .HasColumnName("id_detalle_obs")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.IdObservaciones).HasColumnName("id_observaciones");

                entity.Property(e => e.IdTramite).HasColumnName("id_tramite");

                entity.HasOne(d => d.IdObservacionesNavigation)
                    .WithMany(p => p.DetalleObservacions)
                    .HasForeignKey(d => d.IdObservaciones)
                    .HasConstraintName("id_observaciones");

                entity.HasOne(d => d.IdTramiteNavigation)
                    .WithMany(p => p.DetalleObservacions)
                    .HasForeignKey(d => d.IdTramite)
                    .HasConstraintName("id_tramite");
            });

            modelBuilder.Entity<Historial>(entity =>
            {
                entity.HasKey(e => e.IdHistorial)
                    .HasName("historial_pkey");

                entity.ToTable("historial");

                entity.HasIndex(e => e.IdUsuario, "fki_id_usuario");

                entity.Property(e => e.IdHistorial)
                    .ValueGeneratedNever()
                    .HasColumnName("id_historial");

                entity.Property(e => e.EstadoContraseña).HasColumnName("estado_contraseña");

                entity.Property(e => e.FechaModificacion)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("fecha_modificacion");

                entity.Property(e => e.IdAgencia).HasColumnName("id_agencia");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.Password)
                    .HasMaxLength(64)
                    .HasColumnName("password")
                    .IsFixedLength();

                entity.Property(e => e.UltimoLogin)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("ultimo_login");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Historials)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("id_usuario");
            });

            modelBuilder.Entity<HistorialDerivacion>(entity =>
            {
                entity.HasKey(e => e.IdDerivacion)
                    .HasName("historial_derivacion_pkey");

                entity.ToTable("historial_derivacion");

                entity.HasIndex(e => e.IdTicket, "fki_id_ticket");

                entity.Property(e => e.IdDerivacion)
                    .HasColumnName("id_derivacion")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.IdTicket).HasColumnName("id_ticket");

                entity.Property(e => e.VentanillaDestino).HasColumnName("ventanilla_destino");

                entity.Property(e => e.VentanillaOrigen).HasColumnName("ventanilla_origen");

                entity.HasOne(d => d.IdTicketNavigation)
                    .WithMany(p => p.HistorialDerivacions)
                    .HasForeignKey(d => d.IdTicket)
                    .HasConstraintName("id_ticket");
            });

            modelBuilder.Entity<Multimedium>(entity =>
            {
                entity.HasKey(e => e.IdMulti)
                    .HasName("multimedia_pkey");

                entity.ToTable("multimedia");

                entity.Property(e => e.IdMulti)
                    .HasColumnName("id_multi")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.IdAgencia).HasColumnName("id_agencia");

                entity.Property(e => e.NomVideo).HasColumnName("nom_video");

                entity.Property(e => e.Ruta).HasColumnName("ruta");

                entity.Property(e => e.Tipo).HasColumnName("tipo");

                entity.HasOne(d => d.IdAgenciaNavigation)
                    .WithMany(p => p.MultimediaNavigation)
                    .HasForeignKey(d => d.IdAgencia)
                    .HasConstraintName("id_agencia");
            });

            modelBuilder.Entity<Observacione>(entity =>
            {
                entity.HasKey(e => e.IdObservaciones)
                    .HasName("observaciones_pkey");

                entity.ToTable("observaciones");

                entity.Property(e => e.IdObservaciones)
                    .HasColumnName("id_observaciones")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.NomObservaciones)
                    .HasMaxLength(50)
                    .HasColumnName("nom_observaciones");
            });

            modelBuilder.Entity<PantallaMul>(entity =>
            {
                entity.HasKey(e => e.IdPantallaMul)
                    .HasName("pantalla_mul_pkey");

                entity.ToTable("pantalla_mul");

                entity.HasIndex(e => e.IdMulti, "fki_id_multi");

                entity.HasIndex(e => e.IdTipo, "fki_id_pantalla");

                entity.HasIndex(e => e.IdTipo, "fki_id_tipo");

                entity.Property(e => e.IdPantallaMul)
                    .HasColumnName("id_pantalla_mul")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.IdAgencia).HasColumnName("id_agencia");

                entity.Property(e => e.IdMulti).HasColumnName("id_multi");

                entity.Property(e => e.IdTipo).HasColumnName("id_tipo");

                entity.HasOne(d => d.IdAgenciaNavigation)
                    .WithMany(p => p.PantallaMuls)
                    .HasForeignKey(d => d.IdAgencia)
                    .HasConstraintName("id_agencia");

                entity.HasOne(d => d.IdMultiNavigation)
                    .WithMany(p => p.PantallaMuls)
                    .HasForeignKey(d => d.IdMulti)
                    .HasConstraintName("id_multi");

                entity.HasOne(d => d.IdTipoNavigation)
                    .WithMany(p => p.PantallaMuls)
                    .HasForeignKey(d => d.IdTipo)
                    .HasConstraintName("id_tipo");
            });

            modelBuilder.Entity<Prioridad>(entity =>
            {
                entity.HasKey(e => e.IdPrioridad)
                    .HasName("prioridad_pkey");

                entity.ToTable("prioridad");

                entity.Property(e => e.IdPrioridad)
                    .HasColumnName("id_prioridad")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Rango).HasColumnName("rango");

                entity.Property(e => e.Tipo)
                    .HasMaxLength(50)
                    .HasColumnName("tipo");
            });

            modelBuilder.Entity<Requisito>(entity =>
            {
                entity.HasKey(e => e.IdRequisitos)
                    .HasName("requisitos_pkey");

                entity.ToTable("requisitos");

                entity.Property(e => e.IdRequisitos)
                    .HasColumnName("id_requisitos")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.NomRequisitos)
                    .HasMaxLength(1000)
                    .HasColumnName("nom_requisitos");
            });

            modelBuilder.Entity<RequisitoTramite>(entity =>
            {
                entity.HasKey(e => e.IdRequitram)
                    .HasName("requisito_tramite_pkey");

                entity.ToTable("requisito_tramite");

                entity.HasIndex(e => e.IdRequisitos, "fki_id_requisitos");

                entity.Property(e => e.IdRequitram)
                    .HasColumnName("id_requitram")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.IdRequisitos).HasColumnName("id_requisitos");

                entity.Property(e => e.IdTramite).HasColumnName("id_tramite");

                entity.HasOne(d => d.IdRequisitosNavigation)
                    .WithMany(p => p.RequisitoTramites)
                    .HasForeignKey(d => d.IdRequisitos)
                    .HasConstraintName("id_requisitos");

                entity.HasOne(d => d.IdTramiteNavigation)
                    .WithMany(p => p.RequisitoTramites)
                    .HasForeignKey(d => d.IdTramite)
                    .HasConstraintName("id_tramite");
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.HasKey(e => e.IdTicket)
                    .HasName("ticket_pkey");

                entity.ToTable("ticket");

                entity.HasIndex(e => e.IdAgencia, "fki_id_agencia");

                entity.HasIndex(e => e.IdUsuario, "fki_id_persona");

                entity.HasIndex(e => e.IdPrioridad, "fki_id_prioridad");

                entity.HasIndex(e => e.IdTramite, "fki_id_tramite");

                entity.HasIndex(e => e.IdVentanilla, "fki_id_ventanilla");

                entity.Property(e => e.IdTicket)
                    .HasColumnName("id_ticket")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Derivacion).HasColumnName("derivacion");

                entity.Property(e => e.DuracionAtencion).HasColumnName("duracion_atencion");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.FechaHora).HasColumnName("fecha_hora");

                entity.Property(e => e.HoraAtencion).HasColumnName("hora_atencion");

                entity.Property(e => e.HoraList).HasColumnName("hora_list");

                entity.Property(e => e.IdAgencia).HasColumnName("id_agencia");

                entity.Property(e => e.IdPrioridad).HasColumnName("id_prioridad");

                entity.Property(e => e.IdTramite).HasColumnName("id_tramite");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.IdVentanilla).HasColumnName("id_ventanilla");

                entity.Property(e => e.NTicket).HasColumnName("n_ticket");

                entity.HasOne(d => d.IdAgenciaNavigation)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.IdAgencia)
                    .HasConstraintName("id_agencia");

                entity.HasOne(d => d.IdPrioridadNavigation)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.IdPrioridad)
                    .HasConstraintName("id_prioridad");

                entity.HasOne(d => d.IdTramiteNavigation)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.IdTramite)
                    .HasConstraintName("id_tramite");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("id_usuario");

                entity.HasOne(d => d.IdVentanillaNavigation)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.IdVentanilla)
                    .HasConstraintName("id_ventanilla");
            });

            modelBuilder.Entity<TipoPantalla>(entity =>
            {
                entity.HasKey(e => e.IdTipo)
                    .HasName("tipo_pantalla_pkey");

                entity.ToTable("tipo_pantalla");

                entity.Property(e => e.IdTipo)
                    .HasColumnName("id_tipo")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.IdAgencia).HasColumnName("id_agencia");

                entity.Property(e => e.NomTipo)
                    .HasMaxLength(50)
                    .HasColumnName("nom_tipo");
            });

            modelBuilder.Entity<TipoPerfil>(entity =>
            {
                entity.HasKey(e => e.IdPerfil)
                    .HasName("tipo_perfil_pkey");

                entity.ToTable("tipo_perfil");

                entity.Property(e => e.IdPerfil)
                    .HasColumnName("id_perfil")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.NomTipoP)
                    .HasMaxLength(30)
                    .HasColumnName("nom_tipo_p");
            });

            modelBuilder.Entity<TraVen>(entity =>
            {
                entity.HasKey(e => e.IdTranVen)
                    .HasName("tra_ven_pkey");

                entity.ToTable("tra_ven");

                entity.Property(e => e.IdTranVen)
                    .HasColumnName("id_tran_ven")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.IdTramite).HasColumnName("id_tramite");

                entity.Property(e => e.IdVentanilla).HasColumnName("id_ventanilla");

                entity.HasOne(d => d.IdTramiteNavigation)
                    .WithMany(p => p.TraVens)
                    .HasForeignKey(d => d.IdTramite)
                    .HasConstraintName("id_tramite");

                entity.HasOne(d => d.IdVentanillaNavigation)
                    .WithMany(p => p.TraVens)
                    .HasForeignKey(d => d.IdVentanilla)
                    .HasConstraintName("id_ventanilla");
            });

            modelBuilder.Entity<Tramite>(entity =>
            {
                entity.HasKey(e => e.IdTramite)
                    .HasName("tramites_pkey");

                entity.ToTable("tramites");

                entity.Property(e => e.IdTramite)
                    .HasColumnName("id_tramite")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.IdAgencia).HasColumnName("id_agencia");

                entity.Property(e => e.NomTramite)
                    .HasMaxLength(50)
                    .HasColumnName("nom_tramite");

                entity.HasOne(d => d.IdAgenciaNavigation)
                    .WithMany(p => p.Tramites)
                    .HasForeignKey(d => d.IdAgencia)
                    .HasConstraintName("id_agencia");
            });

            modelBuilder.Entity<UserTicket>(entity =>
            {
                entity.HasKey(e => e.IdUserTicket)
                    .HasName("user_ticket_pkey");

                entity.ToTable("user_ticket");

                entity.HasIndex(e => e.IdUsuario, "fki_id_usuarios");

                entity.Property(e => e.IdUserTicket)
                    .HasColumnName("id_user_ticket")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.IdTicket).HasColumnName("id_ticket");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.HasOne(d => d.IdTicketNavigation)
                    .WithMany(p => p.UserTickets)
                    .HasForeignKey(d => d.IdTicket)
                    .HasConstraintName("id_ticket");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.UserTickets)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("id_usuarios");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("usuario_pkey");

                entity.ToTable("usuario");

                entity.HasIndex(e => e.IdPerfil, "fki_id_perfil");

                entity.Property(e => e.IdUsuario)
                    .HasColumnName("id_usuario")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.ApeMaterno)
                    .HasMaxLength(50)
                    .HasColumnName("ape_materno");

                entity.Property(e => e.ApePaterno)
                    .HasMaxLength(50)
                    .HasColumnName("ape_paterno");

                entity.Property(e => e.CiUsuario).HasColumnName("ci_usuario");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.FechaCreacion).HasColumnName("fecha_creacion");

                entity.Property(e => e.FotoPerfil)
                    .HasMaxLength(64)
                    .HasColumnName("foto_perfil");

                entity.Property(e => e.IdAgencia).HasColumnName("id_agencia");

                entity.Property(e => e.IdPerfil).HasColumnName("id_perfil");

                entity.Property(e => e.NomUsuario)
                    .HasMaxLength(50)
                    .HasColumnName("nom_usuario");

                entity.Property(e => e.UserName)
                    .HasMaxLength(20)
                    .HasColumnName("user_name");

                entity.Property(e => e.UserPassword)
                    .HasMaxLength(64)
                    .HasColumnName("user_password")
                    .IsFixedLength();

                entity.HasOne(d => d.IdAgenciaNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdAgencia)
                    .HasConstraintName("id_agencia");

                entity.HasOne(d => d.IdPerfilNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdPerfil)
                    .HasConstraintName("id_perfil");
            });

            modelBuilder.Entity<UtTramite>(entity =>
            {
                entity.HasKey(e => e.IdUtTramite)
                    .HasName("ut_tramite_pkey");

                entity.ToTable("ut_tramite");

                entity.Property(e => e.IdUtTramite)
                    .HasColumnName("id_ut_tramite")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.IdTramite).HasColumnName("id_tramite");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.HasOne(d => d.IdTramiteNavigation)
                    .WithMany(p => p.UtTramites)
                    .HasForeignKey(d => d.IdTramite)
                    .HasConstraintName("id_tramite");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.UtTramites)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("id_usuario");
            });

            modelBuilder.Entity<VenUsuario>(entity =>
            {
                entity.HasKey(e => e.IdVenUsuario)
                    .HasName("ven_usuario_pkey");

                entity.ToTable("ven_usuario");

                entity.Property(e => e.IdVenUsuario)
                    .HasColumnName("id_ven_usuario")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.IdVentanilla).HasColumnName("id_ventanilla");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.VenUsuarios)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("id_usuario");

                entity.HasOne(d => d.IdVentanillaNavigation)
                    .WithMany(p => p.VenUsuarios)
                    .HasForeignKey(d => d.IdVentanilla)
                    .HasConstraintName("id_ventanilla");
            });

            modelBuilder.Entity<Ventanilla>(entity =>
            {
                entity.HasKey(e => e.IdVentanilla)
                    .HasName("ventanilla_pkey");

                entity.ToTable("ventanilla");

                entity.Property(e => e.IdVentanilla)
                    .HasColumnName("id_ventanilla")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.EstadoV).HasColumnName("estado_v");

                entity.Property(e => e.IdAgencia).HasColumnName("id_agencia");

                entity.Property(e => e.NomVentanilla)
                    .HasMaxLength(50)
                    .HasColumnName("nom_ventanilla");

                entity.HasOne(d => d.IdAgenciaNavigation)
                    .WithMany(p => p.Ventanillas)
                    .HasForeignKey(d => d.IdAgencia)
                    .HasConstraintName("id_agencia");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

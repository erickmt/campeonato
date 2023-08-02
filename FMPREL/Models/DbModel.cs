namespace FMPREL.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Linq;

    [DbConfigurationType(typeof(MySql.Data.EntityFramework.MySqlEFConfiguration))]
    public class DbModel : DbContext
    {
        public DbModel()
            : base("name=DbModel")
        {
            DbConfiguration.SetConfiguration(new MySql.Data.EntityFramework.MySqlEFConfiguration());
        }

        public virtual DbSet<Gol> Gol { get; set; }
        public virtual DbSet<Jogador> Jogador { get; set; }
        public virtual DbSet<Jogo> Jogo { get; set; }
        public virtual DbSet<MelhorJogador> MelhorJogador { get; set; }
        public virtual DbSet<Rodada> Rodada { get; set; }
        public virtual DbSet<Torneio> Torneio { get; set; }
        public virtual DbSet<Time> Time { get; set; }
        public virtual DbSet<AcessoAsPaginas> AcessoAsPaginas { get; set; }
        public virtual DbSet<CartaoAmarelo> CartaoAmarelo { get; set; }
        public virtual DbSet<CartaoVermelho> CartaoVermelho { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Torneio>()
            .HasMany(x => x.Times)
            .WithRequired(x => x.Torneio)
            .HasForeignKey(x => x.IdTorneio)
            .WillCascadeOnDelete(false);

            modelBuilder.Entity<Torneio>()
            .HasMany(x => x.Rodadas)
            .WithRequired(x => x.Torneio)
            .HasForeignKey(x => x.IdTorneio)
            .WillCascadeOnDelete(false);

            modelBuilder.Entity<Time>()
            .HasMany(x => x.Jogadores)
            .WithRequired(x => x.Time)
            .HasForeignKey(x => x.IdTime)
            .WillCascadeOnDelete(false);

            modelBuilder.Entity<Time>()
            .HasMany(x => x.Gols)
            .WithRequired(x => x.Time)
            .HasForeignKey(x => x.IdTime)
            .WillCascadeOnDelete(false);

            modelBuilder.Entity<Time>()
            .HasMany(x => x.CartoesAmarelos)
            .WithRequired(x => x.Time)
            .HasForeignKey(x => x.IdTime)
            .WillCascadeOnDelete(false);

            modelBuilder.Entity<Time>()
            .HasMany(x => x.CartoesVermelhos)
            .WithRequired(x => x.Time)
            .HasForeignKey(x => x.IdTime)
            .WillCascadeOnDelete(false);

            modelBuilder.Entity<Time>()
            .HasMany(x => x.Jogos)
            .WithMany(y => y.Times)
            .Map(x => x.ToTable("TimesJogos"));

            modelBuilder.Entity<Jogo>()
            .HasMany(x => x.Gols)
            .WithRequired(x => x.Jogo)
            .HasForeignKey(x => x.IdJogo)
            .WillCascadeOnDelete(false);

            modelBuilder.Entity<Jogo>()
            .HasMany(x => x.Melhores)
            .WithRequired(x => x.Jogo)
            .HasForeignKey(x => x.IdJogo)
            .WillCascadeOnDelete(false);

            modelBuilder.Entity<Jogo>()
            .HasMany(x => x.CartoesAmarelos)
            .WithRequired(x => x.Jogo)
            .HasForeignKey(x => x.IdJogo)
            .WillCascadeOnDelete(false);

            modelBuilder.Entity<Jogo>()
            .HasMany(x => x.CartoesVermelhos)
            .WithRequired(x => x.Jogo)
            .HasForeignKey(x => x.IdJogo)
            .WillCascadeOnDelete(false);

            modelBuilder.Entity<Jogador>()
            .HasMany(x => x.Gols)
            .WithRequired(x => x.Jogador)
            .HasForeignKey(x => x.IdJogador)
            .WillCascadeOnDelete(false);

            modelBuilder.Entity<Jogador>()
            .HasMany(x => x.Melhor)
            .WithRequired(x => x.Jogador)
            .HasForeignKey(x => x.IdJogador)
            .WillCascadeOnDelete(false);

            modelBuilder.Entity<Jogador>()
            .HasMany(x => x.CartoesAmarelos)
            .WithRequired(x => x.Jogador)
            .HasForeignKey(x => x.IdJogador)
            .WillCascadeOnDelete(false);

            modelBuilder.Entity<Jogador>()
            .HasMany(x => x.CartoesVermelhos)
            .WithRequired(x => x.Jogador)
            .HasForeignKey(x => x.IdJogador)
            .WillCascadeOnDelete(false);

            modelBuilder.Entity<Rodada>()
              .HasMany(x => x.Jogos)
              .WithRequired(x => x.Rodada)
              .HasForeignKey(x => x.IdRodada)
              .WillCascadeOnDelete(false);

        }
    }

}
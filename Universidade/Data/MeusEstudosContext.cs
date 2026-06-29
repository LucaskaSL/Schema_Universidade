using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Universidade.Entities;
using Universidade.Models.Enums;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Universidade.Data;

public partial class MeusEstudosContext : DbContext
{
    public MeusEstudosContext()
    {
    }

    public MeusEstudosContext(DbContextOptions<MeusEstudosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Alocacao> Alocacaos { get; set; }

    public virtual DbSet<Cursa> Cursas { get; set; }

    public virtual DbSet<Curso> Cursos { get; set; }

    public virtual DbSet<Departamento> Departamentos { get; set; }

    public virtual DbSet<Disciplina> Disciplinas { get; set; }

    public virtual DbSet<Estudante> Estudantes { get; set; }

    public virtual DbSet<Horario> Horarios { get; set; }

    public virtual DbSet<Plano> Planos { get; set; }

    public virtual DbSet<Professor> Professors { get; set; }

    public virtual DbSet<Projeto> Projetos { get; set; }

    public virtual DbSet<Sala> Salas { get; set; }

    public virtual DbSet<Semestre> Semestres { get; set; }

    public virtual DbSet<Turma> Turmas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Vinculo> Vinculos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=meus_estudos;Username=admin;Password=senhasecreta");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Conversores para os enums que possuem espaço
        var grauConverter = new ValueConverter<tipo_grau, string>(
               v => v == tipo_grau.LicenciaturaPlena ? "Licenciatura Plena" : v.ToString(),
               v => v == "Licenciatura Plena" ? tipo_grau.LicenciaturaPlena : Enum.Parse<tipo_grau>(v)
           );

           var turnoConverter = new ValueConverter<tipo_turno, string>(
               v => v == tipo_turno.TurnoIndefinido ? "Turno Indefinido" : v.ToString(),
               v => v == "Turno Indefinido" ? tipo_turno.TurnoIndefinido : Enum.Parse<tipo_turno>(v)
           );
        
        modelBuilder.Entity<Alocacao>(entity =>
        {
            entity.HasKey(e => new { e.IdTurma, e.IdHorario }).HasName("pk_alocacao");

            entity.ToTable("alocacao", "universidade");

            entity.HasIndex(e => new { e.IdHorario, e.IdSala }, "uq_alocacao").IsUnique();

            entity.Property(e => e.IdTurma).HasColumnName("id_turma");
            entity.Property(e => e.IdHorario).HasColumnName("id_horario");
            entity.Property(e => e.IdSala).HasColumnName("id_sala");
        });

        modelBuilder.Entity<Cursa>(entity =>
        {
            entity.HasKey(e => new { e.MatEstudante, e.IdTurma }).HasName("pk_cursa");

            entity.ToTable("cursa", "universidade");

            entity.Property(e => e.MatEstudante)
                .HasMaxLength(7)
                .HasColumnName("mat_estudante");
            entity.Property(e => e.IdTurma).HasColumnName("id_turma");
            entity.Property(e => e.Nota).HasColumnName("nota");

            entity.HasOne(d => d.IdTurmaNavigation).WithMany(p => p.Cursas)
                .HasForeignKey(d => d.IdTurma)
                .HasConstraintName("fk_turma");

            entity.HasOne(d => d.MatEstudanteNavigation).WithMany(p => p.Cursas)
                .HasForeignKey(d => d.MatEstudante)
                .HasConstraintName("fk_estudante");
        });

        modelBuilder.Entity<Curso>(entity =>
        {
            entity.HasIndex(e => new { e.Nome, e.TipoTurno, e.Campus, e.TipoNivel });
                
            entity.HasKey(e => e.Idcurso).HasName("curso_pkey");
            entity.ToTable("curso", "universidade");

            entity.Property(e => e.Idcurso)
                .HasColumnName("idcurso")
                .ValueGeneratedOnAdd(); // Importante para respeitar o id SERIAL no SQL
            
            entity.Property(e => e.Campus).HasMaxLength(100).HasColumnName("campus");
            entity.Property(e => e.Nome).HasMaxLength(100).HasColumnName("nome");

            entity.Property(e => e.TipoGrau).HasConversion(grauConverter).HasColumnName("grau"); // Momento da conversão
            entity.Property(e => e.TipoTurno).HasConversion(turnoConverter).HasColumnName("turno"); // Momento da conversão
            entity.Property(e => e.TipoNivel).HasConversion<string>().HasColumnName("nivel");
        });

        modelBuilder.Entity<Departamento>(entity =>
        {
            entity.HasKey(e => e.CodDepto).HasName("pk_departamento");

            entity.ToTable("departamento", "universidade");

            entity.Property(e => e.CodDepto)
                .HasMaxLength(5)
                .HasColumnName("cod_depto");
            entity.Property(e => e.Chefe)
                .HasMaxLength(7)
                .HasColumnName("chefe");
            entity.Property(e => e.Comissal).HasColumnName("comissal");
            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .HasColumnName("nome");
            entity.Property(e => e.Orcamento).HasColumnName("orcamento");

            entity.HasOne(d => d.ChefeNavigation).WithMany(p => p.Departamentos)
                .HasForeignKey(d => d.Chefe)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_chefia");
        });

        modelBuilder.Entity<Disciplina>(entity =>
        {
            entity.HasKey(e => e.CodDisc).HasName("pk_disciplina");

            entity.ToTable("disciplina", "universidade");

            entity.Property(e => e.CodDisc)
                .HasMaxLength(8)
                .HasColumnName("cod_disc");
            entity.Property(e => e.Creditos).HasColumnName("creditos");
            entity.Property(e => e.DeptoResponsavel)
                .HasMaxLength(5)
                .HasColumnName("depto_responsavel");
            entity.Property(e => e.Nome)
                .HasMaxLength(40)
                .HasColumnName("nome");
            entity.Property(e => e.PreReq)
                .HasMaxLength(8)
                .HasColumnName("pre_req");

            entity.HasOne(d => d.DeptoResponsavelNavigation).WithMany(p => p.Disciplinas)
                .HasForeignKey(d => d.DeptoResponsavel)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_responsavel");

            entity.HasOne(d => d.PreReqNavigation).WithMany(p => p.InversePreReqNavigation)
                .HasForeignKey(d => d.PreReq)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_pre_req");
        });

        modelBuilder.Entity<Estudante>(entity =>
        {
            entity.HasKey(e => e.MatEstudante).HasName("pk_estudante");

            entity.ToTable("estudante", "universidade");

            entity.HasIndex(e => e.Cpf, "uq_cpf").IsUnique();

            entity.Property(e => e.MatEstudante)
                .HasMaxLength(7)
                .HasColumnName("mat_estudante");
            entity.Property(e => e.AnoIngresso).HasColumnName("ano_ingresso");
            entity.Property(e => e.Cpf)
                .HasPrecision(13)
                .HasColumnName("cpf");
            entity.Property(e => e.Mc)
                .HasPrecision(2)
                .HasColumnName("mc");

            entity.HasOne(d => d.CpfNavigation).WithOne(p => p.Estudante)
                .HasForeignKey<Estudante>(d => d.Cpf)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_usuario");
        });

        modelBuilder.Entity<Horario>(entity =>
        {
            entity.HasKey(e => e.IdHorario).HasName("pk_horario");

            entity.ToTable("horario", "universidade");

            entity.Property(e => e.IdHorario).HasColumnName("id_horario");
            entity.Property(e => e.Dia)
                .HasMaxLength(15)
                .HasColumnName("dia");
            entity.Property(e => e.Slot).HasColumnName("slot");
        });

        modelBuilder.Entity<Plano>(entity =>
        {
            entity.HasKey(e => new { e.MatEstudante, e.Ano }).HasName("pk_plano");

            entity.ToTable("plano", "universidade");

            entity.Property(e => e.MatEstudante)
                .HasMaxLength(7)
                .HasColumnName("mat_estudante");
            entity.Property(e => e.Ano).HasColumnName("ano");
            entity.Property(e => e.IdProjeto).HasColumnName("id_projeto");
            entity.Property(e => e.MatProfessor)
                .HasMaxLength(7)
                .HasColumnName("mat_professor");

            entity.HasOne(d => d.IdProjetoNavigation).WithMany(p => p.Planos)
                .HasForeignKey(d => d.IdProjeto)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_projeto");

            entity.HasOne(d => d.MatEstudanteNavigation).WithMany(p => p.Planos)
                .HasForeignKey(d => d.MatEstudante)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_mat_estudante");

            entity.HasOne(d => d.MatProfessorNavigation).WithMany(p => p.Planos)
                .HasForeignKey(d => d.MatProfessor)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_professor");
        });

        modelBuilder.Entity<Professor>(entity =>
        {
            entity.HasKey(e => e.MatProfessor).HasName("pk_professor");

            entity.ToTable("professor", "universidade");

            entity.HasIndex(e => e.Cpf, "professor_cpf_key").IsUnique();

            entity.Property(e => e.MatProfessor)
                .HasMaxLength(7)
                .HasColumnName("mat_professor");
            entity.Property(e => e.Cpf)
                .HasPrecision(13)
                .HasColumnName("cpf");
            entity.Property(e => e.DataAdmissao).HasColumnName("data_admissao");
            entity.Property(e => e.Departamento)
                .HasMaxLength(5)
                .HasColumnName("departamento");
            entity.Property(e => e.Salario).HasColumnName("salario");

            entity.HasOne(d => d.CpfNavigation).WithOne(p => p.Professor)
                .HasForeignKey<Professor>(d => d.Cpf)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_usuario");

            entity.HasOne(d => d.DepartamentoNavigation).WithMany(p => p.Professors)
                .HasForeignKey(d => d.Departamento)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_alocacao");
        });

        modelBuilder.Entity<Projeto>(entity =>
        {
            entity.HasKey(e => e.IdProjeto).HasName("pk_projeto");

            entity.ToTable("projeto", "universidade");

            entity.Property(e => e.IdProjeto)
                .ValueGeneratedNever()
                .HasColumnName("id_projeto");
            entity.Property(e => e.Descricao)
                .HasColumnType("character varying")
                .HasColumnName("descricao");
        });

        modelBuilder.Entity<Sala>(entity =>
        {
            entity.HasKey(e => e.IdSala).HasName("pk_sala");

            entity.ToTable("sala", "universidade");

            entity.Property(e => e.IdSala).HasColumnName("id_sala");
            entity.Property(e => e.Descricao)
                .HasColumnType("character varying")
                .HasColumnName("descricao");
        });

        modelBuilder.Entity<Semestre>(entity =>
        {
            entity.HasKey(e => new { e.Ano, e.Semestre1 }).HasName("pk_semestre");

            entity.ToTable("semestre", "universidade");

            entity.Property(e => e.Ano).HasColumnName("ano");
            entity.Property(e => e.Semestre1).HasColumnName("semestre");
            entity.Property(e => e.DataFom).HasColumnName("data_fom");
            entity.Property(e => e.DataInicio).HasColumnName("data_inicio");
        });

        modelBuilder.Entity<Turma>(entity =>
        {
            entity.HasKey(e => e.IdTurma).HasName("pk_turma");

            entity.ToTable("turma", "universidade");

            entity.HasIndex(e => new { e.CodDisc, e.Numero, e.Semestre, e.Ano }, "uq_turma").IsUnique();

            entity.Property(e => e.IdTurma)
                .HasDefaultValueSql("nextval('universidade.seq_turma'::regclass)")
                .HasColumnName("id_turma");
            entity.Property(e => e.Ano).HasColumnName("ano");
            entity.Property(e => e.CodDisc)
                .HasMaxLength(8)
                .HasColumnName("cod_disc");
            entity.Property(e => e.Numero).HasColumnName("numero");
            entity.Property(e => e.Semestre).HasColumnName("semestre");

            entity.HasOne(d => d.CodDiscNavigation).WithMany(p => p.Turmas)
                .HasForeignKey(d => d.CodDisc)
                .HasConstraintName("fk_disciplina");

            entity.HasOne(d => d.SemestreNavigation).WithMany(p => p.Turmas)
                .HasForeignKey(d => new { d.Ano, d.Semestre })
                .HasConstraintName("fk_semestre");

            entity.HasMany(d => d.MatProfessors).WithMany(p => p.IdTurmas)
                .UsingEntity<Dictionary<string, object>>(
                    "Leciona",
                    r => r.HasOne<Professor>().WithMany()
                        .HasForeignKey("MatProfessor")
                        .HasConstraintName("fk_professor"),
                    l => l.HasOne<Turma>().WithMany()
                        .HasForeignKey("IdTurma")
                        .HasConstraintName("fk_turma"),
                    j =>
                    {
                        j.HasKey("IdTurma", "MatProfessor").HasName("pk_leciona");
                        j.ToTable("leciona", "universidade");
                        j.IndexerProperty<int>("IdTurma").HasColumnName("id_turma");
                        j.IndexerProperty<string>("MatProfessor")
                            .HasMaxLength(7)
                            .HasColumnName("mat_professor");
                    });
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Cpf).HasName("pk_usuario");

            entity.ToTable("usuario", "universidade");

            entity.HasIndex(e => e.Login, "usuario_login_key").IsUnique();

            entity.Property(e => e.Cpf)
                .HasPrecision(13)
                .HasColumnName("cpf");
            entity.Property(e => e.DataNascimento).HasColumnName("data_nascimento");
            entity.Property(e => e.Email)
                .HasColumnType("character varying[]")
                .HasColumnName("email");
            entity.Property(e => e.Login)
                .HasMaxLength(45)
                .HasColumnName("login");
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .HasColumnName("nome");
            entity.Property(e => e.Senha)
                .HasMaxLength(32)
                .HasColumnName("senha");
            entity.Property(e => e.Telefone)
                .HasColumnType("character varying[]")
                .HasColumnName("telefone");
        });

        modelBuilder.Entity<Vinculo>(entity =>
        {
            entity.HasKey(e => e.Idvinculo).HasName("vinculo_pkey");

            entity.ToTable("vinculo", "universidade");

            entity.Property(e => e.Idvinculo)
                .HasColumnName("idvinculo")
                .ValueGeneratedOnAdd(); // Importante para respeitar o id SERIAL no SQL
            
            entity.Property(e => e.Curso).HasColumnName("curso");
            entity.Property(e => e.DataEntrada).HasColumnName("data_entrada");
            entity.Property(e => e.DataSaida).HasColumnName("data_saida");
            entity.Property(e => e.MatEstudante)
                .HasMaxLength(7)
                .HasColumnName("mat_estudante");
            
            entity.Property(e => e.StatusEstudante)
                .HasConversion<string>()
                .HasColumnName("status");

            entity.HasOne(d => d.CursoNavigation).WithMany(p => p.Vinculos)
                .HasForeignKey(d => d.Curso)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_curso");

            entity.HasOne(d => d.MatEstudanteNavigation).WithMany(p => p.Vinculos)
                .HasForeignKey(d => d.MatEstudante)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_estudabte");
        });
        modelBuilder.HasSequence("seq_turma", "universidade");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

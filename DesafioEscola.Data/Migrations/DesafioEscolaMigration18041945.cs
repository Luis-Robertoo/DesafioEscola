using FluentMigrator;

namespace DesafioEscola.Data.Migrations;

[Migration(18041945)]
public class DesafioEscolaMigration18041945 : Migration
{
    public override void Up()
    {
        Create.Table("Aluno")
            .WithColumn("Id").AsInt64().PrimaryKey().Identity()
            .WithColumn("Nome").AsString(255).NotNullable()
            .WithColumn("Usuario").AsString(45).NotNullable()
            .WithColumn("Senha").AsString(70).NotNullable()
            .WithColumn("Ativo").AsBoolean().NotNullable();

        Create.Table("Turma")
            .WithColumn("Id").AsInt64().PrimaryKey().Identity()
            .WithColumn("Curso_Id").AsInt32().NotNullable()
            .WithColumn("Nome").AsString(45).NotNullable()
            .WithColumn("Ano").AsInt16().NotNullable()
            .WithColumn("Ativo").AsBoolean().NotNullable();

        Create.Table("Aluno_Turma")
            .WithColumn("Id").AsInt64().Identity()
            .WithColumn("Aluno_Id").AsInt64().NotNullable()
            .WithColumn("Turma_Id").AsInt64().NotNullable()
            .WithColumn("Ativo").AsBoolean().NotNullable();

        Insert.IntoTable("Aluno").Row(new
        {
            Nome = "Administrador",
            Usuario = "ADMIN",
            Senha = "5395546DFB06C37BC99889ACCE742B0BD5E1D48916AD4C9AFA120A884C43966A",
            Ativo = 1
        });


        var compKey = new[] { "Aluno_Id", "Turma_Id" };
        Create.PrimaryKey("PK_Aluno_Turma").OnTable("Aluno_Turma").Columns(compKey);

        Create.ForeignKey("FK_Aluno_Turma_Aluno").FromTable("Aluno_Turma").ForeignColumn("Aluno_Id").ToTable("Aluno").PrimaryColumn("Id");
        Create.ForeignKey("FK_Aluno_Turma_Turma").FromTable("Aluno_Turma").ForeignColumn("Turma_Id").ToTable("Turma").PrimaryColumn("Id");
    }

    public override void Down()
    {
        Delete.Table("Aluno");
        Delete.Table("Turma");

        Delete.ForeignKey("FK_Aluno_Turma_Aluno").OnTable("Aluno_Turma");
        Delete.ForeignKey("FK_Aluno_Turma_Turma").OnTable("Aluno_Turma");
        Delete.Table("Aluno_Turma");
    }
}

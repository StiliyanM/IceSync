using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IceSync.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveWorkflowFromWorkflowColumnNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WorkflowName",
                table: "Workflows");

            migrationBuilder.RenameColumn(
                name: "WorkflowId",
                table: "Workflows",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "MultiExecBehavior",
                table: "Workflows",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Workflows",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Workflows");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Workflows",
                newName: "WorkflowId");

            migrationBuilder.AlterColumn<string>(
                name: "MultiExecBehavior",
                table: "Workflows",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkflowName",
                table: "Workflows",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}

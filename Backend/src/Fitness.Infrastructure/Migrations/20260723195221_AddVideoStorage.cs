using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fitness.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddVideoStorage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VideoUrl",
                table: "ProgramVideos");

            migrationBuilder.DropColumn(
                name: "IconUrl",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Categories",
                newName: "Slug");

            migrationBuilder.RenameColumn(
                name: "ThumbnailUrl",
                table: "Categories",
                newName: "ImageUrl");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ProgramVideos",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Difficulty",
                table: "ProgramVideos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DownloadUrl",
                table: "ProgramVideos",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EstimatedCalories",
                table: "ProgramVideos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "HasMultiAudio",
                table: "ProgramVideos",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasSubtitle",
                table: "ProgramVideos",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFree",
                table: "ProgramVideos",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPublished",
                table: "ProgramVideos",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "VideoStorageId",
                table: "ProgramVideos",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SubscriptionPlanId",
                table: "Payments",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "DisplayOrder",
                table: "Categories",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsPublished",
                table: "Categories",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Categories",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "VideoAudioTrack",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProgramVideoId = table.Column<Guid>(type: "uuid", nullable: false),
                    Language = table.Column<string>(type: "text", nullable: false),
                    AudioUrl = table.Column<string>(type: "text", nullable: false),
                    IsDefault = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoAudioTrack", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VideoAudioTrack_ProgramVideos_ProgramVideoId",
                        column: x => x.ProgramVideoId,
                        principalTable: "ProgramVideos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VideoStorages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StorageProvider = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Bucket = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Region = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    FileKey = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    OriginalFileName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ContentType = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    Checksum = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    DurationSeconds = table.Column<int>(type: "integer", nullable: false),
                    Width = table.Column<int>(type: "integer", nullable: false),
                    Height = table.Column<int>(type: "integer", nullable: false),
                    Bitrate = table.Column<int>(type: "integer", nullable: false),
                    ThumbnailUrl = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    CdnUrl = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoStorages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VideoSubtitle",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProgramVideoId = table.Column<Guid>(type: "uuid", nullable: false),
                    Language = table.Column<string>(type: "text", nullable: false),
                    SubtitleUrl = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoSubtitle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VideoSubtitle_ProgramVideos_ProgramVideoId",
                        column: x => x.ProgramVideoId,
                        principalTable: "ProgramVideos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProgramVideos_VideoStorageId",
                table: "ProgramVideos",
                column: "VideoStorageId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_SubscriptionPlanId",
                table: "Payments",
                column: "SubscriptionPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoAudioTrack_ProgramVideoId",
                table: "VideoAudioTrack",
                column: "ProgramVideoId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoStorages_Checksum",
                table: "VideoStorages",
                column: "Checksum");

            migrationBuilder.CreateIndex(
                name: "IX_VideoStorages_FileKey",
                table: "VideoStorages",
                column: "FileKey");

            migrationBuilder.CreateIndex(
                name: "IX_VideoStorages_Status",
                table: "VideoStorages",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_VideoStorages_StorageProvider_Bucket",
                table: "VideoStorages",
                columns: new[] { "StorageProvider", "Bucket" });

            migrationBuilder.CreateIndex(
                name: "IX_VideoSubtitle_ProgramVideoId",
                table: "VideoSubtitle",
                column: "ProgramVideoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_SubscriptionPlans_SubscriptionPlanId",
                table: "Payments",
                column: "SubscriptionPlanId",
                principalTable: "SubscriptionPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramVideos_VideoStorages_VideoStorageId",
                table: "ProgramVideos",
                column: "VideoStorageId",
                principalTable: "VideoStorages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_SubscriptionPlans_SubscriptionPlanId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_ProgramVideos_VideoStorages_VideoStorageId",
                table: "ProgramVideos");

            migrationBuilder.DropTable(
                name: "VideoAudioTrack");

            migrationBuilder.DropTable(
                name: "VideoStorages");

            migrationBuilder.DropTable(
                name: "VideoSubtitle");

            migrationBuilder.DropIndex(
                name: "IX_ProgramVideos_VideoStorageId",
                table: "ProgramVideos");

            migrationBuilder.DropIndex(
                name: "IX_Payments_SubscriptionPlanId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "ProgramVideos");

            migrationBuilder.DropColumn(
                name: "Difficulty",
                table: "ProgramVideos");

            migrationBuilder.DropColumn(
                name: "DownloadUrl",
                table: "ProgramVideos");

            migrationBuilder.DropColumn(
                name: "EstimatedCalories",
                table: "ProgramVideos");

            migrationBuilder.DropColumn(
                name: "HasMultiAudio",
                table: "ProgramVideos");

            migrationBuilder.DropColumn(
                name: "HasSubtitle",
                table: "ProgramVideos");

            migrationBuilder.DropColumn(
                name: "IsFree",
                table: "ProgramVideos");

            migrationBuilder.DropColumn(
                name: "IsPublished",
                table: "ProgramVideos");

            migrationBuilder.DropColumn(
                name: "VideoStorageId",
                table: "ProgramVideos");

            migrationBuilder.DropColumn(
                name: "SubscriptionPlanId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "DisplayOrder",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "IsPublished",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "Slug",
                table: "Categories",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Categories",
                newName: "ThumbnailUrl");

            migrationBuilder.AddColumn<string>(
                name: "VideoUrl",
                table: "ProgramVideos",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IconUrl",
                table: "Categories",
                type: "text",
                nullable: true);
        }
    }
}

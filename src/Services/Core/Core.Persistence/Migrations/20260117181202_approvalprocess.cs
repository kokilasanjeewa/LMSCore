using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class approvalprocess : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          /*  migrationBuilder.EnsureSchema(
                name: "Core");

            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateSequence<int>(
                name: "BNKBRID",
                schema: "dbo");

            migrationBuilder.CreateSequence<int>(
                name: "BNKID",
                schema: "dbo");

            migrationBuilder.CreateSequence<int>(
                name: "CNTRYID",
                schema: "dbo");

            migrationBuilder.CreateSequence<int>(
                name: "COMID",
                schema: "dbo");

            migrationBuilder.CreateSequence<int>(
                name: "CURNCYID",
                schema: "dbo");

            migrationBuilder.CreateSequence<int>(
                name: "DEPTID",
                schema: "dbo");

            migrationBuilder.CreateSequence<int>(
                name: "GrpComID",
                schema: "dbo");

            migrationBuilder.CreateSequence<int>(
                name: "GrpID",
                schema: "dbo");

            migrationBuilder.CreateSequence<int>(
                name: "GrpMnuID",
                schema: "dbo");

            migrationBuilder.CreateSequence<int>(
                name: "MODID",
                schema: "dbo");

            migrationBuilder.CreateSequence<int>(
                name: "RTID",
                schema: "dbo");

            migrationBuilder.CreateSequence<int>(
                name: "SECTID",
                schema: "dbo");

            migrationBuilder.CreateSequence<int>(
                name: "TheNumberID",
                schema: "dbo");

            migrationBuilder.CreateSequence<int>(
                name: "UserComID",
                schema: "dbo");

            migrationBuilder.CreateSequence<int>(
                name: "UserMnuPermsID",
                schema: "dbo");
          */
            migrationBuilder.CreateTable(
                name: "ApprovalActionHistory",
                schema: "Core",
                columns: table => new
                {
                    ApprovalActionID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApprovalRequestID = table.Column<int>(type: "int", nullable: false),
                    ApprovalStepID = table.Column<int>(type: "int", nullable: false),
                    Action = table.Column<byte>(type: "tinyint", nullable: false),
                    ActionByUserSerialID = table.Column<int>(type: "int", nullable: false),
                    ActionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    PreviousState = table.Column<byte>(type: "tinyint", nullable: false),
                    NewState = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovalActionHistory", x => x.ApprovalActionID);
                });
            /*
            migrationBuilder.CreateTable(
                name: "AuditTrail",
                schema: "Core",
                columns: table => new
                {
                    AudtTralID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuditDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserSerialID = table.Column<int>(type: "int", nullable: false),
                    MnuSerialID = table.Column<int>(type: "int", nullable: false),
                    LoginLogSerialID = table.Column<long>(type: "bigint", nullable: false),
                    TableName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AuditData = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MachineName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Action = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditTrail", x => x.AudtTralID);
                });

            migrationBuilder.CreateTable(
                name: "City",
                schema: "Core",
                columns: table => new
                {
                    CitySerialID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StateSerialID = table.Column<int>(type: "int", nullable: false),
                    StateCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StateName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CntrySerialID = table.Column<int>(type: "int", nullable: false),
                    CountryCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Level = table.Column<int>(type: "int", nullable: true),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    Latitude = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Longitude = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Native = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Population = table.Column<long>(type: "bigint", nullable: true),
                    Timezone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Translations = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Flag = table.Column<bool>(type: "bit", nullable: false),
                    WikiDataId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.CitySerialID);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                schema: "Core",
                columns: table => new
                {
                    CntrySerialID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CntryID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    TwoLetterIsoCode = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    ThreeLetterIsoCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    FlagUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Flag = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: true),
                    CntryCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    NumericCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Capital = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrencyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrencySymbol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tld = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Native = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Region = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegionSerialID = table.Column<int>(type: "int", nullable: false),
                    Subregion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubregionSerialID = table.Column<int>(type: "int", nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AreaSqKm = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PostalCodeFormat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCodeRegex = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Translations = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timezones = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Latitude = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Longitude = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Emoji = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmojiU = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WikiDataId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.CntrySerialID);
                });

            migrationBuilder.CreateTable(
                name: "Currency",
                schema: "Core",
                columns: table => new
                {
                    CurSerialID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurID = table.Column<int>(type: "int", nullable: false),
                    CurNmame = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CurCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currency", x => x.CurSerialID);
                });

            migrationBuilder.CreateTable(
                name: "DelRecord",
                schema: "Core",
                columns: table => new
                {
                    DelRecSerialID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocTable = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DocSerialID = table.Column<int>(type: "int", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DelRecord", x => x.DelRecSerialID);
                });

            migrationBuilder.CreateTable(
                name: "EntityType",
                schema: "Core",
                columns: table => new
                {
                    EntityTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EntityName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityType", x => x.EntityTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Group",
                schema: "Core",
                columns: table => new
                {
                    GrpSerialID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GrpID = table.Column<int>(type: "int", nullable: false, defaultValueSql: "SELECT NEXT VALUE FOR dbo.GrpID"),
                    GropName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Group", x => x.GrpSerialID);
                });

            migrationBuilder.CreateTable(
                name: "InvalidateToken",
                schema: "Core",
                columns: table => new
                {
                    InvdTokenID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpirationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvalidateToken", x => x.InvdTokenID);
                });

            migrationBuilder.CreateTable(
                name: "Module",
                schema: "Core",
                columns: table => new
                {
                    ModSerialID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModID = table.Column<int>(type: "int", nullable: false),
                    ModName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Module", x => x.ModSerialID);
                });

            migrationBuilder.CreateTable(
                name: "Region",
                schema: "Core",
                columns: table => new
                {
                    RegionSerialID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Translations = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Flag = table.Column<bool>(type: "bit", nullable: false),
                    WikiDataId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Region", x => x.RegionSerialID);
                });

            migrationBuilder.CreateTable(
                name: "State",
                schema: "Core",
                columns: table => new
                {
                    StateSerialID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CntrySerialID = table.Column<int>(type: "int", nullable: false),
                    CountryCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FipsCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TwoLetterIsoCode = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
                    ThreeLetterIsoCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Level = table.Column<int>(type: "int", nullable: true),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    Native = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Latitude = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Longitude = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Timezone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Translations = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Flag = table.Column<bool>(type: "bit", nullable: false),
                    WikiDataId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Population = table.Column<long>(type: "bigint", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_State", x => x.StateSerialID);
                });

            migrationBuilder.CreateTable(
                name: "Subregion",
                schema: "Core",
                columns: table => new
                {
                    SubregionSerialID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Translations = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegionSerialID = table.Column<int>(type: "int", nullable: false),
                    Flag = table.Column<bool>(type: "bit", nullable: false),
                    WikiDataId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subregion", x => x.SubregionSerialID);
                });

            migrationBuilder.CreateTable(
                name: "TheNumbers",
                schema: "Core",
                columns: table => new
                {
                    TheNumberSerialID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TheNumberID = table.Column<int>(type: "int", nullable: false, defaultValueSql: "SELECT NEXT VALUE FOR dbo.TheNumberID"),
                    TheNumberName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    ComSerialID = table.Column<int>(type: "int", nullable: true),
                    LastNumber = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TheNumbers", x => x.TheNumberSerialID);
                });

            migrationBuilder.CreateTable(
                name: "Company",
                schema: "Core",
                columns: table => new
                {
                    ComSerialID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComID = table.Column<int>(type: "int", nullable: false, defaultValueSql: "SELECT NEXT VALUE FOR dbo.COMID"),
                    CompanyName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    ComCode = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    CntrySerialID = table.Column<int>(type: "int", nullable: false),
                    Address1 = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Address2 = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Address3 = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    BR = table.Column<string>(name: "BR#", type: "nvarchar(12)", maxLength: 12, nullable: false),
                    Telephone1 = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Telephone2 = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Telephone3 = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Mobile1 = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Mobile2 = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Fax = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: true),
                    WebSite = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    RegAdrs1 = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    RegAdrs2 = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    RegAdrs3 = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    VAT = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SVAT = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    NBT = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CompanyLogoUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    SmallComLogoUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    SOPayrollPeriodStartDay = table.Column<byte>(type: "tinyint", nullable: true),
                    SOPayrollPeriodEndDay = table.Column<byte>(type: "tinyint", nullable: true),
                    WBPayrollPeriodStartDay = table.Column<byte>(type: "tinyint", nullable: true),
                    WBPayrollPeriodEndDay = table.Column<byte>(type: "tinyint", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.ComSerialID);
                    table.ForeignKey(
                        name: "FK_Company_Country_CntrySerialID",
                        column: x => x.CntrySerialID,
                        principalSchema: "Core",
                        principalTable: "Country",
                        principalColumn: "CntrySerialID",
                        onDelete: ReferentialAction.Cascade);
                });
            */
            migrationBuilder.CreateTable(
                name: "ApprovalWorkflow",
                schema: "Core",
                columns: table => new
                {
                    ApprovalWorkflowID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityTypeID = table.Column<int>(type: "int", nullable: false),
                    ComSerialID = table.Column<int>(type: "int", nullable: false),
                    WorkflowCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    WorkflowName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovalWorkflow", x => x.ApprovalWorkflowID);
                    table.ForeignKey(
                        name: "FK_ApprovalWorkflow_EntityType_EntityTypeID",
                        column: x => x.EntityTypeID,
                        principalSchema: "Core",
                        principalTable: "EntityType",
                        principalColumn: "EntityTypeID",
                        onDelete: ReferentialAction.Restrict);
                });
            /*
            migrationBuilder.CreateTable(
                name: "User",
                schema: "Core",
                columns: table => new
                {
                    PasswdSalt = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PasswdHash = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserSerialID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EESerialID = table.Column<long>(type: "bigint", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PermissionType = table.Column<int>(type: "int", nullable: false),
                    GrpSerialID = table.Column<int>(type: "int", nullable: true),
                    LastIp = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    LastSessionId = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    LastToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsLogOut = table.Column<bool>(type: "bit", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserSerialID);
                    table.ForeignKey(
                        name: "FK_User_Group_GrpSerialID",
                        column: x => x.GrpSerialID,
                        principalSchema: "Core",
                        principalTable: "Group",
                        principalColumn: "GrpSerialID");
                });

            migrationBuilder.CreateTable(
                name: "DocType",
                schema: "Core",
                columns: table => new
                {
                    DocTypeSerialID = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModSerialID = table.Column<int>(type: "int", nullable: false),
                    DocumentType = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocType", x => x.DocTypeSerialID);
                    table.ForeignKey(
                        name: "FK_DocType_Module_ModSerialID",
                        column: x => x.ModSerialID,
                        principalSchema: "Core",
                        principalTable: "Module",
                        principalColumn: "ModSerialID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Menu",
                schema: "Core",
                columns: table => new
                {
                    MnuSerialID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MnuID = table.Column<int>(type: "int", nullable: false),
                    ModSerialID = table.Column<int>(type: "int", nullable: false),
                    MnuLevel = table.Column<byte>(type: "tinyint", nullable: false),
                    MnuPosition = table.Column<decimal>(type: "decimal(8,6)", nullable: false),
                    MnuName = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    MnuText = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    PageName = table.Column<string>(type: "varchar(35)", maxLength: 35, nullable: false),
                    IsShown = table.Column<bool>(type: "bit", nullable: false),
                    ParentID = table.Column<int>(type: "int", nullable: true),
                    IsRptInc = table.Column<bool>(type: "bit", nullable: false),
                    IsBtnInc = table.Column<bool>(type: "bit", nullable: false),
                    MnuMTBR = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.MnuSerialID);
                    table.ForeignKey(
                        name: "FK_Menu_Module_ModSerialID",
                        column: x => x.ModSerialID,
                        principalSchema: "Core",
                        principalTable: "Module",
                        principalColumn: "ModSerialID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reason",
                schema: "Core",
                columns: table => new
                {
                    ReasonSerialID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModSerialID = table.Column<int>(type: "int", nullable: false),
                    Document = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    ReasonText = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reason", x => x.ReasonSerialID);
                    table.ForeignKey(
                        name: "FK_Reason_Module_ModSerialID",
                        column: x => x.ModSerialID,
                        principalSchema: "Core",
                        principalTable: "Module",
                        principalColumn: "ModSerialID",
                        onDelete: ReferentialAction.Cascade);
                });
            */
            migrationBuilder.CreateTable(
                name: "ApprovalRequest",
                schema: "Core",
                columns: table => new
                {
                    ApprovalRequestID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityTypeID = table.Column<int>(type: "int", nullable: false),
                    EntityID = table.Column<int>(type: "int", nullable: false),
                    ComSerialID = table.Column<int>(type: "int", nullable: false),
                    PlantID = table.Column<int>(type: "int", nullable: true),
                    ApprovalWorkflowID = table.Column<int>(type: "int", nullable: false),
                    CurrentStepOrder = table.Column<int>(type: "int", nullable: false),
                    CurrentState = table.Column<byte>(type: "tinyint", nullable: false),
                    RequestedByUserSerialID = table.Column<int>(type: "int", nullable: false),
                    RequestedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovalRequest", x => x.ApprovalRequestID);
                    table.ForeignKey(
                        name: "FK_ApprovalRequest_ApprovalWorkflow_ApprovalWorkflowID",
                        column: x => x.ApprovalWorkflowID,
                        principalSchema: "Core",
                        principalTable: "ApprovalWorkflow",
                        principalColumn: "ApprovalWorkflowID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ApprovalStep",
                schema: "Core",
                columns: table => new
                {
                    ApprovalStepID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApprovalWorkflowID = table.Column<int>(type: "int", nullable: false),
                    StepOrder = table.Column<int>(type: "int", nullable: false),
                    StepCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    StepName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ApprovalRole = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PlantID = table.Column<int>(type: "int", nullable: true),
                    IsMandatory = table.Column<bool>(type: "bit", nullable: false),
                    CanReject = table.Column<bool>(type: "bit", nullable: false),
                    IsFinalStep = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovalStep", x => x.ApprovalStepID);
                    table.ForeignKey(
                        name: "FK_ApprovalStep_ApprovalWorkflow_ApprovalWorkflowID",
                        column: x => x.ApprovalWorkflowID,
                        principalSchema: "Core",
                        principalTable: "ApprovalWorkflow",
                        principalColumn: "ApprovalWorkflowID",
                        onDelete: ReferentialAction.Restrict);
                });
            /*
            migrationBuilder.CreateTable(
                name: "LoginLog",
                schema: "Core",
                columns: table => new
                {
                    LoginLogSerialID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserSerialID = table.Column<int>(type: "int", nullable: false),
                    LoginDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IPAddress = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    MachineName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsTimeout = table.Column<bool>(type: "bit", nullable: false),
                    IsSystemLogout = table.Column<bool>(type: "bit", nullable: false),
                    LogoutDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TokenExpire = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginLog", x => x.LoginLogSerialID);
                    table.ForeignKey(
                        name: "FK_LoginLog_User_UserSerialID",
                        column: x => x.UserSerialID,
                        principalSchema: "Core",
                        principalTable: "User",
                        principalColumn: "UserSerialID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                schema: "Core",
                columns: table => new
                {
                    RTSerialID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RTID = table.Column<int>(type: "int", nullable: false, defaultValueSql: "SELECT NEXT VALUE FOR dbo.RTID"),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Expires = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Revoked = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserSerialID = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.RTSerialID);
                    table.ForeignKey(
                        name: "FK_RefreshToken_User_UserSerialID",
                        column: x => x.UserSerialID,
                        principalSchema: "Core",
                        principalTable: "User",
                        principalColumn: "UserSerialID");
                });

            migrationBuilder.CreateTable(
                name: "UserCompany",
                schema: "Core",
                columns: table => new
                {
                    UserComSerialID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserComID = table.Column<int>(type: "int", nullable: false, defaultValueSql: "SELECT NEXT VALUE FOR dbo.UserComID"),
                    UserSerialID = table.Column<int>(type: "int", nullable: false),
                    ComSerialID = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCompany", x => x.UserComSerialID);
                    table.ForeignKey(
                        name: "FK_UserCompany_Company_ComSerialID",
                        column: x => x.ComSerialID,
                        principalSchema: "Core",
                        principalTable: "Company",
                        principalColumn: "ComSerialID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserCompany_User_UserSerialID",
                        column: x => x.UserSerialID,
                        principalSchema: "Core",
                        principalTable: "User",
                        principalColumn: "UserSerialID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocUpload",
                schema: "Core",
                columns: table => new
                {
                    DocUploadSerialID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DocTypeSerialID = table.Column<short>(type: "smallint", nullable: false),
                    DocName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    DocDescription = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    ModuleSerialID = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocUpload", x => x.DocUploadSerialID);
                    table.ForeignKey(
                        name: "FK_DocUpload_DocType_DocTypeSerialID",
                        column: x => x.DocTypeSerialID,
                        principalSchema: "Core",
                        principalTable: "DocType",
                        principalColumn: "DocTypeSerialID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocUpload_Module_ModuleSerialID",
                        column: x => x.ModuleSerialID,
                        principalSchema: "Core",
                        principalTable: "Module",
                        principalColumn: "ModSerialID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FilePaths",
                schema: "Core",
                columns: table => new
                {
                    FilePathSerialID = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocTypeSerialID = table.Column<short>(type: "smallint", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilePaths", x => x.FilePathSerialID);
                    table.ForeignKey(
                        name: "FK_FilePaths_DocType_DocTypeSerialID",
                        column: x => x.DocTypeSerialID,
                        principalSchema: "Core",
                        principalTable: "DocType",
                        principalColumn: "DocTypeSerialID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupMenu",
                schema: "Core",
                columns: table => new
                {
                    GrpMnuSerialID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GrpMnuID = table.Column<int>(type: "int", nullable: false, defaultValueSql: "SELECT NEXT VALUE FOR dbo.GrpMnuID"),
                    GrpSerialID = table.Column<int>(type: "int", nullable: false),
                    MnuID = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupMenu", x => x.GrpMnuSerialID);
                    table.ForeignKey(
                        name: "FK_GroupMenu_Group_GrpSerialID",
                        column: x => x.GrpSerialID,
                        principalSchema: "Core",
                        principalTable: "Group",
                        principalColumn: "GrpSerialID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupMenu_Menu_MnuID",
                        column: x => x.MnuID,
                        principalSchema: "Core",
                        principalTable: "Menu",
                        principalColumn: "MnuSerialID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Report",
                schema: "Core",
                columns: table => new
                {
                    ReportSerialID = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModSerialID = table.Column<int>(type: "int", nullable: false),
                    MnuID = table.Column<int>(type: "int", nullable: false),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataSet = table.Column<string>(type: "nvarchar(max)", maxLength: 200, nullable: false),
                    ReportName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    DataBaseName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    DataBaseSPName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ReportTypeID = table.Column<int>(type: "int", nullable: true),
                    Orientation = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report", x => x.ReportSerialID);
                    table.ForeignKey(
                        name: "FK_Report_Menu_MnuID",
                        column: x => x.MnuID,
                        principalSchema: "Core",
                        principalTable: "Menu",
                        principalColumn: "MnuSerialID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Report_Module_ModSerialID",
                        column: x => x.ModSerialID,
                        principalSchema: "Core",
                        principalTable: "Module",
                        principalColumn: "ModSerialID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserMenuPermission",
                schema: "Core",
                columns: table => new
                {
                    UserMnuPermsSerialID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserMnuPermsID = table.Column<int>(type: "int", nullable: false, defaultValueSql: "SELECT NEXT VALUE FOR dbo.UserMnuPermsID"),
                    UserSerialID = table.Column<int>(type: "int", nullable: false),
                    GrpSerialID = table.Column<int>(type: "int", nullable: true),
                    MnuID = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMenuPermission", x => x.UserMnuPermsSerialID);
                    table.ForeignKey(
                        name: "FK_UserMenuPermission_Group_GrpSerialID",
                        column: x => x.GrpSerialID,
                        principalSchema: "Core",
                        principalTable: "Group",
                        principalColumn: "GrpSerialID");
                    table.ForeignKey(
                        name: "FK_UserMenuPermission_Menu_MnuID",
                        column: x => x.MnuID,
                        principalSchema: "Core",
                        principalTable: "Menu",
                        principalColumn: "MnuSerialID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserMenuPermission_User_UserSerialID",
                        column: x => x.UserSerialID,
                        principalSchema: "Core",
                        principalTable: "User",
                        principalColumn: "UserSerialID",
                        onDelete: ReferentialAction.Cascade);
                });
            */
            migrationBuilder.CreateTable(
                name: "ApprovalRequestStep",
                schema: "Core",
                columns: table => new
                {
                    ApprovalRequestStepID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApprovalRequestID = table.Column<int>(type: "int", nullable: false),
                    ApprovalStepID = table.Column<int>(type: "int", nullable: false),
                    StepOrder = table.Column<int>(type: "int", nullable: false),
                    StepStatus = table.Column<byte>(type: "tinyint", nullable: false),
                    AssignedUserSerialID = table.Column<int>(type: "int", nullable: true),
                    ActionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovalRequestStep", x => x.ApprovalRequestStepID);
                    table.ForeignKey(
                        name: "FK_ApprovalRequestStep_ApprovalRequest_ApprovalRequestID",
                        column: x => x.ApprovalRequestID,
                        principalSchema: "Core",
                        principalTable: "ApprovalRequest",
                        principalColumn: "ApprovalRequestID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApprovalRequestStep_ApprovalStep_ApprovalStepID",
                        column: x => x.ApprovalStepID,
                        principalSchema: "Core",
                        principalTable: "ApprovalStep",
                        principalColumn: "ApprovalStepID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalRequest_ApprovalWorkflowID",
                schema: "Core",
                table: "ApprovalRequest",
                column: "ApprovalWorkflowID");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalRequestStep_ApprovalRequestID",
                schema: "Core",
                table: "ApprovalRequestStep",
                column: "ApprovalRequestID");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalRequestStep_ApprovalStepID",
                schema: "Core",
                table: "ApprovalRequestStep",
                column: "ApprovalStepID");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalStep_ApprovalWorkflowID",
                schema: "Core",
                table: "ApprovalStep",
                column: "ApprovalWorkflowID");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalWorkflow_EntityTypeID",
                schema: "Core",
                table: "ApprovalWorkflow",
                column: "EntityTypeID");
/*
            migrationBuilder.CreateIndex(
                name: "IX_City_IsDeleted",
                schema: "Core",
                table: "City",
                column: "IsDeleted",
                filter: "[IsDeleted] = 1");

            migrationBuilder.CreateIndex(
                name: "IX_Company_CntrySerialID",
                schema: "Core",
                table: "Company",
                column: "CntrySerialID");

            migrationBuilder.CreateIndex(
                name: "IX_Company_ComSerialID",
                schema: "Core",
                table: "Company",
                column: "ComSerialID");

            migrationBuilder.CreateIndex(
                name: "IX_Company_IsDeleted",
                schema: "Core",
                table: "Company",
                column: "IsDeleted",
                filter: "[IsDeleted] = 1");

            migrationBuilder.CreateIndex(
                name: "IX_Country_CntryID",
                schema: "Core",
                table: "Country",
                column: "CntryID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Country_IsDeleted",
                schema: "Core",
                table: "Country",
                column: "IsDeleted",
                filter: "[IsDeleted] = 1");

            migrationBuilder.CreateIndex(
                name: "IX_DelRecord_IsDeleted",
                schema: "Core",
                table: "DelRecord",
                column: "IsDeleted",
                filter: "[IsDeleted] = 1");

            migrationBuilder.CreateIndex(
                name: "IX_DocType_ModSerialID",
                schema: "Core",
                table: "DocType",
                column: "ModSerialID");

            migrationBuilder.CreateIndex(
                name: "IX_DocUpload_DocTypeSerialID",
                schema: "Core",
                table: "DocUpload",
                column: "DocTypeSerialID");

            migrationBuilder.CreateIndex(
                name: "IX_DocUpload_IsDeleted",
                schema: "Core",
                table: "DocUpload",
                column: "IsDeleted",
                filter: "[IsDeleted] = 1");

            migrationBuilder.CreateIndex(
                name: "IX_DocUpload_ModuleSerialID",
                schema: "Core",
                table: "DocUpload",
                column: "ModuleSerialID");

            migrationBuilder.CreateIndex(
                name: "IX_FilePaths_DocTypeSerialID",
                schema: "Core",
                table: "FilePaths",
                column: "DocTypeSerialID");

            migrationBuilder.CreateIndex(
                name: "IX_Group_GrpID",
                schema: "Core",
                table: "Group",
                column: "GrpID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GroupMenu_GrpMnuID",
                schema: "Core",
                table: "GroupMenu",
                column: "GrpMnuID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GroupMenu_GrpSerialID",
                schema: "Core",
                table: "GroupMenu",
                column: "GrpSerialID");

            migrationBuilder.CreateIndex(
                name: "IX_GroupMenu_MnuID",
                schema: "Core",
                table: "GroupMenu",
                column: "MnuID");

            migrationBuilder.CreateIndex(
                name: "IX_LoginLog_UserSerialID",
                schema: "Core",
                table: "LoginLog",
                column: "UserSerialID");

            migrationBuilder.CreateIndex(
                name: "IX_Menu_MnuID",
                schema: "Core",
                table: "Menu",
                column: "MnuID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Menu_MnuSerialID",
                schema: "Core",
                table: "Menu",
                column: "MnuSerialID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Menu_ModSerialID",
                schema: "Core",
                table: "Menu",
                column: "ModSerialID");

            migrationBuilder.CreateIndex(
                name: "IX_Reason_IsDeleted",
                schema: "Core",
                table: "Reason",
                column: "IsDeleted",
                filter: "[IsDeleted] = 1");

            migrationBuilder.CreateIndex(
                name: "IX_Reason_ModSerialID",
                schema: "Core",
                table: "Reason",
                column: "ModSerialID");

            migrationBuilder.CreateIndex(
                name: "IX_Reason_ReasonSerialID",
                schema: "Core",
                table: "Reason",
                column: "ReasonSerialID");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_IsDeleted",
                schema: "Core",
                table: "RefreshToken",
                column: "IsDeleted",
                filter: "[IsDeleted] = 1");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_RTSerialID",
                schema: "Core",
                table: "RefreshToken",
                column: "RTSerialID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_UserSerialID",
                schema: "Core",
                table: "RefreshToken",
                column: "UserSerialID");

            migrationBuilder.CreateIndex(
                name: "IX_Report_IsDeleted",
                schema: "Core",
                table: "Report",
                column: "IsDeleted",
                filter: "[IsDeleted] = 1");

            migrationBuilder.CreateIndex(
                name: "IX_Report_MnuID",
                schema: "Core",
                table: "Report",
                column: "MnuID");

            migrationBuilder.CreateIndex(
                name: "IX_Report_ModSerialID",
                schema: "Core",
                table: "Report",
                column: "ModSerialID");

            migrationBuilder.CreateIndex(
                name: "IX_Report_ReportName",
                schema: "Core",
                table: "Report",
                column: "ReportName");

            migrationBuilder.CreateIndex(
                name: "IX_State_IsDeleted",
                schema: "Core",
                table: "State",
                column: "IsDeleted",
                filter: "[IsDeleted] = 1");

            migrationBuilder.CreateIndex(
                name: "IX_User_GrpSerialID",
                schema: "Core",
                table: "User",
                column: "GrpSerialID");

            migrationBuilder.CreateIndex(
                name: "IX_User_UserID",
                schema: "Core",
                table: "User",
                column: "UserID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserCompany_ComSerialID",
                schema: "Core",
                table: "UserCompany",
                column: "ComSerialID");

            migrationBuilder.CreateIndex(
                name: "IX_UserCompany_IsDeleted",
                schema: "Core",
                table: "UserCompany",
                column: "IsDeleted",
                filter: "[IsDeleted] = 1");

            migrationBuilder.CreateIndex(
                name: "IX_UserCompany_UserComID",
                schema: "Core",
                table: "UserCompany",
                column: "UserComID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserCompany_UserSerialID",
                schema: "Core",
                table: "UserCompany",
                column: "UserSerialID");

            migrationBuilder.CreateIndex(
                name: "IX_UserMenuPermission_GrpSerialID",
                schema: "Core",
                table: "UserMenuPermission",
                column: "GrpSerialID");

            migrationBuilder.CreateIndex(
                name: "IX_UserMenuPermission_IsDeleted",
                schema: "Core",
                table: "UserMenuPermission",
                column: "IsDeleted",
                filter: "[IsDeleted] = 1");

            migrationBuilder.CreateIndex(
                name: "IX_UserMenuPermission_MnuID",
                schema: "Core",
                table: "UserMenuPermission",
                column: "MnuID");

            migrationBuilder.CreateIndex(
                name: "IX_UserMenuPermission_UserMnuPermsID",
                schema: "Core",
                table: "UserMenuPermission",
                column: "UserMnuPermsID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserMenuPermission_UserSerialID",
                schema: "Core",
                table: "UserMenuPermission",
                column: "UserSerialID");
*/        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApprovalActionHistory",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "ApprovalRequestStep",
                schema: "Core");

         /*   migrationBuilder.DropTable(
                name: "AuditTrail",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "City",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "Currency",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "DelRecord",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "DocUpload",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "FilePaths",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "GroupMenu",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "InvalidateToken",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "LoginLog",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "Reason",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "RefreshToken",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "Region",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "Report",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "State",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "Subregion",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "TheNumbers",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "UserCompany",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "UserMenuPermission",
                schema: "Core");
*/
            migrationBuilder.DropTable(
                name: "ApprovalRequest",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "ApprovalStep",
                schema: "Core");
/*
            migrationBuilder.DropTable(
                name: "DocType",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "Company",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "Menu",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "User",
                schema: "Core");
*/
            migrationBuilder.DropTable(
                name: "ApprovalWorkflow",
                schema: "Core");

         /*   migrationBuilder.DropTable(
                name: "Country",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "Module",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "Group",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "EntityType",
                schema: "Core");

            migrationBuilder.DropSequence(
                name: "BNKBRID",
                schema: "dbo");

            migrationBuilder.DropSequence(
                name: "BNKID",
                schema: "dbo");

            migrationBuilder.DropSequence(
                name: "CNTRYID",
                schema: "dbo");

            migrationBuilder.DropSequence(
                name: "COMID",
                schema: "dbo");

            migrationBuilder.DropSequence(
                name: "CURNCYID",
                schema: "dbo");

            migrationBuilder.DropSequence(
                name: "DEPTID",
                schema: "dbo");

            migrationBuilder.DropSequence(
                name: "GrpComID",
                schema: "dbo");

            migrationBuilder.DropSequence(
                name: "GrpID",
                schema: "dbo");

            migrationBuilder.DropSequence(
                name: "GrpMnuID",
                schema: "dbo");

            migrationBuilder.DropSequence(
                name: "MODID",
                schema: "dbo");

            migrationBuilder.DropSequence(
                name: "RTID",
                schema: "dbo");

            migrationBuilder.DropSequence(
                name: "SECTID",
                schema: "dbo");

            migrationBuilder.DropSequence(
                name: "TheNumberID",
                schema: "dbo");

            migrationBuilder.DropSequence(
                name: "UserComID",
                schema: "dbo");

            migrationBuilder.DropSequence(
                name: "UserMnuPermsID",
                schema: "dbo");
 */       }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CIS.API.Migrations
{
    /// <inheritdoc />
    public partial class _00001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "cis");

            migrationBuilder.CreateTable(
                name: "AccountPurpose",
                schema: "cis",
                columns: table => new
                {
                    AccountPurposeID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityID = table.Column<long>(type: "bigint", nullable: false),
                    EntityType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PurposeOfAccount = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PurposeOfAccountOther = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ProductSavings = table.Column<bool>(type: "bit", nullable: false),
                    ProductTimeDeposit = table.Column<bool>(type: "bit", nullable: false),
                    ProductSaleOfROPA = table.Column<bool>(type: "bit", nullable: false),
                    ProductCurrent = table.Column<bool>(type: "bit", nullable: false),
                    ProductLoan = table.Column<bool>(type: "bit", nullable: false),
                    ProductOthers = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountPurpose", x => x.AccountPurposeID);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                schema: "cis",
                columns: table => new
                {
                    AddressID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityID = table.Column<long>(type: "bigint", nullable: false),
                    EntityType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PermanentAddress = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PermanentZipCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    PermanentCountry = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PresentAddress = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PresentZipCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    PresentCountry = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BusinessAddress = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PrincipalAddress = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.AddressID);
                });

            migrationBuilder.CreateTable(
                name: "Contact",
                schema: "cis",
                columns: table => new
                {
                    ContactID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityID = table.Column<long>(type: "bigint", nullable: false),
                    EntityType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    HomePhoneNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MobilePhoneNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ContactPerson = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.ContactID);
                });

            migrationBuilder.CreateTable(
                name: "SourceOfFunds",
                schema: "cis",
                columns: table => new
                {
                    SourceOfFundsID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityID = table.Column<long>(type: "bigint", nullable: false),
                    EntityType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SourceOfFundsType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SourceOfFundsOther = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SourceOfFunds", x => x.SourceOfFundsID);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                schema: "cis",
                columns: table => new
                {
                    CustomerID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerCategory = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CustomerType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CIDNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    AddressID = table.Column<long>(type: "bigint", nullable: true),
                    ContactID = table.Column<long>(type: "bigint", nullable: true),
                    AccountPurposeID = table.Column<long>(type: "bigint", nullable: true),
                    SourceOfFundsID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.CustomerID);
                    table.ForeignKey(
                        name: "FK_Customer_AccountPurpose_AccountPurposeID",
                        column: x => x.AccountPurposeID,
                        principalSchema: "cis",
                        principalTable: "AccountPurpose",
                        principalColumn: "AccountPurposeID");
                    table.ForeignKey(
                        name: "FK_Customer_Address_AddressID",
                        column: x => x.AddressID,
                        principalSchema: "cis",
                        principalTable: "Address",
                        principalColumn: "AddressID");
                    table.ForeignKey(
                        name: "FK_Customer_Contact_ContactID",
                        column: x => x.ContactID,
                        principalSchema: "cis",
                        principalTable: "Contact",
                        principalColumn: "ContactID");
                    table.ForeignKey(
                        name: "FK_Customer_SourceOfFunds_SourceOfFundsID",
                        column: x => x.SourceOfFundsID,
                        principalSchema: "cis",
                        principalTable: "SourceOfFunds",
                        principalColumn: "SourceOfFundsID");
                });

            migrationBuilder.CreateTable(
                name: "BankReview",
                schema: "cis",
                columns: table => new
                {
                    BankReviewID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<long>(type: "bigint", nullable: false),
                    IsNegativeListed = table.Column<bool>(type: "bit", nullable: false),
                    IsPEPListed = table.Column<bool>(type: "bit", nullable: false),
                    IsPoracEmployee = table.Column<bool>(type: "bit", nullable: false),
                    DOSRIType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EmployeePosition = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsRelativeOfEmployee = table.Column<bool>(type: "bit", nullable: false),
                    RelativeEmployeeName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    RelativeEmployeePosition = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    RelativeRelationship = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsEntityOwnedByEmployee = table.Column<bool>(type: "bit", nullable: false),
                    IsEntityOwnedByPEP = table.Column<bool>(type: "bit", nullable: false),
                    NatureOfWorkBusiness = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NatureOfWorkBusinessOther = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DocumentsPresented = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    SignatureAuthenticatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    VerifiedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ApprovedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ReviewedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankReview", x => x.BankReviewID);
                    table.ForeignKey(
                        name: "FK_BankReview_Customer_CustomerID",
                        column: x => x.CustomerID,
                        principalSchema: "cis",
                        principalTable: "Customer",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Beneficiary",
                schema: "cis",
                columns: table => new
                {
                    BeneficiaryID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<long>(type: "bigint", nullable: false),
                    TrustType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BeneficiaryName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Birthday = table.Column<DateOnly>(type: "date", nullable: true),
                    PlaceOfBirth = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Nationality = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    NatureOfWork = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beneficiary", x => x.BeneficiaryID);
                    table.ForeignKey(
                        name: "FK_Beneficiary_Customer_CustomerID",
                        column: x => x.CustomerID,
                        principalSchema: "cis",
                        principalTable: "Customer",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BusinessInfo",
                schema: "cis",
                columns: table => new
                {
                    BusinessInfoID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<long>(type: "bigint", nullable: false),
                    NameOfBusiness = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsGovernment = table.Column<bool>(type: "bit", nullable: false),
                    IsPrivate = table.Column<bool>(type: "bit", nullable: false),
                    TypeOfOrganization = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TypeOfOrganizationOther = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DateOfRegistration = table.Column<DateOnly>(type: "date", nullable: true),
                    BusinessRegNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BusinessRegExpiry = table.Column<DateOnly>(type: "date", nullable: true),
                    PlaceOfRegistration = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    NatureOfBusiness = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    TaxIdentificationNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DTICertNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DTICertExpiry = table.Column<DateOnly>(type: "date", nullable: true),
                    SizeOfBusiness = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AverageAnnualIncome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessInfo", x => x.BusinessInfoID);
                    table.ForeignKey(
                        name: "FK_BusinessInfo_Customer_CustomerID",
                        column: x => x.CustomerID,
                        principalSchema: "cis",
                        principalTable: "Customer",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BusinessInterest",
                schema: "cis",
                columns: table => new
                {
                    BusinessInterestID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<long>(type: "bigint", nullable: false),
                    BusinessName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    OwnershipPercentage = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessInterest", x => x.BusinessInterestID);
                    table.ForeignKey(
                        name: "FK_BusinessInterest_Customer_CustomerID",
                        column: x => x.CustomerID,
                        principalSchema: "cis",
                        principalTable: "Customer",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GovernmentRelation",
                schema: "cis",
                columns: table => new
                {
                    GovernmentRelationID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<long>(type: "bigint", nullable: false),
                    RelationType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Relationship = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    HighestPositionOccupied = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PeriodCovered = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GovernmentRelation", x => x.GovernmentRelationID);
                    table.ForeignKey(
                        name: "FK_GovernmentRelation_Customer_CustomerID",
                        column: x => x.CustomerID,
                        principalSchema: "cis",
                        principalTable: "Customer",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IndividualEmployment",
                schema: "cis",
                columns: table => new
                {
                    EmploymentID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<long>(type: "bigint", nullable: false),
                    EmploymentStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EmploymentStatusOther = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    TypeOfEmployment = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TypeOfEmploymentOther = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    OFWCountry = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EducationalAttainment = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    NatureOfWork = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    AverageMonthlyIncome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NameOfEmployer = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    EmployerBuildingNo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EmployerStreet = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    EmployerBrgyDistrict = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    EmployerCityTown = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    EmployerPhoneNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EmployerEmailAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PositionRank = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndividualEmployment", x => x.EmploymentID);
                    table.ForeignKey(
                        name: "FK_IndividualEmployment_Customer_CustomerID",
                        column: x => x.CustomerID,
                        principalSchema: "cis",
                        principalTable: "Customer",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IndividualFamily",
                schema: "cis",
                columns: table => new
                {
                    FamilyID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<long>(type: "bigint", nullable: false),
                    SpouseLastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SpouseGivenName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SpouseMiddleName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MotherMaidenLastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MotherMaidenGivenName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MotherMaidenMiddleName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndividualFamily", x => x.FamilyID);
                    table.ForeignKey(
                        name: "FK_IndividualFamily_Customer_CustomerID",
                        column: x => x.CustomerID,
                        principalSchema: "cis",
                        principalTable: "Customer",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IndividualForeigner",
                schema: "cis",
                columns: table => new
                {
                    ForeignerID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<long>(type: "bigint", nullable: false),
                    PassportIDNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PassportExpiry = table.Column<DateOnly>(type: "date", nullable: true),
                    IsACR = table.Column<bool>(type: "bit", nullable: false),
                    IsSIRV = table.Column<bool>(type: "bit", nullable: false),
                    IsSRRV = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndividualForeigner", x => x.ForeignerID);
                    table.ForeignKey(
                        name: "FK_IndividualForeigner_Customer_CustomerID",
                        column: x => x.CustomerID,
                        principalSchema: "cis",
                        principalTable: "Customer",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IndividualIdentification",
                schema: "cis",
                columns: table => new
                {
                    IdentificationID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<long>(type: "bigint", nullable: false),
                    TINNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SSSNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    GSISNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DriversLicenseIDNo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DriversLicenseExpiry = table.Column<DateOnly>(type: "date", nullable: true),
                    PassportIDNo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PassportIDExpiry = table.Column<DateOnly>(type: "date", nullable: true),
                    OtherIDType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    OtherIDNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    OtherIDExpiry = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndividualIdentification", x => x.IdentificationID);
                    table.ForeignKey(
                        name: "FK_IndividualIdentification_Customer_CustomerID",
                        column: x => x.CustomerID,
                        principalSchema: "cis",
                        principalTable: "Customer",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IndividualInfo",
                schema: "cis",
                columns: table => new
                {
                    IndividualInfoID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<long>(type: "bigint", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MiddleName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsResident = table.Column<bool>(type: "bit", nullable: false),
                    Citizenship = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CitizenshipOther = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CountryOfOrigin = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: true),
                    PlaceOfBirth = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    MaritalStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MailingPreference = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndividualInfo", x => x.IndividualInfoID);
                    table.ForeignKey(
                        name: "FK_IndividualInfo_Customer_CustomerID",
                        column: x => x.CustomerID,
                        principalSchema: "cis",
                        principalTable: "Customer",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BankReview_CustomerID",
                schema: "cis",
                table: "BankReview",
                column: "CustomerID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Beneficiary_CustomerID",
                schema: "cis",
                table: "Beneficiary",
                column: "CustomerID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BusinessInfo_CustomerID",
                schema: "cis",
                table: "BusinessInfo",
                column: "CustomerID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BusinessInterest_CustomerID",
                schema: "cis",
                table: "BusinessInterest",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_AccountPurposeID",
                schema: "cis",
                table: "Customer",
                column: "AccountPurposeID");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_AddressID",
                schema: "cis",
                table: "Customer",
                column: "AddressID");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_ContactID",
                schema: "cis",
                table: "Customer",
                column: "ContactID");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_SourceOfFundsID",
                schema: "cis",
                table: "Customer",
                column: "SourceOfFundsID");

            migrationBuilder.CreateIndex(
                name: "IX_GovernmentRelation_CustomerID",
                schema: "cis",
                table: "GovernmentRelation",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_IndividualEmployment_CustomerID",
                schema: "cis",
                table: "IndividualEmployment",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_IndividualFamily_CustomerID",
                schema: "cis",
                table: "IndividualFamily",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_IndividualForeigner_CustomerID",
                schema: "cis",
                table: "IndividualForeigner",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_IndividualIdentification_CustomerID",
                schema: "cis",
                table: "IndividualIdentification",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_IndividualInfo_CustomerID",
                schema: "cis",
                table: "IndividualInfo",
                column: "CustomerID",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BankReview",
                schema: "cis");

            migrationBuilder.DropTable(
                name: "Beneficiary",
                schema: "cis");

            migrationBuilder.DropTable(
                name: "BusinessInfo",
                schema: "cis");

            migrationBuilder.DropTable(
                name: "BusinessInterest",
                schema: "cis");

            migrationBuilder.DropTable(
                name: "GovernmentRelation",
                schema: "cis");

            migrationBuilder.DropTable(
                name: "IndividualEmployment",
                schema: "cis");

            migrationBuilder.DropTable(
                name: "IndividualFamily",
                schema: "cis");

            migrationBuilder.DropTable(
                name: "IndividualForeigner",
                schema: "cis");

            migrationBuilder.DropTable(
                name: "IndividualIdentification",
                schema: "cis");

            migrationBuilder.DropTable(
                name: "IndividualInfo",
                schema: "cis");

            migrationBuilder.DropTable(
                name: "Customer",
                schema: "cis");

            migrationBuilder.DropTable(
                name: "AccountPurpose",
                schema: "cis");

            migrationBuilder.DropTable(
                name: "Address",
                schema: "cis");

            migrationBuilder.DropTable(
                name: "Contact",
                schema: "cis");

            migrationBuilder.DropTable(
                name: "SourceOfFunds",
                schema: "cis");
        }
    }
}

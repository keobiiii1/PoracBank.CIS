using System.ComponentModel;

namespace CIS.Assets.Enum;

public enum NatureOfWorkBusiness
{
    [Description("None")]
    None,
    [Description("Agriculture / Animal Farming")]
    AgricultureAnimalFarming,
    [Description("Banking / Financial Services")]
    BankingFinancialServices,
    [Description("BPO / KPO")]
    BPOKPO,
    [Description("Brokerage")]
    Brokerage,
    [Description("Canteen / Eatery / Resto Bar")]
    CanteenEateryRestoBar,
    [Description("Casino / Dealer")]
    CasinoDealer,
    [Description("Drugs / Pharmaceuticals")]
    DrugsPharmaceuticals,
    [Description("Education")]
    Education,
    [Description("Government Service")]
    GovernmentService,
    [Description("Import / Export")]
    ImportExport,
    [Description("Informal Lending")]
    InformalLending,
    [Description("Manufacturing")]
    Manufacturing,
    [Description("Market Vendor")]
    MarketVendor,
    [Description("Medical Services")]
    MedicalServices,
    [Description("Money Changer / FX Dealer")]
    MoneyChangerFXDealer,
    [Description("Online Business")]
    OnlineBusiness,
    [Description("Professional Practice")]
    ProfessionalPractice,
    [Description("Quarrying / Trucking / Mining")]
    QuarryingTruckingMining,
    [Description("Real Estate Activities")]
    RealEstateActivities,
    [Description("Remittance Agent")]
    RemittanceAgent,
    [Description("Rental")]
    Rental,
    [Description("Resorts / Hotel / Accommodation")]
    ResortsHotelAccommodation,
    [Description("Student")]
    Student,
    [Description("Transportation")]
    Transportation,
    [Description("Wholesale / Retail Trade")]
    WholesaleRetailTrade,
    [Description("Unemployed")]
    Unemployed,
    [Description("Others")]
    Others,
}

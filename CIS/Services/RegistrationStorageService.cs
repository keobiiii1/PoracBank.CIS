using Blazored.LocalStorage;
using CIS.Assets.DTO;
using CIS.Assets.Enum;
using Microsoft.JSInterop;

namespace CIS.Client.Services;

public class RegistrationStorageService
{
    private readonly ILocalStorageService _storage;
    private readonly IJSRuntime _js;

    private const string BIZ_KEY = "pwa_biz_draft";
    private const string INDIVIDUAL_KEY = "pwa_individual_draft";

    public RegistrationStorageService(ILocalStorageService storage, IJSRuntime js)
    {
        _storage = storage;
        _js = js;
    }

    public async Task<bool> IsOnlineAsync()
    {
        try { return await _js.InvokeAsync<bool>("eval", "navigator.onLine"); }
        catch { return false; }
    }

    // ── Individual Flow ────────────────────────────────────────────────────

    public async Task SaveIndividualDraftAsync(
        CustomerDTO.PageModel customer,
        IndividualInfoDTO.PageModel individual,
        AddressDTO.PageModel address,
        IndividualIdentificationDTO.PageModel identification,
        IndividualEmploymentDTO.PageModel employment,
        IndividualFamilyDTO.PageModel family,
        ContactDTO.PageModel contact,
        IndividualForeignerDTO.PageModel foreigner,
        BusinessInterestDTO.PageModel busint,
        GovernmentRelationDTO.PageModel govrel,
        AccountPurposeDTO.PageModel accpurp,
        BusinessInfoDTO.PageModel businessInfo,
        BusinessInfoDTO.PageModel accountContact,
        BeneficiaryDTO.PageModel beneficiary,
        ClientAcknowlegdementDTO.PageModel acknowledgement,
        BankReviewDTO.PageModel bankReview,
        int step)
    {
        SyncFamilyFromPersonal(customer, individual, family);

        address.EntityID = customer.CustomerID;
        address.EntityType = EntityType.Individual;
        contact.EntityID = customer.CustomerID;
        contact.EntityType = EntityType.Individual;

        var draft = new IndividualDraft
        {
            Customer = customer,
            Individual = individual,
            Address = address,
            Identification = identification,
            Employment = employment,
            Family = family,
            Contact = contact,
            Foreigner = foreigner,
            BusinessInterest = busint,
            GovernmentRelation = govrel,
            AccountPurpose = accpurp,
            BusinessInfo = businessInfo,
            AccountContact = accountContact,
            Beneficiary = beneficiary,
            Acknowledgement = acknowledgement,
            BankReview = bankReview,
            CurrentStep = step,
            LastSaved = DateTime.Now
        };

        await _storage.SetItemAsync(INDIVIDUAL_KEY, draft);
    }

    public IndividualRegistrationRequest BuildRegistrationRequest(
        CustomerDTO.PageModel customer,
        IndividualInfoDTO.PageModel individual,
        AddressDTO.PageModel address,
        IndividualIdentificationDTO.PageModel identification,
        IndividualEmploymentDTO.PageModel employment,
        IndividualFamilyDTO.PageModel family,
        ContactDTO.PageModel contact,
        IndividualForeignerDTO.PageModel foreigner,
        BusinessInterestDTO.PageModel busint,
        GovernmentRelationDTO.PageModel govrel,
        AccountPurposeDTO.PageModel accpurp,
        BusinessInfoDTO.PageModel businessInfo,
        BusinessInfoDTO.PageModel accountContact,
        BeneficiaryDTO.PageModel beneficiary,
        ClientAcknowlegdementDTO.PageModel acknowledgement,
        BankReviewDTO.PageModel bankReview)
    {
        individual.CustomerCategory = customer.CustomerCategory;
        individual.CustomerType = customer.CustomerType;
        individual.CIDNumber = customer.CIDNumber;

        SyncFamilyFromPersonal(customer, individual, family);

        address.EntityID = customer.CustomerID;
        address.EntityType = EntityType.Individual;
        contact.EntityID = customer.CustomerID;
        contact.EntityType = EntityType.Individual;

        return new IndividualRegistrationRequest
        {
            Customer = customer,
            Individual = individual,
            Address = address,
            Identification = identification,
            Employment = employment,
            Family = family,
            Contact = contact,
            Foreigner = foreigner,
            BusinessInterest = busint,
            GovernmentRelation = govrel,
            AccountPurpose = accpurp,
            Business = businessInfo,
            AccountContact = accountContact,
            Beneficiary = beneficiary,
            Acknowledgement = acknowledgement,
            BankReview = bankReview
        };
    }

    public async Task<IndividualDraft?> GetIndividualDraftAsync()
        => await _storage.GetItemAsync<IndividualDraft>(INDIVIDUAL_KEY);

    public async Task ClearIndividualDraftAsync()
        => await _storage.RemoveItemAsync(INDIVIDUAL_KEY);

    // ── Business Flow ──────────────────────────────────────────────────────

    public async Task SaveBusinessDraftAsync(
        BusinessInfoDTO.PageModel business,
        AccountPurposeDTO.PageModel purpose,
        AddressDTO.PageModel address,
        ContactDTO.PageModel contact,
        BeneficiaryDTO.PageModel beneficiary,
        ClientAcknowlegdementDTO.PageModel acknowledgement,
        BankReviewDTO.PageModel review,
        int step)
    {
        var draft = new BusinessDraft
        {
            Business = business,
            AccountPurpose = purpose,
            Address = address,
            Contact = contact,
            Beneficiary = beneficiary,
            Acknowledgement = acknowledgement,
            Review = review,
            CurrentStep = step,
            LastSaved = DateTime.Now
        };
        await _storage.SetItemAsync(BIZ_KEY, draft);
    }

    public async Task<BusinessDraft?> GetDraftAsync()
        => await _storage.GetItemAsync<BusinessDraft>(BIZ_KEY);

    public async Task ClearDraftAsync()
        => await _storage.RemoveItemAsync(BIZ_KEY);

    // ── Private helpers ────────────────────────────────────────────────────

    private static void SyncFamilyFromPersonal(
        CustomerDTO.PageModel customer,
        IndividualInfoDTO.PageModel individual,
        IndividualFamilyDTO.PageModel family)
    {
        family.CustomerID = customer.CustomerID;
        family.SpouseGivenName = individual.SpouseGivenName;
        family.SpouseLastName = individual.SpouseLastName;
        family.SpouseMiddleName = individual.SpouseMiddleName;
        family.MotherMaidenGivenName = individual.MotherMaidenGivenName;
        family.MotherMaidenLastName = individual.MotherMaidenLastName;
        family.MotherMaidenMiddleName = individual.MotherMaidenMiddleName;
    }

    // ── Draft classes ──────────────────────────────────────────────────────

    public class IndividualDraft
    {
        public CustomerDTO.PageModel? Customer { get; set; }
        public IndividualInfoDTO.PageModel? Individual { get; set; }
        public AddressDTO.PageModel? Address { get; set; }
        public IndividualIdentificationDTO.PageModel? Identification { get; set; }
        public IndividualEmploymentDTO.PageModel? Employment { get; set; }
        public IndividualFamilyDTO.PageModel? Family { get; set; }
        public ContactDTO.PageModel? Contact { get; set; }
        public IndividualForeignerDTO.PageModel? Foreigner { get; set; }
        public BusinessInterestDTO.PageModel? BusinessInterest { get; set; }
        public GovernmentRelationDTO.PageModel? GovernmentRelation { get; set; }
        public AccountPurposeDTO.PageModel? AccountPurpose { get; set; }
        public BusinessInfoDTO.PageModel? BusinessInfo { get; set; }
        public BusinessInfoDTO.PageModel? AccountContact { get; set; }
        public BeneficiaryDTO.PageModel? Beneficiary { get; set; }
        public ClientAcknowlegdementDTO.PageModel? Acknowledgement { get; set; }
        public BankReviewDTO.PageModel? BankReview { get; set; }
        public int CurrentStep { get; set; }
        public DateTime LastSaved { get; set; }
    }

    public class BusinessDraft
    {
        public BusinessInfoDTO.PageModel? Business { get; set; }
        public AccountPurposeDTO.PageModel? AccountPurpose { get; set; }
        public AddressDTO.PageModel? Address { get; set; }
        public ContactDTO.PageModel? Contact { get; set; }
        public BeneficiaryDTO.PageModel? Beneficiary { get; set; }
        public ClientAcknowlegdementDTO.PageModel? Acknowledgement { get; set; }
        public BankReviewDTO.PageModel? Review { get; set; }
        public int CurrentStep { get; set; }
        public DateTime LastSaved { get; set; }
    }
}
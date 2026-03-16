using Blazored.LocalStorage;
using CIS.Assets.DTO;
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
        try
        {
            return await _js.InvokeAsync<bool>("eval", "navigator.onLine");
        }
        catch { return false; }
    }

    #region Individual Flow
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
        int step)
    {
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
            CurrentStep = step,
            LastSaved = DateTime.Now
        };
        await _storage.SetItemAsync(INDIVIDUAL_KEY, draft);
    }

    public async Task<IndividualDraft?> GetIndividualDraftAsync()
    {
        return await _storage.GetItemAsync<IndividualDraft>(INDIVIDUAL_KEY);
    }

    public async Task ClearIndividualDraftAsync()
    {
        await _storage.RemoveItemAsync(INDIVIDUAL_KEY);
    }

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
        public AccountPurposeDTO.PageModel? AccountPurpose { get; set; } = null;
        public int CurrentStep { get; set; }
        public DateTime LastSaved { get; set; }
    }
    #endregion

    #region Business Flow
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
    {
        return await _storage.GetItemAsync<BusinessDraft>(BIZ_KEY);
    }

    public async Task ClearDraftAsync()
    {
        await _storage.RemoveItemAsync(BIZ_KEY);
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
    #endregion
}
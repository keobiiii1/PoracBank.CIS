namespace CIS.Assets.DTO;

public class IndividualRegistrationRequest
{
    public CustomerDTO.PageModel Customer { get; set; } = new();
    public IndividualInfoDTO.PageModel Individual { get; set; } = new();
    public AddressDTO.PageModel Address { get; set; } = new();
    public IndividualIdentificationDTO.PageModel Identification { get; set; } = new();
    public IndividualEmploymentDTO.PageModel Employment { get; set; } = new();
    public IndividualForeignerDTO.PageModel Foreigner { get; set; } = new();
    public IndividualFamilyDTO.PageModel Family { get; set; } = new();
    public ContactDTO.PageModel Contact { get; set; } = new();
    public BusinessInterestDTO.PageModel BusinessInterest { get; set; } = new();
    public GovernmentRelationDTO.PageModel GovernmentRelation { get; set; } = new();
    public AccountPurposeDTO.PageModel AccountPurpose { get; set; } = new();
    public BusinessInfoDTO.PageModel Business { get; set; } = new();
    public BusinessInfoDTO.PageModel AccountContact { get; set; } = new();
    public BeneficiaryDTO.PageModel Beneficiary { get; set; } = new();
    public ClientAcknowlegdementDTO.PageModel Acknowledgement { get; set; } = new();
    public BankReviewDTO.PageModel BankReview { get; set; } = new();
}

public class BusinessRegistrationRequest
{
    public BusinessInfoDTO.PageModel Business { get; set; } = new();
    public AccountPurposeDTO.PageModel AccountPurpose { get; set; } = new();
    public AddressDTO.PageModel Address { get; set; } = new();
    public ContactDTO.PageModel Contact { get; set; } = new();
    public BeneficiaryDTO.PageModel Beneficiary { get; set; } = new();
    public ClientAcknowlegdementDTO.PageModel Acknowledgement { get; set; } = new();
    public BankReviewDTO.PageModel BankReview { get; set; } = new();
}
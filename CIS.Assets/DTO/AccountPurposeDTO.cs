using FluentValidation;
using CIS.Assets.Enum;
using CIS.Assets.Models;

namespace CIS.Assets.DTO;

public class AccountPurposeDTO
{
    public class Browse
    {
        public long AccountPurposeID { get; set; }
        public PurposeOfAccount PurposeOfAccount { get; set; }
    }

    public class PageModel
    {
        public long AccountPurposeID { get; set; }
        public long EntityID { get; set; }
        public EntityType EntityType { get; set; }
        public PurposeOfAccount PurposeOfAccount { get; set; } = PurposeOfAccount.None;
        public string? PurposeOfAccountOther { get; set; }
        public string? ProductsAvailed { get; set; }
        public string? ProductsAvailedOther { get; set; }

        public class Validator : AbstractValidator<PageModel>
        {
            public Validator()
            {
                RuleFor(x => x.PurposeOfAccount).NotEqual(PurposeOfAccount.None);
            }
        }
    }
}
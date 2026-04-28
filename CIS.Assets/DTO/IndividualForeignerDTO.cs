using FluentValidation;
using CIS.Assets.Models;

namespace CIS.Assets.DTO;

public class IndividualForeignerDTO
{
    public class Browse
    {
        public long ForeignerID { get; set; }
        public string? PassportIDNumber { get; set; }
        public bool IsACR { get; set; }
    }

    public class PageModel
    {
        public long ForeignerID { get; set; }
        public long CustomerID { get; set; }
        public string? PassportIDNumber { get; set; }
        public DateOnly? PassportExpiry { get; set; }
        public bool IsACR { get; set; }
        public bool IsSIRV { get; set; }
        public bool IsSRRV { get; set; }

        public class Validator : AbstractValidator<PageModel>
        {
            public Validator()
            {
                RuleFor(x => x.PassportIDNumber).MaximumLength(50);
            }
        }
    }
}
using CIS.Assets.Models;

namespace CIS.Assets.DTO;

public class ClientAcknowledgementDTO
{
    public class PageModel
    {
        public long ClientAcknowledgementID { get; set; }
        public long CustomerID { get; set; }
        public string? SignatureData { get; set; }
        public string? PrintedName { get; set; }
        public DateOnly? DateSigned { get; set; }
    }
}
namespace Sample.Domain.ViewModels
{
    public class Token
    {
        public string UserName { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public int CompanyId { get; set; }
        public int PeriodId { get; set; }
        public string Value { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public string SessionId { get; set; } = string.Empty;
        public string Aft { get; set; } = string.Empty;
    }
}

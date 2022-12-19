namespace TelephoneDirectory.Contracts
{
    public record TokenRejected : BaseContract
    {
        public string Token { get; }
        public string Reason { get; }
    }
}

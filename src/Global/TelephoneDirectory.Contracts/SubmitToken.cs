namespace TelephoneDirectory.Contracts
{
    public record SubmitToken : BaseContract
    {
        public string Token { get; }
    }
}

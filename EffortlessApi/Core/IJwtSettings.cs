namespace EffortlessApi.Core
{
    public interface IJwtSettings
    {
        string SigningKey { get; }
        string Issuer { get; }
    }
}
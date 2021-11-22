namespace FraudDetector.Authorization;

public static class Scopes
{
    private const string ScopeBase = "fraud-detector";
    public static readonly string Read = $"{ScopeBase}/read";
    public static readonly string Write = $"{ScopeBase}/write";
    public static readonly string ClaimType = "scope";
}

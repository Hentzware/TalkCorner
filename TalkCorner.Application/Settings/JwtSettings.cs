namespace TalkCorner.Application.Settings;

public class JwtSettings
{
    public int Expiration { get; set; }

    public string Audience { get; set; } = string.Empty;

    public string Issuer { get; set; } = string.Empty;

    public string IssuerSigningKey { get; set; } = string.Empty;
}
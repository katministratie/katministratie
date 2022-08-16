using System;

namespace Superkatten.Katministratie.Application.Helpers;

public static class ClientSecrets
{
    private const string ENVIRONMENT_VAR_GMAIL_CLIENT_ID = "APPSETTING_MailClientId";
    private const string ENVIRONMENT_VAR_GMAIL_CLIENT_SECRET = "APPSETTING_MailClientSecret";

    public static string GmailClientId { get; } 
        = Environment.GetEnvironmentVariable(ENVIRONMENT_VAR_GMAIL_CLIENT_ID) ?? string.Empty;

    public static string GmailClientSecret { get; } 
        = Environment.GetEnvironmentVariable(ENVIRONMENT_VAR_GMAIL_CLIENT_SECRET) ?? string.Empty;
};

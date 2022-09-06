using Microsoft.Extensions.Configuration;
using System;

namespace Superkatten.Katministratie.Application.Helpers;

public class ClientSecrets : IClientSecrets
{
    private const string ENVIRONMENT_VAR_GMAIL_CLIENT_ID = "APPSETTING_MailClientId";
    private const string ENVIRONMENT_VAR_GMAIL_CLIENT_SECRET = "APPSETTING_MailClientSecret";

    private readonly IConfiguration _configuration;

    public ClientSecrets(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GmailClientId => _configuration.GetValue<string>(ENVIRONMENT_VAR_GMAIL_CLIENT_ID) ?? string.Empty;
    public string GmailClientSecret => _configuration.GetValue<string>(ENVIRONMENT_VAR_GMAIL_CLIENT_SECRET) ?? string.Empty;
};

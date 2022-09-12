using Microsoft.Extensions.Configuration;
using System;

namespace Superkatten.Katministratie.Application.Helpers;

public class EmailSettings : IEmailSettings
{
    // See: https://docs.microsoft.com/en-us/azure/app-service/reference-app-settings?tabs=kudu%2Cdotnet

    public static string ENVIRONMENT_VAR_SMTP_HOST_ADDRESS = "APPSETTING_SmtpAddress";
    public static string ENVIRONMENT_VAR_SMTP_PORT_NUMBER = "APPSETTING_SmtpPortNumber";

    private readonly IConfiguration _configuration;

    public EmailSettings(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string SmtpHost => _configuration.GetValue<string>(ENVIRONMENT_VAR_SMTP_HOST_ADDRESS) ?? string.Empty;

    public int SmtpPortNumber => _configuration.GetValue<int>(ENVIRONMENT_VAR_SMTP_PORT_NUMBER);
}
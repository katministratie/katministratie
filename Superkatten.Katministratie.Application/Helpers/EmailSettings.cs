using System;

namespace Superkatten.Katministratie.Application.Helpers;

public static class EmailSettings
{
    private const string ENVIRONMENT_VAR_SMTP_HOST_ADDRESS = "APPSETTING_SmtpAddress";
    private const string ENVIRONMENT_VAR_SMTP_PORT_NUMBER = "APPSETTING_SmtpPortNumber";
    private const string DEFAULT_SMTP_PORT_NUMBER = "0";

    public static string SmtpHost { get; }
        = Environment.GetEnvironmentVariable(ENVIRONMENT_VAR_SMTP_HOST_ADDRESS) ?? string.Empty;

    public static int SmtpPortNumber { get; }
        = int.Parse(Environment.GetEnvironmentVariable(ENVIRONMENT_VAR_SMTP_PORT_NUMBER) ?? DEFAULT_SMTP_PORT_NUMBER);
};

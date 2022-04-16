namespace Superkatten.Katministratie.Application.Services.Authentication
{
    internal class ProblemDetails
    {
        public string Title { get; set; } = string.Empty;
        public string Detail { get; set; } = string.Empty;
        public int Status { get; set; }
    }
}

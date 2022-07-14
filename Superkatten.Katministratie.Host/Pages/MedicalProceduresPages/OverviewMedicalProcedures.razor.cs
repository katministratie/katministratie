using Microsoft.AspNetCore.Components;
using Superkatten.Katministratie.Host.Helpers;
using Superkatten.Katministratie.Host.Services.Interfaces;
using Superkatten.Katministratie.Contract.ApiInterface;

namespace Superkatten.Katministratie.Host.Pages.MedicalProceduresPages;

public partial class OverviewMedicalProcedures
{
    [Inject]
    private Navigation? _navigation { get; set; }

    [Inject]
    private IMedicalProcedureService? _medicalProcedureService { get; set; }

    private IReadOnlyCollection<MedicalProcedureInformation> MedicalProcedures { get; set; } = new List<MedicalProcedureInformation>();

    protected override async Task OnInitializedAsync()
    {
        MedicalProcedures = await _medicalProcedureService.GetAllMedicalProcedures();
    }

    private void OnBack()
    {
        if (_navigation is null)
        {
            return;
        }

        _navigation.NavigateBack();
    }
}



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

    private Dictionary<string, List<MedicalProcedureInformation>> MedicalProcedureInformationDictionary { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        var medicalProcedures = await _medicalProcedureService.GetAllMedicalProcedures();

        MedicalProcedures = medicalProcedures.OrderByDescending(o => o.SuperkatNumber).ToList();

        foreach (var medicalProcedure in MedicalProcedures)
        {
            var key = MedicalProcedureInformationDictionary.TryAdd(
                medicalProcedure.SuperkatNumber ?? "unkown", 
                new List<MedicalProcedureInformation>()
            );

            MedicalProcedureInformationDictionary[medicalProcedure.SuperkatNumber ?? "unkown"].Add(medicalProcedure);
        }

        /*
                Enumerable
                .Range(1, _medicalProcedureNames.Length)
                .Select(x => new MySelectModel
                {
                    MyTextField = _medicalProcedureNames[x - 1],
                    MyValueField = x*/

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



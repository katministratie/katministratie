using Microsoft.AspNetCore.Components;
using Superkatten.Katministratie.Contract.ApiInterface;
using Superkatten.Katministratie.Contract.Entities;
using Superkatten.Katministratie.Host.Helpers;
using Superkatten.Katministratie.Host.Services;
using Superkatten.Katministratie.Host.Services.Interfaces;


namespace Superkatten.Katministratie.Host.Pages.MedicalProceduresPages;

partial class AddMedicalProcedure
{
    [Inject]
    private IMedicalProcedureService? _medicalprocedureService { get; set; }

    [Inject]
    private Navigation? _navigation { get; set; }

    [Inject]
    private ISuperkattenListService? _superkattenService { get; set; }

    [Parameter]
    public Guid SuperkatId { get; set; }

    private Superkat? _superkat;
    private string SuperkatNumber => _superkat?.CatchDate.Year.ToString() + "-" + _superkat?.Number.ToString("000");
    private DateTime TimeStamp { get; set; } = DateTime.UtcNow;
    private string Remark { get; set; } = string.Empty;
    private MedicalProcedureType ProcedureType { get; set; }


    protected override async Task OnInitializedAsync()
    {
        if (_superkattenService is null)
        {
            return;
        }

        SelectedListValue = (int)MedicalProcedureType.Sickness;

        _superkat = await _superkattenService.GetSuperkatAsync(SuperkatId); 
    }

    private async Task OnOk()
    {
        if (_superkat is null)
        {
            return;
        }

        if (_medicalprocedureService is null)
        {
            return;
        }

        if (_navigation is null)
        {
            return;
        }

        var parameters = new AddMedicalProcedureParameters
        {
            SuperkatId = _superkat.Id,
            Remark = Remark,
            ProcedureType = ProcedureType,
            Timestamp = TimeStamp
        };

        await _medicalprocedureService.AddMedicalProcedureAsync(_superkat.Id, parameters);

        _navigation.NavigateBack();
    }

    private void OnCancel()
    {
        if (_navigation is null)
        {
            return;
        }

        _navigation.NavigateBack();
    }

    public class MySelectModel
    {
        public int MyValueField { get; set; }
        public string MyTextField { get; set; } = string.Empty;
    }

    private static readonly string[] _medicalProcedureNames = { 
        "Stronghold", 
        "Neutraliseren", 
        "Controle", 
        "Bezoek dierenarts"
    };
    
    private readonly IEnumerable<MySelectModel> _procedureTypeListData = Enumerable
        .Range(1, _medicalProcedureNames.Length)
        .Select(x => new MySelectModel { 
            MyTextField = _medicalProcedureNames[x - 1], 
            MyValueField = x 
        });

    private int SelectedListValue
    {
        get
        {
            return (int)ProcedureType + 1;
        }
        set
        {
            if (value < 0)
            {
                ProcedureType = MedicalProcedureType.Checkup;
            }

            ProcedureType = (MedicalProcedureType)(value - 1);
        }
    }

    private void MyListValueChangedHandler(int newValue)
    {
        SelectedListValue = newValue;
        StateHasChanged();
    }
}

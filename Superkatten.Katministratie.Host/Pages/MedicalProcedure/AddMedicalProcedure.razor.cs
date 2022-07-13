using AntDesign;
using Microsoft.AspNetCore.Components;
using Superkatten.Katministratie.Contract.ApiInterface;
using Superkatten.Katministratie.Contract.Entities;
using Superkatten.Katministratie.Host.Helpers;
using Superkatten.Katministratie.Host.Services;
using Superkatten.Katministratie.Host.Services.Interfaces;


namespace Superkatten.Katministratie.Host.Pages.MedicalProcedure;

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
    private DateTime TimeStamp { get; set; }
    private string Remark { get; set; } = string.Empty;
    private MedicalProcedureType ProcedureType { get; set; }

    protected override async Task OnInitializedAsync()
    {
        if (_superkattenService is null)
        {
            return;
        }

        _superkat = await _superkattenService.GetSuperkatAsync(SuperkatId); 
    }

    private void OnOk()
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

        _medicalprocedureService.AddMedicalProcedure(_superkat.Id, parameters);

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
        public string MyTextField { get; set; }
    }

    static string[] Countries = { 
        "Albania", "Andorra", "Armenia", "Austria", "Azerbaijan", "Belarus", "Belgium", "Bosnia & Herzegovina", "Bulgaria", "Croatia", "Cyprus", "Czech Republic", "Denmark", "Estonia", "Finland", "France", "Georgia", "Germany", "Greece", "Hungary", "Iceland", "Ireland", "Italy", "Kosovo", "Latvia", "Liechtenstein", "Lithuania", "Luxembourg", "Macedonia", "Malta", "Moldova", "Monaco", "Montenegro", "Netherlands", "Norway", "Poland", "Portugal", "Romania", "Russia", "San Marino", "Serbia", "Slovakia", "Slovenia", "Spain", "Sweden", "Switzerland", "Turkey", "Ukraine", "United Kingdom", "Vatican City" };
    IEnumerable<MySelectModel> myDdlData = Enumerable.Range(1, Countries.Length).Select(x => new MySelectModel { MyTextField = Countries[x - 1], MyValueField = x });

    int selectedListValue { get; set; } = 3;

    void MyListValueChangedHandler(int newValue)
    {
        selectedListValue = newValue;
        StateHasChanged();
    }
}

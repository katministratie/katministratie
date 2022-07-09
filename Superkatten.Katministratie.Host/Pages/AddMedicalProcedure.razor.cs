using AntDesign;
using Microsoft.AspNetCore.Components;
using Superkatten.Katministratie.Contract.ApiInterface;
using Superkatten.Katministratie.Contract.Entities;
using Superkatten.Katministratie.Host.Helpers;
using Superkatten.Katministratie.Host.Services;
using Superkatten.Katministratie.Host.Services.Interfaces;


namespace Superkatten.Katministratie.Host.Pages;

partial class AddMedicalProcedure
{
    [Inject]
    private IMedicalProcedureService? _medicalprocedureService { get; set; }

    [Inject]
    private Navigation? _navigation { get; set; }

    [Inject]
    private ISuperkattenListService _superkattenService { get; set; }

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

    private void OnChangeDate(DateTimeChangedEventArgs args)
    {
        TimeStamp = args.Date;
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
}

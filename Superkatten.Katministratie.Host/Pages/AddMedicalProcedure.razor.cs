using AntDesign;
using Microsoft.AspNetCore.Components;
using Superkatten.Katministratie.Contract.ApiInterface;
using Superkatten.Katministratie.Contract.Entities;
using Superkatten.Katministratie.Host.Helpers;
using Superkatten.Katministratie.Host.Services.Interfaces;


namespace Superkatten.Katministratie.Host.Pages;

partial class AddMedicalProcedure
{
    [Inject]
    private IMedicalProcedureService _medicalprocedureService { get; set; }
    [Inject]
    private Navigation Navigation { get; set; }


    [Parameter]
    public Guid SuperkatId { get; set; }


    private DateTime TimeStamp { get; set; }
    private string Remark { get; set; } = string.Empty;
    private MedicalProcedureType ProcedureType { get; set; }

    protected override async Task OnInitializedAsync()
    {

    }

    private void OnChangeDate(DateTimeChangedEventArgs args)
    {
        TimeStamp = args.Date;
    }

    private void OnOk()
    {
        var parameters = new AddMedicalProcedureParameters
        {
            SuperkatId = SuperkatId,
            Remark = Remark,
            ProcedureType = ProcedureType,
            Timestamp = TimeStamp
        };

        _medicalprocedureService.AddMedicalProcedure(SuperkatId, parameters);

        Navigation.NavigateBack();
    }

    private void OnCancel()
    {
        Navigation.NavigateBack();
    }
}

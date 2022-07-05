using AntDesign;
using Microsoft.AspNetCore.Components;
using Superkatten.Katministratie.Contract.ApiInterface;
using Superkatten.Katministratie.Host.Services;

namespace Superkatten.Katministratie.Host.Pages.MedicalProcedure;

public partial class AddMedicalProcedure
{
    private Guid _superkatId;
    private int _procedureType;
    private DateTime _timestamp;
    private string _remark = string.Empty;

    [Inject]
    private NavigationManager _navigationManager { get; set; }
    
    [Inject]
    private IMedicalProcedureService _medicalProcedureService { get; set; }


    public async Task OnOk()
    {
        await StoreMedicalProcedureAsync();
        _navigationManager.NavigateTo("/");
    }

    public void OnCancel()
    {
        _navigationManager.NavigateTo("/");
    }
    private void OnChangeDate(DateTimeChangedEventArgs args)
    {
        _timestamp = args.Date;
    }

    private async Task StoreMedicalProcedureAsync()
    {
        var parameters = new AddMedicalProcedureParameters
        {
            SuperkatId = _superkatId,
            ProcedureType = _procedureType,
            Timestamp = _timestamp,
            Remark = _remark
        };

        await _medicalProcedureService.AddMedicalProcedureAsync(parameters);

        await _message.Success($"Medical procedure is toegevoegd", 2);        
    }
}

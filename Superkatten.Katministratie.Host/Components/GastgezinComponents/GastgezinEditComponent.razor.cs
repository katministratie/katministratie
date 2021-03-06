using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Superkatten.Katministratie.Contract.ApiInterface;
using Superkatten.Katministratie.Contract.Entities;
using Superkatten.Katministratie.Host.Entities;
using Superkatten.Katministratie.Host.Services;

namespace Superkatten.Katministratie.Host.Components.GastgezinComponents;

public partial class GastgezinEditComponent
{
    [Inject]
    private IGastgezinService? _gastgezinService { get; set; }

    [Parameter]
    public Gastgezin? Gastgezin 
    { 
        set
        {
            if (value is null)
            {
                return;
            }

            _gastgezinData.Id = value.Id;
            _gastgezinData.Name = value.Name;
            _gastgezinData.Address = value.Address ?? string.Empty;
            _gastgezinData.City = value.City ?? string.Empty;
            _gastgezinData.Phone = value.Phone ?? string.Empty;
        }
    }

    [Parameter]
    public EventCallback OnFinish { get; set; }

    private GastgezinData _gastgezinData = new();
    private async Task OnEditOk()
    {
        await StoreAsync();
        await OnFinish.InvokeAsync();
    }

    private Task OnEditCancel()
    {
        return OnFinish.InvokeAsync();
    }

    private async Task StoreAsync()
    {
        if (_gastgezinData.Id is null)
        {
            return;
        }

        var updateGastgezinParameters = new CreateUpdateGastgezinParameters()
        {
            Name = _gastgezinData.Name,
            Address = _gastgezinData.Address,
            City = _gastgezinData.City,
            Phone = _gastgezinData.Phone
        };

        await _gastgezinService!.UpdateGastgezinAsync((Guid)_gastgezinData.Id, updateGastgezinParameters);
    }

    private class GastgezinData
    {
        public Guid? Id;
        public string Name = string.Empty;
        public string Address = string.Empty;
        public string City = string.Empty;
        public string Phone = string.Empty;
    }
}

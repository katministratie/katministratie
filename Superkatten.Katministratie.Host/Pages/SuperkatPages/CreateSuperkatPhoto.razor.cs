using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Superkatten.Katministratie.Contract.ApiInterface;
using Superkatten.Katministratie.Host.Helpers;
using Superkatten.Katministratie.Host.Services;
using Superkatten.Katministratie.Contract.Entities;

namespace Superkatten.Katministratie.Host.Pages.SuperkatPages;

public partial class CreateSuperkatPhoto
{
    [Inject] private IJSRuntime JSRuntime { get; set; } = null!;

    [Inject] public Navigation Navigation { get; set; } = null!;

    [Inject] public ISuperkattenListService SuperkattenService { get; set; } = null!;

    private IReadOnlyCollection<Superkat> _superkatten = Array.Empty<Superkat>();
    private IReadOnlyCollection<string> _superkatNames = null!;

    private Superkat? _selectedSuperkat;

    protected override async Task OnInitializedAsync()
    {
        var selectedVideo = "{ facingMode: { exact: \"environment\" } ";
        await JSRuntime.InvokeVoidAsync("startVideo", "videoFeed", selectedVideo);

        _superkatten = await SuperkattenService.GetAllSuperkattenAsync();
        _superkatNames = _superkatten.OrderBy(s => s.UniqueNumber).Select(s => s.UniqueNumber).ToList();
    }

    private async Task OnOk()
    {
        await JSRuntime.InvokeAsync<string>(
            "getFrame", 
            "videoFeed", 
            "currentFrame", 
            DotNetObjectReference.Create(this)
        );
    }

    private void OnCancel()
    {
        Navigation.NavigateBack();
    }

    private void OnSelectSuperkat(Superkat superkat)
    {
        _selectedSuperkat = superkat;
    }

    [JSInvokable]
    public async Task ProcessImage(string imageString)
    {
        if (_selectedSuperkat is null)
        {
            return;
        }

        var updateSuperkatPhotoParameters = new UpdateSuperkatPhotoParameters
        {
            PhotoData = Convert.FromBase64String(imageString.Split(',')[1])
        };

        await SuperkattenService.UpdateSuperkatPhoto(_selectedSuperkat.Id, updateSuperkatPhotoParameters);

        Navigation.NavigateBack();
    }
}
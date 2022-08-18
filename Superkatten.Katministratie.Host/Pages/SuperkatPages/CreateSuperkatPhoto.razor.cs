using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Superkatten.Katministratie.Contract.ApiInterface;
using Superkatten.Katministratie.Host.Helpers;
using Superkatten.Katministratie.Host.Services;

namespace Superkatten.Katministratie.Host.Pages.SuperkatPages;

public partial class CreateSuperkatPhoto
{
    [Inject] private IJSRuntime JSRuntime { get; set; } = null!;

    [Inject] public Navigation Navigation { get; set; } = null!;

    [Inject] public ISuperkattenListService SuperkattenService { get; set; } = null!;

    private string _captionText = "superkatten (c)";

    private byte[]? _imageData;

    protected override async Task OnInitializedAsync()
    {
        var selectedVideo = "{ facingMode: { exact: \"environment\" } ";
        await JSRuntime.InvokeVoidAsync("startVideo", "videoFeed", selectedVideo);
    }

    private async Task CaptureFrame()
    {
        await JSRuntime.InvokeAsync<string>(
            "getFrame", 
            "videoFeed", 
            "currentFrame", 
            DotNetObjectReference.Create(this)
        );
    }

    [JSInvokable]
    public async Task ProcessImage(string imageString)
    {
        var updateSuperkatPhotoParameters = new UpdateSuperkatPhotoParameters
        {
            PhotoData = Convert.FromBase64String(imageString.Split(',')[1])
        };

        // TODO: superkat selecteren
        try
        {
            await SuperkattenService.UpdateSuperkatPhoto(Guid.NewGuid(), updateSuperkatPhotoParameters);
        }
        catch(Exception ex)
        {

        }
        Navigation.NavigateTo("/");
    }
}
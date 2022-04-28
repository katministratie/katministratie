using Microsoft.AspNetCore.Components;
using Superkatten.Katministratie.Host.Entities;

namespace Superkatten.Katministratie.Host.Components;

public partial class PrinterSelectDialog
{
    [Parameter]
    public bool IsVisible { get; set; }

    [Parameter]
    public string ModelTitle { get; set; } = string.Empty;

    [Parameter]
    public EventCallback OnClose { get; set; }

    [Parameter]
    public EventCallback<string> OnPrint { get; set; }


    private List<Printer>? _printers = new();

    private string _selectedPrinter = string.Empty;

    public void SelectPrinter(string printerName)
    {
        _selectedPrinter = printerName;
    }

    public async Task HandleOk()
    {
        await OnPrint.InvokeAsync(_selectedPrinter);
        await OnClose.InvokeAsync();
    }

    public async Task HandleCancel()
    {
        await OnClose.InvokeAsync();
    }

    protected override async Task OnInitializedAsync()
    {
        _printers = await _printerService.GetPrintersAsync();
    }
}

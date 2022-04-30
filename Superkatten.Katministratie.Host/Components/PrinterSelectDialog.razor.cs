using Microsoft.AspNetCore.Components;
using Superkatten.Katministratie.Host.Entities;

namespace Superkatten.Katministratie.Host.Components;

public partial class PrinterSelectDialog
{
    [Parameter]
    public bool IsVisible { get; set; }

    [Parameter]
    public EventCallback<bool> IsVisibleChanged { get; set; }

    [Parameter]
    public string ModelTitle { get; set; } = string.Empty;

    [Parameter]
    public EventCallback<string> OnPrint { get; set; }


    private List<Printer> _printers = new();

    private string _selectedPrinter = string.Empty;


    protected override void OnInitialized()
    {
        _printers = _printerService.GetPrinters();
    }


    public void SelectPrinter(string printerName)
    {
        _selectedPrinter = printerName;
    }

    public async Task HandleOk()
    {
        await OnPrint.InvokeAsync(_selectedPrinter);
        IsVisible = false;
    }

    public void HandleCancel()
    {
        IsVisible = false;
    }
}

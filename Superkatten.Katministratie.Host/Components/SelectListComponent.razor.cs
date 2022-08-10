
using Microsoft.AspNetCore.Components;
using Superkatten.Katministratie.Host.Entities;

namespace Superkatten.Katministratie.Host.Components;

public partial class SelectListComponent<TItem>
{

    [Parameter]
    public List<TItem> SelectionItems
    {
        get
        {
            return _selectionItems;
        }

        set
        {
            if (value is null)
            {
                return;
            }

            _selectionItems = value;
            UpdateSelectionListData();
        }
    }

    [Parameter]
    public EventCallback<TItem> OnSelectedItem { get; set; }

    [Parameter]
    public string DefaultText { get; set; } = string.Empty;



    private List<TItem> _selectionItems = Array.Empty<TItem>().ToList();

    private IEnumerable<SelectionListModel<TItem>>? _selectionListData;
    private TItem? SelectedListValue { get; set; }

    private void UpdateSelectionListData()
    {
        _selectionListData = Enumerable
            .Range(1, _selectionItems.Count)
            .Select(x => new SelectionListModel<TItem>
            {
                Value = _selectionItems[x - 1],
                Key = GetItemName(x)
            });
    }

    private string GetItemName(int itemIndex)
    {
        if (itemIndex <= 0)
        {
            throw new Exception($"Index '{itemIndex}' is not allowed to be lower or equal to 0.");
        }

        var item = _selectionItems[itemIndex - 1];
        return item is null 
            ? throw new Exception($"No item found at index '{itemIndex}'") 
            : item.ToString() ?? string.Empty;
    }

    private async Task OnSelectedValueChanged(TItem newValue)
    {
        SelectedListValue = newValue;
        await OnSelectedItem.InvokeAsync(newValue);
    }

}

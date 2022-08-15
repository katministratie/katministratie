using Microsoft.AspNetCore.Components;
using MoreLinq;
using Superkatten.Katministratie.Contract.Entities;
using System.Diagnostics.CodeAnalysis;

namespace Superkatten.Katministratie.Host.Components;

public partial class SelectListComponent<TItem>
{
    [Parameter]
    public string DefaultText { get;  set; } = string.Empty;

    [Parameter]
    public string EmptyListText { get; set; } = "...";

    [Parameter]
    public IReadOnlyCollection<string> ItemNames { get; set; } = Array.Empty<string>();

    [Parameter]
    public IReadOnlyCollection<TItem> Items
    {
        get
        {
            return _items;
        }

        set
        {
            if (value is null)
            {
                return;
            }

            if (!value.Any())
            { 
                return;
            }

            if (_items == value)
            {
                return;
            }

            _items = value;
            if (!ItemNames.Any())
            {
                ItemNames = _items.Select(x => x?.ToString() ?? String.Empty).ToList();
            }

            UpdateItems();
        }
    }

    [Parameter]
    public EventCallback<TItem> OnSelectionChanged { get; set; }

    private IReadOnlyCollection<TItem> _items = Array.Empty<TItem>();

    private IEnumerable<SelectListItem<TItem>> _selectionItemData = Array.Empty<SelectListItem<TItem>>();

    protected async override Task OnInitializedAsync()
    {
        if (ItemNames is null || Items is null)
        {
            return;
        }

        UpdateItems();
        await OnSelectedValueChanged(0);
    }

    public void UpdateItems()
    { 
        _selectionItemData = _items
            .Select((itemObject, index) =>
            {
                return new SelectListItem<TItem>
                {
                    KeyId = index + 1,
                    ItemName = ItemNames.ElementAt(index),
                    Item = itemObject
                };
            })
            .ToList();
    }

    private async Task OnSelectedValueChanged(int selectedListIndex)
    {
        if (selectedListIndex > 0)
        {
            // index 0 is actually the remark placeholder of the SelectList component
            // therefore the selected list index == the item key
            var item = GetSelectedItem(selectedListIndex);
            await OnSelectionChanged.InvokeAsync(item);
        }
    }

    private TItem? GetSelectedItem(int keyId)
    {
        var selectedListItem = _selectionItemData
            .Where(x => x.KeyId == keyId)
            .FirstOrDefault();

        return selectedListItem is null || selectedListItem.Item is null
            ? default
            : selectedListItem.Item;
    }
}

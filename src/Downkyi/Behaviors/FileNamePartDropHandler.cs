using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.VisualTree;
using Avalonia.Xaml.Interactions.DragAndDrop;
using Downkyi.UI.Models;
using Downkyi.ViewModels.Settings;

namespace Downkyi.Behaviors;

public class FileNamePartDropHandler : DropHandlerBase
{
    private bool Validate<T>(ItemsControl list, DragEventArgs e, object? sourceContext, object? targetContext, bool bExecute) where T : FileNamePartDisplay
    {
        if (sourceContext is not T sourceItem
            || targetContext is not VideoViewModelProxy vm
            || list.GetVisualAt(e.GetPosition(list)) is not Control targetControl
            || targetControl.DataContext is not T targetItem)
        {
            return false;
        }

        var items = vm.SelectedFileName;
        var sourceIndex = items.IndexOf(sourceItem);
        var targetIndex = items.IndexOf(targetItem);

        if (sourceIndex < 0 || targetIndex < 0)
        {
            return false;
        }

        switch (e.DragEffects)
        {
            case DragDropEffects.Copy:
                {
                    if (bExecute)
                    {
                        var clone = new FileNamePartDisplay() { Title = sourceItem.Title + "_copy" };
                        InsertItem(items, clone, targetIndex + 1);
                    }
                    return true;
                }
            case DragDropEffects.Move:
                {
                    if (bExecute)
                    {
                        MoveItem(items, sourceIndex, targetIndex);
                    }
                    return true;
                }
            case DragDropEffects.Link:
                {
                    if (bExecute)
                    {
                        SwapItem(items, sourceIndex, targetIndex);
                    }
                    return true;
                }
            default:
                return false;
        }
    }

    public override bool Validate(object? sender, DragEventArgs e, object? sourceContext, object? targetContext, object? state)
    {
        if (e.Source is Control && sender is ItemsControl list)
        {
            return Validate<FileNamePartDisplay>(list, e, sourceContext, targetContext, false);
        }
        return false;
    }

    public override bool Execute(object? sender, DragEventArgs e, object? sourceContext, object? targetContext, object? state)
    {
        if (e.Source is Control && sender is ItemsControl list)
        {
            return Validate<FileNamePartDisplay>(list, e, sourceContext, targetContext, true);
        }
        return false;
    }
}
using DownKyi.ViewModels.Settings;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DownKyi.Views.Settings
{
    /// <summary>
    /// Interaction logic for ViewVideo
    /// </summary>
    public partial class ViewVideo : UserControl
    {
        public ViewVideo()
        {
            InitializeComponent();
        }

        // ListBox拖拽的代码参考
        // https://stackoverflow.com/questions/3350187/wpf-c-rearrange-items-in-listbox-via-drag-and-drop

        private void SelectedFileName_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListBoxItem)
            {
                ListBoxItem draggedItem = sender as ListBoxItem;
                DragDrop.DoDragDrop(draggedItem, draggedItem.DataContext, DragDropEffects.Move);
                draggedItem.IsSelected = true;
            }
        }

        private void SelectedFileName_Drop(object sender, DragEventArgs e)
        {
            DisplayFileNamePart droppedData = e.Data.GetData(typeof(DisplayFileNamePart)) as DisplayFileNamePart;
            DisplayFileNamePart target = ((ListBoxItem)sender).DataContext as DisplayFileNamePart;

            int removedIdx = nameSelectedFileName.Items.IndexOf(droppedData);
            int targetIdx = nameSelectedFileName.Items.IndexOf(target);

            var ItemsSource = (ObservableCollection<DisplayFileNamePart>)nameSelectedFileName.ItemsSource;

            if (removedIdx < targetIdx)
            {
                ItemsSource.Insert(targetIdx + 1, droppedData);
                ItemsSource.RemoveAt(removedIdx);
            }
            else
            {
                int remIdx = removedIdx + 1;
                if (ItemsSource.Count + 1 > remIdx)
                {
                    ItemsSource.Insert(targetIdx, droppedData);
                    ItemsSource.RemoveAt(remIdx);
                }
            }
        }
    }
}

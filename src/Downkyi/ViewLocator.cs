using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Downkyi.UI.Mvvm;
using System;

namespace Downkyi;

public class ViewLocator : IDataTemplate
{
    public Control? Build(object? data)
    {
        string name = data!.GetType().FullName!.Replace("ViewModel", "View");

        if (name.Contains(".UI"))
        {
            name = name.Replace(".UI", "");
        }
        if (name.Contains("Proxy"))
        {
            name = name.Replace("Proxy", "");
        }

        var type = Type.GetType(name);

        if (type != null)
        {
            return (Control)Activator.CreateInstance(type)!;
        }

        return new TextBlock { Text = "Not Found: " + name };
    }

    public bool Match(object? data)
    {
        return data is ViewModelBase;
    }
}
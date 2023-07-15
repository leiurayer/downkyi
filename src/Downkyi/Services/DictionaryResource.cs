using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Media;
using Avalonia.Styling;
using Downkyi.UI.Services;
using System;
using System.Collections.Generic;

namespace Downkyi.Services;

public class DictionaryResource : IDictionaryResource
{
    public string GetColor(string resourceKey)
    {
        object? value = GetResource(Application.Current!.Resources, resourceKey);
        return value == null ? "#00000000" : ((Color)value).ToString();
    }

    public string GetString(string resourceKey)
    {
        object? value = GetResource(Application.Current!.Resources, resourceKey);
        return value == null ? "" : (string)value;
    }

    public void LoadLanguage(string languageCode)
    {
        LoadXamlDictionary(languageCode, "Languages");
    }

    public void LoadTheme(string theme)
    {
        LoadXamlDictionary($"{theme}/Theme", "Themes");
        LoadXamlStyle($"{theme}/Styles", "Themes");
    }

    /// <summary>
    /// 递归遍历所有资源
    /// </summary>
    /// <param name="dictionary"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static object? GetResource(IResourceDictionary? dictionary, string value)
    {
        if (dictionary == null)
            return null;

        object? result = null;
        if (dictionary.ContainsKey(value))
        {
            result = dictionary[value];
            return result;
        }

        foreach (var provider in dictionary.MergedDictionaries)
        {
            if (provider is ResourceDictionary resources)
            {
                object? temp = GetResource(resources, value);
                if (temp != null)
                {
                    result = temp;
                }
            }
            if (provider is ResourceInclude include)
            {
                object? temp = GetResource(include.Loaded, value);
                if (temp != null)
                {
                    result = temp;
                }
            }
        }

        return result;
    }

    /// <summary>
    /// 更换xaml资源字典
    /// </summary>
    /// <param name="fileName">xaml文件名，不包含扩展名</param>
    /// <param name="package">文件所在包（路径）</param>
    private static void LoadXamlDictionary(string fileName, string package)
    {
        if (string.IsNullOrEmpty(fileName) == false)
        {
            var dictionariesToRemove = new List<IResourceProvider>();

            foreach (var dictionary in Application.Current!.Resources.MergedDictionaries)
            {
                if (dictionary.ToString()!.Contains("ResourceInclude"))
                {
                    dictionariesToRemove.Add(dictionary);
                }
            }

            foreach (var item in dictionariesToRemove)
            {
                Application.Current.Resources.MergedDictionaries.Remove(item);
            }

            //string? assemblyName = Assembly.GetEntryAssembly()!.GetName().Name;
            //var xaml = new Uri($"avares://{assemblyName}/{package}/{fileName}.axaml");
            // 直接指定程序集名称，为了预览是可以正确找到资源
            var xaml = new Uri($"avares://Downkyi/{package}/{fileName}.axaml");

            var languageDictionary = (ResourceDictionary)AvaloniaXamlLoader.Load(xaml);
            Application.Current.Resources.MergedDictionaries.Add(languageDictionary);
        }
    }

    /// <summary>
    /// 更换xaml样式
    /// </summary>
    /// <param name="fileName">xaml文件名，不包含扩展名</param>
    /// <param name="package">文件所在包（路径）</param>
    private static void LoadXamlStyle(string fileName, string package)
    {
        if (string.IsNullOrEmpty(fileName) == false)
        {
            var stylesToRemove = new List<IStyle>();

            foreach (var style in Application.Current!.Styles)
            {
                if (style.ToString()!.Contains("StyleInclude"))
                {
                    stylesToRemove.Add(style);
                }
            }

            foreach (var item in stylesToRemove)
            {
                Application.Current.Styles.Remove(item);
            }

            //string? assemblyName = Assembly.GetEntryAssembly()!.GetName().Name;
            //var xaml = new Uri($"avares://{assemblyName}/{package}/{fileName}.axaml");
            // 直接指定程序集名称，为了预览是可以正确找到资源
            var xaml = new Uri($"avares://Downkyi/{package}/{fileName}.axaml");

            var styles = (Styles)AvaloniaXamlLoader.Load(xaml);
            Application.Current.Styles.Add(styles);
        }
    }

}
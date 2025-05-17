using System.Windows.Input;
using CraftUI.Library.Maui.Common.Extensions;

namespace CraftUI.Library.Maui.Controls;

public partial class CfEntry
{
    public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(CfEntry), propertyChanged: TextChanged, defaultBindingMode: BindingMode.TwoWay);
    public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(CfEntry), propertyChanged: PlaceholderChanged);
    public static readonly BindableProperty KeyboardProperty = BindableProperty.Create(nameof(Keyboard), typeof(Keyboard), typeof(CfEntry), defaultValue: Keyboard.Plain, propertyChanged: KeyboardChanged);
    public static readonly BindableProperty ReturnTypeProperty = BindableProperty.Create(nameof(ReturnType), typeof(ReturnType), typeof(CfEntry), defaultValue: ReturnType.Done, propertyChanged: ReturnTypeChanged);
    public static readonly BindableProperty ReturnCommandProperty = BindableProperty.Create(nameof(ReturnCommand), typeof(ICommand), typeof(CfEntry), defaultValue: null, propertyChanged: ReturnCommandChanged);
    public static readonly BindableProperty TextTransformProperty = BindableProperty.Create(nameof(TextTransform), typeof(TextTransform), typeof(CfEntry), defaultValue: TextTransform.Default, propertyChanged: TextTransformChanged);
    public static readonly BindableProperty IsReadOnlyProperty = BindableProperty.Create(nameof(IsReadOnly), typeof(bool), typeof(CfEntry), false, propertyChanged: IsReadOnlyChanged);

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public string Placeholder
    {
        get => (string)GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }

    public Keyboard Keyboard
    {
        get => (Keyboard)GetValue(KeyboardProperty);
        set => SetValue(KeyboardProperty, value);
    }

    public ReturnType ReturnType
    {
        get => (ReturnType)GetValue(ReturnTypeProperty);
        set => SetValue(ReturnTypeProperty, value);
    }

    public ICommand ReturnCommand
    {
        get => (ICommand)GetValue(ReturnCommandProperty);
        set => SetValue(ReturnCommandProperty, value);
    }
    
    public TextTransform TextTransform
    {
        get => (TextTransform)GetValue(TextTransformProperty);
        set => SetValue(TextTransformProperty, value);
    }
    
    public bool IsReadOnly
    {
        get => (bool)GetValue(IsReadOnlyProperty);
        set => SetValue(IsReadOnlyProperty, value);
    }
    
    public CfEntry()
    {
        InitializeComponent();
        
        Element.SetVisualElementBinding();
        Element.SetBinding(Entry.TextProperty, nameof(Text), BindingMode.TwoWay);
        Element.BindingContext = this;
    }
    
    protected override void OnPropertyChanged(string? propertyName = null)
    {
        if (propertyName == IsEnabledProperty.PropertyName)
        {
            Element.IsEnabled = IsEnabled;
        }
    }
    
    private static void TextChanged(BindableObject bindable, object oldValue, object newValue) => ((CfEntry)bindable).UpdateTextView();
    private static void PlaceholderChanged(BindableObject bindable, object oldValue, object newValue) => ((CfEntry)bindable).UpdatePlaceholderView();
    private static void KeyboardChanged(BindableObject bindable, object oldValue, object newValue) => ((CfEntry)bindable).UpdateKeyboardView();
    private static void ReturnTypeChanged(BindableObject bindable, object oldValue, object newValue) => ((CfEntry)bindable).UpdateReturnTypeView();
    private static void ReturnCommandChanged(BindableObject bindable, object oldValue, object newValue) => ((CfEntry)bindable).UpdateReturnCommandView();
    private static void TextTransformChanged(BindableObject bindable, object oldValue, object newValue) => ((CfEntry)bindable).UpdateTextTransformView();
    private static void IsReadOnlyChanged(BindableObject bindable, object oldValue, object newValue) => ((CfEntry)bindable).UpdateIsReadOnlyView();
    
    private void UpdateTextView()
    {
        if (Keyboard == Keyboard.Numeric)
        {
            if (string.IsNullOrEmpty(Text))
            {
                Element.Text = Text;
                return;
            }

            Element.Text = int.TryParse(Text, out var number) 
                ? number.ToString() 
                : Text[..^1];
        }
        else
        {
            Element.Text = Text;
        }
    }

    private void UpdatePlaceholderView() => Element.Placeholder = Placeholder;
    private void UpdateKeyboardView() => Element.Keyboard = Keyboard;
    private void UpdateReturnTypeView() => Element.ReturnType = ReturnType;
    private void UpdateReturnCommandView() => Element.ReturnCommand = ReturnCommand;
    private void UpdateTextTransformView() => Element.TextTransform = TextTransform;
    private void UpdateIsReadOnlyView() => Element.IsReadOnly = IsReadOnly;
}
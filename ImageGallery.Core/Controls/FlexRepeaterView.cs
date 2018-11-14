using System.Collections.Generic;
using ImageGallery.Core.Extensions;
using Xamarin.Forms;

namespace ImageGallery.Core.Controls
{
    public class FlexRepeaterView : Grid
    {
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
            nameof(ItemsSource),
            typeof(IEnumerable<object>),
            typeof(FlexRepeaterView),
            null,
            propertyChanged: ItemsSourcePropertyChanged);

        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(
            nameof(ItemTemplate),
            typeof(DataTemplate),
            typeof(FlexRepeaterView));

        public IEnumerable<object> ItemsSource
        {
            get => (IEnumerable<object>)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public DataTemplate ItemTemplate
        {
            get => (DataTemplate)GetValue(ItemTemplateProperty);
            set => SetValue(ItemTemplateProperty, value);
        }

        public double ItemWidth { get; set; }

        protected override void OnSizeAllocated(double width, double height)
        {
            var columnCount = 0;

            if ((int) ItemWidth != 0)
            {
                columnCount = (int) (width / ItemWidth);
            }

            for (int i = ColumnDefinitions.Count; i > columnCount; i--)
            {
                ColumnDefinitions.RemoveAt(ColumnDefinitions.Count - 1);
            }

            for (int i = ColumnDefinitions.Count; i < columnCount; i++)
            {
                ColumnDefinitions.Add(new ColumnDefinition());
            }

            PushToLeft(Children, columnCount);
            base.OnSizeAllocated(width, height);
        }

        private void PushToLeft(IEnumerable<View> children, int columnCount)
        {
            int currentColumn = 0, currentRow = 0;

            foreach (var child in children)
            {
                child.WidthRequest = ItemWidth;
                SetColumn(child, currentColumn++);
                SetRow(child, currentRow);
                if (currentColumn >= columnCount)
                {
                    currentColumn = 0;
                    currentRow++;
                }
            }
        }

        private static void ItemsSourcePropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (!(bindable is FlexRepeaterView control))
            {
                return;
            }

            control.Children.Clear();

            if (newvalue is IEnumerable<object> items)
            {
                foreach (var item in items)
                {
                    var view = control.ItemTemplate.GetViewFromTemplate(item);
                    control.Children.Add(view);
                }
            }
        }
    }
}

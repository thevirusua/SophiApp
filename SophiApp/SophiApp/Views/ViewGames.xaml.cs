﻿using SophiApp.Commons;
using SophiApp.Helpers;
using SophiApp.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace SophiApp.Views
{
    /// <summary>
    /// Логика взаимодействия для ViewGames.xaml
    /// </summary>
    public partial class ViewGames : UserControl
    {
        // Using a DependencyProperty as the backing store for Tag.  This enables animation, styling, binding, etc...
        public new static readonly DependencyProperty TagProperty =
            DependencyProperty.Register("Tag", typeof(string), typeof(ViewGames), new PropertyMetadata(default));

        // Using a DependencyProperty as the backing store for ElementsCount.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextedElementsCountProperty =
            DependencyProperty.Register("TextedElementsCount", typeof(int), typeof(ViewGames), new PropertyMetadata(0));

        public ViewGames()
        {
            InitializeComponent();
            AddHandler(PreviewMouseWheelEvent, new MouseWheelEventHandler(OnChildMouseWheelEvent), true);
        }

        public new string Tag
        {
            get => (string)GetValue(TagProperty);
            set => SetValue(TagProperty, value);
        }

        public int TextedElementsCount
        {
            get { return (int)GetValue(TextedElementsCountProperty); }
            set { SetValue(TextedElementsCountProperty, value); }
        }

        private void OnChildMouseWheelEvent(object sender, MouseWheelEventArgs e)
        {
            e.Handled = true;
            var mouseWheelEventArgs = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta) { RoutedEvent = MouseWheelEvent };
            var scrollViewer = Template.FindName("ScrollViewerContent", this) as ScrollViewer;
            scrollViewer.RaiseEvent(mouseWheelEventArgs);
        }

        private void TextedElementsFilter(object sender, FilterEventArgs e)
        {
            var element = e.Item as TextedElement;
            var isValidElement = FilterHelper.FilterByTag(elementTag: element.Tag, viewTag: Tag);

            if (isValidElement && element.Status != ElementStatus.DISABLED)
                TextedElementsCount++;

            e.Accepted = isValidElement;
        }

        private void ViewGames_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (IsVisible)
            {
                var scrollViewer = Template.FindName("ScrollViewerContent", this) as ScrollViewer;
                scrollViewer?.ScrollToTop();
            }
        }
    }
}
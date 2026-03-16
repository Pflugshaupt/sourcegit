using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.VisualTree;

namespace SourceGit.Views
{
    public partial class DiffView : UserControl
    {
        public static readonly StyledProperty<bool> IsStackedProperty =
            AvaloniaProperty.Register<DiffView, bool>(nameof(IsStacked), false);

        public bool IsStacked
        {
            get => GetValue(IsStackedProperty);
            set => SetValue(IsStackedProperty, value);
        }

        public DiffView()
        {
            InitializeComponent();
        }

        protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
        {
            base.OnPropertyChanged(change);

            if (change.Property == IsStackedProperty)
                UpdateContentRowDefinition();
        }

        protected override void OnLoaded(RoutedEventArgs e)
        {
            base.OnLoaded(e);

            UpdateContentRowDefinition();

            if (DataContext is ViewModels.DiffContext vm)
                vm.CheckSettings();
        }

        private void UpdateContentRowDefinition()
        {
            var grid = this.FindNameScope()?.Find<Grid>("ContentGrid");
            if (grid == null || grid.RowDefinitions.Count < 2)
                return;

            grid.RowDefinitions[1] = IsStacked
                ? new RowDefinition(GridLength.Auto)
                : new RowDefinition(new GridLength(1, GridUnitType.Star));
        }

        private void OnGotoFirstChange(object _, RoutedEventArgs e)
        {
            this.FindDescendantOfType<ThemedTextDiffPresenter>()?.GotoChange(ViewModels.BlockNavigationDirection.First);
            e.Handled = true;
        }

        private void OnGotoPrevChange(object _, RoutedEventArgs e)
        {
            this.FindDescendantOfType<ThemedTextDiffPresenter>()?.GotoChange(ViewModels.BlockNavigationDirection.Prev);
            e.Handled = true;
        }

        private void OnGotoNextChange(object _, RoutedEventArgs e)
        {
            this.FindDescendantOfType<ThemedTextDiffPresenter>()?.GotoChange(ViewModels.BlockNavigationDirection.Next);
            e.Handled = true;
        }

        private void OnGotoLastChange(object _, RoutedEventArgs e)
        {
            this.FindDescendantOfType<ThemedTextDiffPresenter>()?.GotoChange(ViewModels.BlockNavigationDirection.Last);
            e.Handled = true;
        }
    }
}

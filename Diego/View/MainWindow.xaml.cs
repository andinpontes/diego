using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Diego.ViewModel;

namespace Diego.View
{
    public partial class MainWindow : Window
    {
        private ColumnDefinition placeHolderColumnForSidebar;

        public MainWindow()
        {
            InitializeComponent();
            Closing += (s, e) => ViewModelLocator.Cleanup();

            placeHolderColumnForSidebar = new ColumnDefinition
            {
                SharedSizeGroup = "pinSpalte"
            };
        }

        public bool IsSidebarVisible
        {
            get { return sidebarLayer.Visibility == Visibility.Visible; }
        }

        public double SidebarWidth
        {
            get { return sidebarLayer.ColumnDefinitions[0].Width.Value; }
        }

        public Duration SidebarMoveDuration
        {
            get { return new Duration(TimeSpan.FromMilliseconds(200)); }
        }

        private void HandlePinning(object sender, RoutedEventArgs e)
        {
            PinSidebar();
        }
        private void HandleUnpinning(object sender, RoutedEventArgs e)
        {
            UnpinSidebar();
        }

        void HandleButtonExpMouseEnter(object sender, RoutedEventArgs e)
        {
            if (!IsSidebarVisible)
            {
                MoveSidebarIn();
            }
            else
            {
                UnpinSidebar();
                MoveSidebarOut();
            }
        }

        void HandleBackgroundLayerMouseDown(object sender, RoutedEventArgs e)
        {
            if (!btnPinIt.IsChecked.GetValueOrDefault()
              && IsSidebarVisible)
            {
                MoveSidebarOut();
            }
        }

        void ShowSidebar()
        {
            sidebarLayer.Visibility = Visibility.Visible;
        }

        void HideSidebar()
        {
            sidebarLayer.Visibility = Visibility.Collapsed;
        }

        void MoveSidebarIn()
        {
            double fromValue = -SidebarWidth;
            double toValue = 0.0;

            BeginSidebarAnimation(fromValue, toValue, null);
            ShowSidebar();
        }

        void MoveSidebarOut()
        {
            double fromValue = 0.0;
            double toValue = -SidebarWidth;

            BeginSidebarAnimation(fromValue, toValue, HideSidebar);
        }

        private void BeginSidebarAnimation(double fromValue, double toValue, Action completedAction)
        {
            DoubleAnimation ani = new DoubleAnimation(toValue, SidebarMoveDuration);
            sidebarLayerTransform.X = fromValue;

            if (completedAction != null)
            {
                ani.Completed += (sender, args) => completedAction();
            }

            sidebarLayerTransform.BeginAnimation(TranslateTransform.XProperty, ani);
        }

        void PinSidebar()
        {
            backgroundLayer.ColumnDefinitions.Insert(0, placeHolderColumnForSidebar);
            //pinImage.Source = new BitmapImage(new Uri(@"Images\pinned.bmp", UriKind.Relative));
            btnShowExplorer.Visibility = Visibility.Collapsed;
        }

        void UnpinSidebar()
        {
            backgroundLayer.ColumnDefinitions.Remove(placeHolderColumnForSidebar);
            //pinImage.Source = new BitmapImage(new Uri(@"Images\unpinned.bmp", UriKind.Relative));
            btnShowExplorer.Visibility = Visibility.Visible;
        }
    }
}
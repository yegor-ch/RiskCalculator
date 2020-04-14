using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RiskCalculator.Views
{
    /// <summary>
    /// Interaction logic for UserMenu.xaml
    /// </summary>
    public partial class UserMenu : UserControl
    {
        public UserMenu()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ExpanderMenu.Visibility = SubItems.Items.Count == 0 ? Visibility.Collapsed : Visibility.Visible;
            TextBlockItemMenu.Visibility = SubItems.Items.Count == 0 ? Visibility.Visible : Visibility.Collapsed;

            if(SubItems.Items.Count != 0)
            {
                MenuIcon.Margin = new Thickness(6, 5, 0, 0);
                MenuIcon.VerticalAlignment = VerticalAlignment.Top;
                ExpanderMenu.Margin = new Thickness(15, 0, 0, 0);
            }
        }
    }
}

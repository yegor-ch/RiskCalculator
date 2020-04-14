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
using System.Windows.Shapes;

namespace RiskCalculator.Views
{
    /// <summary>
    /// Interaction logic for ShellView.xaml
    /// </summary>
    public partial class ShellView : Window
    {
        public ShellView()
        {
            InitializeComponent();
            
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            int index = MenuItems.SelectedIndex;
            UpdateCursor(index);
        }

        private void UpdateCursor(int index)
        {
            TrainsitionigContentSlide.OnApplyTemplate();

            GridCursor.Margin = new Thickness(0, (100 + (60 * index)), 0, 0);
        }
    }
}

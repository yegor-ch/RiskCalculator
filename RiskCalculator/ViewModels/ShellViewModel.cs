using Caliburn.Micro;
using RiskCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RiskCalculator.ViewModels
{
    class MenuItem
    {
        public int Index { get; set; }
        public string Title { get; set; }
        public string Kind { get; set; }

        public object ViewModel { get; set; }
    }

    class ShellViewModel : Conductor<object>
    {
        public ShellViewModel()
        {
            // Initialaze menu items.
            MenuItems.Add(new MenuItem
            {
                Index = 0,
                Title = "Головна",
                Kind = "Home",
                ViewModel = new HomePageViewModel(),
            });

            MenuItems.Add(new MenuItem { Index = 1, Title = "Список вразливостей", Kind = "ViewList" });
            MenuItems.Add(new MenuItem { Index = 2, Title = "CVSS калькулятор", Kind = "Calculator" });
            MenuItems.Add(new MenuItem { Index = 3, Title = "Метрики", Kind = "ChartBar" });
            MenuItems.Add(new MenuItem { Index = 4, Title = "Оцінювання ризиків", Kind = "Security" });
            MenuItems.Add(new MenuItem { Index = 5, Title = "Результати оцінки", Kind = "Newspaper" });
        }

        private MenuItem _selectedMenuItem;

        public BindableCollection<MenuItem> _menuItems = new BindableCollection<MenuItem>();

        public BindableCollection<MenuItem> MenuItems
        {
            get
            {
                return _menuItems;
            }
            set 
            {
                _menuItems = value;
            } 
        }

        public MenuItem SelectedMunuItem
        {
            get { return _selectedMenuItem; }
            set
            {
                _selectedMenuItem = value;
                NotifyOfPropertyChange(() => SelectedMunuItem);

                if(SelectedMunuItem.ViewModel != null)
                    SwithToView(SelectedMunuItem.ViewModel);
            }
        }
        public void SwithToView(object viewModel)
        {
            ActivateItem(viewModel);
        }

        public void MouseDrag()
        {
            Window.GetWindow((Window)this.GetView()).DragMove();
        }

        public void CloseProgram()
        {
            Application.Current.Shutdown();
        }
    }
}

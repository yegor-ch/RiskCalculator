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

        public BindableCollection<SubItem> SubItems { get; set; }
    }

    class SubItem
    {
        public string Title { get; set; }
        public object ViewModel { get; set; }
    }

    class ShellViewModel : Conductor<object>
    {
        public ShellViewModel()
        {
            SubMenuItems.Add(new SubItem() { Title = "Сканувати систему" });
            SubMenuItems.Add(new SubItem() { Title = "Додати СVE з NVD" });


            // Initialaze menu items.
            MenuItems.Add(new MenuItem
            {
                Index = 0,
                Title = "Головна",
                Kind = "Home",
                ViewModel = new HomePageViewModel(),
                SubItems = SubMenuItems
            });

            MenuItems.Add(new MenuItem { Index = 1, Title = "Список вразливостей", Kind = "ViewList" });
            MenuItems.Add(new MenuItem { Index = 2, Title = "CVSS калькулятор", Kind = "Calculator" });
            MenuItems.Add(new MenuItem { Index = 3, Title = "Метрики", Kind = "ChartBar" });
            MenuItems.Add(new MenuItem { Index = 4, Title = "Оцінювання ризиків", Kind = "Security" });
            MenuItems.Add(new MenuItem { Index = 5, Title = "Результати оцінки", Kind = "Newspaper" });
        }

        private MenuItem _selectedMenuItem;
        private SubItem _selectedSubMenuItem;

        public BindableCollection<MenuItem> _menuItems = new BindableCollection<MenuItem>();
        public BindableCollection<SubItem> _subMenuItems = new BindableCollection<SubItem>();


        public BindableCollection<SubItem> SubMenuItems
        {
            get
            {
                return _subMenuItems;
            }
            set
            {
                _subMenuItems = value;
            }
        }

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

                if(value != null && SelectedMunuItem.ViewModel != null)
                    SwithToView(SelectedMunuItem.ViewModel);
            }
        }
        public SubItem SelectedSubMenuItem
        {
            get { return _selectedSubMenuItem; }
            set
            {
                _selectedSubMenuItem = value;
                NotifyOfPropertyChange(() => SelectedSubMenuItem);

                if (value != null && SelectedMunuItem.ViewModel != null)
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

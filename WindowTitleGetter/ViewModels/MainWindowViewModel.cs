namespace WindowTitleGetter.ViewModels
{
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Windows;
    using Prism.Commands;
    using Prism.Mvvm;
    using WindowTitleGetter.Models;

    public class MainWindowViewModel : BindableBase
    {
        private string title = "Window title getter";
        private ObservableCollection<WindowInfo> windows;
        private WindowInfo selectedItem;
        private WindowInfoDbContext dbContext = new WindowInfoDbContext();

        public MainWindowViewModel()
        {
            GetWindowTitleList();
            dbContext.Database.EnsureCreated();
        }

        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        public ObservableCollection<WindowInfo> Windows { get => windows; set => SetProperty(ref windows, value); }

        public WindowInfo SelectedItem { get => selectedItem; set => SetProperty(ref selectedItem, value); }

        public DelegateCommand CopyTextCommand => new DelegateCommand(() =>
        {
            if (SelectedItem != null)
            {
                Clipboard.SetText(SelectedItem.Title);
            }
        });

        public DelegateCommand ReloadCommand => new DelegateCommand(() =>
        {
            GetWindowTitleList();
        });

        private void GetWindowTitleList()
        {
            var processes = Process.GetProcesses();
            var windowInfos = processes
                .Where(process => process.MainWindowTitle.Length != 0)
                .Select(process => new WindowInfo() { Title = process.MainWindowTitle });

            Windows = new ObservableCollection<WindowInfo>(windowInfos);
        }
    }
}

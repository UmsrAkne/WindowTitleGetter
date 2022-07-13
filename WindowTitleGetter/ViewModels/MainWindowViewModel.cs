namespace WindowTitleGetter.ViewModels
{
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;
    using Prism.Mvvm;
    using WindowTitleGetter.Models;

    public class MainWindowViewModel : BindableBase
    {
        private string title = "Windot title getter";
        private ObservableCollection<WindowInfo> windows;

        public MainWindowViewModel()
        {
            GetWindowTitleList();
        }

        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        public ObservableCollection<WindowInfo> Windows { get => windows; set => SetProperty(ref windows, value); }

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

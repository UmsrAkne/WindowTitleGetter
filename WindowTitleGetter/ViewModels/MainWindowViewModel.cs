namespace WindowTitleGetter.ViewModels
{
    using System;
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

                //// クリップボードにテキストをセットした後、データベースにそのことを記録しておく

                var now = DateTime.Now;

                SelectedItem.LastCopiedDateTime = now;
                dbContext.Add(SelectedItem);
                dbContext.SaveChanges();

                var windowInfo = dbContext.WindowInfos.FirstOrDefault(w => w.Title == SelectedItem.Title);
                windowInfo.LastCopiedDateTime = now;
                dbContext.SaveChanges();
            }
        });

        public DelegateCommand ReloadCommand => new DelegateCommand(() =>
        {
            GetWindowTitleList();
        });

        public DelegateCommand ShowHistoryCommand => new DelegateCommand(() =>
        {
            var windowInfos = dbContext.WindowInfos.Where(info => true)
                .OrderByDescending(info => info.LastCopiedDateTime)
                .ThenByDescending(info => info.CreationDateTime);

            Windows = new ObservableCollection<WindowInfo>(windowInfos);
        });

        private void GetWindowTitleList()
        {
            var processes = Process.GetProcesses();
            var windowInfos = processes
                .Where(process => process.MainWindowTitle.Length != 0)
                .Select(process => new WindowInfo() { Title = process.MainWindowTitle });

            Windows = new ObservableCollection<WindowInfo>(windowInfos);

            foreach (var w in windowInfos)
            {
                dbContext.Add(w);
            }

            dbContext.SaveChanges();
        }
    }
}

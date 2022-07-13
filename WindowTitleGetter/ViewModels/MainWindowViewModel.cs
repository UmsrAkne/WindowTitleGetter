namespace WindowTitleGetter.ViewModels
{
    using System.Diagnostics;
    using System.Linq;
    using Prism.Mvvm;

    public class MainWindowViewModel : BindableBase
    {
        private string title = "Windot title getter";

        public MainWindowViewModel()
        {
        }

        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        private void GetWindowTitleList()
        {
            var processes = Process.GetProcesses();

            foreach (var p in processes.Where(process => process.MainWindowTitle.Length != 0))
            {
                System.Diagnostics.Debug.WriteLine($"MainWindowViewModel : {p.MainWindowTitle}");
            }
        }
    }
}

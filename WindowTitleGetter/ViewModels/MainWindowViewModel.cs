namespace WindowTitleGetter.ViewModels
{
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
    }
}

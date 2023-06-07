using BudgetTracker.ViewModel;

namespace BudgetTracker.View
{
    /// <summary>
    /// Interakční logika pro MainMenuWindow.xaml
    /// </summary>
    public partial class MainMenuWindow
    {
        public MainMenuWindow()
        {
            InitializeComponent();
            DataContext = new MainMenuViewModel();
        }
    }
}

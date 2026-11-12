using pcrespoT5.Repositories;

namespace pcrespoT5
{
    public partial class App : Application
    {
        public static PersonRepository _personRepo { get; set; } = null!;

        public App(PersonRepository repo)
        {
            InitializeComponent();
            _personRepo = repo;
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new Views.vPersona()) { Title = "pcrespoT5" };
        }
    }
}

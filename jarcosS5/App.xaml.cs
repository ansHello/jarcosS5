using jarcosS5.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace jarcosS5
{
    public partial class App : Application
    {
        public static PersonaRepository _personRepo {  get; set; }
        public App(PersonaRepository person)
        {
            InitializeComponent();
            _personRepo = person;
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new Views.vPersona());
        }
    }
}
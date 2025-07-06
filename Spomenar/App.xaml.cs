using Spomenar.Data;

namespace Spomenar {
    public partial class App : Application {
        public static AppDatabase? Database { get; private set; }

        public App() {
            InitializeComponent();
            Database = new AppDatabase(Path.Combine(FileSystem.AppDataDirectory, "spomenar.db3"));
        }

        protected override Window CreateWindow(IActivationState? activationState) {
            return new Window(new AppShell());
        }
    }
}
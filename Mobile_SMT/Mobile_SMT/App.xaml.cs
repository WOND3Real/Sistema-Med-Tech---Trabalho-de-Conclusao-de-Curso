namespace Mobile_SMT
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            // Cria a janela principal com o conteúdo do AppShell
            var window = new Window(new AppShell());

            // Ajusta a largura e altura da janela para simular um dispositivo Android
            window.Width = 425;  // Largura típica de celular
            window.Height = 900; // Altura típica de celular

            // Retorna a janela ajustada
            return window;
        }
    }
}
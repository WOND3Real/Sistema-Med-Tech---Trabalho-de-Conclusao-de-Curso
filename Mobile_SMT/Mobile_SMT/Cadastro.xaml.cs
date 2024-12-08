namespace Mobile_SMT;

public partial class Cadastro : ContentPage
{
	public Cadastro()
	{
		InitializeComponent();
	}
   private void OnRegisterClicked(object sender, EventArgs e)
    {
        string firstName = FirstNameEntry.Text;
        string lastName = LastNameEntry.Text;
        string phone = PhoneEntry.Text;
        string cpf = CpfEntry.Text;
        string gender = GenderEntry.Text;
        string email = EmailEntry.Text;
        string password = RegisterPasswordEntry.Text;
        string confirmPassword = ConfirmPasswordEntry.Text;

        if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) ||
            string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(cpf) ||
            string.IsNullOrWhiteSpace(gender) || string.IsNullOrWhiteSpace(email) ||
            string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmPassword))
        {
            DisplayAlert("Erro", "Todos os campos devem ser preenchidos!", "OK");
            return;
        }

        if (password != confirmPassword)
        {
            DisplayAlert("Erro", "As senhas não coincidem!", "OK");
            return;
        }

        DisplayAlert("Sucesso", "Usuário cadastrado com sucesso!", "OK");
        Navigation.PopAsync(); 
    }

    private void OnLoginTapped(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}
namespace Mobile_SMT.Services
{
    public class UserSessionService
    {
        private string _cpfPaciente;

        public void SetCpfPaciente(string cpf)
        {
            _cpfPaciente = cpf;
        }

        public string GetCpfPaciente()
        {
            return _cpfPaciente;
        }
    }
}

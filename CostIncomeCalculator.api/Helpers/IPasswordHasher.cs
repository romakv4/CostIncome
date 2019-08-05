namespace cost_income_calculator.api.Helpers
{
    public interface IPasswordHasher
    {
        void CreatePasswordHash(string password, out string passwordHash);

        bool VerifyPasswordHash(string password, string passwordHash);
    }
}
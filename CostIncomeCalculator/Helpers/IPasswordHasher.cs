namespace CostIncomeCalculator.Helpers
{
    /// <summary>
    /// IPasswordHasher interface.
    /// </summary>
    public interface IPasswordHasher
    {
        /// <summary>
        /// Create hash of password. See implementation here <see cref="PasswordHasher.CreatePasswordHash" />.
        /// </summary>
        /// <param name="password">string</param>
        /// <param name="passwordHash">string</param>
        void CreatePasswordHash(string password, out string passwordHash);

        /// <summary>
        /// Verify password hash. See implementation here <see cref="PasswordHasher.VerifyPasswordHash" />.
        /// </summary>
        /// <param name="password">string</param>
        /// <param name="passwordHash">string</param>
        /// <returns>True if password valid, else false.</returns>
        bool VerifyPasswordHash(string password, string passwordHash);
    }
}
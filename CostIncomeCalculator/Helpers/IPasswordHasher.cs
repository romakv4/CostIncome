namespace cost_income_calculator.Helpers
{
    /// <summary>
    /// IPasswordHasher interface.
    /// </summary>
    public interface IPasswordHasher
    {
        /// <summary>
        /// Create hash of password.
        /// </summary>
        /// <param name="password">string</param>
        /// <param name="passwordHash">string</param>
        void CreatePasswordHash(string password, out string passwordHash);

        /// <summary>
        /// Verify password hash.
        /// </summary>
        /// <param name="password">string</param>
        /// <param name="passwordHash">string</param>
        /// <returns>True if password valid, else false.</returns>
        bool VerifyPasswordHash(string password, string passwordHash);
    }
}
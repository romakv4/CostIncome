namespace cost_income_calculator.Helpers
{
    /// <summary>
    /// PasswordHasher class.
    /// Contains methods to work with user password.
    /// </summary>
    public class PasswordHasher : IPasswordHasher
    {
        /// <summary>
        /// Get random salt from BCrypt.
        /// </summary>
        /// <returns>BCrypt random salt</returns>
        private string GetRandomSalt()
        {
            return BCrypt.Net.BCrypt.GenerateSalt(12);
        }

        /// <summary>
        /// Create hash of password.
        /// </summary>
        /// <param name="password">string</param>
        /// <param name="passwordHash">string</param>
        public void CreatePasswordHash(string password, out string passwordHash)
        {
            passwordHash = BCrypt.Net.BCrypt.HashPassword(password, GetRandomSalt());
        }

        /// <summary>
        /// Verify password hash.
        /// </summary>
        /// <param name="password">string</param>
        /// <param name="passwordHash">string</param>
        /// <returns>True if password valid, else false.</returns>
        public bool VerifyPasswordHash(string password, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }
    }
}
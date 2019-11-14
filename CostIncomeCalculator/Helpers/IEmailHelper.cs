namespace CostIncomeCalculator.Helpers
{
    /// <summary>
    /// Email helper interface.
    /// See implementation here <see cref="EmailHelper"/>
    /// </summary>
    public interface IEmailHelper
    {
        /// <summary>
        /// Send reset password email.
        /// See implementation here <see cref="EmailHelper.SendResetPasswordEmail"/>.
        /// </summary>
        /// <param name="email">User email</param>
        /// <param name="newPassword">New user password</param>
        /// <returns></returns>
        void SendResetPasswordEmail(string email, string newPassword);
    }
}
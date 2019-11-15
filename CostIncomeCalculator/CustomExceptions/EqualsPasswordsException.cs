namespace CostIncomeCalculator.CustomExceptions
{
    /// <summary>
    /// Equals passwords exception class.
    /// </summary>
    [System.Serializable]
    public class EqualsPasswordsException : System.Exception
    {
        /// <summary>
        /// Old and new password equality exception.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <returns></returns>
        public EqualsPasswordsException(string message) : base(message) { }
    }
}
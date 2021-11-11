namespace Moneysystem.Utilities
{
    public static class PasswordChecker
    {
        /// <summary>
        /// Checks that the password contains at least one number and a letter
        /// </summary>
        /// <param name="password">The password to check</param>
        /// <returns>True if the password is ok, false if not</returns>
        public static bool CheckPassword(string password)
        {
            bool containsNumber = false, containsLetter = false;
            foreach (char c in password)
            {
                if (!char.IsDigit(c) && !char.IsLetter(c))
                {
                    return false;   // returns false if c is another 
                                    // character than letter or number, 
                                    // such as whitespace or symbol
                }
                if (!(containsNumber && containsLetter))
                {
                    if (char.IsDigit(c))
                    {
                        containsNumber = true;
                    }
                    else if (char.IsLetter(c))
                    {
                        containsLetter = true;
                    }
                }
            }
            return containsNumber && containsLetter;
        }

    }
}
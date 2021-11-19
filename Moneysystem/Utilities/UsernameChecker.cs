namespace Moneysystem.Utilities
{
    public static class UsernameChecker
    {
        /// <summary>
        /// Checks that the username contains at least one number and a letter and is at least of length 3
        /// </summary>
        /// <param name="username">The username to check</param>
        /// <returns>True if the username is ok, false if not</returns>
        public static bool CheckUsername(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return false;
            }
            bool containsNumber = false, containsLetter = false;
            foreach (char c in username)
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
using System;
using System.Collections.Generic;

namespace Moneysystem.Utilities
{
    public static class Roles
    {
        public readonly static List<string> ListOfRoles = new()
        {
            "Executive",
            "Manager",
            "Operations",
            "Production",
            "Administrator",
            "User",
            "Minesweeper"
        };
        /// <summary>
        /// Checks if an assigned role is the same as the predefined ones.
        /// </summary>
        /// <param name="role"></param>
        /// <returns>True if it is, false if not</returns>
        public static bool ValidateRole(string role)
        {
            foreach (var item in ListOfRoles)
            {
                if (role.Equals(item))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Sets start salary for a given role
        /// </summary>
        /// <param name="role">The role to set the salary for</param>
        /// <returns>The salary, or 0 if it is an invalid role</returns>
        public static int SetBasicSalary(string role)
        {
            int result = 0;
            if (ValidateRole(role))
            {
                switch (role)
                {
                    case "Executive":
                        result = 50000;
                        break;
                    case "Manager":
                        result = 45000;
                        break;
                    case "Operations":
                        result = 40000;
                        break;
                    case "Production":
                        result = 38000;
                        break;
                    case "Administrator":
                        result = 500;
                        break;
                    case "User":
                        result = 100;
                        break;
                    case "Minesweeper":
                        result = 10;
                        break;
                }
            }
            return result;
        }


    }
}
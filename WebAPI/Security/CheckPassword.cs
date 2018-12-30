using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace WebAPI.Security
{
    public class CheckPassword
    {
        public static bool TestPassword(string passwordText, int minimumLength = 6, int maximumLength = 12, int minimumNumbers = 1, int minimumChar = 1, int minimumSpecialCharacters=1)
        {
            //Assumes that special characters are anything except upper and lower case letters and digits
            //Assumes that ASCII is being used (not suitable for many languages)

            int letters = 0;
            int digits = 0;
            int specialCharacters = 0;

            //Make sure there are enough total characters
            if (passwordText.Length < minimumLength)
            {
                
                return false;
            }
            //Make sure there are enough total characters
            if (passwordText.Length > maximumLength)
            {
                
                return false;
            }

            foreach (var ch in passwordText)
            {
                if (char.IsLetter(ch)) letters++; //increment letters
                if (char.IsDigit(ch)) digits++; //increment digits

                //Test for only letters and numbers...
                if (!((ch > 47 && ch < 58) || (ch > 64 && ch < 91) || (ch > 96 && ch < 123)))
                {
                    specialCharacters++;
                }
            }

            //Make sure there are enough digits
            if (digits < minimumNumbers)
            {
                return false;
            }
            //Make sure there are enough special characters -- !(a-zA-Z0-9)
            if (specialCharacters < minimumSpecialCharacters)
            {
                //ShowError("You must have at least " + minimumSpecialCharacters + " special characters (like @,$,%,#) in your password.");
                return false;
            }

            return true;
        }
    }
}
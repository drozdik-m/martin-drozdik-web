using System;
using System.Collections.Generic;
using System.Text;
using Bonsai.RazorPages.User.Services.LanguageDictionary;
using Bonsai.Services.LanguageDictionary.Static.Languages;

namespace Bonsai.RazorPages.Error.Services.LanguageDictionary.Languages
{
    public class CzechUserLanguageDictionary: CzechStaticLanguageDictionary, IUserLanguageDictionary
    {

        public CzechUserLanguageDictionary()
            :base(dictionary)
        {

        }

        public static readonly Dictionary<string, string> dictionary = new()
        {
            //USER
            { UserLanguageDictionary.RegisterHeading, "Registrace" },
            { UserLanguageDictionary.RegisterButton, "Registrovat" },
            { UserLanguageDictionary.LoginHeading, "Přihlášení" },
            { UserLanguageDictionary.LoginButton, "Přihlásit" },
            { UserLanguageDictionary.Email, "Email" },
            { UserLanguageDictionary.EmailPlaceholder, "jan@novak.cz" },
            { UserLanguageDictionary.EmailFormatError, "Email není ve správném formátu" },
            { UserLanguageDictionary.EmailNotEmpty, "Email musí být vyplněn" },
            { UserLanguageDictionary.Password, "Heslo" },
            { UserLanguageDictionary.PasswordNotEmpty, "Heslo musí být vyplněno" },
            { UserLanguageDictionary.PasswordConfirm, "Potvrzení hesla" },
            { UserLanguageDictionary.PasswordConfirmSame, "Heslo a jeho potvrzení se musí shodovat" },
            { UserLanguageDictionary.DeniedHeading, "Přístup zakázan" },
            { UserLanguageDictionary.DeniedText, "Bohužel, k tomuto zdroji nemáte přístup" },
            { UserLanguageDictionary.ReturnButton, "Zpět" },
            { UserLanguageDictionary.WebButton, "Na web" },
            { UserLanguageDictionary.ChangePasswordHeading, "Změna hesla" },
            { UserLanguageDictionary.ChangePasswordButton, "Změnit heslo" },
            { UserLanguageDictionary.OldPassword, "Staré heslo" },
            { UserLanguageDictionary.OldPasswordNotEmpty, "Staré heslo musí být vyplněno" },
            { UserLanguageDictionary.NewPassword, "Nové heslo" },
            { UserLanguageDictionary.NewPasswordNotEmpty, "Nové heslo musí být vyplněno" },
            { UserLanguageDictionary.ConfirmNewPasswordLabel, "Potvrzení nového hesla" },
            { UserLanguageDictionary.ConfirmNewPasswordSame, "Heslo a jeho potvrzení se musí shodovat" },
        };
    }
}

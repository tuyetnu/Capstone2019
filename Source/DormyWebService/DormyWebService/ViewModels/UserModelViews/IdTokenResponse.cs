namespace DormyWebService.ViewModels.UserModelViews
{
    //What returns when id token is returned from Google API
    public class IdTokenResponse
    {
        public string Email;
        //The Issuer Identifier for the Issuer of the response. Always https://accounts.google.com or accounts.google.com for Google ID tokens.
        public string Iss;
        //The time the ID token was issued, represented in Unix time (integer seconds).
        public string Iat;
        //The time the ID token expires, represented in Unix time (integer seconds).
        public string Exp;
        //True if the user's e-mail address has been verified; otherwise false.
        public bool Email_Verified;

    }
}
namespace Example.Services.TokenValidation
{
    using Interfaces.TokenValidation;

    public class TokenValidator : ITokenValidator
    {
        public bool Validate(string token)
        {
            const string validToken = "12345";

            if (string.IsNullOrWhiteSpace(token))
            {
                return false;
            }

            return token == validToken;
        }
    }
}
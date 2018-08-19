namespace Example.Services.Interfaces.TokenValidation
{
    public interface ITokenValidator
    {
        bool Validate(string token);
    }
}
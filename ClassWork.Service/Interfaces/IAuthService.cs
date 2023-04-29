namespace ClassWork.Service.Interfaces
{
    public interface IAuthService
    {
        ValueTask<string> GenerateTokenAsync(string email, string password);
    }
}

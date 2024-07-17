namespace GustoGlide.Web.Service.IService
{
    // для работ с токеном, получаемым при регистрации. Работы с токеном производятся на этапе входа в систему.
    public interface ITokenProvider 
    {
        void SetToken(string token);
        string? GetToken();
        void ClearToken();

    }
}

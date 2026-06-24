namespace Api_Venda_Ingressos.BoundedContext.Auth.Application.DTOs.Response
{
    public class LoginResponse
    {
        public string AccessToken { get; set; }
        public DateTime ExpiresAt { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }

        public LoginResponse(string accessToken, DateTime expiresAt, string userName, string role)
        {
            AccessToken = accessToken;
            ExpiresAt = expiresAt;
            UserName = userName;
            Role = role;
        }
    }
}

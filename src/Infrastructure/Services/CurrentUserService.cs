using Application.Services;

namespace Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService( )
        {
        }

        public string UserName
          => "admin";
    }
}

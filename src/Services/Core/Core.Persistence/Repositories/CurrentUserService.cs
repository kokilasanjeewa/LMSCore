using Core.Application.Interfaces.Repositories;


namespace Core.Persistence.Repositories
{
    public class CurrentUserService : ICurrentUserService
    {
        public int UserSerialID { get; set; }
        public long LoginLogSerialID { get; set; }
    }
}

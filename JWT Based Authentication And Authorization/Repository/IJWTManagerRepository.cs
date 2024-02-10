using JWT_Based_Authentication_And_Authorization.Model;

namespace JWT_Based_Authentication_And_Authorization.Repository
{
    public interface IJWTManagerRepository
    {
        Tokens Authenticate(Users users);
    }
}

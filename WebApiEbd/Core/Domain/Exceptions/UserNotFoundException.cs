namespace WebApiEbd.Core.Domain.Exceptions
{
    public class UserNotFoundException(int id) : Exception($"Usuario con id {id} no fue encontrado.")
    {
    }
}
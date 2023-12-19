using AuthMicroservice.DataAccess.Models;

namespace AuthMicroservice.BusinessLogic.Interfaces
{
    public interface IUserContext
    {
        User CurrentUser { get; set; }
    }
}

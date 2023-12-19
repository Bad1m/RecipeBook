using AuthMicroservice.BusinessLogic.Interfaces;
using AuthMicroservice.DataAccess.Models;

namespace AuthMicroservice.BusinessLogic.Models
{
    public class UserContext : IUserContext
    {
        public User CurrentUser { get; set; }
    }
}
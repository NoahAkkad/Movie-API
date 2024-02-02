using Microsoft.AspNetCore.Identity;


namespace MovieApi.Models
{
    public class CustomUser : IdentityUser
    {
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}

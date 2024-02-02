using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace MovieApi.Models
{
	public class Movie
	{
		[Key]
		public int Id { get; set; }
		[Required]

        public string Title { get; set; }

        public int ReleaseYear { get; set; }

        [ValidateNever]
        public ICollection<Review> Reviews { get; set; }
    }
}


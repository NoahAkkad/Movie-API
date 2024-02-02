using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Text.Json.Serialization;

namespace MovieApi.Models
{
	public class Review
	{
        [Key]

        public int Id { get; set; }

        [Required]

        public string Rating { get; set; }

        public string Comment { get; set; }

        [ValidateNever]

        public string MovieName { get; set; }

        [ValidateNever]
        [ForeignKey("Movie")]
        public int MovieId { get; set; }

        [ValidateNever]
        [JsonIgnore]
        public Movie Movie { get; set; }
    }
}


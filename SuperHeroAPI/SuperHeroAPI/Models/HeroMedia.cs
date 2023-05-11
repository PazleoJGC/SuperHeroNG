using System.ComponentModel.DataAnnotations;

namespace SuperHeroAPI.Models
{
    public class HeroMedia
    {
        [Key]
        public int          Id { get; set; }
        public MediaType    Type { get; set; }
        public string       Title { get; set; } = string.Empty;
        public DateTime     ReleaseDate { get; set; }
        public ICollection<SuperHero> Characters { get; set; }

        public enum MediaType
        {
            Unknown = 0,
            Comic,
            Game,
            Movie,
            Novel
        }
    }
}

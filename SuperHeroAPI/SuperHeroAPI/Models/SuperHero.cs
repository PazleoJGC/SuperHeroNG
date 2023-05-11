using System.ComponentModel.DataAnnotations;

namespace SuperHeroAPI.Models
{
    public class SuperHero
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Place { get; set; } = string.Empty;
        public virtual ICollection<HeroMedia> Media { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Kaiheila.OneBot.Cq.Database
{
    public class CqAsset
    {
        [Key]
        [Required]
        public string Url { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Path { get; set; }
    }
}

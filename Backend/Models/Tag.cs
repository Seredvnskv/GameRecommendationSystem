namespace Backend.Models
{
    public class Tag
    {
        public int TagId { get; set; }

        public string TagName { get; set; } = string.Empty;

        public List<GameTag> GameTags { get; set; } = [];
    }
}

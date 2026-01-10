using Octokit;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace PortfolioSite.Models
{
    [Table("repositories")]
    public class Repository : BaseModel
    {
        [PrimaryKey("github_id")]
        public long GithubId { get; set; }

        [Column("name")]
        public string Name { get; set; } = null!;

        [Column("description")]
        public string? Description { get; set; }

        [Column("html_url")]
        public string? HtmlUrl { get; set; }

        [Column("my_project_story")]
        public string? MyProjectStory { get; set; }

        [Column("my_project_story_en")]
        public string? MyProjectStoryEn { get; set; } // Yeni

        [Column("is_visible")]
        public bool IsVisible { get; set; } // Sitede gözüksün mü?

        [Column("is_private")]
        public bool IsPrivate { get; set; } // İstatistik için

        [Column("last_activity_at")]
        public DateTime LastActivityAt { get; set; }

        // İlişkiler (Commits tablosuna bağlantı)
        [Reference(typeof(Commit))]
        public List<Commit> Commits { get; set; } = new();
    }
}
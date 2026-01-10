using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace PortfolioSite.Models
{
    [Table("commits")]
    public class Commit : BaseModel
    {
        [PrimaryKey("commit_hash")]
        public string CommitHash { get; set; } = null!;

        [Column("repo_id")]
        public long RepoId { get; set; }

        [Column("message")]
        public string? Message { get; set; }

        [Column("committed_at")]
        public DateTime CommittedAt { get; set; }

        [Column("my_journal_entry")]
        public string? MyJournalEntry { get; set; }

        [Column("my_journal_entry_en")]
        public string? MyJournalEntryEn { get; set; } // Yeni
    }
}
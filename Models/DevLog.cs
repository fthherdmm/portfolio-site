using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace PortfolioSite.Models
{
    [Table("dev_logs")]
    public class DevLog : BaseModel
    {
        [PrimaryKey("log_date")]
        public DateTime LogDate { get; set; }

        [Column("content")]
        public string Content { get; set; } = ""; // Türkçe (Varsayılan)

        [Column("content_en")] 
        public string? ContentEn { get; set; } // İngilizce (Yeni)

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}
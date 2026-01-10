using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace PortfolioSite.Models
{
    [Table("contact_messages")]
    public class ContactMessage : BaseModel
    {
        [PrimaryKey("id")]
        public long Id { get; set; }

        [Column("sender_name")]
        public string SenderName { get; set; } = "";

        [Column("sender_email")]
        public string SenderEmail { get; set; } = "";

        [Column("message_text")]
        public string MessageText { get; set; } = "";

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}

using Supabase;
using Supabase.Gotrue;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System;

namespace YourApp.Models
{
    [Table("todos")]
    public class Todo : BaseModel
    {
        [PrimaryKey("id", false)]
        public Guid Id { get; set; }

        [Column("user_id")]
        public Guid UserId { get; set; }

        [Column("title")]
        public string Title { get; set; }

        [Column("is_complete")]
        public bool IsComplete { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}

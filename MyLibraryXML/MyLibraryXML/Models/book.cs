namespace MyLibrary.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class book
    {
        [Key]
        public int book_id { get; set; }

        [Required]
        [StringLength(50)]
        public string title { get; set; }

        public string description { get; set; }

        public string content { get; set; }

        public DateTime? last_edited { get; set; }
    }
}

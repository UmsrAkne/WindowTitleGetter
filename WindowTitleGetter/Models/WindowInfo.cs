namespace WindowTitleGetter.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class WindowInfo
    {
        [Key] // ID などの名前のプロパティがあるときは自動で主キーに指定されるらしい。
        [Required]
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime CreationDateTime { get; set; } = DateTime.Now;

        public DateTime LastCopiedDateTime { get; set; }
    }
}

namespace WindowTitleGetter.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Prism.Mvvm;

    public class WindowInfo : BindableBase
    {
        private DateTime lastCopiedDateTime;

        [Key] // ID などの名前のプロパティがあるときは自動で主キーに指定されるらしい。
        [Required]
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime CreationDateTime { get; set; } = DateTime.Now;

        public DateTime LastCopiedDateTime { get => lastCopiedDateTime; set => SetProperty(ref lastCopiedDateTime, value); }
    }
}

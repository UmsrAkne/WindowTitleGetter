namespace WindowTitleGetter.Models
{
    using System.Data.SQLite;
    using System.IO;
    using System.Linq;
    using Microsoft.Data.Sqlite;
    using Microsoft.EntityFrameworkCore;

    public class WindowInfoDbContext : DbContext
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public DbSet<WindowInfo> WindowInfos { get; set; }

        private string DbFileName { get; set; } = "windowInfos.sqlite";

        public void Add(WindowInfo info)
        {
            if (!WindowInfos.Any(w => w.Title == info.Title))
            {
                WindowInfos.Add(info);
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!File.Exists(DbFileName))
            {
                SQLiteConnection.CreateFile(DbFileName); // ファイルが存在している場合は問答無用で上書き。
            }

            var connectionString = new SqliteConnectionStringBuilder { DataSource = DbFileName }.ToString();
            optionsBuilder.UseSqlite(new SQLiteConnection(connectionString));
        }
    }
}
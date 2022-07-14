namespace WindowTitleGetter.Models
{
    using System.Data.SQLite;
    using System.IO;
    using System.Linq;
    using Microsoft.Data.Sqlite;
    using Microsoft.EntityFrameworkCore;

    public class WindowInfoDbContext : DbContext
    {
        public DbSet<WindowInfo> WindowInfos { get; set; }

        private string DBFileName { get; set; } = "windowInfos.sqlite";

        public void Add(WindowInfo info)
        {
            if (!WindowInfos.Any(w => w.Title == info.Title))
            {
                WindowInfos.Add(info);
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!File.Exists(DBFileName))
            {
                SQLiteConnection.CreateFile(DBFileName); // ファイルが存在している場合は問答無用で上書き。
            }

            var connectionString = new SqliteConnectionStringBuilder { DataSource = DBFileName }.ToString();
            optionsBuilder.UseSqlite(new SQLiteConnection(connectionString));
        }
    }
}

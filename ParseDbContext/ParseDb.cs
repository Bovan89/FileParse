using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FileParse.ParseDbContext
{
    public class ParseDb : DbContext
    {
        public string ConnectionString { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }

        public ParseDb(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public DbSet<Good> Goods { get; set; }
    }

    [Table("Goods")]
    public class Good
    {
        [Key]
        public int Id { get; set; }
        public int? SoldTo { get; set; }
        public string CustName { get; set; }
        public int? ShipTo { get; set; }
        public string ShipToName { get; set; }
        public string OrderType { get; set; }
        public int? Dv { get; set; }
        public long? OrderNum { get; set; }
        public string Material { get; set; }
        public string MatDes { get; set; }
        public decimal? Size { get; set; }
        public decimal? AltSize { get; set; }
        public int? OnOrdQty { get; set; }
        public int? ShipQty { get; set; }
        public int? RejecQty { get; set; }
        public string SourceFolder { get; set; }
    }
}

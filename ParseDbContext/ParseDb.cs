using FileParse.Model;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
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
        public string SoldTo { get; set; }
        public string CustName { get; set; }
        public string ShipTo { get; set; }
        public string ShipToName { get; set; }
        public string OrderType { get; set; }
        public string Dv { get; set; }
        public string OrderNum { get; set; }
        public string Material { get; set; }
        public string MatDes { get; set; }
        public string Size { get; set; }
        public string AltSize { get; set; }        
        public string SourceFolder { get; set; }

        public Good()
        {

        }

        public static Good Create(GoodData goodData)
        {
            return new Good
            {
                SoldTo = goodData.SoldTo,
                CustName = goodData.CustName,
                ShipTo = goodData.ShipTo,
                ShipToName = goodData.ShipToName,
                OrderType = goodData.OrderType,
                Dv = goodData.Dv,
                OrderNum = goodData.OrderNum,
                Material = goodData.Material,
                MatDes = goodData.MatDes,
                Size = goodData.Size,
                AltSize = goodData.AltSize,
                SourceFolder = goodData.SourceFolder
            };
        }
    }
}

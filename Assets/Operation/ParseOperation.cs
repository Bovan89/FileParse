using FileParse.ParseDbContext;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FileParse.Assets.Operation
{
    public class ParseOperation : IOperation
    {
        private List<Good> Goods;

        public string FilePath { get; set; }

        private string FolderPath { get; set; }

        public bool IsComplete { get; set; }

        public ParseOperation(string filePath, List<Good> goods)
        {
            FilePath = filePath;
            FolderPath = Path.GetDirectoryName(FilePath);
            Goods = goods;
        }

        public void Do()
        {
            using (var package = new ExcelPackage(new FileInfo(FilePath)))
            {
                var worksheet = package.Workbook.Worksheets[0];
                var rowCount = worksheet.Dimension.Rows;

                for (int row = 2; row <= rowCount; row++)
                {
                    var good = new Good
                    {
                        SoldTo = worksheet.Cells[row, 1].ValueInt(),
                        CustName = worksheet.Cells[row, 2].Value(),
                        ShipTo = worksheet.Cells[row, 3].ValueInt(),
                        ShipToName = worksheet.Cells[row, 4].Value(),
                        OrderType = worksheet.Cells[row, 5].Value(),
                        Dv = worksheet.Cells[row, 6].ValueInt(),
                        OrderNum = worksheet.Cells[row, 7].ValueLong(),
                        Material = worksheet.Cells[row, 8].Value(),
                        MatDes = worksheet.Cells[row, 9].Value(),
                        Size = worksheet.Cells[row, 10].ValueDecimal(),
                        AltSize = worksheet.Cells[row, 11].ValueDecimal(),
                        OnOrdQty = worksheet.Cells[row, 12].ValueInt(),
                        ShipQty = worksheet.Cells[row, 13].ValueInt(),
                        RejecQty = worksheet.Cells[row, 14].ValueInt(),
                        SourceFolder = FolderPath
                    }.Check();

                    AddGood(good);                                        
                }
            }

            IsComplete = true;
        }

        private void AddGood(Good good)
        {
            Good existGood = Goods.FirstOrDefault(g => g.OrderNum == good.OrderNum && g.Material == good.Material && g.Size == good.Size);

            if (existGood != null)
            {
                good.OnOrdQty += existGood.OnOrdQty;
                good.ShipQty += existGood.ShipQty;                

                foreach (var item in Goods.Where(g => g.OrderNum == good.OrderNum && g.Material == good.Material && g.Size == good.Size))
                {
                    item.OnOrdQty = good.OnOrdQty;
                    item.ShipQty = good.ShipQty;
                }
            }

            Goods.Add(good);
        }

        public void Back()
        {
            IsComplete = false;
        }
    }

    public static class ExcelExtension
    {
        public static string Value(this ExcelRange excelRange)
        {            
            return excelRange.Value.ToString();
        }

        public static int ValueInt(this ExcelRange excelRange)
        {
            return Convert.ToInt32(excelRange.Value.ToString());
        }

        public static long ValueLong(this ExcelRange excelRange)
        {
            return Convert.ToInt64(excelRange.Value.ToString());
        }

        public static decimal ValueDecimal(this ExcelRange excelRange)
        {
            return Convert.ToDecimal(excelRange.Value.ToString());
        }
    }
}

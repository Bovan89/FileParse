using FileParse.Model;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileParse.Assets.Operation
{
    public class ParseOperation : IOperation
    {
        private List<GoodData> GoodData { get; set; }

        private string FilePath { get; set; }        

        private string SourceFolder { get; set; }

        public ParseOperation(string filePath, List<GoodData> goodData, string sourceFolder)
        {
            FilePath = filePath;

            GoodData = goodData;

            SourceFolder = sourceFolder;
        }

        public void Do()
        {
            using (var package = new ExcelPackage(new FileInfo(FilePath)))
            {
                var worksheet = package.Workbook.Worksheets[0];
                var rowCount = worksheet.Dimension.Rows;

                for (int row = 2; row <= rowCount; row++)
                {
                    var goodData = new GoodData
                    {
                        SoldTo = worksheet.Cells[row, 1].Value(),
                        CustName = worksheet.Cells[row, 2].Value(),
                        ShipTo = worksheet.Cells[row, 3].Value(),
                        ShipToName = worksheet.Cells[row, 4].Value(),
                        OrderType = worksheet.Cells[row, 5].Value(),
                        Dv = worksheet.Cells[row, 6].Value(),
                        OrderNum = worksheet.Cells[row, 7].Value(),
                        Material = worksheet.Cells[row, 8].Value(),
                        MatDes = worksheet.Cells[row, 9].Value(),
                        Size = worksheet.Cells[row, 10].Value(),
                        AltSize = worksheet.Cells[row, 11].Value(),
                        OnOrdQty = worksheet.Cells[row, 12].ValueInt(),
                        ShipQty = worksheet.Cells[row, 13].ValueInt(),
                        RejecQty = worksheet.Cells[row, 14].ValueInt(),
                        SourceFolder = SourceFolder
                    }.Check();

                    AddGood(goodData);                                        
                }
            }
        }

        private void AddGood(GoodData goodData)
        {
            GoodData existGood = GoodData.FirstOrDefault(g => g.OrderNum == goodData.OrderNum && g.Material == goodData.Material && g.Size == goodData.Size);

            if (existGood != null)
            {
                goodData.OnOrdQty += existGood.OnOrdQty;
                goodData.ShipQty += existGood.ShipQty;                

                foreach (var item in GoodData.Where(g => g.OrderNum == goodData.OrderNum && g.Material == goodData.Material && g.Size == goodData.Size))
                {
                    item.OnOrdQty = goodData.OnOrdQty;
                    item.ShipQty = goodData.ShipQty;
                }
            }

            GoodData.Add(goodData);
        }
    }

    static class ExcelExtension
    {
        public static string Value(this ExcelRange excelRange)
        {            
            return excelRange.Value.ToString();
        }

        public static int ValueInt(this ExcelRange excelRange)
        {
            return Convert.ToInt32(excelRange.Value.ToString());
        }
    }
}

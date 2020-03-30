using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileParse.Model
{
    public class GoodData
    {
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
        public int OnOrdQty { get; set; }
        public int ShipQty { get; set; }
        public int RejecQty { get; set; }
        public string SourceFolder { get; set; }

        public GoodData Check()
        {            
            if (OnOrdQty < 0)
            {
                throw new Exception("Данные поля OnOrdQty не правильного формата");
            }
            if (ShipQty < 0)
            {
                throw new Exception("Данные поля ShipQty не правильного формата");
            }
            if (RejecQty < 0)
            {
                throw new Exception("Данные поля RejecQty не правильного формата");
            }

            return this;
        }
    }
}

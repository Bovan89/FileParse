using FileParse.Assets.Task;
using FileParse.Model;
using FileParse.ParseDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileParse.Assets.Operation
{
    public class SaveOperation : IOperation
    {
        private GoodData GoodData { get; set; }

        private int Count { get; set; }

        public bool IsComplete { get; set; }

        public ParseDb ParseDb { private get; set; }

        public SaveOperation(GoodData goodData, int count)
        {
            GoodData = goodData;
            Count = count;
        }

        public void Do()
        {            
            if (ParseDb != null)
            {
                List<Good> simpleGoods = ParseDb.Goods.Where(g => 
                    g.OrderNum == GoodData.OrderNum && g.Material == GoodData.Material && g.Size == GoodData.Size
                    ).ToList();

                UpdateGoods(simpleGoods);

                InsertGoods(simpleGoods);
            }
        }

        private void InsertGoods(List<Good> simpleGoods)
        {
            if (simpleGoods?.Count < Count)
            {                
                for (int i = 0; i < (Count - simpleGoods.Count); i++)
                {
                    ParseDb.Goods.Add(Good.Create(GoodData));
                }

                ParseDb.SaveChanges();
            }
        }

        private void UpdateGoods(List<Good> simpleGoods)
        {
            if (simpleGoods?.Count <= Count)
            {
                foreach (var item in simpleGoods.Where(g => g.OrderType != GoodData.OrderType || g.SoldTo != GoodData.SoldTo || g.CustName != GoodData.CustName))
                {
                    item.OrderType = GoodData.OrderType;
                    item.SoldTo = GoodData.SoldTo;
                    item.CustName = GoodData.CustName;
                }

                ParseDb.SaveChanges();
            }
        }
    }
}

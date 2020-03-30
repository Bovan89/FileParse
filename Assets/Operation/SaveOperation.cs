using FileParse.Assets.Task;
using FileParse.ParseDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileParse.Assets.Operation
{
    public class SaveOperation : IOperation
    {
        private Good Good { get; set; }

        private int Count { get; set; }

        public bool IsComplete { get; set; }

        public ParseDb ParseDb { private get; set; }

        public SaveOperation(Good good, int count)
        {
            Good = good;
            Count = count;
        }

        public void Do()
        {            
            if (ParseDb != null)
            {
                List<Good> simpleGoods = ParseDb.Goods.Where(g => g.OrderNum == Good.OrderNum && g.Material == Good.Material && g.Size == Good.Size).ToList();

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
                    ParseDb.Goods.Add(Good.ShallowCopy());
                }

                ParseDb.SaveChanges();
            }
        }

        private void UpdateGoods(List<Good> simpleGoods)
        {
            if (simpleGoods?.Count > 0)
            {
                foreach (var item in simpleGoods.Where(g => g.OrderType != Good.OrderType || g.SoldTo != Good.SoldTo || g.CustName != Good.CustName))
                {
                    item.OrderType = Good.OrderType;
                    item.SoldTo = Good.SoldTo;
                    item.CustName = Good.CustName;
                }

                ParseDb.SaveChanges();
            }
        }
    }
}

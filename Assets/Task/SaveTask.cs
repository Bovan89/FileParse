using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileParse.Assets.Operation;
using FileParse.ParseDbContext;
using Microsoft.EntityFrameworkCore.Storage;

namespace FileParse.Assets.Task
{
    public class SaveTask : ITask
    {
        private List<Good> Goods { get; set; }                

        private string ConnectionString { get; set; }

        private List<IOperation> OperationList { get; set; }

        private ParseDb parseDb { get; set; }

        public SaveTask(List<Good> goods, string connectionString)
        {
            Goods = goods;
            ConnectionString = connectionString;

            Prepare();
        }

        private void Prepare()
        {
            OperationList = new List<IOperation>();

            var goodGroup = from g in Goods
                    group g by new { g.OrderNum, g.Material, g.Size } into gg
                    select new
                    {
                        Good = Goods.First(gf => gf.OrderNum == gg.Key.OrderNum && gf.Material == gg.Key.Material && gf.Size == gg.Key.Size),
                        Count = gg.Count()
                    };

            foreach (var item in goodGroup)
            {
                OperationList.Add(new SaveOperation(item.Good, item.Count));
            }
        }

        public bool Run()
        {
            using (ParseDb db = new ParseDb(ConnectionString))
            {
                using (IDbContextTransaction transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (SaveOperation operation in OperationList)
                        {
                            operation.ParseDb = db;
                            operation.Do();
                        }

                        db.SaveChanges();

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();

                        throw;
                    }
                }
            }

            return true;
        }
    }
}

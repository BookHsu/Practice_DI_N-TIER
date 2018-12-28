using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Repository_Prictice.Models.DbContextFactory
{
    public class DbContextFactory:IDbContextFactory
    {
        private string _ConnectionString = string.Empty;
        public DbContextFactory(string connectionstring)
        {
            this._ConnectionString = connectionstring;
        }
        private DbContext _dbContext;
        private DbContext dbContext
        {
            get
            {
                if (this._dbContext == null)
                {
                    Type t = typeof(DbContext);
                    this._dbContext = (DbContext)Activator.CreateInstance(t, this._ConnectionString);
                }
                return _dbContext;
            }
        }
        public DbContext  GetDbContext()
        {
            return this.dbContext;
        }
       
    }
}

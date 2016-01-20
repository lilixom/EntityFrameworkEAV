using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TAP.Persistence;
using TAP.Utility;
using TAP.Utility.Exceptions;

namespace TAP.FileService.Infrastructure.Impl.EF
{
    public class EFUnitOfWork : DbContext, IUnitOfWork, IDisposable
    {
        private bool cancel;

        [InjectionConstructor]
        public EFUnitOfWork()
            : base(ConnectionManager.GetConnection("Default") as DbConnection, true)
        {
        }

        public EFUnitOfWork(string name)
            : base(ConnectionManager.GetConnection(name) as DbConnection, true)
        {
        }

        public void RollBack()
        {
            cancel = true;
        }

        public int Commit()
        {
            int num;
            try
            {
                if (!this.cancel)
                {
                    return base.SaveChanges();
                }
                num = 0;
            }
            catch (DbEntityValidationException ex)
            {
                var t = (from c in ex.EntityValidationErrors
                         from b in c.ValidationErrors
                         select b.ErrorMessage).ToList();

                throw ExceptionManager.Instance.CreateException(ex, "提交数据时，验证异常:" + string.Join(",", t));
            }
            catch (Exception ex)
            {
                throw ExceptionManager.Instance.CreateException(ex, "提交数据时，出现错误：" + ex.Message);
            }
            finally
            {
                this.cancel = false;
            }
            return num;
        }

        public void Dispose()
        {
            this.Dispose();
        }

        private string _schemaName;

        public string DbName { get; private set; }

        internal string SchemaName
        {
            get
            {
                if (string.IsNullOrEmpty(_schemaName))
                {
                    var connStr = this.Database.Connection.ConnectionString;
                    const string userIdRegex = "User Id=(?<schema>[^;]*);";
                    var item = Regex.Match(connStr, userIdRegex, RegexOptions.IgnoreCase);
                    _schemaName = item.Groups["schema"].Value;
                }
                return _schemaName;
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var list = ComponentFactory.GetAll<IModelCreating>();
            foreach (var modelCreating in list)
            {
                var config = modelCreating.Building(SchemaName);
                modelBuilder.Configurations.Add(config);
            }
        }
    }
}
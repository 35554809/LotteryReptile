﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace LotteryReptile.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class SqlEntities : DbContext
    {
        public SqlEntities()
            : base("name=SqlEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tb_Lottery> tb_Lottery { get; set; }
        public virtual DbSet<tb_LotteryNumber> tb_LotteryNumber { get; set; }
    
        public virtual int Procedure_CustomSql(string sql)
        {
            var sqlParameter = sql != null ?
                new ObjectParameter("sql", sql) :
                new ObjectParameter("sql", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Procedure_CustomSql", sqlParameter);
        }
    }
}

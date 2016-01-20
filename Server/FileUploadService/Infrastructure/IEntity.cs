using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAP.FileService.Infrastructure
{
    /// <summary>
    /// 领域实体
    /// </summary>
    public interface IEntity
    {
    }

    /// <summary>
    /// 领域实体
    /// 必须有ID
    /// </summary>
    /// <typeparam name="TId">Id的类型</typeparam>
    public interface IEntity<TId> : IEntity
    {
        TId Id { get; }
    }
}
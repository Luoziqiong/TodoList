using System;
using System.Collections.Generic;
using System.Text;

namespace TodoList.Service
{
    public interface IMyTodoListService
    {
        Guid MyGuid { get; }
    }
    /// <summary>
    /// 瞬时IService
    /// </summary>
    public interface ITransientService : IMyTodoListService
    {

    }

    /// <summary>
    /// 作用域IService
    /// </summary>
    public interface IScopedService : IMyTodoListService
    {

    }

    /// <summary>
    /// 单例IService
    /// </summary>
    public interface ISingletonService : IMyTodoListService
    {

    }
}

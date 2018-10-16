using System;
using System.Collections.Generic;
using System.Text;
using TodoList.Repository.Entity;

namespace TodoList.Service.Impl
{
    public class MyTodoListService : BaseService<Todo>, IMyTodoListService
    {
        public Guid MyGuid => throw new NotImplementedException();
    }
    /// <summary>
    /// 瞬时Service
    /// </summary>
    public class TransientService : ITransientService
    {
        private Guid _guid;

        public TransientService()
        {
            _guid = Guid.NewGuid();
        }

        public TransientService(Guid guid)
        {
            _guid = guid;
        }

        public Guid MyGuid => _guid;
    }

    /// <summary>
    /// 作用域Service
    /// </summary>
    public class ScopedService : IScopedService
    {
        private Guid _guid;

        public ScopedService()
        {
            _guid = Guid.NewGuid();
        }

        public ScopedService(Guid guid)
        {
            _guid = guid;
        }

        public Guid MyGuid => _guid;
    }

    /// <summary>
    /// 单例Service
    /// </summary>
    public class SingletonService : ISingletonService
    {
        private Guid _guid;

        public SingletonService()
        {
            _guid = Guid.NewGuid();
        }

        public SingletonService(Guid guid)
        {
            _guid = guid;
        }

        public Guid MyGuid => _guid;
    }
}

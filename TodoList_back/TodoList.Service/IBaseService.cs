using System;
using System.Collections.Generic;
using System.Text;

namespace TodoList.Service
{
    interface IBaseService<TEntity> where TEntity:class
    {
    }
}

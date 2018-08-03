using System;
using System.Collections.Generic;
using System.Text;

namespace TodoList.Service.Dto
{
     public class UpdateBookDto
    {
        public long Id { set; get; }
        ///<summary>
        ///价格
        /// </summary>
        public decimal Price { set; get; }
        ///<summary>
        ///简介
        /// </summary>
        public string Description { set; get; }
    }
}

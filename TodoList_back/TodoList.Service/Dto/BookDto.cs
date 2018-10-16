﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TodoList.Service.Dto
{
    public class BookDto
    {
        public long Id { set; get; }

        ///<summary>
        ///名称
        /// </summary>
        public string Name { set; get; }
        ///<summary>
        ///价格
        /// </summary>
        public decimal Price { set; get; }
        ///<summary>
        ///简介
        /// </summary>
        public string Description { set; get; }
        ///<summary>
        ///出版社
        /// </summary>
        public string Publisher { set; get; }
        ///<summary>
        ///出版日期
        /// </summary>
        public DateTime PublishDate { set; get; }
    }
}
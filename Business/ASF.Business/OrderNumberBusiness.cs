﻿//====================================================================================================
// Base code generated with LeatherGoods - ASF.Business
// Architecture Solutions Foundation
//
// Generated by academic at LeatherGoods V 1.0 
//====================================================================================================

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ASF.Entities;
using ASF.Data;


namespace ASF.Business
{
    /// <summary>
    /// OrderNumberBusiness business component.
    /// </summary>
    public class OrderNumberBusiness
    {
        /// <summary>
        /// Add method. 
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public OrderNumber Add(OrderNumber order)
        {
            var orderDac = new OrderNumberDac();
            return orderDac.Create(order);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void Remove(int id)
        {
            var orderDac = new OrderNumberDac();
            orderDac.DeleteById(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<OrderNumber> All()
        {
            var orderDac = new OrderNumberDac();
            var result = orderDac.Select();
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public OrderNumber Find(int id)
        {
            var orderDac = new OrderNumberDac();
            var result = orderDac.SelectById(id);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        public void Edit(OrderNumber order)
        {
            var orderDac = new OrderNumberDac();
            orderDac.UpdateById(order);
        }
    }
}
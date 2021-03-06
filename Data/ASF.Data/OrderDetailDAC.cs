﻿//====================================================================================================
// Base code generated with LeatherGoods - ASF.Data
// Architecture Solutions Foundation
//
// Generated by academic at LeatherGoods V 1.0 
//====================================================================================================

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ASF.Entities;

namespace ASF.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class OrderDetailDac : DataAccessComponent
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrderDetail"></param>
        /// <returns></returns>
        public OrderDetail Create(OrderDetail OrderDetail)
        {
            const string sqlStatement = "INSERT INTO [dbo].[OrderDetail] ([OrderId], [ProductId], [Price], [Quantity], [CreatedOn], [CreatedBy], [ChangedOn], [ChangedBy])"+
                "VALUES (@OrderId, @ProductId, @Price, @Quantity, @CreatedOn, @CreatedBy, @ChangedOn, @ChangedBy); SELECT SCOPE_IDENTITY(); ";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@OrderId", DbType.Int32, OrderDetail.OrderId);
                db.AddInParameter(cmd, "@ProductId", DbType.Int32, OrderDetail.ProductId);
                db.AddInParameter(cmd, "@Price", DbType.Double, OrderDetail.Price);
                db.AddInParameter(cmd, "@Quantity", DbType.Int32, OrderDetail.Quantity);
                db.AddInParameter(cmd, "@CreatedOn", DbType.DateTime, OrderDetail.CreatedOn);
                db.AddInParameter(cmd, "@CreatedBy", DbType.String, OrderDetail.CreatedBy);
                db.AddInParameter(cmd, "@ChangedOn", DbType.DateTime, OrderDetail.ChangedOn);
                db.AddInParameter(cmd, "@ChangedBy", DbType.String, OrderDetail.ChangedBy);
                // Obtener el valor de la primary key.
                OrderDetail.Id = Convert.ToInt32(db.ExecuteScalar(cmd));
            }

            return OrderDetail;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrderDetail"></param>
        public void UpdateById(OrderDetail OrderDetail)
        {
            const string sqlStatement = "UPDATE [dbo].[OrderDetail]" +
            "   SET [OrderId] = @OrderId" +
            "      ,[ProductId] = @ProductId" +
            "      ,[Price] = @Price" +
            "      ,[Quantity] = @Quantity" +
            "      ,[CreatedOn] = @CreatedOn" +
            "      ,[CreatedBy] = @CreatedBy" +
            "      ,[ChangedOn] = @ChangedOn" +
            "      ,[ChangedBy] = @ChangedBy" +
            " WHERE [Id]=@Id ";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, OrderDetail.Id);
                db.AddInParameter(cmd, "@OrderId", DbType.Int32, OrderDetail.OrderId);
                db.AddInParameter(cmd, "@ProductId", DbType.Int32, OrderDetail.ProductId);
                db.AddInParameter(cmd, "@Price", DbType.Double, OrderDetail.Price);
                db.AddInParameter(cmd, "@Quantity", DbType.Int32, OrderDetail.Quantity);
                db.AddInParameter(cmd, "@CreatedOn", DbType.DateTime, OrderDetail.CreatedOn);
                db.AddInParameter(cmd, "@CreatedBy", DbType.String, OrderDetail.CreatedBy);
                db.AddInParameter(cmd, "@ChangedOn", DbType.DateTime, OrderDetail.ChangedOn);
                db.AddInParameter(cmd, "@ChangedBy", DbType.String, OrderDetail.ChangedBy);

                db.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void DeleteById(int id)
        {
            const string sqlStatement = "DELETE [dbo].[OrderDetail] WHERE [Id]=@Id ";
            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, id);
                db.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public OrderDetail SelectById(int id)
        {
            const string sqlStatement = "SELECT [Id], [OrderId], [ProductId], [Price], [Quantity], [CreatedOn], [CreatedBy], [ChangedOn], [ChangedBy]" +
                "FROM [dbo].[OrderDetail] WHERE [Id]=@Id ";

            OrderDetail OrderDetail = null;
            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, id);
                using (var dr = db.ExecuteReader(cmd))
                {
                    if (dr.Read()) OrderDetail = LoadOrderDetail(dr);
                }
            }

            return OrderDetail;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>		
        public List<OrderDetail> Select()
        {
            // WARNING! Performance
            const string sqlStatement = "SELECT [Id], [OrderId], [ProductId], [Price], [Quantity], [CreatedOn], [CreatedBy], [ChangedOn], [ChangedBy] FROM [dbo].[OrderDetail] ";

            var result = new List<OrderDetail>();
            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                using (var dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        var OrderDetail = LoadOrderDetail(dr); // Mapper
                        result.Add(OrderDetail);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Crea una nueva Categoría desde un Datareader.
        /// </summary>
        /// <param name="dr">Objeto DataReader.</param>
        /// <returns>Retorna un objeto Categoria.</returns>		
        private static OrderDetail LoadOrderDetail(IDataReader dr)
        {
            var OrderDetail = new OrderDetail
            {
                Id = GetDataValue<int>(dr, "Id"),
                OrderId = GetDataValue<int>(dr, "OrderId"),
                ProductId = GetDataValue<int>(dr, "ProductId"),
                Price = GetDataValue<float>(dr, "Price"),
                Quantity = GetDataValue<int>(dr, "Quantity"),
                CreatedOn = GetDataValue<DateTime>(dr, "CreatedOn"),
                CreatedBy = GetDataValue<int>(dr, "CreatedBy"),
                ChangedOn = GetDataValue<DateTime>(dr, "ChangedOn"),
                ChangedBy = GetDataValue<int>(dr, "ChangedBy")
            };
            return OrderDetail;
        }
    }
}


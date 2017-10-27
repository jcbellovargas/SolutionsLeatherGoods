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
    public class OrderDac : DataAccessComponent
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Order"></param>
        /// <returns></returns>
        public Order Create(Order Order)
        {
            const string sqlStatement = "INSERT INTO [dbo].[Order] ([ClientId], [OrderDate], [TotalPrice], [State], [OrderNumber], [ItemCount], [Rowid], [CreatedOn], [CreatedBy], [ChangedOn], [ChangedBy]) " +
           "VALUES (@ClientId, @OrderDate, @TotalPrice, @State, @OrderNumber, @ItemCount ,@Rowid, @CreatedOn, @CreatedBy, @ChangedOn, @ChangedBy); SELECT SCOPE_IDENTITY(); ";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@ClientId", DbType.Int32, Order.ClientId);
                db.AddInParameter(cmd, "@OrderDate", DbType.DateTime, Order.OrderDate);
                db.AddInParameter(cmd, "@TotalPrice", DbType.Double, Order.TotalPrice);
                db.AddInParameter(cmd, "@State", DbType.String, Order.State);
                db.AddInParameter(cmd, "@OrderNumber", DbType.Int32, Order.OrderNumber);
                db.AddInParameter(cmd, "@ItemCount", DbType.Int32, Order.ItemCount);
                db.AddInParameter(cmd, "@Rowid", DbType.Guid, Order.Rowid);
                db.AddInParameter(cmd, "@CreatedOn", DbType.DateTime, Order.CreatedOn);
                db.AddInParameter(cmd, "@CreatedBy", DbType.String, Order.CreatedBy);
                db.AddInParameter(cmd, "@ChangedOn", DbType.DateTime, Order.ChangedOn);
                db.AddInParameter(cmd, "@ChangedBy", DbType.String, Order.ChangedBy);
                // Obtener el valor de la primary key.
                Order.Id = Convert.ToInt32(db.ExecuteScalar(cmd));
            }

            return Order;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Order"></param>
        public void UpdateById(Order Order)
        {
            const string sqlStatement = "UPDATE [dbo].[Order] " +
            "   SET [ClientId] = @ClientId" +
            "      ,[OrderDate] = @OrderDate" +
            "      ,[TotalPrice] = @TotalPrice" +
            "      ,[State] = @State" +
            "      ,[OrderNumber] = @OrderNumber" +
            "      ,[ItemCount] = @ItemCount" +
            "      ,[Rowid] = @Rowid" +
            "      ,[CreatedOn] = @CreatedOn" +
            "      ,[CreatedBy] = @CreatedBy" +
            "      ,[ChangedOn] = @ChangedOn" +
            "      ,[ChangedBy] = @ChangedBy" +
            " WHERE [Id]=@Id ";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, Order.Id);
                db.AddInParameter(cmd, "@ClientId", DbType.Int32, Order.ClientId);
                db.AddInParameter(cmd, "@OrderDate", DbType.DateTime, Order.OrderDate);
                db.AddInParameter(cmd, "@TotalPrice", DbType.Double, Order.TotalPrice);
                db.AddInParameter(cmd, "@State", DbType.String, Order.State);
                db.AddInParameter(cmd, "@OrderNumber", DbType.Int32, Order.OrderNumber);
                db.AddInParameter(cmd, "@ItemCount", DbType.Int32, Order.ItemCount);
                db.AddInParameter(cmd, "@Rowid", DbType.Guid, Order.Rowid);
                db.AddInParameter(cmd, "@CreatedOn", DbType.DateTime, Order.CreatedOn);
                db.AddInParameter(cmd, "@CreatedBy", DbType.String, Order.CreatedBy);
                db.AddInParameter(cmd, "@ChangedOn", DbType.DateTime, Order.ChangedOn);
                db.AddInParameter(cmd, "@ChangedBy", DbType.String, Order.ChangedBy);

                db.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void DeleteById(int id)
        {
            const string sqlStatement = "DELETE [dbo].[Order] WHERE [Id]=@Id ";
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
        public Order SelectById(int id)
        {
            const string sqlStatement = "SELECT [Id], [ClientId], [OrderDate], [TotalPrice], [State], [OrderNumber], [ItemCount], [Rowid], [CreatedOn], [CreatedBy], [ChangedOn], [ChangedBy]" +
                "FROM [dbo].[Order] WHERE [Id]=@Id ";

            Order Order = null;
            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, id);
                using (var dr = db.ExecuteReader(cmd))
                {
                    if (dr.Read()) Order = LoadOrder(dr);
                }
            }

            return Order;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>		
        public List<Order> Select()
        {
            // WARNING! Performance
            const string sqlStatement = "SELECT [Id], [ClientId], [OrderDate], [TotalPrice], [State], [OrderNumber], [ItemCount], [Rowid], [CreatedOn], [CreatedBy], [ChangedOn], [ChangedBy] FROM [dbo].[Order] ";

            var result = new List<Order>();
            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                using (var dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        var Order = LoadOrder(dr); // Mapper
                        result.Add(Order);
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
        private static Order LoadOrder(IDataReader dr)
        {
            var Order = new Order
            {
                Id = GetDataValue<int>(dr, "Id"),
                ClientId = GetDataValue<int>(dr, "ClientId"),
                OrderDate = GetDataValue<DateTime>(dr, "OrderDate"),
                TotalPrice = GetDataValue<float>(dr, "TotalPrice"),
                State = GetDataValue<string>(dr, "State"),
                OrderNumber = GetDataValue<int>(dr, "OrderNumber"),
                ItemCount = GetDataValue<int>(dr, "ItemCount"),
                Rowid = GetDataValue<Guid>(dr, "Rowid"),
                CreatedOn = GetDataValue<DateTime>(dr, "CreatedOn"),
                CreatedBy = GetDataValue<int>(dr, "CreatedBy"),
                ChangedOn = GetDataValue<DateTime>(dr, "ChangedOn"),
                ChangedBy = GetDataValue<int>(dr, "ChangedBy")
            };
            return Order;
        }
    }
}


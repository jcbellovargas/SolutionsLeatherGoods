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
    public class CartItemDac : DataAccessComponent
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CartItem"></param>
        /// <returns></returns>
        public CartItem Create(CartItem CartItem)
        {
            const string sqlStatement = "INSERT INTO [dbo].[CartItem] ([CartId],[ProductId], [Price], [Quantity], [CreatedOn], [CreatedBy], [ChangedOn], [ChangedBy]) "+
                "VALUES (@CartId, @ProductId, @Price, @Quantity, @CreatedOn, @CreatedBy, @ChangedOn, @ChangedBy); SELECT SCOPE_IDENTITY(); ";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@CartId", DbType.Int32, CartItem.CartId);
                db.AddInParameter(cmd, "@ProductId", DbType.Int32, CartItem.ProductId);
                db.AddInParameter(cmd, "@Price", DbType.Double, CartItem.Price);
                db.AddInParameter(cmd, "@Quantity", DbType.Int32, CartItem.Quantity);
                db.AddInParameter(cmd, "@CreatedOn", DbType.DateTime, CartItem.CreatedOn);
                db.AddInParameter(cmd, "@CreatedBy", DbType.String, CartItem.CreatedBy);
                db.AddInParameter(cmd, "@ChangedOn", DbType.DateTime, CartItem.ChangedOn);
                db.AddInParameter(cmd, "@ChangedBy", DbType.String, CartItem.ChangedBy);
                // Obtener el valor de la primary key.
                CartItem.Id = Convert.ToInt32(db.ExecuteScalar(cmd));
            }

            return CartItem;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CartItem"></param>
        public void UpdateById(CartItem CartItem)
        {
            const string sqlStatement = "UPDATE [dbo].[CartItem]" +
            "   SET [CartId] = @CartId" +
            "      ,[ProductId] = @ProductId" +
            "      ,[Price] = @Price" +
            "      ,[Quantity] = @Quantity" +
            "      ,[CreatedOn] = @CreatedOn" +
            "      ,[CreatedBy] = @CreatedBy" +
            "      ,[ChangedOn] = @ChangedOn" +
            "      ,[ChangedBy] = @ChangedBy" +
            " WHERE [Id]=@Id";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, CartItem.Id);
                db.AddInParameter(cmd, "@CartId", DbType.Int32, CartItem.CartId);
                db.AddInParameter(cmd, "@ProductId", DbType.Int32, CartItem.ProductId);
                db.AddInParameter(cmd, "@Price", DbType.Double, CartItem.Price);
                db.AddInParameter(cmd, "@Quantity", DbType.Int32, CartItem.Quantity);
                db.AddInParameter(cmd, "@CreatedOn", DbType.DateTime, CartItem.CreatedOn);
                db.AddInParameter(cmd, "@CreatedBy", DbType.String, CartItem.CreatedBy);
                db.AddInParameter(cmd, "@ChangedOn", DbType.DateTime, CartItem.ChangedOn);
                db.AddInParameter(cmd, "@ChangedBy", DbType.String, CartItem.ChangedBy);

                db.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void DeleteById(int id)
        {
            const string sqlStatement = "DELETE dbo.CartItem WHERE [Id]=@Id ";
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
        public CartItem SelectById(int id)
        {
            const string sqlStatement = "SELECT [Id], [CartId],[ProductId], [Price], [Quantity], [CreatedOn], [CreatedBy], [ChangedOn], [ChangedBy]" +
                "FROM dbo.CartItem WHERE [Id]=@Id ";

            CartItem CartItem = null;
            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, id);
                using (var dr = db.ExecuteReader(cmd))
                {
                    if (dr.Read()) CartItem = LoadCartItem(dr);
                }
            }

            return CartItem;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>		
        public List<CartItem> Select()
        {
            // WARNING! Performance
            const string sqlStatement = "SELECT [Id], [CartId],[ProductId], [Price], [Quantity], [CreatedOn], [CreatedBy], [ChangedOn], [ChangedBy] FROM dbo.CartItem ";

            var result = new List<CartItem>();
            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                using (var dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        var CartItem = LoadCartItem(dr); // Mapper
                        result.Add(CartItem);
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
        private static CartItem LoadCartItem(IDataReader dr)
        {
            var CartItem = new CartItem
            {
                Id = GetDataValue<int>(dr, "Id"),
                CartId = GetDataValue<int>(dr, "CartId"),
                ProductId = GetDataValue<int>(dr, "ProductId"),
                Price = GetDataValue<float>(dr, "Price"),
                Quantity = GetDataValue<int>(dr, "Quantity"),
                CreatedOn = GetDataValue<DateTime>(dr, "CreatedOn"),
                CreatedBy = GetDataValue<int>(dr, "CreatedBy"),
                ChangedOn = GetDataValue<DateTime>(dr, "ChangedOn"),
                ChangedBy = GetDataValue<int>(dr, "ChangedBy")
            };
            return CartItem;
        }
    }
}


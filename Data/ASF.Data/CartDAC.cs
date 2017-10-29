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
    public class CartDac : DataAccessComponent
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Cart"></param>
        /// <returns></returns>
        public Cart Create(Cart Cart)
        {
            const string sqlStatement = "INSERT INTO [dbo].[Cart] ([Cookie], [CartDate], [ItemCount], [Rowid], [CreatedOn], [CreatedBy], [ChangedOn], [ChangedBy]) " +
           "VALUES (@Cookie, @CartDate, @ItemCount, @Rowid, @CreatedOn, @CreatedBy, @ChangedOn, @ChangedBy); SELECT SCOPE_IDENTITY(); ";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@Cookie", DbType.String, Cart.Cookie);
                db.AddInParameter(cmd, "@CartDate", DbType.DateTime, Cart.CartDate);
                db.AddInParameter(cmd, "@ItemCount", DbType.Int32, Cart.ItemCount);
                db.AddInParameter(cmd, "@Rowid", DbType.Guid, Cart.Rowid);
                db.AddInParameter(cmd, "@CreatedOn", DbType.DateTime, Cart.CreatedOn);
                db.AddInParameter(cmd, "@CreatedBy", DbType.String, Cart.CreatedBy);
                db.AddInParameter(cmd, "@ChangedOn", DbType.DateTime, Cart.ChangedOn);
                db.AddInParameter(cmd, "@ChangedBy", DbType.String, Cart.ChangedBy);
                // Obtener el valor de la primary key.
                Cart.Id = Convert.ToInt32(db.ExecuteScalar(cmd));
            }

            return Cart;
        }

        public Cart SelectByCookie(string id) {
            const string sqlStatement = "SELECT [Id], [Cookie], [CartDate], [ItemCount], [Rowid], [CreatedOn], [CreatedBy], [ChangedOn], [ChangedBy]" +
            "FROM dbo.Cart WHERE [Cookie]=@Cookie ";

            Cart Cart = null;
            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement)) {
                db.AddInParameter(cmd, "@Cookie", DbType.String, id);
                using (var dr = db.ExecuteReader(cmd)) {
                    if (dr.Read()) Cart = LoadCart(dr);
                }
            }

            return Cart;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Cart"></param>
        public void UpdateById(Cart Cart)
        {
            const string sqlStatement = "UPDATE [dbo].[Cart]" +
                "   SET [Cookie] = @Cookie" +
                "      ,[CartDate] = @CartDate" +
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
                db.AddInParameter(cmd, "@Id", DbType.Int32, Cart.Id);
                db.AddInParameter(cmd, "@Cookie", DbType.String, Cart.Cookie);
                db.AddInParameter(cmd, "@CartDate", DbType.DateTime, Cart.CartDate);
                db.AddInParameter(cmd, "@ItemCount", DbType.Int32, Cart.ItemCount);
                db.AddInParameter(cmd, "@Rowid", DbType.Guid, Cart.Rowid);
                db.AddInParameter(cmd, "@CreatedOn", DbType.DateTime, Cart.CreatedOn);
                db.AddInParameter(cmd, "@CreatedBy", DbType.String, Cart.CreatedBy);
                db.AddInParameter(cmd, "@ChangedOn", DbType.DateTime, Cart.ChangedOn);
                db.AddInParameter(cmd, "@ChangedBy", DbType.String, Cart.ChangedBy);

                db.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void DeleteById(int id)
        {
            const string sqlStatement = "DELETE dbo.Cart WHERE [Id]=@Id ";
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
        public Cart SelectById(int id)
        {
            const string sqlStatement = "SELECT [Id], [Cookie], [CartDate], [ItemCount], [Rowid], [CreatedOn], [CreatedBy], [ChangedOn], [ChangedBy]" +
                "FROM dbo.Cart WHERE [Id]=@Id ";

            Cart Cart = null;
            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, id);
                using (var dr = db.ExecuteReader(cmd))
                {
                    if (dr.Read()) Cart = LoadCart(dr);
                }
            }

            return Cart;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>		
        public List<Cart> Select()
        {
            // WARNING! Performance
            const string sqlStatement = "SELECT [Id], [Cookie], [CartDate], [ItemCount], [Rowid], [CreatedOn], [CreatedBy], [ChangedOn], [ChangedBy] FROM dbo.Cart ";

            var result = new List<Cart>();
            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                using (var dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        var Cart = LoadCart(dr); // Mapper
                        result.Add(Cart);
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
        private static Cart LoadCart(IDataReader dr)
        {
            var Cart = new Cart {
                Id = GetDataValue<int>(dr, "Id"),
                Cookie = GetDataValue<string>(dr, "Cookie"),
                CartDate = GetDataValue<DateTime>(dr, "CartDate"),
                ItemCount = GetDataValue<int>(dr, "ItemCount"),
                Rowid = GetDataValue<Guid>(dr, "Rowid"),
                CreatedOn = GetDataValue<DateTime>(dr, "CreatedOn"),
                CreatedBy = GetDataValue<int>(dr, "CreatedBy"),
                ChangedOn = GetDataValue<DateTime>(dr, "ChangedOn"),
                ChangedBy = GetDataValue<int>(dr, "ChangedBy"),
                Items = new List<CartItem>()
            };
            return Cart;
        }
    }
}


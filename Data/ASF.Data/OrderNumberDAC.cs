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
    public class OrderNumberDac : DataAccessComponent
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrderNumber"></param>
        /// <returns></returns>
        public OrderNumber Create(OrderNumber OrderNumber)
        {
            const string sqlStatement = "INSERT INTO [dbo].[OrderNumber] ([Number], [CreatedOn], [CreatedBy], [ChangedOn], [ChangedBy])"+
                "VALUES (@Number, @CreatedOn, @CreatedBy, @ChangedOn, @ChangedBy); SELECT SCOPE_IDENTITY(); ";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@Number", DbType.Int32, OrderNumber.Number);
                db.AddInParameter(cmd, "@CreatedOn", DbType.DateTime, OrderNumber.CreatedOn);
                db.AddInParameter(cmd, "@CreatedBy", DbType.String, OrderNumber.CreatedBy);
                db.AddInParameter(cmd, "@ChangedOn", DbType.DateTime, OrderNumber.ChangedOn);
                db.AddInParameter(cmd, "@ChangedBy", DbType.String, OrderNumber.ChangedBy);
                // Obtener el valor de la primary key.
                OrderNumber.Id = Convert.ToInt32(db.ExecuteScalar(cmd));
            }

            return OrderNumber;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrderNumber"></param>
        public void UpdateById(OrderNumber OrderNumber)
        {
            const string sqlStatement = "UPDATE [dbo].[OrderNumber]" +
            "   SET [Number] = @Number" +
            "      ,[CreatedOn] = @CreatedOn" +
            "      ,[CreatedBy] = @CreatedBy" +
            "      ,[ChangedOn] = @ChangedOn" +
            "      ,[ChangedBy] = @ChangedBy" +
            " WHERE [Id] = @Id"; 

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, OrderNumber.Id);
                db.AddInParameter(cmd, "@Number", DbType.Int32, OrderNumber.Number);
                db.AddInParameter(cmd, "@CreatedOn", DbType.DateTime, OrderNumber.CreatedOn);
                db.AddInParameter(cmd, "@CreatedBy", DbType.String, OrderNumber.CreatedBy);
                db.AddInParameter(cmd, "@ChangedOn", DbType.DateTime, OrderNumber.ChangedOn);
                db.AddInParameter(cmd, "@ChangedBy", DbType.String, OrderNumber.ChangedBy);

                db.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void DeleteById(int id)
        {
            const string sqlStatement = "DELETE [dbo].[OrderNumber] WHERE [Id]=@Id ";
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
        public OrderNumber SelectById(int id)
        {
            const string sqlStatement = "SELECT [Id], [Number], [CreatedOn], [CreatedBy], [ChangedOn], [ChangedBy]" +
                "FROM [dbo].[OrderNumber] WHERE [Id]=@Id ";

            OrderNumber OrderNumber = null;
            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, id);
                using (var dr = db.ExecuteReader(cmd))
                {
                    if (dr.Read()) OrderNumber = LoadOrderNumber(dr);
                }
            }

            return OrderNumber;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>		
        public List<OrderNumber> Select()
        {
            // WARNING! Performance
            const string sqlStatement = "SELECT [Id], [Number], [CreatedOn], [CreatedBy], [ChangedOn], [ChangedBy] FROM [dbo].[OrderNumber] ";

            var result = new List<OrderNumber>();
            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                using (var dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        var OrderNumber = LoadOrderNumber(dr); // Mapper
                        result.Add(OrderNumber);
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
        private static OrderNumber LoadOrderNumber(IDataReader dr)
        {
            var OrderNumber = new OrderNumber
            {

                Id = GetDataValue<int>(dr, "Id"),
                Number = GetDataValue<int>(dr, "Number"),
                CreatedOn = GetDataValue<DateTime>(dr, "CreatedOn"),
                CreatedBy = GetDataValue<int>(dr, "CreatedBy"),
                ChangedOn = GetDataValue<DateTime>(dr, "ChangedOn"),
                ChangedBy = GetDataValue<int>(dr, "ChangedBy")
            };
            return OrderNumber;
        }
    }
}


using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public abstract class BaseDataAccess
    {
        protected string ConnectionString { get; set; }
        private readonly IConfiguration _Configuration;
        private readonly string connectionName;
        public BaseDataAccess(IConfiguration Configuration,string connectionName="FoodDeliveryDb")
        {
            _Configuration = Configuration;
            this.connectionName = connectionName;
        }
        private MySqlConnection GetConnection()
        {
            try
            {
                ConnectionString = _Configuration.GetConnectionString(this.connectionName);
                MySqlConnection connection = new MySqlConnection(this.ConnectionString);
                
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                return connection;
            }
            catch (SqlException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DbCommand GetCommand(DbConnection connection, string commandText, CommandType commandType)
        {
            try
            {
                MySqlCommand command = new MySqlCommand(commandText, connection as MySqlConnection);
                command.CommandType = commandType;
                return command;
            }
            catch (Exception ex)
            {
                throw new Exception("Cannot create SqlCommand", ex);
            }
        }


        public MySqlParameter GetParameter(string parameter, object value)
        {
            try
            {
                MySqlParameter parameterObject = new MySqlParameter(parameter, value != null ? value : DBNull.Value);
                parameterObject.Direction = ParameterDirection.Input;
                return parameterObject;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    
        public int ExecuteNonQuery(string queryOrProcedureName, List<DbParameter> parameters, CommandType commandType = CommandType.StoredProcedure)
        {
            int returnValue = -1;

            try
            {
                using (MySqlConnection connection = this.GetConnection())
                {
                    DbCommand cmd = this.GetCommand(connection, queryOrProcedureName, commandType);

                    if (parameters != null && parameters.Count > 0)
                    {
                        cmd.Parameters.AddRange(parameters.ToArray());
                    }

                    returnValue = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return returnValue;
        }


        public object ExecuteScalar(string queryOrProcedureName, List<SqlParameter> parameters, CommandType commandType = CommandType.Text)
        {
            object returnValue = null;

            try
            {
                using (DbConnection connection = this.GetConnection())
                {
                    DbCommand cmd = this.GetCommand(connection, queryOrProcedureName, commandType);

                    if (parameters != null && parameters.Count > 0)
                    {
                        cmd.Parameters.AddRange(parameters.ToArray());
                    }

                    returnValue = cmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                //LogException("Failed to ExecuteScalar for " + procedureName, ex, parameters);
                throw;
            }

            return returnValue;
        }


        public DbDataReader GetDataReader(string queryOrProcedureName, List<DbParameter> parameters, CommandType commandType = CommandType.StoredProcedure, int timeout = 30)
        {
            DbDataReader ds;

            try
            {
                DbConnection connection = this.GetConnection();
                {
                    DbCommand cmd = this.GetCommand(connection, queryOrProcedureName, commandType);
                    if (parameters != null && parameters.Count > 0)
                    {
                        cmd.Parameters.AddRange(parameters.ToArray());
                    }
                    cmd.CommandTimeout = timeout;
                    ds = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                }
            }
            catch (MySqlException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Issue in executing " + commandType.ToString() + " " + queryOrProcedureName, ex);
            }

            return ds;
        }

        public IList<T> FetchResult<T>(DbDataReader reader, bool closeReader = true)
        {
            try
            {
                Dictionary<string, string> columnList = new Dictionary<string, string>();
                foreach (var col in reader.GetColumnSchema())
                {
                    columnList.Add(col.ColumnName, col.ColumnName);
                    System.Diagnostics.Debug.WriteLine(col.ColumnName);
                }
                var result = new List<T>();
                var type = typeof(T);

                while (reader.Read())
                {
                    var obj = (T)Activator.CreateInstance(type);

                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        if (prop.PropertyType == typeof(bool))
                        {
                            if (!columnList.ContainsKey(prop.Name) || (reader.IsDBNull(reader.GetOrdinal(prop.Name))))
                                continue;
                            var value = reader[prop.Name].ToString().Equals("0") || reader[prop.Name].Equals(false) ? false : true;
                            //Convert.ChangeType(reader[prop.Name].ToString(), prop.PropertyType);
                            prop.SetValue(obj, value);
                        }
                        else if (prop.PropertyType == typeof(byte[]))
                        {
                            if (!columnList.ContainsKey(prop.Name) || (reader.IsDBNull(reader.GetOrdinal(prop.Name))))
                                continue;
                            var value = reader[prop.Name];
                            prop.SetValue(obj, value);
                        }

                        else
                        {
                            //  if (reader.IsDBNull(reader.GetOrdinal(prop.Name)))
                            // System.Diagnostics.Debug.WriteLine(reader.GetColumnSchema()[0].ColumnName);
                            if (!columnList.ContainsKey(prop.Name) || (reader.IsDBNull(reader.GetOrdinal(prop.Name))))
                                continue;
                            object value;
                            var t = prop.PropertyType;
                            if (t.IsGenericType && t.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                            {

                                if (reader[prop.Name].ToString() == null)
                                {
                                    value = default(T);
                                }
                                else
                                {
                                    t = Nullable.GetUnderlyingType(t);
                                    value = Convert.ChangeType(reader[prop.Name].ToString(), t);
                                }
                            }
                            else
                            {
                                value = Convert.ChangeType(reader[prop.Name].ToString(), prop.PropertyType);
                            }
                            prop.SetValue(obj, value);

                        }
                    }

                    result.Add(obj);
                }
                return result;
            }
            catch (Exception ex)
            {
                closeReader = true;
                throw;
            }
            finally
            {
                if (closeReader)
                {
                    reader.Close();
                }
            }

        }

    }
}

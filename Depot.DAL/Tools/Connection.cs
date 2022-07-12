using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Depot.DAL.Tools
{
    public class Connection
    {
        private readonly string _connectionString;
        private readonly DbProviderFactory _providerFactory;

        public Connection(string connectionString, DbProviderFactory providerFactory)
        {
            if (providerFactory is null)
                throw new ArgumentException($"{ nameof(providerFactory) } is not valid!");

            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException($"{ nameof(connectionString)} is not valid!");

            _connectionString = connectionString;
            _providerFactory = providerFactory;

            try
            {
                using (MySqlConnection mySqlConnection = CreateConnection())
                {
                    mySqlConnection.Open();
                }
            }
            catch (Exception)
            {
                throw new InvalidOperationException("The connection string is not valid or the server is down...");
            }
        }

        public MySqlConnection CreateConnection()
        {
            MySqlConnection mySqlConnection = (MySqlConnection)_providerFactory.CreateConnection();
            mySqlConnection.ConnectionString = _connectionString;

            return mySqlConnection;
        }

        public int ExecuteNonQuery(Command command)
        {
            using (MySqlConnection mySqlConnection = CreateConnection())
            {
                using (MySqlCommand mySqlCommand = CreateCommand(command, mySqlConnection))
                {
                    mySqlConnection.Open();
                    int rows = mySqlCommand.ExecuteNonQuery();

                    if (command.IsStoredProcedure)
                        FinalizeQuery(command, mySqlCommand);

                    return rows;
                }
            }
        }

        public IEnumerable<TResult> ExecuteReader<TResult>(Command command, Func<IDataRecord, TResult> selector)
        {
            using (MySqlConnection mySqlConnection = CreateConnection())
            {
                using (MySqlCommand mySqlCommand = CreateCommand(command, mySqlConnection))
                {
                    mySqlConnection.Open();
                    using (MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader())
                    {
                        while (mySqlDataReader.Read())
                        {
                            yield return selector(mySqlDataReader);
                        }
                    }

                    if (command.IsStoredProcedure)
                        FinalizeQuery(command, mySqlCommand);
                }
            }
        }

        public object ExecuteScalar(Command command)
        {
            using (MySqlConnection mySqlConnection = CreateConnection())
            {
                using (MySqlCommand mySqlCommand = CreateCommand(command, mySqlConnection))
                {
                    mySqlConnection.Open();
                    object result = mySqlCommand.ExecuteScalar();

                    if (command.IsStoredProcedure)
                        FinalizeQuery(command, mySqlCommand);

                    return result is DBNull ? null : result;
                }
            }
        }

        public DataTable GetDataTable(Command command)
        {
            using (MySqlConnection mySqlConnection = CreateConnection())
            {
                using (MySqlCommand mySqlCommand = CreateCommand(command, mySqlConnection))
                {
                    using (MySqlDataAdapter mySqlDataAdapter = (MySqlDataAdapter)_providerFactory.CreateDataAdapter())
                    {
                        DataTable dataTable = new DataTable();
                        mySqlDataAdapter.SelectCommand = mySqlCommand;
                        mySqlDataAdapter.Fill(dataTable);

                        if (command.IsStoredProcedure)
                            FinalizeQuery(command, mySqlCommand);

                        return dataTable;
                    }
                }
            }
        }

        public DataSet GetDataSet(Command command)
        {
            using (MySqlConnection mySqlConnection = CreateConnection())
            {
                using (MySqlCommand mySqlCommand = CreateCommand(command, mySqlConnection))
                {
                    using (MySqlDataAdapter mySqlDataAdapter = (MySqlDataAdapter)_providerFactory.CreateDataAdapter())
                    {
                        DataSet dataSet = new DataSet();
                        mySqlDataAdapter.SelectCommand = mySqlCommand;
                        mySqlDataAdapter.Fill(dataSet);

                        if (command.IsStoredProcedure)
                            FinalizeQuery(command, mySqlCommand);

                        return dataSet;
                    }
                }
            }
        }

        public static MySqlCommand CreateCommand(Command command, MySqlConnection connection)
        {
            MySqlCommand mySqlCommand = connection.CreateCommand();
            mySqlCommand.CommandText = command.Query;

            if (command.IsStoredProcedure)
                mySqlCommand.CommandType = CommandType.StoredProcedure;

            foreach (KeyValuePair<string, Parameter> parameter in command.Parameters)
            {
                MySqlParameter mySqlParameter = mySqlCommand.CreateParameter();
                mySqlParameter.ParameterName = parameter.Key;
                mySqlParameter.Value = parameter.Value.ParameterValue;

                if (parameter.Value.direction == Direction.Output)
                    mySqlParameter.Direction = ParameterDirection.Output;

                mySqlCommand.Parameters.Add(mySqlParameter);
            }

            return mySqlCommand;
        }

        private void FinalizeQuery(Command command, MySqlCommand mySqlCommand)
        {
            foreach (KeyValuePair<string, Parameter> parameter in command.Parameters)
            {
                if (parameter.Value.direction == Direction.Output)
                {
                    parameter.Value.ParameterValue = mySqlCommand.Parameters[parameter.Key].Value;
                }
            }
        }
    }
}

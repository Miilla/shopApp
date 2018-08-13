using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

/// <summary>
/// Summary description for ADOHelper
/// </summary>
public class ADOHelper
{
    private SqlConnection Connection { get; set; }
    private SqlCommand Command { get; set; }
    private const int Timeout = 60;


    public ADOHelper(string procedureName)
    {
        Connection = new SqlConnection(CommonConst.ConnectionString);
        Command = new SqlCommand(procedureName, Connection);
        Command.CommandType = System.Data.CommandType.StoredProcedure;
        Command.CommandTimeout = Timeout;
    }

    public ADOHelper(string dbname, string procedureName)
    {
        Connection = new SqlConnection(CommonConst.GetConnectionString(dbname));
        Command = new SqlCommand(procedureName, Connection);
        Command.CommandType = System.Data.CommandType.StoredProcedure;
        Command.CommandTimeout = Timeout;
    }

    public ADOHelper(string commandText, CommandType type)
    {
        Connection = new SqlConnection(CommonConst.ConnectionString);
        Command = new SqlCommand(commandText, Connection);
        Command.CommandType = type;
        Command.CommandTimeout = Timeout;
    }


    /// <summary>
    /// You have to close reader after read data
    /// </summary>
    /// <returns></returns>
    public SqlDataReader ExecuteReader()
    {
        Connection.Open();
        return Command.ExecuteReader(CommandBehavior.CloseConnection);
    }


    public DataTable Execute()
    {
        DataTable dt = new DataTable();
        SqlDataAdapter adapter = new SqlDataAdapter(Command);

        try
        {
            Connection.Open();
            adapter.Fill(dt);

            return dt;
        }
        finally
        {
            if (Connection != null || Connection.State != ConnectionState.Closed)
                Connection.Close();
        }
    }


    public void ExecuteNonQuery()
    {
        try
        {
            Connection.Open();
            Command.ExecuteNonQuery();
        }
        finally
        {
            if (Connection != null || Connection.State != ConnectionState.Closed)
                Connection.Close();
        }
    }

    public object ExecuteScalar()
    {
        try
        {
            Connection.Open();
            return Command.ExecuteScalar();
        }
        finally
        {
            if (Connection != null || Connection.State != ConnectionState.Closed)
                Connection.Close();
        }
    }


    public void AddParameter(SqlParameter parameter)
    {
        if (parameter.Value == null)
            parameter.Value = DBNull.Value;

        Command.Parameters.Add(parameter);
    }


    public void AddParameter(string name, object value)
    {
        SqlParameter parameter = new SqlParameter(name, value);

        if (parameter.Value == null)
            parameter.Value = DBNull.Value;

        Command.Parameters.Add(parameter);
    }


    public void AddParameter(string name, object value, SqlDbType type)
    {
        SqlParameter parameter = new SqlParameter(name, type);
        parameter.Value = value;

        if (parameter.Value == null)
            parameter.Value = DBNull.Value;

        Command.Parameters.Add(parameter);
    }


    public void AddParameter(string name, object value, SqlDbType type, int valueSize)
    {
        SqlParameter parameter = new SqlParameter(name, type, valueSize);
        parameter.Value = value;

        if (parameter.Value == null)
            parameter.Value = DBNull.Value;

        Command.Parameters.Add(parameter);
    }


    public void AddParameters(List<SqlParameter> parameters)
    {
        foreach (SqlParameter item in parameters)
        {
            if (item.Value == null)
                item.Value = DBNull.Value;
        }

        Command.Parameters.AddRange(parameters.ToArray());
    }

}

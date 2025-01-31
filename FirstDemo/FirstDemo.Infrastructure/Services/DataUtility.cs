using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;


namespace FirstDemo.Infrastructure.Services;

public class DataUtility : IDataUtility
{
    private readonly string? _connectionString;

    //// From IConfiguration, can Access  appsettings.json file all info -
    public DataUtility(IConfiguration config)
    {
        _connectionString = config?.GetConnectionString("DefaultConnection");
    }

    private SqlCommand _PrepareCommand(string sql, Dictionary<string, object>? parameters, CommandType? cmdType)
    {
        SqlConnection sqlConnection = new SqlConnection();
        sqlConnection.ConnectionString = _connectionString;

        SqlCommand sqlCommand = sqlConnection.CreateCommand();
        sqlCommand.Connection = sqlConnection;
        sqlCommand.CommandText = sql;

        if(cmdType is not null)
            sqlCommand.CommandType = (CommandType)cmdType;

        if (parameters != null)
        {
            foreach (var item in parameters)
            {
                sqlCommand.Parameters.Add(new SqlParameter(item.Key, item.Value));
            }
        }

        return sqlCommand;
    }

    public async Task ExecuteCommandAsync(string command, Dictionary<string, object>? parameters, CommandType? cmdType)
    {
        using SqlCommand sqlCommand = _PrepareCommand(command, parameters, cmdType);

        try
        {
            if (sqlCommand.Connection.State != System.Data.ConnectionState.Open)
            {
                sqlCommand.Connection.Open();
            }

            int impact = await sqlCommand.ExecuteNonQueryAsync();
        }
        catch (Exception ex)
        {
        }
        finally
        {
        }
    }

    public async Task<List<Dictionary<string, object>>> GetDataAsync(string command, Dictionary<string, object>? parameters, CommandType? cmdType)
    {
        using SqlCommand sqlCommand = _PrepareCommand(command, parameters, cmdType);

        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();

        try
        {
            if (sqlCommand.Connection.State != System.Data.ConnectionState.Open)
            {
                sqlCommand.Connection.Open();
            }

            SqlDataReader reader = await sqlCommand.ExecuteReaderAsync();

            while (reader.Read())
            {
                Dictionary<string, object> row = new Dictionary<string, object>();

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    row.Add(reader.GetName(i), reader.GetValue(i));
                }

                rows.Add(row);
            }
        }
        catch (Exception ex)
        {
        }
        finally
        {
        }

        return rows;
    }
}
using System.Data;

namespace FirstDemo.Infrastructure.Services
{
    public interface IDataUtility
    {
        Task ExecuteCommandAsync(string command, Dictionary<string, object>? parameters, CommandType? cmdType);
        Task<List<Dictionary<string, object>>> GetDataAsync(string command, Dictionary<string, object>? parameters, CommandType? cmdType);
    }
}
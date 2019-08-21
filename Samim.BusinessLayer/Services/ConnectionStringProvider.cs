using Microsoft.Extensions.Configuration;

namespace Samim.BusinessLayer.Services
{
	public interface IConnectionStringProvider
	{
		string GetConnectionString();
	}
	public class ConnectionStringProvider : IConnectionStringProvider
	{
		private const string ConnectionStringName = "DBConnection";
		private readonly IConfiguration _configuration;
		private string _connectionString = string.Empty;
		public ConnectionStringProvider(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		public string GetConnectionString()
		{
			if (_connectionString == null)
			{
				_connectionString = _configuration.GetConnectionString(ConnectionStringName);
			}
			return _connectionString;
		}
	}
}

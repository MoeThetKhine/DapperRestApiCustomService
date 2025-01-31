using Microsoft.Data.SqlClient;
using System.Data;

namespace DapperRestApiCustomService;

public class DapperService
{
	private readonly string _connectionString = "Data Source=.;Database=DotNetTrainingBatch5;User Id=sa;Password=sasa@123;TrustServerCertificate=True;";

	private IDbConnection CreateConnection() => new SqlConnection(_connectionString);


}

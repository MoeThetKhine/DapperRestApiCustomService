using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DapperRestApiCustomService;

public class DapperService
{
	private readonly string _connectionString = "Data Source=.;Database=DotNetTrainingBatch5;User Id=sa;Password=sasa@123;TrustServerCertificate=True;";

	private IDbConnection CreateConnection() => new SqlConnection(_connectionString);

	public List<T> Query<T>(string query, object? parameters = null)
	{
		using var db = CreateConnection();
		return db.Query<T>(query, parameters).ToList();
	}

	public async Task<List<T>> QueryAsync<T>(string query, object? parameters = null)
	{
		using var db = CreateConnection();
		var result = await db.QueryAsync<T>(query, parameters);
		return result.ToList();
	}

	public T? QueryFirstOrDefault<T>(string query, object? parameters = null)
	{
		using var db = CreateConnection();
		return db.QueryFirstOrDefault<T>(query, parameters);
	}

	public async Task<T?> QueryFirstOrDefaultAsync<T>(string query, object? parameters = null)
	{
		using var db = CreateConnection();
		return await db.QueryFirstOrDefaultAsync<T>(query, parameters);
	}
	public int Execute(string sql, object? parameters = null)
	{
		using var db = CreateConnection();
		return db.Execute(sql, parameters);
	}

	public async Task<int> ExecuteAsync(string sql, object? parameters = null)
	{
		using var db = CreateConnection();
		return await db.ExecuteAsync(sql, parameters);
	}
}

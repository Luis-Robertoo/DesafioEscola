using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DesafioEscola.Data.Context;

public sealed class DbSession : IDisposable
{
    public IDbConnection Connection { get; }

    private readonly IConfiguration _configuration;

    public DbSession(IConfiguration configuration)
    {
    _configuration = configuration;
    Connection = new SqlConnection(_configuration.GetConnectionString("Principal"));
    Connection.Open();
    }

    public void Dispose() => Connection?.Dispose();
}

using System.Data;

namespace ND_2023_12_06.Interfaces;

public interface IDapperDbContext
{
    public IDbConnection CreateConnection();

}

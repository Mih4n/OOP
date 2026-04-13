using Microsoft.Data.SqlClient;
using Second.Classlib.Sixth.Models;

namespace Second.Classlib.Sixth.Repositories;

public class MaterialsRepository(string cs) : BaseRepository<Material>(cs)
{
    protected override string TableName => "Materials";
    protected override Material Map(SqlDataReader r) => new() { Id = (int)r["Id"], Name = r["Name"].ToString()! };

    public override void Create(Material item)
    {
        using var conn = new SqlConnection(_cs);
        using var cmd = new SqlCommand("INSERT INTO Materials (Name) VALUES (@n)", conn);
        cmd.Parameters.AddWithValue("@n", item.Name);
        conn.Open(); cmd.ExecuteNonQuery();
    }

    public override void Update(Material item)
    {
        using var conn = new SqlConnection(_cs);
        using var cmd = new SqlCommand("UPDATE Materials SET Name = @n WHERE Id = @id", conn);
        cmd.Parameters.AddWithValue("@n", item.Name);
        cmd.Parameters.AddWithValue("@id", item.Id);
        conn.Open(); cmd.ExecuteNonQuery();
    }
}

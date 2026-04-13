using Microsoft.Data.SqlClient;
using Second.Classlib.Sixth.Models;

namespace Second.Classlib.Sixth.Repositories;

public class SuppliersRepository(string cs) : BaseRepository<Supplier>(cs)
{
    protected override string TableName => "Suppliers";
    protected override Supplier Map(SqlDataReader r) => new() { Id = (int)r["Id"], Name = r["Name"].ToString()! };
    
    public override void Create(Supplier item)
    {
        using var conn = new SqlConnection(_cs);
        using var cmd = new SqlCommand("INSERT INTO Suppliers (Name) VALUES (@n)", conn);
        cmd.Parameters.AddWithValue("@n", item.Name);
        conn.Open(); cmd.ExecuteNonQuery();
    }

    public override void Update(Supplier item)
    {
        using var conn = new SqlConnection(_cs);
        using var cmd = new SqlCommand("UPDATE Suppliers SET Name = @n WHERE Id = @id", conn);
        cmd.Parameters.AddWithValue("@n", item.Name);
        cmd.Parameters.AddWithValue("@id", item.Id);
        conn.Open(); cmd.ExecuteNonQuery();
    }
}

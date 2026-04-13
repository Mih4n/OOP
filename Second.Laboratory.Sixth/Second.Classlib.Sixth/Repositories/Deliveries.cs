using Microsoft.Data.SqlClient;
using Second.Classlib.Sixth.Models;

namespace Second.Classlib.Sixth.Repositories;

public class DeliveriesRepository(string cs) : BaseRepository<Delivery>(cs)
{
    protected override string TableName => "Deliveries";
    protected override Delivery Map(SqlDataReader r) => new() {
        Id = (int)r["Id"], SupplierId = (int)r["SupplierId"], MaterialId = (int)r["MaterialId"], DeliveryDate = (DateTime)r["DeliveryDate"]
    };

    public override void Create(Delivery item)
    {
        using var conn = new SqlConnection(_cs);
        using var cmd = new SqlCommand("INSERT INTO Deliveries (SupplierId, MaterialId, DeliveryDate) VALUES (@sid, @mid, @d)", conn);
        cmd.Parameters.AddWithValue("@sid", item.SupplierId);
        cmd.Parameters.AddWithValue("@mid", item.MaterialId);
        cmd.Parameters.AddWithValue("@d", item.DeliveryDate);
        conn.Open(); cmd.ExecuteNonQuery();
    }

    public override void Update(Delivery item)
    {
        using var conn = new SqlConnection(_cs);
        using var cmd = new SqlCommand("UPDATE Deliveries SET SupplierId = @sid, MaterialId = @mid WHERE Id = @id", conn);
        cmd.Parameters.AddWithValue("@sid", item.SupplierId);
        cmd.Parameters.AddWithValue("@mid", item.MaterialId);
        cmd.Parameters.AddWithValue("@id", item.Id);
        conn.Open(); cmd.ExecuteNonQuery();
    }
}

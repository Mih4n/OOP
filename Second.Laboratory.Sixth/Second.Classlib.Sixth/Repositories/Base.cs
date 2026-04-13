using Microsoft.Data.SqlClient;

namespace Second.Classlib.Sixth.Repositories;

public abstract class BaseRepository<T>(string cs) : IRepository<T> where T : class
{
    protected readonly string _cs = cs;
    protected abstract string TableName { get; }
    protected abstract T Map(SqlDataReader r);

    public IEnumerable<T> GetAll()
    {
        var list = new List<T>();
        using var conn = new SqlConnection(_cs);
        using var cmd = new SqlCommand($"SELECT * FROM {TableName}", conn);
        conn.Open();
        using var r = cmd.ExecuteReader();
        while (r.Read()) list.Add(Map(r));
        return list;
    }

    public T? GetById(int id)
    {
        using var conn = new SqlConnection(_cs);
        using var cmd = new SqlCommand($"SELECT * FROM {TableName} WHERE Id = @id", conn);
        cmd.Parameters.AddWithValue("@id", id);
        conn.Open();
        using var r = cmd.ExecuteReader();
        return r.Read() ? Map(r) : null;
    }

    public void Delete(int id)
    {
        using var conn = new SqlConnection(_cs);
        using var cmd = new SqlCommand($"DELETE FROM {TableName} WHERE Id = @id", conn);
        cmd.Parameters.AddWithValue("@id", id);
        conn.Open();
        cmd.ExecuteNonQuery();
    }

    public abstract void Create(T item);
    public abstract void Update(T item);
}
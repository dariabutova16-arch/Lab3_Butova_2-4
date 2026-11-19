using SQLite;
using StudentCabinet.Models;

namespace StudentCabinet.Services;

public class StudentRepository : IStudentRepository
{
    private readonly SQLiteAsyncConnection _db;
    private bool _ready;

    public StudentRepository(string dbPath)
    {
        _db = new SQLiteAsyncConnection(dbPath);
    }

    private async Task InitAsync()
    {
        if (_ready) return;
        await _db.CreateTableAsync<Student>();
        _ready = true;
    }

    public async Task<List<Student>> GetAllAsync()
    {
        await InitAsync();
        return await _db.Table<Student>().ToListAsync();
    }

    public async Task<Student?> GetByIdAsync(int id)
    {
        await InitAsync();
        return await _db.FindAsync<Student>(id);
    }

    public async Task<int> SaveAsync(Student student)
    {
        await InitAsync();
        if (student.Id == 0)
            return await _db.InsertAsync(student);
        return await _db.UpdateAsync(student);
    }

    public async Task<int> DeleteAsync(Student student)
    {
        await InitAsync();
        return await _db.DeleteAsync(student);
    }
}
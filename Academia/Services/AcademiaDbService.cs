using Academia.MVVM.Models;
using SQLite;

namespace Academia.Services;

public class AcademiaDbService : IAcademiaDbService
{
    private SQLiteAsyncConnection? _db;
    private const string DbName = "academia.db3";

    private async Task<SQLiteAsyncConnection> GetConnectionAsync()
    {
        if (_db != null) return _db;
        var path = Path.Combine(FileSystem.AppDataDirectory, DbName);
        _db = new SQLiteAsyncConnection(path);
        await _db.CreateTableAsync<Exercicio>();
        return _db;
    }

    public async Task InitAsync()
    {
        var db = await GetConnectionAsync();

        // Migrar coluna nova se faltar
        var info = await db.GetTableInfoAsync("exercicios");
        if (!info.Any(c => c.Name == nameof(Exercicio.DuracaoMinutos)))
        {
            await db.ExecuteAsync($"ALTER TABLE exercicios ADD COLUMN {nameof(Exercicio.DuracaoMinutos)} INTEGER NOT NULL DEFAULT 0");
        }
    }

    public async Task<List<DateTime>> GetDatasComRegistroAsync()
    {
        var db = await GetConnectionAsync();
        // devolve datas distintas (apenas dia, ignorando hora)
        var todos = await db.Table<Exercicio>().ToListAsync();
        return todos
            .Select(e => e.Data.Date)
            .Distinct()
            .OrderByDescending(d => d)
            .ToList();
    }

    public async Task<List<Exercicio>> GetByDateAsync(DateTime date)
    {
        var db = await GetConnectionAsync();
        var start = date.Date;
        var end = start.AddDays(1);

        return await db.Table<Exercicio>()
        .Where(e => e.Data >= start && e.Data < end)
        .OrderByDescending(e => e.Id)
        .ToListAsync();
    }

    public async Task<int> AddAsync(Exercicio item)
    {
        var db = await GetConnectionAsync();
        return await db.InsertAsync(item);
    }

    public async Task<int> UpdateAsync(Exercicio item)
    {
        var db = await GetConnectionAsync();
        return await db.UpdateAsync(item);
    }

    public async Task<int> DeleteAsync(Exercicio item)
    {
        var db = await GetConnectionAsync();
        return await db.DeleteAsync(item);
    }
}

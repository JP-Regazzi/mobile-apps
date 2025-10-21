using Academia.MVVM.Models;

namespace Academia.Services;
public interface IAcademiaDbService
{
    Task InitAsync();
    Task<List<DateTime>> GetDatasComRegistroAsync();
    Task<List<Exercicio>> GetByDateAsync(DateTime date);
    Task<int> AddAsync(Exercicio item);
    Task<int> UpdateAsync(Exercicio item);
    Task<int> DeleteAsync(Exercicio item);
}

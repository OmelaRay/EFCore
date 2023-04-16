using EFCore.Model;

namespace EFCore.Shared.Interfaces;
public interface ICategoryService
{
    Category? Add(string name);
    void Delete(Category category);
    void Edit(Category category);
    bool Exists(string name);
    IEnumerable<object> GetCatagoriesList();
    List<Category> GetCategoriesByName(string name);
    Category? GetCategoryById(int id);
    Category? GetCategoryByName(string name);
    int LoadProducts(Category category);
}
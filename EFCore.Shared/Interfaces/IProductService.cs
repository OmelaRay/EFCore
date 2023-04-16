using EFCore.Model;

namespace EFCore.Shared.Interfaces;
public interface IProductService
{
    Product Add(Product product);
    void Delete(Product product);
    Product Edit(Product product);
    Product? GetProductById(int id);
    List<Product> GetProductsByName(string productName);
    int LoadCategories(Product product);
    List<Product> Search(Func<Product, bool> filter, bool loadRalatedData = false);
}
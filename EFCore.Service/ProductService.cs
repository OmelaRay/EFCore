using EFCore.DAL;
using EFCore.Model;
using EFCore.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Service;
public class ProductService : IProductService
{
    private readonly DataContext context;

    public ProductService(DataContext context)
    {
        this.context = context;
    }

    public List<Product> GetProductsByName(string productName)
        => this.context.Product.Where(p => p.Name.ToLower().Contains(productName.ToLower())).ToList();

    public Product? GetProductById(int id) => this.context.Product.Find(id);

    public Product Add(Product product)
    {
        this.context.Product.Add(product);
        this.context.SaveChanges();
        return product;
    }

    public int LoadCategories(Product product)
    {
        this.context.Product.Entry(product).Collection(p => p.Categories).Load();
        return product.Categories.Count;
    }

    public List<Product> Search(Func<Product, bool> filter, bool loadRalatedData = false)
        => (loadRalatedData) ? this.context.Product.Include(p => p.Categories).Where(filter).ToList() : this.context.Product.Where(filter).ToList();

    public Product Edit(Product product)
    {
        Product set = this.context.Product.Where(p => p.Id == product.Id).FirstOrDefault();
        set.Name = product.Name;
        set.Description = product.Description;
        set.Price = product.Price;
        foreach (var item in product.Categories)
        {
            if (!set.Categories.Contains(item)) {
                set.Categories.Add(context.Category.Where(p => p.Id == item.Id).FirstOrDefault());
            }
        }
        foreach (var item in set.Categories)
        {
            if (!product.Categories.Contains(item))
            {
                set.Categories.Remove(item);
            }
        }
        this.context.SaveChanges();
        return product;
    }

    public void Delete(Product product)
    {
        this.context.Product.Remove(product);
        this.context.SaveChanges();
    }
}

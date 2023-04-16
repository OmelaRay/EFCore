using EFCore.DAL;
using EFCore.Model;
using EFCore.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Service;
public class CategoryService : ICategoryService
{
    private readonly DataContext context;

    public CategoryService(DataContext context)
    {
        this.context = context;
    }

    public bool Exists(string name) => this.context.Category.Where(c => c.Name == name).Any();

    public Category? GetCategoryByName(string name) => this.context.Category.Where(c => c.Name == name).FirstOrDefault();

    public List<Category> GetCategoriesByName(string name)
        => this.context.Category.Where(c => c.Name.ToLower().Contains(name.ToLower())).ToList();

    public Category? GetCategoryById(int id) => this.context.Category.Find(id);

    public Category? Add(string name)
    {
        Category? added;
        try
        {
            added = this.context.Category.Add(new() { Name = name }).Entity;
            this.context.SaveChanges();
        }
        catch (DbUpdateException)
        {
            added = null;
        }
        return added;
    }

    public int LoadProducts(Category category)
    {
        this.context.Entry(category).Collection(c => c.Products).Load();
        return category.Products.Count;
    }

    public IEnumerable<object> GetCatagoriesList()
    {
        List<string> categories = new();
        foreach ( var item in this.context.Category)
        {
            string catagory = item.Name + ": " + "\n";
            foreach (var thing in item.Products)
            {
                catagory += "-" + thing.Name + "\n";
            }
            categories.Add(catagory);
        }
        return categories;
    }

    public void Edit(Category category)
    {
        var set = this.context.Category.Where(p => p.Id == category.Id).FirstOrDefault();
        set.Name = category.Name;
        this.context.SaveChanges();
    }

    public void Delete(Category category)
    {
        this.context.Remove(category);
        this.context.SaveChanges();
    }
}

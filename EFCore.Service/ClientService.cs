using EFCore.DAL;
using EFCore.Model;
using EFCore.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Service;
public class ClientService : IClientService
{
    private readonly DataContext context;

    public ClientService(DataContext context)
    {
        this.context = context;
    }

    public List<Client> GetClientsByLastName(string lastName)
        => this.context.Client.Where(c => c.LastName.ToLower().Contains(lastName.ToLower())).ToList();

    public Client? GetClientById(int id) => this.context.Client.Find(id);

    public bool IsEmailInUse(string email) => this.context.Client.Where(c => c.Email == email).Any();
    public Client? Add(Client client)
    {
        Client? added;
        try
        {
            added = this.context.Client.Add(client).Entity;
            this.context.SaveChanges();
        }
        catch (DbUpdateException)
        {
            added = null;
        }
        return added;
    }

    public List<Client> GetClientsByEmail(string v)
   => this.context.Client.Where(c => c.Email.Contains(v)).ToList();

    public List<Client> GetClientsByPhone(string v)
   => this.context.Client.Where(c => c.Phone.Contains(v)).ToList();

    public void Edit(Client client)
    {
        var set = this.context.Client.Where(p => p.Id == client.Id).FirstOrDefault();
        set.FirstName = client.FirstName;
        set.LastName = client.LastName;
        set.Phone = client.Phone;
        set.Email = client.Email;
        this.context.SaveChanges();
    }

    public void Delete(Client client)
    {
        this.context.Remove(client);
        this.context.SaveChanges();
    }
}

using EFCore.Model;

namespace EFCore.Shared.Interfaces;
public interface IClientService
{
    Client? Add(Client client);
    void Delete(Client client);
    void Edit(Client client);
    Client? GetClientById(int id);
    List<Client> GetClientsByEmail(string v);
    List<Client> GetClientsByLastName(string lastName);
    List<Client> GetClientsByPhone(string v);
    bool IsEmailInUse(string email);
}
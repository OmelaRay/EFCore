using EFCore.Model;

namespace EFCore.Shared.Interfaces;
public interface IOrderService
{
    Order? Add(Order order);
    List<int> All(int Id);
    void Delete(Order order);
    Order? FindById(int orderId, bool loadRalatedData = false);
    List<Order> Search(Func<Order, bool> filter, bool loadRalatedData = false);
}
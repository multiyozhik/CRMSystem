using Microsoft.EntityFrameworkCore;
namespace CRMSystem.Models
{
    public class HomeModel
    {
        private readonly OrdersDbContext context;
        public HomeModel(OrdersDbContext ordersDBContext)
        {
            context = ordersDBContext;
        }

        public async Task Add(string name, string email, string message)
        {
            await context.Orders.AddAsync(new Order(Guid.NewGuid(), DateTime.Now.ToLocalTime(), name, email, message));
            context.SaveChanges();
        }

    }
}

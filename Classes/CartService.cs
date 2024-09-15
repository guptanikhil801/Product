using Product.Classes.Models;
using Product.Contexts;
using Product.Interfaces;

namespace Product.Classes
{
    public class CartService : ICartService
    {
        private readonly ApplicationDBContext context;

        public CartService(ApplicationDBContext context)
        {
            this.context = context;
        }

        public async Task<bool> AddProductsToCartAsync(Cart cartItem)
        {
            await context.UserCart.AddAsync(cartItem);
            return context.SaveChanges() == 1;
        }

        public IEnumerable<Cart> GetCart()
        {
            return context.UserCart.ToList();
        }
    }
}

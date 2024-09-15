using Product.Classes.Models;

namespace Product.Interfaces
{
    public interface ICartService
    {
        Task<bool> AddProductsToCartAsync(Product.Classes.Models.Cart product);
        IEnumerable<Cart> GetCart();

    }
}

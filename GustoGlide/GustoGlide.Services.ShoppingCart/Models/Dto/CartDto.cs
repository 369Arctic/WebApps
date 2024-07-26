namespace GustoGlide.Services.ShoppingCartAPI.Models.Dto
{
    public class CartDto
    {
        // basically when we want to retrive a shopping cart for a user
        // that will have one cart header and in can have multiple car details
        public CartHeaderDto CartHeader { get; set; }
        public IEnumerable<CartDetailsDto>? CartDetails { get; set; }
    }
}

namespace Codecool.CodecoolShop.Models
{
    public class CartItem
    {
        public Product Product { get; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Sum { get; private set; }
        public int Id { get; set; }

        public CartItem(){}
        public CartItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
            CountSum();
        }
        
        public void CountSum()
        {
            Sum = Product.DefaultPrice * Quantity;
        }

        public void IncrementQuantity(int num = 1)
        {
            Quantity += num;
        }

        public void DecrementQuantity(int num = 1)
        {
            int newQuantity = Quantity - num;
            Quantity = newQuantity < 0 ? 0 : newQuantity;
        }

        public void EditQuantity(int newQuantity)
        {
            Quantity = newQuantity < 0 ? 0 : newQuantity;
        }
    }
}
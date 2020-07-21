using System;

namespace ObjectOriented.SystemDesigns
{
    interface IProduct
    {
        bool GetStatus(int id);
        ProductDetails GetProductDetails(int id);
        bool UpdateProduct(int id, ProductDetails product);
        int FindTopic(int productId);
    }

    class Product : IProduct
    {
        private ProductDetails productDetails;

        public bool GetStatus(int id)
        {
            throw new System.NotImplementedException();
        }

        public bool UpdateProduct(int id, ProductDetails product)
        {
            throw new System.NotImplementedException();
        }

        public int FindTopic(int productId)
        {
            throw new System.NotImplementedException();
        }

        public ProductDetails GetProductDetails(int id)
        {
            throw new System.NotImplementedException();
        }
    }

    class ProductDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    interface ICatalog
    {
        bool Create(Topic topic);
        bool Delete(int topicId);
        bool AddProduct(Product product);
        bool DeleteProduct(int productId);
        Product[] GetProducts(int topicId);
    }

    class Topic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Product[] Products { get; set; }
    }

    class Catalog : ICatalog
    {
        private Topic[] topics;

        public bool AddProduct(Product product)
        {
            throw new System.NotImplementedException();
        }

        public bool Create(Topic topic)
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(int topicId)
        {
            throw new System.NotImplementedException();
        }

        public bool DeleteProduct(int productId)
        {
            throw new System.NotImplementedException();
        }

        public Product[] GetProducts(int topicId)
        {
            throw new System.NotImplementedException();
        }
    }

    interface IShoppingCart
    {
        bool AddItem(CartItem item);
        bool RemoveItem(CartItem item);
        CartSummary CheckOut();
    }

    class CartItem
    {
        public int Quantity { get; set; }
        public int Discount { get; set; }
        public Product Product { get; set; }

        public decimal GetTotal()
        {
            return 0;
        }
    }

    class CartSummary
    {
        public decimal TotalAmount { get; set; }
        public decimal TotalDiscount { get; set; }
        public Product[] Products { get; set; }
    }

    class ShoppingCart : IShoppingCart
    {
        private int orderId;

        protected CartItem[] cartItems;

        public bool AddItem(CartItem item)
        {
            throw new System.NotImplementedException();
        }

        public CartSummary CheckOut()
        {
            throw new System.NotImplementedException();
        }

        public bool RemoveItem(CartItem item)
        {
            throw new System.NotImplementedException();
        }
    }

    interface IOrder
    {
        void CreateOrder();
        bool Cancel(int id);
        bool Close(int id);
        string CheckShipment(int id);
    }

    class Order : IOrder
    {
        private OrderDetails orderDetails;

        public bool Cancel(int id)
        {
            throw new NotImplementedException();
        }

        public string CheckShipment(int id)
        {
            throw new NotImplementedException();
        }

        public bool Close(int id)
        {
            throw new NotImplementedException();
        }

        public void CreateOrder()
        {
            throw new NotImplementedException();
        }
    }

    class OrderDetails
    {
        public int Number { get; set; }
        public DateTime Date { get; set; }
        public bool status { get; set; }
        public string Address { get; set; }
    }

    interface ICustomer
    {
        bool AddToCart(int productId);
        bool RemoveFromCart(int productId);
        bool Checkout();
        bool ProcessPayment();
        int PlaceOrder();
    }

    interface IMember : ICustomer
    {
        bool GetProfile(int memberId);
        bool UpdateProfile(int memberId);
    }

    interface IGuest : ICustomer
    {
        bool CreateProfile(MemberDetails memberDetails);
    }

    class MemberDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string CreditInfo { get; set; }
    }


    class Guest : IGuest
    {
        private ShoppingCart shoppingCart;

        public bool AddToCart(int productId)
        {
            throw new NotImplementedException();
        }

        public bool Checkout()
        {
            throw new NotImplementedException();
        }

        public bool CreateProfile(MemberDetails memberDetails)
        {
            throw new NotImplementedException();
        }

        public int PlaceOrder()
        {
            throw new NotImplementedException();
        }

        public bool ProcessPayment()
        {
            throw new NotImplementedException();
        }

        public bool RemoveFromCart(int productId)
        {
            throw new NotImplementedException();
        }
    }

    class Member : IMember
    {
        private ShoppingCart shoppingCart;

        public bool AddToCart(int productId)
        {
            throw new NotImplementedException();
        }

        public bool Checkout()
        {
            throw new NotImplementedException();
        }

        public bool GetProfile(int memberId)
        {
            throw new NotImplementedException();
        }

        public int PlaceOrder()
        {
            throw new NotImplementedException();
        }

        public bool ProcessPayment()
        {
            throw new NotImplementedException();
        }

        public bool RemoveFromCart(int productId)
        {
            throw new NotImplementedException();
        }

        public bool UpdateProfile(int memberId)
        {
            throw new NotImplementedException();
        }
    }
}

namespace Ecom.Client.Core.HttpClients
{
    public class ApiConstants
    {
        #region Customer
        public const string GetAllCustomers = "api/Customer/GetAllCustomers";
        public const string AddCustomer = "api/Customer/AddCustomer";
        public const string UpdateCustomer = "api/Customer/EditCustomer";
        public const string GetCustomer = "api/Customer/GetCustomerById";
        public const string DeleteCustomer = "api/Customer/DeleteCustomer";
        public const string CheckLogin = "api/Account/LoginUser";
        public const string RegisterUser = "api/Account/RegisterUser";
        #endregion

        #region Product
        public const string GetAllProducts = "api/Product/GetAllProducts";
        public const string AddProduct = "api/Product/AddProduct";
        public const string UpdateProduct = "api/Product/EditProduct";
        public const string GetProduct = "api/Product/GetProductById";
        public const string DeleteProduct = "api/Product/DeleteProduct";
        #endregion

        #region Category
        public const string GetAllCategories = "api/Category/GetAllCategories";
        public const string AddCategory = "api/Category/AddCategory";
        public const string UpdateCategory = "api/Category/EditCategory";
        public const string GetCategory = "api/Category/GetCategoryById";
        public const string DeleteCategory = "api/Category/DeleteCategory";
        #endregion

        #region Cart
        public const string GetCart = "api/Cart/GetCart";
        public const string GetCartItems = "api/Cart/GetCartItems";
        public const string AddCartItem = "api/Cart/AddCartItem";
        public const string RemoveCartItem = "api/Cart/RemoveCartItem";
        public const string EmptyCart = "api/Cart/EmptyCart";
        #endregion

        #region Order
        public const string GetOrders = "api/Order/GetOrders";
        public const string GetOrderItems = "api/Order/GetOrderItems";
        public const string CreateOrder = "api/Order/CreateOrder";
        public const string AddOrderItem = "api/Order/AddOrderItem";

        #endregion
    }
}

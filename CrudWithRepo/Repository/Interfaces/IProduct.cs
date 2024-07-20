using CrudWithRepo.Models;

namespace CrudWithRepo.Repository.Interfaces
{
    public interface IProduct
    {
        List<Product> GetAllProducts();
        Product GetProductById(int? id);
        Product SaveProduct(Product product);
        Product UpdateProduct(Product product);
        Product DeleteProduct(int? id);
        string UploadImage(Product product);
    }
}

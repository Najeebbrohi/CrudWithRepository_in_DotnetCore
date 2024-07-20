using CrudWithRepo.Data;
using CrudWithRepo.Models;
using CrudWithRepo.Repository.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace CrudWithRepo.Repository.Services
{
    public class ProductService : IProduct
    {
        private readonly ApplicationContext context;
        private readonly IWebHostEnvironment env;

        public ProductService(ApplicationContext context, IWebHostEnvironment env)
        {
            this.context = context;
            this.env = env;
        }
        public List<Product> GetAllProducts()
        {
            var data = context.Products.ToList();
            return data;
        }

        public Product GetProductById(int? id)
        {
            var product = context.Products.Find(id);
            return product!;
        }
        public Product SaveProduct(Product product)
        {
            string uniqueFileName = UploadImage(product);
            Product data = new Product()
            {
                Name = product.Name,
                Descripion = product.Descripion,
                Price = product.Price,
                Photo = uniqueFileName
            };
            context.Add(data);
            context.SaveChanges();
            return data;
        }
        public Product UpdateProduct(Product product)
        {
            string uniqueFileName = UploadImage(product);
            var data = context.Products.Where(x => x.Id == product.Id).FirstOrDefault();
            if (data != null)
            {
                data.Name = product.Name;
                data.Descripion = product.Descripion;
                data.Price = product.Price;
                data.Photo = uniqueFileName;
                context.Update(data);
                context.SaveChanges();
                return data;
            }
            else
            {
                return null;
            }
        }
        public Product DeleteProduct(int? id)
        {
            var product = context.Products.Find(id);
            if (product != null)
            {
                context.Remove(product);
                context.SaveChanges();
                return product;
            }
            else
            {
                return null;
            }
        }
        public string UploadImage(Product product)
        {
            string uniqueFileName = string.Empty;
            string extension = Path.GetExtension(product.ImgPath.FileName).ToLower();
            string[] isExtension = { ".jpeg", ".png", ".jpg" };
            if (product.ImgPath != null)
            {
                if (isExtension.Contains(extension))
                {
                    if (product.ImgPath.Length <= 1000000)
                    {
                        string fileFolder = Path.Combine(env.WebRootPath, "/images/products");
                        uniqueFileName = Guid.NewGuid().ToString() + "_" + product.ImgPath.FileName;
                        string filePath = Path.Combine(fileFolder, uniqueFileName);
                        using (var fileStrem = new FileStream(filePath, FileMode.Create))
                        {
                            product.ImgPath.CopyTo(fileStrem);
                        }
                    }
                }
            }
            return uniqueFileName;
        }
    }
}

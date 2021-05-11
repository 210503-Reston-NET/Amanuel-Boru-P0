using System;
using StoreDL;
using StoreModels;
using System.Collections.Generic;

namespace StoreBL
{
    public class ProductBL
    {
        private ProductRepo _repo;
        public ProductBL(ProductRepo newProductRepo)
        {
            _repo = newProductRepo;
        }

        public Product AddProduct(Product newProduct){
            return _repo.AddProduct(newProduct);
        }

        public List<Product> GetAllProduct(){
            return _repo.GetAllProduct();
        }

        public bool ProductExists(Product newProduct){
            if (_repo.GetProduct(newProduct) != null) return true;
            else{
                return false;
            }
        }
    }
}
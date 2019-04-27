using ClothBazar.Entities;
using ClothBazar.Services;
using ClothBazar.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClothBazar.Web.Controllers
{
    public class ProductController : Controller
    {
        ProductsService productsService = new ProductsService();
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        
        public ActionResult ProductTable(string search)
        {
            var products = productsService.GetProducts();

            if(string.IsNullOrEmpty(search)==false)
            {
                products = products.Where(p => p.Name.ToLower().Contains(search.ToLower())).ToList();

            }


            return PartialView(products);
        }


        // GET: Create a Category
        [HttpGet]
        public ActionResult Create()
        {

            CategoriesService categoriesService = new CategoriesService();
            var categories = categoriesService.GetCategories();

            return PartialView(categories);
        }

        // Post: Category
        [HttpPost]
        public ActionResult Create(NewCategoryViewModel model)
        {
            CategoriesService categoriesService = new CategoriesService();
            var newProduct = new Product();
            newProduct.Name = model.Name;
            newProduct.Description = model.Decription;
            newProduct.Price = model.Price;
            newProduct.Category = categoriesService.GetCategory(model.CategoryID);

            productsService.SaveProduct(newProduct);
            return RedirectToAction("ProductTable");
        }


        // GET: Edit a Category
        [HttpGet]
        public ActionResult Edit(int ID)
        {
            var product = productsService.GetProduct(ID);
            return PartialView(product);
        }

        // Post: Category
        [HttpPost]
        public ActionResult Edit(Product product)
        {

            productsService.UpdateProduct(product);
            return RedirectToAction("ProductTable");
        }

        // Post: Category
        [HttpPost]
        public ActionResult Delete(int ID)
        {

            productsService.DeleteProduct(ID);
            return RedirectToAction("ProductTable");
        }


    }
}
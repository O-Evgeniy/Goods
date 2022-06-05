using GoodsLib;
using GoodsLib.Entity;
using GoodsManagerWeb.Models;
using GoodsManagerWeb.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GoodsManagerWeb.Controllers
{
    public class ProductController : Controller
    {
        IndexViewModel vm;

        public ProductController(IndexViewModel vm) 
        {
            this.vm = vm;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(vm);
        }

        [HttpPost]
        public IActionResult Index(ProductProviderEnum provider, double markup, int round, IFormFile uploadedFile)
        {
            if (provider == ProductProviderEnum.none || uploadedFile == null)
                return RedirectToAction("Index");
            markup /= 100;
            vm.LoadFile(provider, uploadedFile.FileName, uploadedFile.OpenReadStream(), markup+1, round);
            //vm.LoadFile(provider, uploadedFile.FileName, markup, round);
            return View(vm);
        }

        [HttpPost]
        public IActionResult Clear()
        {
            //vm.Products.Clear();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Save(IndexViewModel model)
        {
            var book  = vm.GetBook();
            using(var stream = new MemoryStream())
            { 
                book.Write(stream);
                var name = "накладная_" + DateTime.Now.ToString("d")+".xls";
                return File(stream.ToArray(), "text/plain",name);
            }
        }
    }
}

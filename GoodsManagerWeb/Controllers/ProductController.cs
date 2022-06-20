using GoodsLib;
using GoodsLib.Entity;
using GoodsManagerWeb.Models;
using GoodsManagerWeb.ViewModels;
using GoodsViewModel;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.UserModel;

namespace GoodsManagerWeb.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductContext db;
        public ProductController(ProductContext context) 
        {
            db = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var vm = new IndexViewModel();
            return View(vm);
        }

        [HttpPost]
        public IActionResult Index(ProductProviderEnum provider, double markup, int round, IFormFile uploadedFile)
        {
            if (provider == ProductProviderEnum.none || uploadedFile == null)
                return RedirectToAction("Index");

            markup /= 100;

            IndexViewModel vm = new IndexViewModel();
            vm.LoadFile(provider, uploadedFile.FileName, uploadedFile.OpenReadStream(), markup+1, round);

            return View(vm);
        }

        [HttpPost]
        public IActionResult Clear()
        {
            var products = db.Products;
            foreach(var product in products)
            {
                db.Products.Remove(product);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Save(IEnumerable<ProductBase> model)
        {
            var book  = GetBook(products);
            using(var stream = new MemoryStream())
            { 
                book.Write(stream);
                var name = "накладная_" + DateTime.Now.ToString("d")+".xls";
                return File(stream.ToArray(), "text/plain",name);
            }
        }

        private IWorkbook GetBook(IList<ProductBase> products)
        {
            return ExcelBuilder.GetBook(products, ExcelFormat.XLS);
        }
    }
}

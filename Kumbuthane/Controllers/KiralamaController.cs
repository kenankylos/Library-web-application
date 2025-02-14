using Kumbuthane.Models;
using Kumbuthane.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kumbuthane.Controllers
{
    [Authorize(Roles = UserRoles.Role_Admin)]
    public class KiralamaController : Controller
    {
        private readonly IKiralamaRepository _kiralamaRepo;
        private readonly IKitapRepository _kitapRepo;
        public readonly IWebHostEnvironment _webHostEnvironment;

        public KiralamaController(IKiralamaRepository kiralamaRepo, IKitapRepository kitapRepo,IWebHostEnvironment webHostEnvironment)
        {
            _kiralamaRepo = kiralamaRepo;
            _kitapRepo = kitapRepo;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Kiralama> objKiralamaList = _kiralamaRepo.GetAll(includeProps:"Kitap").ToList();
            return View(objKiralamaList);
        }

        public IActionResult EkleGuncelle(int? id)
        {
            IEnumerable<SelectListItem> KitapList = _kitapRepo.GetAll().Select(k => new SelectListItem
            {
                Text = k.KitapAdi,
                Value = k.Id.ToString()
            });

            ViewBag.KitapList = KitapList;

            if (id==null || id==0)
            {
            return View();

            }
            else
            {
                Kiralama? kiralamaVt = _kiralamaRepo.Get(u => u.Id == id);
                if (kiralamaVt == null)
                {
                    return NotFound();
                }
                return View(kiralamaVt);
            }
        }
        [HttpPost]
        public IActionResult EkleGuncelle(Kiralama kiralama)
        {
            //var errors = ModelState.Values.SelectMany(x => x.Errors);

            if (ModelState.IsValid)
            {
                
                if (kiralama.Id==0)
                {
                    _kiralamaRepo.Ekle(kiralama);
                    TempData["basarili"] = "Yeni Kiralama kaydı başarıyla oluşturuldu!";
                }
                else
                {
                    _kiralamaRepo.Guncelle(kiralama);
                    TempData["basarili"] = "Kiralama kaydı başarıyla güncellendi!";
                }

                _kiralamaRepo.Kaydet();
                return RedirectToAction("Index", "Kiralama");
            }

            return View();
        }
        
        public IActionResult Sil(int? id)
        {
            IEnumerable<SelectListItem> KitapList = _kitapRepo.GetAll().Select(k => new SelectListItem
            {
                Text = k.KitapAdi,
                Value = k.Id.ToString()
            });

            ViewBag.KitapList = KitapList;

            if (id == null || id == 0)
            {
                return NotFound();
            }
            Kiralama? kiralamaVt = _kiralamaRepo.Get(u => u.Id == id);
            if (kiralamaVt == null)
            {
                return NotFound();
            }
            return View(kiralamaVt);
        }
        [HttpPost,ActionName("Sil")]
        public IActionResult SilPOST(int? id)
        {
            Kiralama? kiralama = _kiralamaRepo.Get(u => u.Id == id);
            if (kiralama == null)
            {
                return NotFound();
            }
            _kiralamaRepo.Sil(kiralama);
            _kiralamaRepo.Kaydet();
            TempData["basarili"] = "Kayıt Silme işlemi başarılı.";
            return RedirectToAction("Index", "Kiralama");
        }
    }
}

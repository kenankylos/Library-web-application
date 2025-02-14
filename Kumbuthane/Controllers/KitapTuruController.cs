using Kumbuthane.Models;
using Kumbuthane.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kumbuthane.Controllers
{
    [Authorize(Roles = UserRoles.Role_Admin)]
    public class KitapTuruController : Controller
    {
        private readonly IKitapTuruRepository _kitapTuruRepo;

        public KitapTuruController(IKitapTuruRepository context)
        {
            _kitapTuruRepo = context;
        }
        public IActionResult Index()
        {
            List<KitapTuru>objKitapTuruList = _kitapTuruRepo.GetAll().ToList();
            return View(objKitapTuruList);
        }

        public IActionResult Ekle()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Ekle(KitapTuru kitapTuru)
        {
            if (ModelState.IsValid)
            {
                _kitapTuruRepo.Ekle(kitapTuru);
                _kitapTuruRepo.Kaydet();
                TempData["basarili"] = "Ekleme işlemi başarılı.";
                return RedirectToAction("Index", "KitapTuru");
            }
            return View();
        }
        public IActionResult Guncelle(int? id)
        {
            if (id==null || id==0)
            {
                return NotFound();
            }
            KitapTuru? kitapTuruVt = _kitapTuruRepo.Get(u=>u.Id==id);
            if (kitapTuruVt==null)
            {
                return NotFound();
            }
            return View(kitapTuruVt);
        }
        [HttpPost]
        public IActionResult Guncelle(KitapTuru kitapTuru)
        {
            if (ModelState.IsValid)
            {
                _kitapTuruRepo.Guncelle(kitapTuru);
                _kitapTuruRepo.Kaydet();
                TempData["basarili"] = "Güncelleme işlemi başarılı.";
                return RedirectToAction("Index", "KitapTuru");
            }
            return View();
        }
        public IActionResult Sil(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            KitapTuru? kitapTuruVt = _kitapTuruRepo.Get(u => u.Id == id);
            if (kitapTuruVt == null)
            {
                return NotFound();
            }
            return View(kitapTuruVt);
        }
        [HttpPost,ActionName("Sil")]
        public IActionResult SilPOST(int? id)
        {
            KitapTuru? kitapTuru = _kitapTuruRepo.Get(u => u.Id == id);
            if (kitapTuru == null)
            {
                return NotFound();
            }
            _kitapTuruRepo.Sil(kitapTuru);
            _kitapTuruRepo.Kaydet();
            TempData["basarili"] = "Silme işlemi başarılı.";
            return RedirectToAction("Index", "KitapTuru");
        }
    }
}

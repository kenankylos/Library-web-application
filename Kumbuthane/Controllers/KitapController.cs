using Kumbuthane.Models;
using Kumbuthane.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kumbuthane.Controllers
{
    
    public class KitapController : Controller
    {
        
        private readonly IKitapRepository _kitapRepo;
        private readonly IKitapTuruRepository _kitapTuruRepo;
        public readonly IWebHostEnvironment _webHostEnvironment;

        public KitapController(IKitapRepository kitapRepo, IKitapTuruRepository kitapTuruRepo,IWebHostEnvironment webHostEnvironment)
        {
            _kitapRepo = kitapRepo;
            _kitapTuruRepo = kitapTuruRepo;
            _webHostEnvironment = webHostEnvironment;
        }
        [Authorize(Roles = "Admin,Ogrenci")]
        public IActionResult Index()
        {
            //List<Kitap>objKitapList = _kitapRepo.GetAll().ToList();
            List<Kitap> objKitapList = _kitapRepo.GetAll(includeProps:"KitapTuru").ToList();
            return View(objKitapList);
        }
        [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult EkleGuncelle(int? id)
        {
            IEnumerable<SelectListItem> KitapTuruList = _kitapTuruRepo.GetAll().Select(k => new SelectListItem
            {
                Text = k.Name,
                Value = k.Id.ToString()
            });

            ViewBag.KitapTuruList = KitapTuruList;

            if (id==null || id==0)
            {
            return View();

            }
            else
            {
                Kitap? kitapVt = _kitapRepo.Get(u => u.Id == id);
                if (kitapVt == null)
                {
                    return NotFound();
                }
                return View(kitapVt);
            }
        }
        [HttpPost]
        [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult EkleGuncelle(Kitap kitap,IFormFile? file)
        {
            //var errors = ModelState.Values.SelectMany(x => x.Errors);

            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string kitapPath = Path.Combine(wwwRootPath, @"img");
                if (file!=null)
                {
                    using (var fileStream = new FileStream(Path.Combine(kitapPath, file.FileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    kitap.ResimUrl = @"\img\" + file.FileName;
                }
                
                if (kitap.Id==0)
                {
                    _kitapRepo.Ekle(kitap);
                    TempData["basarili"] = "Yeni Kitap başarıyla oluşturuldu!";
                }
                else
                {
                    _kitapRepo.Guncelle(kitap);
                    TempData["basarili"] = "Kitap başarıyla güncellendi!";
                }

                _kitapRepo.Kaydet();
                return RedirectToAction("Index", "Kitap");
            }

            return View();
        }
        /*
        public IActionResult Guncelle(int? id)
        {
            if (id==null || id==0)
            {
                return NotFound();
            }
            Kitap? kitapVt = _kitapRepo.Get(u=>u.Id==id);
            if (kitapVt==null)
            {
                return NotFound();
            }
            return View(kitapVt);
        }
        */
        /*
        [HttpPost]
        public IActionResult Guncelle(Kitap kitap)
        {
            if (ModelState.IsValid)
            {
                _kitapRepo.Guncelle(kitap);
                _kitapRepo.Kaydet();
                TempData["basarili"] = "Güncelleme işlemi başarılı.";
                return RedirectToAction("Index", "Kitap");
            }
            return View();
        }
        */
        [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult Sil(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Kitap? kitapVt = _kitapRepo.Get(u => u.Id == id);
            if (kitapVt == null)
            {
                return NotFound();
            }
            return View(kitapVt);
        }
        [HttpPost,ActionName("Sil")]
        [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult SilPOST(int? id)
        {
            Kitap? kitap = _kitapRepo.Get(u => u.Id == id);
            if (kitap == null)
            {
                return NotFound();
            }
            _kitapRepo.Sil(kitap);
            _kitapRepo.Kaydet();
            TempData["basarili"] = "Silme işlemi başarılı.";
            return RedirectToAction("Index", "Kitap");
        }
    }
}

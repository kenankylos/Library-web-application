using Kumbuthane.Utility;

namespace Kumbuthane.Models
{
    public class KitapTuruRepository : Repository<KitapTuru>, IKitapTuruRepository
    {
        private KumbuthaneDbContext _kumbuthaneDbContext;
        public KitapTuruRepository(KumbuthaneDbContext kumbuthaneDbContext) : base(kumbuthaneDbContext)
        {
            _kumbuthaneDbContext = kumbuthaneDbContext;
        }

        public void Guncelle(KitapTuru kitapTuru)
        {
            _kumbuthaneDbContext.Update(kitapTuru);
        }

        public void Kaydet()
        {
            _kumbuthaneDbContext.SaveChanges();
        }
    }
}

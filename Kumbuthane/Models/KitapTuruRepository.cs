using Kumbuthane.Utility;

namespace Kumbuthane.Models
{
    public class KitapRepository : Repository<Kitap>, IKitapRepository
    {
        private KumbuthaneDbContext _kumbuthaneDbContext;
        public KitapRepository(KumbuthaneDbContext kumbuthaneDbContext) : base(kumbuthaneDbContext)
        {
            _kumbuthaneDbContext = kumbuthaneDbContext;
        }

        public void Guncelle(Kitap kitap)
        {
            _kumbuthaneDbContext.Update(kitap);
        }

        public void Kaydet()
        {
            _kumbuthaneDbContext.SaveChanges();
        }
    }
}

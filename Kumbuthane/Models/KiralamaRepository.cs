using Kumbuthane.Utility;

namespace Kumbuthane.Models
{
    public class KiralamaRepository : Repository<Kiralama>, IKiralamaRepository
    {
        private KumbuthaneDbContext _kumbuthaneDbContext;
        public KiralamaRepository(KumbuthaneDbContext kumbuthaneDbContext) : base(kumbuthaneDbContext)
        {
            _kumbuthaneDbContext = kumbuthaneDbContext;
        }

        public void Guncelle(Kiralama kiralama)
        {
            _kumbuthaneDbContext.Update(kiralama);
        }

        public void Kaydet()
        {
            _kumbuthaneDbContext.SaveChanges();
        }
    }
}

namespace Actris.Infrastructure.EntityFramework.Queries
{
    public abstract class BaseCrudQuery
    {
        /// <summary>
        /// Query dasar ambil kolom apa saja tanpa filter where
        /// </summary>
        public abstract  string SelectPagedQuery { get; }

        /// <summary>
        /// Where query yang akan yang otomatis dipasang di grid nanti
        /// Contoh : ambil data yg isdeleted == false
        /// Format : e.IsDeleted = 0
        /// Kalo tidak ada kondisi khusus isi string kosong saja
        /// </summary>
        public abstract string SelectPagedQueryAdditionalWhere{ get; }
        public abstract string CountQuery { get; }
        public abstract string LookupTextQuery { get; }
        public abstract string AdaptiveFilterQuery { get; }
    }
}

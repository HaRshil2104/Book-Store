using Bookstore.Models.ViewModel;


namespace BookStore.repository
{
    public class BaseRepository
    {
        protected readonly postgres2Context _context = new postgres2Context();
    }
}

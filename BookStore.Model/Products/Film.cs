namespace BookStore.Model
{
    public class Film
    {
        public string Title { get; set; }

        public string ISBN { get; set; }

        public string YearOfPublishing { get; set; }

        public TypeGenre Genre { get; set; }
    }
}
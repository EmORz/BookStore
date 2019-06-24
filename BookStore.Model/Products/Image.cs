namespace BookStore.Model
{
    public class Image
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; }

        public int ProductId { get; set; }
        public virtual NewProduct Product { get; set; }
    }
}
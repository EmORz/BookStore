namespace BookStore.Model
{
    public class Image
    {
        public string Id { get; set; }

        public string ImageUrl { get; set; }

        public int ProductId { get; set; }
        //public virtual Product Product { get; set; }
    }
}
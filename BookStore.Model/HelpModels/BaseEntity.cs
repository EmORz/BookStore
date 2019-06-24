namespace BookStore.Model.HelpModels
{
    public abstract class EntityBase<T>
    {
        public T Id { get; set; }
    }
}
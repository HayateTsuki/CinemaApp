namespace Cinema.SharedModels.BaseModels
{
    public class SingleResult<T>
        where T : class, new()
    {
        public T Data { get; set; }
    }
}

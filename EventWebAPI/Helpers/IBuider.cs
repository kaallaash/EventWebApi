namespace EventWebAPI.Helpers
{
    public interface IBuider<T>
    {
        public T Build();
        public Task<T> BuildAsync();
    }
}

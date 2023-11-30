using Newtonsoft.Json;

namespace Cinema.Core.Filters
{
    public record ErrorResult(string Message, string Code)
    {
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}

using CheckMyStatus.Domain.Enum;
using Microsoft.AspNetCore.Http;

namespace CheckMyStatus.Domain.Response
{
    public class BaseResponse<T> : IBaseResponse<T>
    {
        public string Description { get; set; }
        public StatusCode StatusCode { get; set; }
        public T Data { get; set; }
        public bool LocalStatus { get; set; }
        public bool ApiStatus { get; set; }
    }

    public interface IBaseResponse<T>
    {
        string Description { get; }
        StatusCode StatusCode { get; }
        T Data { get; }
        bool LocalStatus { get; }
        bool ApiStatus { get; }
    }
}

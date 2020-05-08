using Newtonsoft.Json;
using MyWebApi.Models;
using WebApplication1.Models;

namespace Client.Entity
{
    public class HttpResponseMsg
    {
        public int StatusCode { get; set; }
        public object Data { get; set; }
        public string Info { get; set; }
    }

    public class ProductResultMsg : HttpResponseMsg
    {
        public Product Result
        {
            get
            {
                if (StatusCode == (int)StatusCodeEnum.Success)
                {
                    return JsonConvert.DeserializeObject<Product>(Data.ToString());
                }

                return null;
            }
        }
    }

    public class TokenResultMsg : HttpResponseMsg
    {
        public Token Result
        {
            get
            {
                if (StatusCode == (int)StatusCodeEnum.Success)
                {
                    return JsonConvert.DeserializeObject<Token>(Data.ToString());
                }

                return null;
            }
        }
    }
}
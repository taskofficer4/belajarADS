namespace Actris.Abstraction.Model.Response
{
    public class BaseJsonResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public BaseJsonResult()
        {
                
        }

        public static BaseJsonResult Error(string  message)
        {
            var error = new BaseJsonResult
            {
                Message = message,
                Success = false
            };
            return error;
        }

        public static BaseJsonResult Ok(object data)
        {
            var result = new BaseJsonResult
            {
                Data = data,
                Success = true
            };
            return result;
        }

    }
}

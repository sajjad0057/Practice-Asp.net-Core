namespace FirstDemo.Web.Models
{
    public enum ResponseTypes
    {
        Success,
        Warning,
        Info,
        Danger,

    }

    public class ResponseModel
    {
        public string Message { get; set; }
        public ResponseTypes Type { get; set; }
    }
}

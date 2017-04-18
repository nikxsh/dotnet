namespace WebApiServices.Helper
{
    public class Error
    {
        public int ErrorCode { get; set; }

        public string FieldName { get; set; }

        public string ErrorMessage { get; set; }

        public string Header { get; set; }
    }
}
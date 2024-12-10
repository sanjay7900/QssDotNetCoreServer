namespace EmployeeManagement
{
    public class ApiResponse<T> 
    {
        public int StatusCode {  get; set; }        
        public string Message {  get; set; }    
        public  T Data { get; set; }    
        public ApiResponse(T data,string message="Success", int statusCode = 200) 
        {
            Data = data;
            Message=message;
            StatusCode = statusCode;        
        }    
    }
}

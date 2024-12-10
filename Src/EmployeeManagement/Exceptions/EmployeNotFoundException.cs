namespace EmployeeManagement.Exceptions
{
    public class EmployeNotFoundException:Exception
    {
        public EmployeNotFoundException(string msg, Exception exception) : base(msg, exception) { }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}

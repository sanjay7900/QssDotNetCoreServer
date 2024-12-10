namespace EmployeeManagement.Exceptions
{
    public class DeleteEmployeException:Exception
    {
        public DeleteEmployeException(string msg, Exception exception) : base(msg, exception) { }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}

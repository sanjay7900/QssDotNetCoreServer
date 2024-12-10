namespace EmployeeManagement.Exceptions
{
    public class AddEmployeException:Exception
    {
        public AddEmployeException(string msg, Exception exception) : base(msg, exception) { }
        public override string ToString()
        {
            return base.ToString();
        }

    }
}

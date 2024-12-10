namespace EmployeeManagement.Exceptions
{
    public class UpdateEmployeException:Exception
    {
        public UpdateEmployeException(string msg, Exception exception) : base(msg,exception) { }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}

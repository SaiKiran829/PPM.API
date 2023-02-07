namespace PPM.DTO
{
    public class EmployeeDto
    {
            public int employeeID { get; set; }
            public string employeefirstName { get; set; }
            public string lastName { get; set; }
            public string email { get; set; }
            public string mobile { get; set; }
            public string address { get; set; }
            public int roleId { get; set; }


            public EmployeeDto(int empid, string FirstName, string LastName, string Email, string Mobile, string Address, int ROleID)
            {
                this.employeeID = empid;
                this.employeefirstName = FirstName;
                this.lastName = LastName;
                this.email = Email;
                this.mobile = Mobile;
                this.address = Address;
                this.roleId = ROleID;
            }

            public EmployeeDto()
            {

            }
    }
}
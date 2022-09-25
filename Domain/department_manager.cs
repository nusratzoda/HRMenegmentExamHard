namespace Domain;

public class department_manager
{
    public int EmployeeId { get; set; }
    public int DepartmentId { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public bool CurrentDepartment { get; set; }
}

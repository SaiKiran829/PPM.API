using PPM.Model;
using IEntityOperation;
using DAL;

namespace PPM.Domain
{
    public class ProjectManagement : IProject
    {
        readonly ProjectDal projectDal = new ProjectDal();
        public void AddProject(Project project)
        {
            projectDal.InsertIntoProjectTable(project.id, project.projectName, project.startDate, project.endDate);
        }

        public void DisplayAllProjects()
        {
            projectDal.ReadData();
        }

        public void DisplayProjectById(int projectId)
        {
            projectDal.ReadDataById(projectId);
        }

        public void AddEmployeesIntoProject(int projectId, int employeeId)
        {
            projectDal.AddEmployeesIntoProject(projectId, employeeId);
        }

        public void DisplayAllEmployeeInProjectById(int projectId)
        {
            projectDal.DisplayAllEmployeeInProjectById(projectId);
        }

        public void DeleteProject(int projectId)
        {
            projectDal.DeleteProject(projectId);
        }

        public void DeleteEmployeeFromProject(int projectId, int employeeId)
        {
            projectDal.DeleteEmployeeFromProject(projectId, employeeId);
        }
    }
}

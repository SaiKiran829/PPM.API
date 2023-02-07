using PPM.Model;

namespace IEntityOperation
{
    public interface IProject
    {
        void AddProject(Project project);

        void DisplayAllProjects();

        void DisplayProjectById(int projectId);

        void AddEmployeesIntoProject(int projectId, int employeeId);

        void DisplayAllEmployeeInProjectById(int projectId);

        void DeleteProject(int projectId);

        void DeleteEmployeeFromProject(int projectId, int employeeId);

    }
}


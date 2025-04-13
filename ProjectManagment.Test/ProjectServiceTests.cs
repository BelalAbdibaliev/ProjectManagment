using AutoMapper;
using Moq;
using NUnit.Framework;
using ProjectManagment.Application.Dtos;
using ProjectManagment.Application.Interfaces.Repositories;
using ProjectManagment.Application.Services;
using ProjectManagment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagment.Tests.Services
{
    [TestFixture]
    public class ProjectServiceTests
    {
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private Mock<IMapper> _mockMapper;
        private Mock<IProjectRepository> _mockProjectRepository;
        private Mock<IEmployeeRepository> _mockEmployeeRepository;
        private ProjectService _projectService;

        [SetUp]
        public void Setup()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
            _mockProjectRepository = new Mock<IProjectRepository>();
            _mockEmployeeRepository = new Mock<IEmployeeRepository>();

            _mockUnitOfWork.Setup(u => u.Projects).Returns(_mockProjectRepository.Object);
            _mockUnitOfWork.Setup(u => u.Employees).Returns(_mockEmployeeRepository.Object);

            _projectService = new ProjectService(_mockUnitOfWork.Object, _mockMapper.Object);
        }

        [Test]
        public async Task GetAllProjectsAsync_ShouldReturnMappedProjects()
        {
            int pageNumber = 1;
            int pageSize = 10;
            var projects = new List<Project>
            {
                new Project {
                    Id = 1,
                    Name = "Project 1",
                    Priority = 1,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddMonths(3),
                    ProjectLeadId = 1,
                    ClientId = 1,
                    SupplierId = 1
                },
                new Project {
                    Id = 2,
                    Name = "Project 2",
                    Priority = 2,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddMonths(6),
                    ProjectLeadId = 2,
                    ClientId = 1,
                    SupplierId = 2
                }
            }.AsQueryable();

            var projectResponses = new List<ProjectResponse>
            {
                new ProjectResponse { Id = 1, Name = "Project 1" },
                new ProjectResponse { Id = 2, Name = "Project 2" }
            }.AsQueryable();

            _mockProjectRepository.Setup(r => r.GetAll(pageNumber, pageSize))
                .ReturnsAsync(projects);

            _mockMapper.Setup(m => m.Map<ProjectResponse>(It.IsAny<Project>()))
                .Returns<Project>(p => new ProjectResponse { Id = p.Id, Name = p.Name });

            var result = await _projectService.GetAllProjectsAsync(pageNumber, pageSize);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result.First().Id, Is.EqualTo(1));
            Assert.That(result.First().Name, Is.EqualTo("Project 1"));
            _mockProjectRepository.Verify(r => r.GetAll(pageNumber, pageSize), Times.Once);
        }

        [Test]
        public async Task GetProjectByIdAsync_ShouldReturnMappedProject()
        {
            int projectId = 1;
            var project = new Project
            {
                Id = projectId,
                Name = "Test Project",
                Priority = 1,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(3),
                ProjectLeadId = 1,
                ClientId = 1,
                SupplierId = 1
            };
            var projectResponse = new ProjectResponse { Id = projectId, Name = "Test Project" };

            _mockProjectRepository.Setup(r => r.GetById(projectId))
                .ReturnsAsync(project);
            _mockMapper.Setup(m => m.Map<ProjectResponse>(project))
                .Returns(projectResponse);

            var result = await _projectService.GetProjectByIdAsync(projectId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(projectId));
            Assert.That(result.Name, Is.EqualTo("Test Project"));
            _mockProjectRepository.Verify(r => r.GetById(projectId), Times.Once);
            _mockMapper.Verify(m => m.Map<ProjectResponse>(project), Times.Once);
        }

        [Test]
        public async Task CreateProjectAsync_WithoutEmployee_ShouldAddProjectAndSave()
        {
            var createProjectDto = new CreateProjectDto
            {
                Name = "New Project",
                EmployeeId = 0,
                Priority = 1,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(3),
                ProjectLeadId = 1,
                ClientId = 1,
                SupplierId = 1
            };

            var project = new Project
            {
                Name = "New Project",
                Priority = 1,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(3),
                ProjectLeadId = 1,
                ClientId = 1,
                SupplierId = 1,
                Employees = new List<Employee>()
            };

            _mockMapper.Setup(m => m.Map<Project>(createProjectDto))
                .Returns(project);

            await _projectService.CreateProjectAsync(createProjectDto);

            _mockMapper.Verify(m => m.Map<Project>(createProjectDto), Times.Once);
            _mockProjectRepository.Verify(r => r.Add(project), Times.Once);
            _mockUnitOfWork.Verify(u => u.SaveAsync(), Times.Once);
            _mockEmployeeRepository.Verify(r => r.GetById(It.IsAny<int>()), Times.Never);
        }

        [Test]
        public async Task CreateProjectAsync_WithEmployee_ShouldAddProjectWithEmployeeAndSave()
        {
            int employeeId = 1;
            var createProjectDto = new CreateProjectDto
            {
                Name = "New Project",
                EmployeeId = employeeId,
                Priority = 1,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(3),
                ProjectLeadId = 1,
                ClientId = 1,
                SupplierId = 1
            };

            var project = new Project
            {
                Name = "New Project",
                Priority = 1,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(3),
                ProjectLeadId = 1,
                ClientId = 1,
                SupplierId = 1,
                Employees = new List<Employee>()
            };

            var employee = new Employee
            {
                Id = employeeId,
                FirstName = "User",
                LastName = "User",
                MiddleName = "User",
                Email = "user@example.com"
            };

            _mockMapper.Setup(m => m.Map<Project>(createProjectDto))
                .Returns(project);
            _mockEmployeeRepository.Setup(r => r.GetById(employeeId))
                .ReturnsAsync(employee);

            await _projectService.CreateProjectAsync(createProjectDto);

            _mockMapper.Verify(m => m.Map<Project>(createProjectDto), Times.Once);
            _mockProjectRepository.Verify(r => r.Add(project), Times.Once);
            _mockEmployeeRepository.Verify(r => r.GetById(employeeId), Times.Once);
            _mockUnitOfWork.Verify(u => u.SaveAsync(), Times.Exactly(2));
            Assert.That(project.Employees, Does.Contain(employee));
        }

        [Test]
        public async Task UpdateProjectAsync_ShouldUpdateProjectAndSave()
        {
            var updateProjectDto = new UpdateProjectDto
            {
                Id = 1,
                Name = "Updated Project",
                Priority = 2,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(4),
                ProjectLeadId = 2,
                ClientId = 2,
                SupplierId = 2
            };

            var project = new Project
            {
                Id = 1,
                Name = "Updated Project",
                Priority = 2,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(4),
                ProjectLeadId = 2,
                ClientId = 2,
                SupplierId = 2
            };

            _mockMapper.Setup(m => m.Map<Project>(updateProjectDto))
                .Returns(project);

            await _projectService.UpdateProjectAsync(updateProjectDto);

            _mockMapper.Verify(m => m.Map<Project>(updateProjectDto), Times.Once);
            _mockProjectRepository.Verify(r => r.Update(project), Times.Once);
            _mockUnitOfWork.Verify(u => u.SaveAsync(), Times.Once);
        }

        [Test]
        public async Task DeleteProjectAsync_WhenProjectExists_ShouldDeleteProjectAndSave()
        {
            int projectId = 1;
            var project = new Project
            {
                Id = projectId,
                Name = "Project",
                Priority = 1,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(3),
                ProjectLeadId = 1,
                ClientId = 1,
                SupplierId = 1
            };

            _mockProjectRepository.Setup(r => r.GetById(projectId))
                .ReturnsAsync(project);

            await _projectService.DeleteProjectAsync(projectId);

            _mockProjectRepository.Verify(r => r.GetById(projectId), Times.Once);
            _mockProjectRepository.Verify(r => r.Delete(project), Times.Once);
            _mockUnitOfWork.Verify(u => u.SaveAsync(), Times.Once);
        }

        [Test]
        public void DeleteProjectAsync_WhenProjectDoesNotExist_ShouldThrowException()
        {
            int projectId = 1;
            Project? nullProject = null;

            _mockProjectRepository.Setup(r => r.GetById(projectId))
                .ReturnsAsync(nullProject);

            var exception = Assert.ThrowsAsync<Exception>(
                async () => await _projectService.DeleteProjectAsync(projectId));

            Assert.That(exception.Message, Is.EqualTo("Project not found"));
            _mockProjectRepository.Verify(r => r.GetById(projectId), Times.Once);
            _mockProjectRepository.Verify(r => r.Delete(It.IsAny<Project>()), Times.Never);
            _mockUnitOfWork.Verify(u => u.SaveAsync(), Times.Never);
        }

        [Test]
        public async Task AddEmployee_WhenBothExist_ShouldAddEmployeeToProject()
        {
            int projectId = 1;
            int employeeId = 2;
            var project = new Project
            {
                Id = projectId,
                Name = "Test Project",
                Priority = 1,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(3),
                ProjectLeadId = 1,
                ClientId = 1,
                SupplierId = 1
            };

            var employee = new Employee
            {
                Id = employeeId,
                FirstName = "user",
                LastName = "user",
                MiddleName = "user",
                Email = "user@example.com"
            };

            _mockProjectRepository.Setup(r => r.GetById(projectId))
                .ReturnsAsync(project);
            _mockEmployeeRepository.Setup(r => r.GetById(employeeId))
                .ReturnsAsync(employee);

            await _projectService.AddEmployee(projectId, employeeId);

            _mockProjectRepository.Verify(r => r.GetById(projectId), Times.Once);
            _mockEmployeeRepository.Verify(r => r.GetById(employeeId), Times.Once);
            _mockProjectRepository.Verify(r => r.AddEmployee(project, employee), Times.Once);
            _mockUnitOfWork.Verify(u => u.SaveAsync(), Times.Once);
        }

        [Test]
        public void AddEmployee_WhenEmployeeDoesNotExist_ShouldThrowException()
        {
            int projectId = 1;
            int employeeId = 2;
            Employee? nullEmployee = null;

            _mockEmployeeRepository.Setup(r => r.GetById(employeeId))
                .ReturnsAsync(nullEmployee);

            var exception = Assert.ThrowsAsync<NullReferenceException>(
                async () => await _projectService.AddEmployee(projectId, employeeId));

            Assert.That(exception.Message, Is.EqualTo("Employee not found"));
            _mockProjectRepository.Verify(r => r.AddEmployee(It.IsAny<Project>(), It.IsAny<Employee>()), Times.Never);
            _mockUnitOfWork.Verify(u => u.SaveAsync(), Times.Never);
        }

        [Test]
        public void AddEmployee_WhenProjectDoesNotExist_ShouldThrowException()
        {
            int projectId = 1;
            int employeeId = 2;
            Project? nullProject = null;
            var employee = new Employee
            {
                Id = employeeId,
                FirstName = "user",
                LastName = "user",
                MiddleName = "user",
                Email = "user@example.com"
            };

            _mockEmployeeRepository.Setup(r => r.GetById(employeeId))
                .ReturnsAsync(employee);
            _mockProjectRepository.Setup(r => r.GetById(projectId))
                .ReturnsAsync(nullProject);

            var exception = Assert.ThrowsAsync<NullReferenceException>(
                async () => await _projectService.AddEmployee(projectId, employeeId));

            Assert.That(exception.Message, Is.EqualTo("Project not found"));
            _mockProjectRepository.Verify(r => r.AddEmployee(It.IsAny<Project>(), It.IsAny<Employee>()), Times.Never);
            _mockUnitOfWork.Verify(u => u.SaveAsync(), Times.Never);
        }

        [Test]
        public async Task RemoveEmployee_WhenBothExist_ShouldRemoveEmployeeFromProject()
        {
            int projectId = 1;
            int employeeId = 2;
            var project = new Project
            {
                Id = projectId,
                Name = "Test Project",
                Priority = 1,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(3),
                ProjectLeadId = 1,
                ClientId = 1,
                SupplierId = 1
            };

            var employee = new Employee
            {
                Id = employeeId,
                FirstName = "user",
                LastName = "user",
                MiddleName = "user",
                Email = "user@example.com"
            };

            _mockProjectRepository.Setup(r => r.GetEmployeesByProject(projectId))
                .ReturnsAsync(project);
            _mockEmployeeRepository.Setup(r => r.GetById(employeeId))
                .ReturnsAsync(employee);

            await _projectService.RemoveEmployee(projectId, employeeId);

            _mockProjectRepository.Verify(r => r.GetEmployeesByProject(projectId), Times.Once);
            _mockEmployeeRepository.Verify(r => r.GetById(employeeId), Times.Once);
            _mockProjectRepository.Verify(r => r.RemoveEmployee(project, employee), Times.Once);
            _mockUnitOfWork.Verify(u => u.SaveAsync(), Times.Once);
        }

        [Test]
        public void RemoveEmployee_WhenEmployeeDoesNotExist_ShouldThrowException()
        {
            int projectId = 1;
            int employeeId = 2;
            Employee? nullEmployee = null;

            _mockEmployeeRepository.Setup(r => r.GetById(employeeId))
                .ReturnsAsync(nullEmployee);

            var exception = Assert.ThrowsAsync<NullReferenceException>(
                async () => await _projectService.RemoveEmployee(projectId, employeeId));

            Assert.That(exception.Message, Is.EqualTo("Employee not found"));
            _mockProjectRepository.Verify(r => r.RemoveEmployee(It.IsAny<Project>(), It.IsAny<Employee>()), Times.Never);
            _mockUnitOfWork.Verify(u => u.SaveAsync(), Times.Never);
        }

        [Test]
        public void RemoveEmployee_WhenProjectDoesNotExist_ShouldThrowException()
        {
            int projectId = 1;
            int employeeId = 2;
            Project? nullProject = null;
            var employee = new Employee
            {
                Id = employeeId,
                FirstName = "user",
                LastName = "user",
                MiddleName = "user",
                Email = "user@example.com"
            };

            _mockEmployeeRepository.Setup(r => r.GetById(employeeId))
                .ReturnsAsync(employee);
            _mockProjectRepository.Setup(r => r.GetEmployeesByProject(projectId))
                .ReturnsAsync(nullProject);

            var exception = Assert.ThrowsAsync<NullReferenceException>(
                async () => await _projectService.RemoveEmployee(projectId, employeeId));

            Assert.That(exception.Message, Is.EqualTo("Project not found"));
            _mockProjectRepository.Verify(r => r.RemoveEmployee(It.IsAny<Project>(), It.IsAny<Employee>()), Times.Never);
            _mockUnitOfWork.Verify(u => u.SaveAsync(), Times.Never);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkOrder.Core.Entities;
using WorkOrder.Core.Repositories;

namespace WorkOrder.Business.Managers
{
    public class AdminManager
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<Department> _departmentRepository;
        private readonly IGenericRepository<WorkPlace> _workPlaceRepository;
        private readonly IGenericRepository<WorkPLaceType> _workPlaceTypeRepository;
        private readonly IGenericRepository<CaseType> _caseTypeTypeRepository;
        private readonly IGenericRepository<Notification> _notificationRepository;
        public AdminManager(IGenericRepository<User> userRepository, IGenericRepository<Department> departmentRepository, IGenericRepository<WorkPlace> workPlaceRepository, IGenericRepository<WorkPLaceType> workPlaceTypeRepository, IGenericRepository<CaseType> caseTypeTypeRepository, IGenericRepository<Notification> notificationRepository)
        {
            _userRepository = userRepository;
            _departmentRepository = departmentRepository;
            _workPlaceRepository = workPlaceRepository;
            _workPlaceTypeRepository = workPlaceTypeRepository;
            _caseTypeTypeRepository = caseTypeTypeRepository;
            _notificationRepository = notificationRepository;
        }

        public List<User> AllUser()
        {
            return _userRepository.GetAll().Where(x => x.UserRole == UserRole.User).ToList();
        }

        public async Task<User> AddNewUser(User user)
        {
            var res = await _userRepository.SingleOrDefaultAsync(x => x.UserName == user.UserName);
            if (res != null)
            {
                return null;
            }
            else
            {
                var createdUser = new User
                {
                    UserName = user.UserName,
                    LastName = user.LastName,
                    Name = user.Name,
                    Email = user.Email,
                    Password = user.Password,
                    PhoneNumber = user.PhoneNumber,
                    CreateDate = DateTime.Now,
                    Department = user.Department,
                    UserRole = UserRole.User,
                };
                await _userRepository.AddAsync(createdUser);
                return createdUser;
            }
        }

        public async Task<User> EditUser(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return user;
        }

        public void UpdateUser(User user)
        {
            user.UserRole = UserRole.User;
            _userRepository.Update(user);
        }
        public void DeleteUser(User user)
        {
            _userRepository.Delete(user);
        }
        public List<Department> AllDepartments()
        {
            var departments = _departmentRepository.GetAll().ToList();
            return departments;
        }
        public async Task<Department> AddNewDepartment(Department department)
        {
            var res = await _departmentRepository.SingleOrDefaultAsync(x => x.Name == department.Name);
            if (res == null)
            {
                await _departmentRepository.AddAsync(department);
                return department;
            }
            else
            {
                return null;
            }
        }

        public async Task<Department> EditDepartment(int id)
        {
            var department = await _departmentRepository.GetByIdAsync(id);
            return department;
        }

        public void UpdateDepartment(Department department)
        {
            _departmentRepository.Update(department);
        }

        public void DeleteDepartment(Department department)
        {
            _departmentRepository.Delete(department);
        }

        public List<WorkPlace> AllWorkPlace()
        {
            var workPlaces = _workPlaceRepository.GetAll().ToList();
            return workPlaces;
        }

        public async Task<WorkPlace> NewWorkPlace(WorkPlace workPlace)
        {
            var res = await _workPlaceRepository.SingleOrDefaultAsync(x => x.Name == workPlace.Name);
            if (res == null)
            {
                await _workPlaceRepository.AddAsync(workPlace);
                return workPlace;
            }
            else
            {
                return null;
            }
        }

        public async Task<WorkPlace> EditWorkPlace(int id)
        {
            var department = await _workPlaceRepository.GetByIdAsync(id);
            return department;
        }

        public void UpdateWorkPlace(WorkPlace workPlace)
        {
            _workPlaceRepository.Update(workPlace);
        }

        public void DeleteWorkPlace(WorkPlace workPlace)
        {
            _workPlaceRepository.Delete(workPlace);
        }

        public List<WorkPLaceType> AllWorkPlaceType()
        {
            var workPlaceTypes = _workPlaceTypeRepository.GetAll().ToList();
            return workPlaceTypes;
        }

        public async Task<WorkPLaceType> NewWorkPlaceType(WorkPLaceType workPLaceType)
        {
            var res = await _workPlaceTypeRepository.SingleOrDefaultAsync(x => x.Name == workPLaceType.Name);
            if (res == null)
            {
                await _workPlaceTypeRepository.AddAsync(workPLaceType);
                return workPLaceType;
            }
            else
            {
                return null;
            }
        }

        public async Task<WorkPLaceType> EditWorkPlaceType(int id)
        {
            var workPlaceType = await _workPlaceTypeRepository.GetByIdAsync(id);
            return workPlaceType;
        }

        public void UpdateWorkPlaceType(WorkPLaceType workPLaceType)
        {
            _workPlaceTypeRepository.Update(workPLaceType);
        }

        public void DeleteWorkPlaceType(WorkPLaceType workPLaceType)
        {
            _workPlaceTypeRepository.Delete(workPLaceType);
        }

        public List<CaseType> AllCaseType()
        {
            var caseTypes = _caseTypeTypeRepository.GetAll().ToList();
            return caseTypes;
        }

        public async Task<CaseType> NewCase(CaseType caseType)
        {
            var res = await _caseTypeTypeRepository.SingleOrDefaultAsync(x => x.Name == caseType.Name);
            if (res == null)
            {
                await _caseTypeTypeRepository.AddAsync(caseType);
                return caseType;
            }
            else
            {
                return null;
            }
        }


        public async Task<CaseType> EditCaseType(int id)
        {
            var caseType = await _caseTypeTypeRepository.GetByIdAsync(id);
            return caseType;
        }

        public void UpdateCaseType(CaseType caseType)
        {
            _caseTypeTypeRepository.Update(caseType);
        }

        public void DeleteCase(CaseType caseType)
        {
            _caseTypeTypeRepository.Delete(caseType);
        }

        public List<Notification> AllNotification()
        {
            var res =  _notificationRepository.GetAll().ToList();

            return res;
        }
        public async Task<Notification> NewNotification(Notification notification)
        {
            await _notificationRepository.AddAsync(notification);
            return notification;
        }
        public async Task<Notification> EditNotification(int id)
        {
            var notification = await _notificationRepository.GetByIdAsync(id);
            return notification;
        }

        public void UpdateNotification(Notification notification)
        {
            _notificationRepository.Update(notification);
        }
        public void DeleteNotification(Notification notification)
        {
            _notificationRepository.Delete(notification);
        }

        public async Task<string> AdminFullName(string userName)
        {
            var user = await _userRepository.SingleOrDefaultAsync(x => x.UserName == userName);

            return $"{user.Name} {user.LastName}";
        }
    }
}
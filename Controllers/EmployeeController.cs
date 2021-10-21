using matxicorp.Data.Contracts;
using matxicorp.Data.Entities;
using matxicorp.Helpers;
using matxicorp.Models;
using matxicorp.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace matxicorp.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public EmployeeController(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        // GET: Employees        
        public IActionResult Index()
        {
            var viewModel = new EmployeeListViewModel();
            var employees = _repositoryWrapper.EmployeeRepository.FindAll().ToList();
            var employeeInfoList = new List<BasicEmployeeInfo>();
            foreach (var em in employees)
            {
                var employee = AutoMapperHelper.Instance.Map<Employee, BasicEmployeeInfo>(em);
                if (!string.IsNullOrEmpty(em.ParentCode))
                {
                    employee.DirectManagerName = employees.Where(x => x.EmployeeCode == em.ParentCode)
                        .Select(x => x.LastName + ' ' + x.FirstName)
                        .FirstOrDefault();
                }
                if (!string.IsNullOrEmpty(em.LeaderCode))
                {
                    employee.TeamLeaderName = employees.Where(x => x.EmployeeCode == em.LeaderCode)
                        .Select(x => x.LastName + ' ' + x.FirstName)
                        .FirstOrDefault();
                }
                employee.LevelName = EnumHelper.GetEnumDescription((Levels)em.Level);
                employeeInfoList.Add(employee);
            }
            viewModel.Employees = employeeInfoList;
            return View(viewModel);
        }

        // GET: Employee/Detail/5
        public IActionResult Detail(int id)
        {
            var employee = _repositoryWrapper.EmployeeRepository.FindByCondition(x => x.Id == id).FirstOrDefault();

            if (employee == null)
            {
                return NotFound();
            }

            var viewModel = AutoMapperHelper.Instance.Map<Employee, EmployeeDetailViewModel>(employee);
            viewModel.LevelName = EnumHelper.GetEnumDescription((Levels)employee.Level);

            var teamLeaderInfo = _repositoryWrapper.EmployeeRepository.FindByCondition(x => x.EmployeeCode == employee.LeaderCode)
                .Select(x => new BasicEmployeeInfo
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    EmployeeCode = x.EmployeeCode
                })
                .FirstOrDefault();

            if (teamLeaderInfo != null)
            {
                viewModel.TeamLeaderName = $"{teamLeaderInfo.LastName} {teamLeaderInfo.FirstName}";
                viewModel.TeamMembers = _repositoryWrapper.EmployeeRepository.FindByCondition(x => x.LeaderCode == teamLeaderInfo.EmployeeCode && x.EmployeeCode != employee.EmployeeCode)
                .Select(x => x.LastName + ' ' + x.FirstName)
                .ToList();
            }
            else if (employee.Level == (int)Levels.TeamLeader)
            {
                viewModel.TeamMembers = _repositoryWrapper.EmployeeRepository.FindByCondition(x => x.LeaderCode == employee.EmployeeCode)
                .Select(x => x.LastName + ' ' + x.FirstName)
                .ToList();
            }

            viewModel.DirectManagerName = _repositoryWrapper.EmployeeRepository.FindByCondition(x => x.EmployeeCode == employee.ParentCode)
                .Select(x => x.LastName + ' ' + x.FirstName)
                .FirstOrDefault();

            return View(viewModel);
        }

        // GET: Employee/Create
        public IActionResult Create()
        {
            var teamLeaders = _repositoryWrapper.EmployeeRepository
                .FindByCondition(x => x.Level == (int)Levels.TeamLeader)
                .Select(x => new BasicEmployeeInfo { EmployeeCode = x.EmployeeCode, FirstName = x.FirstName, LastName = x.LastName, Level = x.Level })
                .ToList();
            var directManagers = _repositoryWrapper.EmployeeRepository
                .FindByCondition(x => x.Level == (int)Levels.DirectManager).ToList()
                .Select(x => new BasicEmployeeInfo { EmployeeCode = x.EmployeeCode, FirstName = x.FirstName, LastName = x.LastName, Level = x.Level })
                .ToList();

            List<SelectListItem> levels = GetLevels();
            ViewData["LevelCode"] = new SelectList(levels, "Value", "Text");

            var viewModel = new AddEmployeeViewModel
            {
                JsonTeamLeaders = JsonConvert.SerializeObject(teamLeaders),
                JsonDirectManagers = JsonConvert.SerializeObject(directManagers)
            };

            return View(viewModel);
        }

        // POST: Employee/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("FirstName,LastName,Mobile,Email,ParentCode,LeaderCode,Level,EmployeeCode")] AddEmployeeViewModel employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var levels = GetLevels();
                    ViewData["LevelCode"] = new SelectList(levels, "Value", "Text");

                    if (EmployeeCodeExists(employee.EmployeeCode))
                    {
                        ModelState.AddModelError(string.Empty, "This Employee already existed");
                        return View(employee);
                    }

                    var employeeEntity = AutoMapperHelper.Instance.Map<AddEmployeeViewModel, Employee>(employee);
                    employeeEntity.EntryDateTime = DateTime.Now;

                    if (employee.Level == (int)Levels.Staff)
                    {
                        var directManager = _repositoryWrapper.EmployeeRepository.FindByCondition(x => x.EmployeeCode == employee.ParentCode).FirstOrDefault();
                        if (directManager != null)
                        {
                            var teamLead = _repositoryWrapper.EmployeeRepository.FindByCondition(x => x.EmployeeCode == directManager.LeaderCode).FirstOrDefault();
                            employeeEntity.LeaderCode = teamLead.EmployeeCode;
                        }

                    }

                    _repositoryWrapper.EmployeeRepository.Add(employeeEntity);
                    _repositoryWrapper.Save();

                    return RedirectToAction(nameof(Index), nameof(Employee));
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Create new employee failed");
            }

            return View(employee);
        }

        // GET: Employee/Edit/5
        public IActionResult Edit(int id)
        {
            var employee = _repositoryWrapper.EmployeeRepository.FindByCondition(x => x.Id == id).FirstOrDefault();
            if (employee == null)
            {
                return NotFound();
            }

            var teamLeaders = _repositoryWrapper.EmployeeRepository
                .FindByCondition(x => x.Level == (int)Levels.TeamLeader)
                .Select(x => new BasicEmployeeInfo { EmployeeCode = x.EmployeeCode, FirstName = x.FirstName, LastName = x.LastName })
                .ToList();

            var directManagers = _repositoryWrapper.EmployeeRepository
                .FindByCondition(x => x.Level == (int)Levels.DirectManager).ToList()
                .Select(x => new BasicEmployeeInfo { EmployeeCode = x.EmployeeCode, FirstName = x.FirstName, LastName = x.LastName })
                .ToList();

            var levels = GetLevels();
            ViewData["LevelCode"] = new SelectList(levels, "Value", "Text");

            var viewModel = AutoMapperHelper.Instance.Map<Employee, EditEmployeeViewModel>(employee);
            viewModel.JsonDirectManagers = JsonConvert.SerializeObject(directManagers);
            viewModel.JsonTeamLeaders = JsonConvert.SerializeObject(teamLeaders);

            viewModel.LeaderName = teamLeaders.Where(x => x.EmployeeCode == employee.LeaderCode)
                .Select(x => x.LastName + ' ' + x.FirstName)
                .FirstOrDefault();

            viewModel.ParentName = directManagers.Where(x => x.EmployeeCode == employee.ParentCode)
                .Select(x => x.LastName + ' ' + x.FirstName)
                .FirstOrDefault();

            return View(viewModel);
        }

        // POST: Employee/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("FirstName,LastName,Mobile,Email,ParentCode,LeaderCode,Level,EmployeeCode,ID")] EditEmployeeViewModel employee)
        {
            if (id != employee.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var levels = GetLevels();
                ViewData["LevelCode"] = new SelectList(levels, "Value", "Text");
                try
                {
                    var employeeExisting = _repositoryWrapper.EmployeeRepository.FindByCondition(x => x.Id == employee.ID).FirstOrDefault();
                    employeeExisting.FirstName = employee.FirstName;
                    employeeExisting.LastName = employee.LastName;
                    employeeExisting.Mobile = employee.Mobile;
                    employeeExisting.Email = employee.Email;
                    employeeExisting.LeaderCode = employee.LeaderCode;
                    employeeExisting.Level = employee.Level;

                    _repositoryWrapper.EmployeeRepository.Update(employeeExisting);
                    _repositoryWrapper.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeIdExists(employee.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index), nameof(Employee));
            }

            return View(employee);
        }

        // GET: Employee/Delete/5
        public IActionResult Delete(int id)
        {
            var employee = _repositoryWrapper.EmployeeRepository.FindByCondition(m => m.Id == id).FirstOrDefault();

            if (employee == null)
            {
                return NotFound();
            }

            var viewModel = AutoMapperHelper.Instance.Map<Employee, DeleteEmployeeViewModel>(employee);

            viewModel.LevelName = EnumHelper.GetEnumDescription((Levels)employee.Level);
            viewModel.TeamLeaderName = _repositoryWrapper.EmployeeRepository.FindByCondition(x => x.EmployeeCode == employee.LeaderCode)
                .Select(x => x.LastName + ' ' + x.FirstName)
                .FirstOrDefault();
            viewModel.DirectManagerName = _repositoryWrapper.EmployeeRepository.FindByCondition(x => x.EmployeeCode == employee.ParentCode)
                .Select(x => x.LastName + ' ' + x.FirstName)
                .FirstOrDefault();

            return View(viewModel);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var employee = _repositoryWrapper.EmployeeRepository.FindByCondition(x => x.Id == id).FirstOrDefault();

            _repositoryWrapper.EmployeeRepository.Delete(employee);
            _repositoryWrapper.Save();

            return RedirectToAction(nameof(Index));
        }

        private static List<SelectListItem> GetLevels()
        {
            var levels = new List<SelectListItem>();
            foreach (var value in Enum.GetValues(typeof(Levels)))
            {
                levels.Add(new SelectListItem
                {
                    Text = EnumHelper.GetEnumDescription((Levels)value),
                    Value = ((int)value).ToString()
                });
            }

            return levels;
        }

        private bool EmployeeIdExists(int id)
        {
            return _repositoryWrapper.EmployeeRepository.FindAll().Any(e => e.Id == id);
        }

        private bool EmployeeCodeExists(string code)
        {
            return _repositoryWrapper.EmployeeRepository.FindAll().Any(e => e.EmployeeCode == code);
        }
    }
}

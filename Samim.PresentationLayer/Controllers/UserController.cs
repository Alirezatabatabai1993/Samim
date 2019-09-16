using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Samim.DataLayer.AppUser;
using Samim.DataLayer.Context;
using Samim.DataLayer.Helpers;
using Samim.DataLayer.UnitOfWork.UserRepo;
using Samim.ViewModel;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace Samim.PresentationLayer.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public UserController(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }
        public IActionResult Index(string orderBy = "", string orderType = "", int pageNumber = 1)
        {
            //Todo
            int rowCounter = 0;
            var pageSize = _configuration.GetValue<int>("PageSize");
            int skip = (pageSize * pageNumber) - pageSize;
            List<VMUserIndex> users = _userRepository.GetAllApplicationUsers().Select(x => new VMUserIndex(x, rowCounter += 1)).ToList();
            var usersForPagination = users.Skip(skip).Take(pageSize).ToList();
            var vmUser = new VMUser(
                                    usersForPagination,
                                    users.Count() % pageSize == 0 ? users.Count() / pageSize : users.Count() / pageSize + 1,
                                    pageNumber);
            if (!string.IsNullOrEmpty(orderBy))
            {
                switch (orderBy)
                {
                    case "RowNumber":
                        usersForPagination = orderType == "DESC" ? usersForPagination.OrderByDescending(x => x.RowNumber).ToList() : usersForPagination.OrderBy(x => x.RowNumber).ToList();
                        break;
                    case "FirstName":
                        usersForPagination = orderType == "DESC" ? usersForPagination.OrderByDescending(x => x.FirstName).ToList() : usersForPagination.OrderBy(x => x.FirstName).ToList();
                        break;
                    case "LastName":
                        usersForPagination = orderType == "DESC" ? usersForPagination.OrderByDescending(x => x.LastName).ToList() : usersForPagination.OrderBy(x => x.LastName).ToList();
                        break;
                    case "UserName":
                        usersForPagination = orderType == "DESC" ? usersForPagination.OrderByDescending(x => x.UserName).ToList() : usersForPagination.OrderBy(x => x.UserName).ToList();
                        break;
                    default:
                        break;
                }
                return Json(new { rows = usersForPagination });
            }
            return View(vmUser);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return PartialView("_CreateAndEdit", new VMUserCreateAndEdit());
        }

        [HttpPost]
        public IActionResult Create(VMUserCreateAndEdit vMUserCreate)
        {
            if (string.IsNullOrEmpty(vMUserCreate.Password))
                ModelState.AddModelError("Password", "The password field is reuired.");
            if (ModelState.IsValid)
            {
                _userRepository.AddApplicationUser(vMUserCreate);
                return Json(new { success = true });
            }
            return Json(new { success = false, errors = ModelState.PopulateErrors() });
        }

        [HttpGet]
        public IActionResult EditPassword(string id)
        {
            var applicationUser = _userRepository.GetApplicationUserById(id);
            return PartialView("_EditPassword", new VMUserEditPassword(applicationUser));
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var applicationUser = _userRepository.GetApplicationUserById(id);
            var applicationUserToVMUserCreateAndEdit = new VMUserCreateAndEdit(applicationUser);
            return PartialView("_CreateAndEdit", applicationUserToVMUserCreateAndEdit);
        }

        [HttpPost]
        public IActionResult Edit(VMUserCreateAndEdit vMUserEdit)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(vMUserEdit.Password))
                {
                    _userRepository.ResetApplicationUserPassword(vMUserEdit.Id, vMUserEdit.Password);
                }
                _userRepository.EditApplicationUser(vMUserEdit);
                return Json(new { success = true });
            }

            return Json(new { success = false, errors = ModelState.PopulateErrors() });
        }

        [HttpPost]
        public IActionResult Delete(string id)
        {
            _userRepository.DeleteApplicationUser(id);
            return Json(new { success = true });
        }
    }
}
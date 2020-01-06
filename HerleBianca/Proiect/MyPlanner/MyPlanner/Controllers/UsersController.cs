using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyPlanner.Data;
using MyPlanner.Models;
using MyPlanner.Repository;

namespace MyPlanner.Controllers
{
    public class UsersController : Controller
    {
        private readonly MyPlannerContext _context;
        private IUserRepository _repository;
        public bool use_test_repository = false;
        public static User logged_user;
        public UsersController(MyPlannerContext? context, IUserRepository repository=null)
        {
            _context = context;
            _repository = repository;
        }
        /*public UsersController(IUserRepository repository)
        {
            _repository = repository;
        }*/

        // GET: Users
        public async Task<IActionResult>  Index()
        {
            return View( await  _context.User.ToListAsync());
        }

        public IActionResult Index_test()
        {
            return View("Index", _repository.GetAllItems());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                id = logged_user.id;
            }

            var user = await _context.User.FirstOrDefaultAsync(m => m.id == id);

            var myTasks = from m in _context.MyTask select m;
            var myTasks2 = from m in _context.MyTask select m;
            myTasks = myTasks.Where(s => s.Asignee.Contains(user.name));
            myTasks2 = myTasks.Where(s => s.Owner.Contains(user.name));
            user.MyTasks_asigned = await myTasks.ToListAsync();
            user.MyTasks_owner = await myTasks2.ToListAsync();

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,name,username,encrypted_password")] User user)
        {
            if (ModelState.IsValid)
            {
                user.id = Guid.NewGuid();
                user.encrypted_password = SecurePasswordHasherHelper.Hash(user.encrypted_password);
                if (!use_test_repository)
                {
                    _context.Add(user);
                    await _context.SaveChangesAsync();
                }
                _repository.AddItem(user);
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
          
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            if (logged_user.id != user.id)
            {
                ViewBag.Message = string.Format("You are not allowed to edit this user's information");
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("id,name,username,encrypted_password,age,other_ocupation")] User user)
        {
            if (id != user.id)
            {
                return NotFound();
            }
            if (logged_user.id == user.id)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        var updated = _context.User.Find(user.id);
                        updated.name = user.name;
                        updated.username = user.username;
                        updated.age = user.age;
                        updated.other_ocupation = user.other_ocupation;
                        _context.Update(updated);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!UserExists(user.id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await _context.User.FirstOrDefaultAsync(m => m.id == id);
            
                if (user == null)
            {
                return NotFound();
            }
            if (logged_user.id != user.id)
            {
                ViewBag.Message = string.Format("You are not allowed to delete this user");
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var user = await _context.User.FindAsync(id);
            if (logged_user.id == user.id)
            {
                _context.User.Remove(user);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(Guid id)
        {
            return _context.User.Any(e => e.id == id);
        }
        //GET: Login
        public ActionResult Login()
        {
            return View();
        }

        //POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public /*async Task<IActionResult>*/ IActionResult Login(User objUser)
        {
            User user=null;
            if (ModelState.IsValid)
            {
                
                // var obj = _context.Where(a => a.UserName.Equals(objUser.UserName) && a.Password.Equals(objUser.Password)).FirstOrDefault();
                if (use_test_repository)
                {
                    foreach(User item in _repository.GetAllItems())
                    {
                        if (item.username==objUser.username)
                        {
                            user = item;
                        }
                    }
                }
                    
                else
                     user = /*await*/ _context.User.FirstOrDefault/*Async*/(m => m.username == objUser.username);
                if (user != null)
                {
                    if (SecurePasswordHasherHelper.Verify(objUser.encrypted_password, user.encrypted_password))
                    {
                        logged_user = user;                       
                        return RedirectToAction("Dashboard", logged_user);// new User(user.name,user.username,user.encrypted_password));
                    }
                        
                }
                
            }
            return View("Login",objUser);
        }
        //GET : Dashboard
        public async Task<IActionResult> Dashboard(string name)
        {
            if (UsersController.logged_user == null)
            {
                return RedirectToAction("Privacy", "Home"); //Privacy is used as default empty page
            }

            // Use LINQ to get list of genres.
            IQueryable<string> asigneeQuery = from m in _context.MyTask
                                              orderby m.Asignee
                                              select m.Asignee;

            var myTasks = from m in _context.MyTask
                          select m;

            if (string.IsNullOrEmpty(name))
            {
                name = logged_user.name;
            }

            myTasks = myTasks.Where(x => x.Asignee == name);

            var myTaskAsigneeVM = new MyTaskAsigneeViewModel
            {
                Asignees = new SelectList(await asigneeQuery.Distinct().ToListAsync()),
                MyTasks = await myTasks.ToListAsync()
            };

            return View(myTaskAsigneeVM);
        }
    }
}

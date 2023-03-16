using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TimeKeepingApp.Data;
using TimeKeepingApp.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace TimeKeepingApp.Controllers
{
    [Authorize]
    public class ShiftsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ShiftsController(ApplicationDbContext context,
            UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        //public async Task<IActionResult> FilterIndex(string actFilter, string descFilter, string fromDate, string toDate, string pageNumber)
        //{
        //    // Get user claim
        //    var claim = User.FindFirst(ClaimTypes.NameIdentifier);
        //    string userID = claim.Value;
        //    var user = await _context.Users.Where(u => u.Id == userID).FirstOrDefaultAsync();

        //    // Parse date strings into date objects
        //    DateTime tDate = DateTime.Parse(toDate).AddDays(1);

        //    DateTime fDate = DateTime.Parse(fromDate);

        //    IQueryable<Shift> ShiftIQ = from t in _context.Shift.Where(
        //        t => t.EmployeeID == user.Id
        //        && t.ShiftStart >= fDate
        //        && t.ShiftStart <= tDate).OrderByDescending(d => d.ShiftStart)
        //                                            select t;

        //    List<Employee> employeeList = ShiftIQ.AsNoTracking().ToList()
        //        .GroupBy(p => p.EmployeeID)
        //        .Select(g => g.First())
        //        .Select(x => new Employee { EmployeeID = x.EmployeeID })
        //        .ToList();

        //    //if (actFilter != "all")
        //    //{
        //    //    transactionIQ = transactionIQ.Where(t => t.actID == actFilter);

        //    //}
        //    //if (!string.IsNullOrEmpty(descFilter))
        //    //{
        //    //    transactionIQ = transactionIQ.Where(t => t.description.Contains(descFilter));
        //    //}

        //    List<Shift> sList = await ShiftIQ.AsNoTracking().ToListAsync();

        //    tDate = DateTime.Parse(toDate);

        //    ShiftIndexViewModel vmod = new ShiftIndexViewModel(
        //        employees: employeeList,
        //        shifts: sList,
        //        start: fDate,
        //        end: tDate
        //        //desc: descFilter,
        //        //page: int.Parse(pageNumber),
        //        //acct: actFilter
        //        );


        //    return View("Index", vmod);//, vmod);
        //}

        // GET: Shifts
        public async Task<IActionResult> Index()
        {
            //return View(await _context.Shift.ToListAsync());
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);
            string userID = claim.Value;
            var user = await _context.Users.Where(u => u.Id == userID).FirstOrDefaultAsync();

            // Parse date strings into date objects
            //DateTime tDate = DateTime.Parse(toDate).AddDays(1);

            //DateTime fDate = DateTime.Parse(fromDate);

            IQueryable<Shift> ShiftIQ = from t in _context.Shift.Where(
                t => t.EmployeeID == user.Id).OrderByDescending(t => t.ShiftStart)
                                        select t;

            List<Shift> sList = await ShiftIQ.AsNoTracking().ToListAsync();

            //List<Employee> employeeList = ShiftIQ.AsNoTracking().ToList()
            //    .GroupBy(p => p.EmployeeID)
            //    .Select(g => g.First())
            //    .Select(x => new Employee { EmployeeID = x.EmployeeID })
            //    .ToList();

            //if (actFilter != "all")
            //{
            //    transactionIQ = transactionIQ.Where(t => t.actID == actFilter);

            //}
            //if (!string.IsNullOrEmpty(descFilter))
            //{
            //    transactionIQ = transactionIQ.Where(t => t.description.Contains(descFilter));
            //}


            //tDate = DateTime.Parse(toDate);

            ShiftIndexViewModel vmod = new ShiftIndexViewModel(
                //employees: employeeList,
                shifts: sList
                //start: fDate,
                //end: tDate
                //desc: descFilter,
                //page: int.Parse(pageNumber),
                //acct: actFilter
                );


            return View("Index", sList);//, vmod);

        }

        public async Task<IActionResult> ManagerShifts()
        {
            //return View(await _context.Shift.ToListAsync());
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);
            string userID = claim.Value;
            var user = await _context.Users.Where(u => u.Id == userID).FirstOrDefaultAsync();

            // Parse date strings into date objects
            //DateTime tDate = DateTime.Parse(toDate).AddDays(1);

            //DateTime fDate = DateTime.Parse(fromDate);

            IQueryable<Shift> ShiftIQ = from t in _context.Shift.OrderByDescending(p => p.ShiftStart)
                                        select t;

            List<Shift> sList = await ShiftIQ.AsNoTracking().ToListAsync();

            IQueryable<Employee> EmployeeIQ = from e in _context.Employee select e;

            List<Employee> employeeList = new List<Employee>();
            foreach (var f in sList)
            {
                 employeeList.Add(EmployeeIQ.Where(p => p.EmployeeID == f.EmployeeID).FirstOrDefault());

            }
            List<string> nameList = new List<string>(); 
            List<string> lastNameList = new List<string>();
            foreach (var l in employeeList)
            {
                nameList.Add(l.EmployeeName);
                lastNameList.Add(l.EmployeeLastName);
            }

            var nameTup = new Tuple<List<string>, List<string>>(nameList, lastNameList);
            var bigTup = new Tuple<List<Shift>, Tuple<List<string>, List<string>>>(sList, nameTup);

            //foreach (var f in sList)
            //{
            //    )
            //}

            ShiftManagerShiftsViewModel vmod = new ShiftManagerShiftsViewModel(
                employees: employeeList,
                shifts: sList,
                firstName: nameList,
                lastName: lastNameList
                //start: fDate,
                //end: tDate
                //desc: descFilter,
                //page: int.Parse(pageNumber),
                //acct: actFilter
                );


            return View(vmod);//, vmod);

        }

        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Shift.ToListAsync());
        //}

        // GET: Shifts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shift = await _context.Shift
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shift == null)
            {
                return NotFound();
            }

            return View(shift);
        }

        // GET: Shifts/Create
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult ClockIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ClockIn(String location)
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);
            string userID = claim.Value;
            var user = await _userManager.GetUserAsync(User);//await _context.Users.Where(u => u.Id == userID).FirstOrDefaultAsync();



            Employee emp = _context.Employee.Where(u => u.EmployeeID == user.Id).FirstOrDefault();

            Shift ongoing = await _context.Shift.Where(u => u.EmployeeID == userID
            && u.Status == ShiftStatus.Ongoing)
                .FirstOrDefaultAsync();

            if (ongoing != null)
            {
                ModelState.AddModelError("", "You are already clocked in");
            }

            Shift s = new Shift();

            //s.Id = emp.Id;
            s.EmployeeID = user.Id;
            s.ShiftStart = DateTime.Now;
            s.ShiftEnd = null;
            s.Location = location;
            s.Status = ShiftStatus.Ongoing;

            

            if (ModelState.IsValid)
            {
                _context.Add(s);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(s);
        }

        public IActionResult ClockOut()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ClockOut(string location)
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);
            string userID = claim.Value;
            var user = await _context.Users.Where(u => u.Id == userID).FirstOrDefaultAsync();
            Shift s = await _context.Shift.Where(u => u.EmployeeID == userID
            && u.Status == ShiftStatus.Ongoing)
                .FirstOrDefaultAsync();


            if (s != null)
            {
                //s.EmployeeID = user.Id;
                //s.ShiftStart = DateTime.Now;
                s.ShiftEnd = DateTime.Now;
                //s.Location = location;
                s.Status = ShiftStatus.Pending;
            }
            else
            {
                ModelState.AddModelError("", "You are not currently clocked in");
            }

            if (ModelState.IsValid)
            {
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // POST: Shifts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,EmployeeID,ShiftStart,ShiftEnd,Location")] Shift shift)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(shift);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(shift);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DateTime shiftStart, DateTime shiftEnd, string location)
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);
            string userID = claim.Value;
            var user = await _context.Users.Where(u => u.Id == userID).FirstOrDefaultAsync();
            Shift s = new Shift();

            Employee emp = _context.Employee.Where(u => u.EmployeeID == user.Id).FirstOrDefault();

            //s.Id = emp.Id;
            s.EmployeeID = user.Id;
            s.ShiftStart = shiftStart;
            s.ShiftEnd = shiftEnd;
            s.Location = location;
            s.Status = ShiftStatus.Pending;

            if (shiftEnd < shiftStart)
            {
                ModelState.AddModelError(nameof(shiftEnd), "Shift End cannot be before Shift Start");
            }

            if (ModelState.IsValid)
            {
                _context.Add(s);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
                return View();
        }

        // GET: Shifts/Edit/5
        [Authorize(Roles = "Manager, Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shift = await _context.Shift.FindAsync(id);
            if (shift == null)
            {
                return NotFound();
            }
            return View(shift);
        }

        // POST: Shifts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager, Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id, ShiftStart, ShiftEnd, Location")] Shift shift)
        {
            var shiftOld = await _context.Shift.FindAsync(id);
            if (id != shift.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //shift.ShiftStart = start;
                    //shift.ShiftEnd = end;
                    //shift.Location = loc;
                    _context.Attach(shiftOld);
                    if (shift.ShiftStart != null)
                    {
                        shiftOld.ShiftStart = shift.ShiftStart;
                    }
                    if (shift.ShiftEnd != null)
                    {
                        shiftOld.ShiftEnd = shift.ShiftEnd;
                    }
                    if (shift.Location != null)
                    {
                        shiftOld.Location = shift.Location;
                    }

                    //shiftOld.ShiftStart = shift.ShiftStart;
                    //shiftOld.ShiftEnd = end;
                    //shiftOld.Location = loc;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShiftExists(shift.Id))
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
            return View(shift);
        }

        // GET: Shifts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shift = await _context.Shift
                .FirstOrDefaultAsync(m => m.Id == id);

            if (shift.Status == ShiftStatus.Approved)
            {
                ModelState.AddModelError(nameof(shift.Status), "You cannot delete approved shifts");
            }

            if (shift == null)
            {
                return NotFound();
            }

            return View(shift);
        }

        // POST: Shifts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            //if ( < shiftStart)
            //{
            //    ModelState.AddModelError(nameof(shiftEnd), "Shift End cannot be before Shift Start");
            //}

            if (ModelState.IsValid)
            {
                var shift = await _context.Shift.FindAsync(id);
                _context.Shift.Remove(shift);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Approve(int id)
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);
            string userID = claim.Value;
            var user = await _context.Users.Where(u => u.Id == userID).FirstOrDefaultAsync();

            Shift s = await _context.Shift.FirstOrDefaultAsync(u => u.Id == id);

            if (s.Status == ShiftStatus.Approved)
            {
                ModelState.AddModelError("", "This Shift is already approved");
                return View();
            }
            else if (s.Status == ShiftStatus.Ongoing)
            {
                ModelState.AddModelError("", "This Shift is still ongoing");
                return View();
            }

            s.Status = ShiftStatus.Approved;

            return View();
        }

        private bool ShiftExists(int id)
        {
            return _context.Shift.Any(e => e.Id == id);
        }
    }
}

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

namespace TimeKeepingApp.Controllers
{
    [Authorize]
    public class ShiftsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShiftsController(ApplicationDbContext context)
        {
            _context = context;
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
                t => t.EmployeeID == user.Id)
                                        select t;

            List<Shift> sList = await ShiftIQ.AsNoTracking().ToListAsync();

            List<Employee> employeeList = ShiftIQ.AsNoTracking().ToList()
                .GroupBy(p => p.EmployeeID)
                .Select(g => g.First())
                .Select(x => new Employee { EmployeeID = x.EmployeeID })
                .ToList();

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
                employees: employeeList,
                shifts: sList
                //start: fDate,
                //end: tDate
                //desc: descFilter,
                //page: int.Parse(pageNumber),
                //acct: actFilter
                );


            return View("Index", sList);//, vmod);

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

            s.EmployeeID = user.Id;
            s.ShiftStart = shiftStart;
            s.ShiftEnd = shiftEnd;
            s.Location = location;

            _context.Add(s);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Shifts/Edit/5
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmployeeID,ShiftStart,ShiftEnd,Location")] Shift shift)
        {
            if (id != shift.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shift);
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
            var shift = await _context.Shift.FindAsync(id);
            _context.Shift.Remove(shift);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShiftExists(int id)
        {
            return _context.Shift.Any(e => e.Id == id);
        }
    }
}

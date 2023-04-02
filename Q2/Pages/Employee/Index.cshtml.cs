using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Q2.Models;

namespace Q2.Pages.Employees
{
    public class IndexModel : PageModel
    {
        private readonly PRN_Spr23_B1Context _context;
        public IndexModel(PRN_Spr23_B1Context context)
        {
            _context = context;
        }
        public List<Employee> ListOfEmployee = new List<Employee>();

        public void OnGet()
        {
            ListOfEmployee =  _context.Employees.Include(x => x.Department).ToList();
        }
    }
}

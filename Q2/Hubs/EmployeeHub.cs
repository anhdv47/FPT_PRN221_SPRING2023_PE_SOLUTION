using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Q2.Models;

namespace Q2.Hubs
{
    public class EmployeeHub : Hub
    {
        private readonly PRN_Spr23_B1Context _context;
        public EmployeeHub(PRN_Spr23_B1Context context)
        {
            _context = context;
        }
        public async Task DeleteEmployee(int? deleteId)
        {
            if (deleteId != null)
            {
                var employee = _context.Employees.FirstOrDefault(x=> x.EmployeeId == deleteId);
                if(employee != null)
                {
                    var order = _context.Orders.Include(x=> x.OrderDetails).Where(x => x.EmployeeId == employee.EmployeeId).ToList();
                    foreach(var a in order)
                    {
                        _context.OrderDetails.RemoveRange(a.OrderDetails);
                    }
                    _context.Orders.RemoveRange(order);
                    _context.Employees.Remove(employee);
                    _context.SaveChanges();
                }
            }
            await Clients.All.SendAsync("EmployeeDeletedID", deleteId);
        }
      
    }
}

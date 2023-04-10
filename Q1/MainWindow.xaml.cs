using Microsoft.EntityFrameworkCore;
using Q1.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Q1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly PRN_Spr23_B1Context _context;
        public class Courtesy
        {
            public string Name { get; set; }
            public string Code { get; set; }
        }
        public MainWindow(PRN_Spr23_B1Context context)
        {

            _context = context;
            InitializeComponent();
            courtesy.ItemsSource = new List<Courtesy>()
            {
                new Courtesy { Name = "Dr.", Code = "Dr" },
                new Courtesy { Name = "Mr.", Code = "Mr" },
                new Courtesy { Name = "Mrs.", Code = "Mrs" },
                new Courtesy { Name = "Ms.", Code = "Ms" }
            };
            department.ItemsSource = _context.Departments.ToList();
            department.SelectedIndex = 0;
            UpdateGridView();

        }

        private Employee GetEmployeeObject()
        {

            Employee employee = new Employee
            {
                EmployeeId = string.IsNullOrEmpty(employeeId.Text) ? 0 : int.Parse(employeeId.Text),
                FirstName = firstName.Text,
                LastName = lastName.Text,
                DepartmentId = ((Department)department.SelectedItem).DepartmentId,
                Title = title.Text,
                TitleOfCourtesy = ((Courtesy)courtesy.SelectedItem).Name,
                BirthDate = dob.SelectedDate
            };
            return employee;
        }


        private void UpdateGridView()
        {
            lvEmployee.ItemsSource = null;
            lvEmployee.ItemsSource = _context.Employees.Include(x => x.Department).ToList();
        }

        private void Button_Add(object sender, RoutedEventArgs e)
        {
            try
            {
                Employee employee = GetEmployeeObject();
                employee.EmployeeId = 0;
                _context.Employees.Add(employee);
                if (_context.SaveChanges() > 0)
                {
                    MessageBox.Show("Add Employee Success");
                    UpdateGridView();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please enter correct information");
            }
        }
        private void Button_Refresh(object sender, RoutedEventArgs e)
        {
            employeeId.Text = string.Empty;
            firstName.Text = string.Empty;
            lastName.Text = string.Empty;
            department.SelectedIndex = 0;
            title.Text = string.Empty;
            courtesy.SelectedIndex = 0;
            dob.SelectedDate = null;
        }
        private void Button_Edit(object sender, RoutedEventArgs e)
        {
            try
            {
                Employee temployee = GetEmployeeObject();
                var employ = _context.Employees.FirstOrDefault(x => x.EmployeeId == temployee.EmployeeId);
                if (employ != null)
                {
                    employ.FirstName = temployee.FirstName;
                    employ.LastName = temployee.LastName;
                    employ.DepartmentId = temployee.DepartmentId;
                    employ.TitleOfCourtesy = temployee.TitleOfCourtesy;
                    employ.Title = temployee.Title;
                    employ.BirthDate = temployee.BirthDate;
                    _context.Employees.Update(employ);
                    if (_context.SaveChanges() > 0)
                    {
                        MessageBox.Show("Edit Employee Success");
                        UpdateGridView();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please enter correct information");
            }
        }

        private void btnClickOnGrid(object sender, MouseButtonEventArgs e)
        {
            var item = (sender as ListView).SelectedItem;
            if (item != null)
            {
                var employee = ((Employee)item);
                if (employee != null)
                {

                }
            }

        }
    }
}

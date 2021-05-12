using GSBlazor.Services;
using GSBlazor.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GSBlazor.Pages
{
    public partial class EmployeeOverviewVirtual
    {
        public List<Employee> Employees { get; set; } = new List<Employee>();
       
        [Inject]
        public IEmployeeDataService EmployeeDataService { get; set; }

        private float itemHeight = 50;

        protected async override Task OnInitializedAsync()
        {
            Employees = (await EmployeeDataService.GetLongEmployeeList()).ToList();
        }
    }
}

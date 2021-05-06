using BethanysPieShopHRM.ComponentsLibrary.Map;
using GSBlazor.Services;
using GSBlazor.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GSBlazor.Pages
{
    public partial class EmployeeDetail
    {
        [Parameter]
        public string EmployeeId { get; set; } = "0";
        public Employee Employee { get; set; } = new Employee();
        [Inject]
        public IEmployeeDataService EmployeeDataService { get; set; }
        public List<Marker> MapMarkers { get; set; } = new List<Marker>();


        public int MyProperty { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            Employee = await EmployeeDataService.GetEmployeeDetails(int.Parse(EmployeeId));

            MapMarkers = new List<Marker>
            {
                new Marker{Description = $"{Employee.FirstName} {Employee.LastName}",  ShowPopup = false, X = Employee.Longitude, Y = Employee.Latitude}
            };



        }

        //protected override async Task OnInitializedAsync()
        //{

        //    Employee = await EmployeeDataService.GetEmployeeDetails(int.Parse(EmployeeId));
        //}

    }
}

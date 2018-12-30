using System;
using System.Text;

namespace WebAPI.Model.Department
{
  public class DepartmentModelReq
  {
      public string Id { get; set; }
      public string PhoneNumber { get; set; }
      public string Name { get; set; }
      public bool? Active { get; set; }
  }
}
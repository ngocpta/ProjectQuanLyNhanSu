using System;
using System.Text;

namespace WebAPI.Model.Specialize
{
  public class SpecializeModelReq
  {
      public string Id { get; set; }
      public string Name { get; set; }
      public bool? Active { get; set; }
  }
}
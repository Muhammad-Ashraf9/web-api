﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Day2.Models;

public partial class Student
{
    public int StId { get; set; }

    public string StFname { get; set; }

    public string StLname { get; set; }

    public string StAddress { get; set; }

    public int? StAge { get; set; }

    public int? DeptId { get; set; }

    public int? StSuper { get; set; }
    [JsonIgnore]
    public virtual Department Dept { get; set; }
    [JsonIgnore]
    public virtual ICollection<Student> InverseStSuperNavigation { get; set; } = new List<Student>();
    [JsonIgnore]
    public virtual Student StSuperNavigation { get; set; }
}
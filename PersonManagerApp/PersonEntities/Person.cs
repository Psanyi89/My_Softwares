﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonEntities
{
   public class Person
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public override string ToString()
        {
            return $"{Id},{Name},{DateOfBirth},{Email}"; 
        }
    }
}

﻿using System;
using System.Collections.Generic;

#nullable disable

namespace backendlib.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}

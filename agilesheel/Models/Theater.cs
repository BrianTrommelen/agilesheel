﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace agilesheel.Models
{
    public class Theater
    {
        [Key]
        private int Id { get; set; }

        private int Seats { get; set; }

        private bool Has3D { get; set; }
    }
}

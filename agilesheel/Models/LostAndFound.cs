﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace agilesheel.Models
{
    public class LostAndFound
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsFound { get; set; }

        public string UserId { get; set; }

        public IdentityUser User { get; set; }
    }
}

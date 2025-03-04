﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Accounts.DTOs
{
    public class UserDTO
    {
        public string Username { get; set; } = null!;
        public string Token { get; set; } = null!;
    }
}

﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Awushi.Application.Services.Interface
{
    public interface IAuthService 
    {
        Task<IEnumerable<IdentityError>> Register();
    }
}

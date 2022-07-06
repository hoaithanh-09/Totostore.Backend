﻿using Microsoft.AspNetCore.Identity;

namespace Totostore.Backend.Domain.Identity;
public class ApplicationRoleClaim : IdentityRoleClaim<string>
{
    public string? CreatedBy { get; init; }
    public DateTime CreatedOn { get; init; }
}

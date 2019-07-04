﻿using BookStore_Inspiration.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore_Inspiration.Areas.Administrator.Comtrollers
{
    [Area("Administrator")]
    [Authorize(Roles = "Admin")]
    public class AdministratorController : BaseController
    {

    }
}
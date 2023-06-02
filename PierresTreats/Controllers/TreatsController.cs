using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using PierresTreats.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace PierresTreats.Controllers
{
  [Authorize]
  public class TreatsController : Controller
  {
    private readonly PierresTreatsContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public TreatsController(UserManager<ApplicationUser> userManager, PierresTreatsContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public async Task<ActionResult> Index(string sortOrder)
    {
      if (sortOrder == "price"){
        string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
        List<Treat> userTreats = _db.Treats
                            .Where(entry => entry.User.Id == currentUser.Id)
                            .Include(treat => treat.FlavorTreatJoinEntities)
                            .OrderByDescending(treat => treat.TreatPrice)
                            .ToList();
        return View(userTreats);
      } else {
        string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
        List<Treat> userTreats = _db.Treats
                            .Where(entry => entry.User.Id == currentUser.Id)
                            .Include(treat => treat.FlavorTreatJoinEntities)
                            .OrderBy(treat => treat.TreatName)
                            .ToList();
        return View(userTreats);
      }
    }

    public ActionResult Create()  
    {
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Treat treat, int FlavorId)
    {
      if (!ModelState.IsValid)
      {
        return View();
      }
      else
      {
        string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
        treat.User = currentUser;
        _db.Treats.Add(treat);
        _db.SaveChanges();
        return RedirectToAction("Index");
      }
    }
    public ActionResult Details(int id)  
    {
      Treat thisTreat = _db.Treats
          .Include(treat => treat.FlavorTreatJoinEntities)
          .ThenInclude(join => join.Flavor)
          .FirstOrDefault(treat => treat.TreatId == id);
      return View(thisTreat);
    }
  }
}
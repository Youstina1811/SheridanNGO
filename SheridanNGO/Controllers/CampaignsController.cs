using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SheridanNGO.Models;
using System.Linq;
using System.Threading.Tasks;

public class CampaignsController : Controller
{
    private readonly DonationDbContext _context;

    public CampaignsController(DonationDbContext context)
    {
        _context = context;
    }

    // GET: Campaigns
    // Example for async methods in your CampaignsController
    public async Task<IActionResult> Index()
    {
        //return View(await _context.Campaigns.ToListAsync());
        var campaigns = _context.Campaigns.ToList();  // Get all campaigns
        return View(campaigns);  // Pass them to the view
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var campaign = await _context.Campaigns
            .FirstOrDefaultAsync(m => m.CampaignID == id);

        if (campaign == null)
        {
            return NotFound();
        }

        return View(campaign);
    }


     public IActionResult ViewCampaign(int id)
    {
        // Get the campaign from the database by ID
        var campaign = _context.Campaigns.FirstOrDefault(c => c.CampaignID == id);

        if (campaign == null)
        {
            return NotFound();
        }

        return View(campaign); // Return the campaign details view
    }


    // GET: Campaigns/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Campaigns/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Title,Description,GoalAmount,StartDate,EndDate,NGOId")] Campaign campaign)
    {
        if (ModelState.IsValid)
        {
            _context.Add(campaign);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(campaign);
    }

    // GET: Campaigns/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var campaign = await _context.Campaigns.FindAsync(id);
        if (campaign == null) return NotFound();

        return View(campaign);
    }

    // POST: Campaigns/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("CampaignId,Title,Description,GoalAmount,CurrentAmount,StartDate,EndDate,NGOId")] Campaign campaign)
    {
        if (id != campaign.CampaignID) return NotFound();

        if (ModelState.IsValid)
        {
            _context.Update(campaign);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(campaign);
    }

    // GET: Campaigns/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var campaign = await _context.Campaigns.FirstOrDefaultAsync(m => m.CampaignID == id);
        if (campaign == null) return NotFound();

        return View(campaign);
    }

    // POST: Campaigns/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var campaign = await _context.Campaigns.FindAsync(id);
        _context.Campaigns.Remove(campaign);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}

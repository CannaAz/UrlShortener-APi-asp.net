using Microsoft.AspNetCore.Mvc;

using URLSHORTENER.Models;
using URLSHORTENER.Dtos;
using System.Threading.Tasks;
using URLSHORTENER.Services;
using Microsoft.EntityFrameworkCore;

namespace URLSHORTENER.Controllers;


[ApiController]
[Route("Short")]
public class ShortenerController : Controller
{
    private readonly AppDbContext _context;

    private readonly UrlShorteningService _urlService;

    private readonly QrGeneratorService _QrSerivice;
    public ShortenerController(AppDbContext context, UrlShorteningService urlService, QrGeneratorService qrService)
    {
        _context = context;
        _urlService = urlService;
        _QrSerivice = qrService;
    }


    [HttpGet]
    public IActionResult GetDebug()
    {
        return Ok(HttpContext.Request.Scheme);
    }

    [HttpGet("{code}")]
    public async Task<IActionResult> GetShortenerUrl(string code)
    {

        var shortUrl = await _context.ShortenedUrl.FirstOrDefaultAsync(url => url.Code == code);
        if(shortUrl is null) return NotFound();

        var LinkInteraction = await _context.LinkInteractions.FirstOrDefaultAsync(L => L.LinkId == shortUrl.Id);

        if(LinkInteraction == null)
        {
            LinkInteractions NewLinkInter = new LinkInteractions
            {
                Id = new Guid(),
                LinkId = shortUrl.Id,
                LinkUsages = 1
            };
            _context.Add(NewLinkInter);
        }
        else LinkInteraction.LinkUsages += 1;

        LinkUsage linkUsage = new LinkUsage
        {
            Id = new Guid(),
            LinkId = shortUrl.Id,
            UsedAt = DateTime.Now
        };
        _context.LinkUsages.Add(linkUsage);
        _context.SaveChanges();

        
        return Redirect(shortUrl.LongUrl);
    }

    [HttpPost("EngagementData")]
    public async Task<IActionResult> GetShortenedUrlEngagementData(ShortenUrlRequestDto request)
    {
        if(!ModelState.IsValid) return BadRequest();
        

        var ShortUrl  = await _context.ShortenedUrl.FirstOrDefaultAsync(url => url.ShortUrl == request.Url);
        if(ShortUrl == null) return BadRequest("Specified Url doesn't exist");

        var Interactions = await _context.LinkInteractions.FirstOrDefaultAsync(Inter => Inter.LinkId == ShortUrl.Id);
        if(Interactions == null) return Ok("This Link hasn't had any engagement");
        
        List<DateTimeOffset> AllDates = new List<DateTimeOffset>();

        AllDates = await _context.LinkUsages.Where(s => s.LinkId == ShortUrl.Id).Select(s => s.UsedAt).ToListAsync();

        UrlEngagementDto newDto = new UrlEngagementDto(
            AllDates,
            Interactions.LinkUsages
        );

        return Ok(newDto);
    }

    [HttpPost("CustomShortenUrl")]
    public async Task<IActionResult> ShortenCustomUrl(CustomShortenUrlRequestDto request)
    {
        if(!ModelState.IsValid) return BadRequest();

        if(!Uri.TryCreate(request.Url, UriKind.Absolute, out _)) return BadRequest("The Specified URL is invalid");

        string CustomUrlName = request.customName;

        if(request.customName is null) CustomUrlName = _urlService.GenerateUniqueCode().Result;

        if(await _context.ShortenedUrl.FirstOrDefaultAsync(url => url.Code == CustomUrlName) == null)
        {
            ShortenedUrl ShortenedUrl = new ShortenedUrl
            {
                Id = Guid.NewGuid(),
                LongUrl = request.Url,
                Code = CustomUrlName,
                ShortUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/Short/{CustomUrlName}",
                CreatedAt = DateTimeOffset.Now
            };

            await _context.ShortenedUrl.AddAsync(ShortenedUrl);
            await _context.SaveChangesAsync();

            return Ok(ShortenedUrl.ShortUrl);
        }
        else return BadRequest("The Specified URL code already exists");
    }

    [HttpPost("MakeQrCode")]
    public async Task<IActionResult> MakeQrCodeWithUrl(ShortenUrlRequestDto request)
    {
        if(!ModelState.IsValid) return BadRequest();

        if(!Uri.TryCreate(request.Url, UriKind.Absolute, out _)) return BadRequest("Invalid URL");

        var QrCodeFormat = _QrSerivice.GenerateQRCode(request.Url);
        return Ok(QrCodeFormat);
    }

    [HttpPost("RandomShortenUrl")]
    public async  Task<IActionResult> ShortenUrl(ShortenUrlRequestDto request)
    {
        if(!ModelState.IsValid) return BadRequest();

        if(!Uri.TryCreate(request.Url, UriKind.Absolute, out _)) return BadRequest("The specified URL is invalid");

        

        var code = _urlService.GenerateUniqueCode();
        ShortenedUrl ShortenedUrl = new ShortenedUrl
        {
            Id = Guid.NewGuid(),
            LongUrl = request.Url,
            Code = code.Result,
            ShortUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/Short/{code.Result}",
            CreatedAt = DateTime.Now
        };

        await _context.ShortenedUrl.AddAsync(ShortenedUrl);
        await _context.SaveChangesAsync();

        
        return Ok(ShortenedUrl.ShortUrl);
    }
   
}

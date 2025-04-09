using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using URLSHORTENER.Models;

namespace URLSHORTENER;

public class AppDbContext : IdentityDbContext<AppUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<ShortenedUrl> ShortenedUrl {get; set;}

    public DbSet<LinkInteractions> LinkInteractions {get; set;}

    public DbSet<LinkUsage> LinkUsages{get; set;}
}

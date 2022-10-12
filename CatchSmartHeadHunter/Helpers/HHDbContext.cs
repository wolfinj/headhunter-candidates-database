using CatchSmartHeadHunter.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace CatchSmartHeadHunter.Helpers;

public class HhDbContext : DbContext
{
    public DbSet<Candidate> Candidates { get; set; }
    public DbSet<Position> Positions { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Skill> Skills { get; set; }
    public DbSet<CandidatePosition> CandidatePositions { get; set; }
    public DbSet<CompanyPosition> CompanyPositions { get; set; }

    private readonly IConfiguration _configuration;

    public HhDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder.Entity<Company>().HasMany(c=>c.OpenPositions).WithOne();
        // modelBuilder.Entity<Candidate>().HasMany(c=>c.AppliedPositions).WithOne();
        // modelBuilder.Entity<Candidate>().HasMany(c=>c.Skills);
        // modelBuilder.Entity<Position>().HasMany(c=>c.RequiredSkills);
        // modelBuilder.Entity<CompanyPosition>().HasOne(c => c.Company);
        // modelBuilder.Entity<CompanyPosition>().HasOne(c => c.Position);
        // modelBuilder.Entity<CandidatePosition>().HasOne(c => c.Position);
        // modelBuilder.Entity<CandidatePosition>().HasOne(c => c.Candidate);
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // options.UseSqlite(_configuration.GetConnectionString("Headhunters"));
        options.UseSqlServer(_configuration.GetConnectionString("HeadhuntersSqlServer"));
    }
}

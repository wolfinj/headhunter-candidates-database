using CatchSmartHeadHunter.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CatchSmartHeadHunter.Data;

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
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer(_configuration.GetConnectionString("HeadhuntersSqlServer"));
    }
}

using BenchmarkDotNet.Attributes;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Repositories;
using RepositoryContracts;

namespace Benchmark
{
    [MemoryDiagnoser]
    [RankColumn]
    public class PersonsRepositoryBenchmark
    {
        private IPersonsRepository _personsRepository;
        private ApplicationDbContext _db;

        [GlobalSetup]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=PersonsDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False")
                .Options;

            _db = new ApplicationDbContext(options);

            var logger = new NullLogger<PersonsRepository>();

            _personsRepository = new PersonsRepository(_db, logger);
        }

        [GlobalCleanup]
        public void Cleanup()
        {
            _db?.Dispose();
        }

        [Benchmark]
        public async Task GetAllPersonsWithAsNoTrackingWithIdentityResolution()
        {
            await _personsRepository.GetAllPersons();
        }
    }
}

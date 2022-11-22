using DemoSite.Models.Domain;
using DemoSite.Ports;
using Microsoft.EntityFrameworkCore;
using File = DemoSite.Models.Domain.File;

namespace DemoSite.Repositories
{
    public class FileEntryRepository: DatabaseRepositoryBase, IFileEntryRepository
    {
        public FileEntryRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<File> Import(Guid id)
        {
            return await DbContext.Files.SingleAsync(f => f.Id == id);
        }

        public async Task Export(File fileEntry)
        {
            var anyFileEntry = await DbContext.Files.AnyAsync(file => file.Id == fileEntry.Id);
            if (anyFileEntry) throw new FileIdConflictException();
            DbContext.Files.Add(fileEntry);
            await DbContext.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var fileEntry = await DbContext.Files.SingleOrDefaultAsync(file => file.Id == id);
            if (fileEntry == null) throw new FileEntryNotFoundException();
            DbContext.Files.Remove(fileEntry);
            await DbContext.SaveChangesAsync();
        }
    }
}

using DemoSite.Models.Domain;
using DemoSite.Models.DTO;
using File = DemoSite.Models.Domain.File;

namespace DemoSite.Ports;

/**
 * <summary>The repository which handles CRUD to file entry in database.</summary>
 */
public interface IFileEntryRepository
{
    Task<File> Import(Guid id);
    /**
     * <summary>Export a file entry to database.</summary>
     * <exception cref="FileIdConflictException">
     * Thrown when there is another file entry with same GUID in the database.
     * </exception>
     */
    Task Export(File fileEntry);
    /**
     * <summary>Delete a file entry in the database.</summary>
     * <exception cref="FileEntryNotFoundException">
     * Thrown when the file entry to delete is not exists in the database.
     * </exception>
     */
    Task Delete(Guid id);
}

/**
 * <summary>The repository which handles CRUD to file storage.</summary>
 */
public interface IFileRepository
{
    Task<byte[]> Import(Guid id);
    /**
    * <summary>Export a file itself to somewhere.</summary>
    */
    Task Export(File fileEntry, IFormFile formFile);
    Task Delete(Guid id);
}
using System.ComponentModel.DataAnnotations.Schema;
using DemoSite.Exceptions;

namespace DemoSite.Models.Domain
{
    public class File
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; } = Guid.NewGuid();
        public required long UserId { get; set; }
        public required string ContentType { get; set; }
        public string FileName => GetFileName(Id);
        public static string GetFileName(Guid id) => $"{id}.data";
    }

    public class FileIdConflictException : PropertyConflictException<File>
    {
        public FileIdConflictException(string message) : base(message, nameof(File.Id))
        {
        }

        public FileIdConflictException() : base(nameof(File.Id)) { }
    }

    public class FileEntryNotFoundException : Exception
    {
        public FileEntryNotFoundException(string message = "The file entry is not found in the database."): base(message) {}
    }

    
}

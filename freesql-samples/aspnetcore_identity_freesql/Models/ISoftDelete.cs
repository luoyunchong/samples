namespace aspnetcore_identity_freesql.Models;

public interface ISoftDelete
{
    bool IsDeleted { get; set; }
}
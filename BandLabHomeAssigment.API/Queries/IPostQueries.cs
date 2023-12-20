using BandLabHomeAssigment.API.Responses;
using BandLabHomeAssigment.Domain;

namespace BandLabHomeAssigment.API.Queries;

public interface IPostQueries
{
    Task<IEnumerable<PostModel>> GetPostsSortedByComments(string cursor, int limit, Guid userId);
}

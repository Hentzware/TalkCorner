using TalkCorner.Domain.Common;
using TalkCorner.Domain.ValueObjects;

namespace TalkCorner.Domain.Entities;

public class Post : BaseEntity
{
    public Post()
    {
    }

    private Post(PostContent content, Guid createdByUserId, Guid threadId, Guid? parentPostId = null)
    {
        Content = content;
        CreatedByUserId = createdByUserId;
        ThreadId = threadId;
        ParentPostId = parentPostId;
    }

    public Guid CreatedByUserId { get; private set; }

    public Guid ThreadId { get; private set; }

    public Guid? ParentPostId { get; private set; }

    public IReadOnlyCollection<Post> Replies { get; private set; } = new List<Post>();

    public Post? ParentPost { get; private set; }

    public PostContent Content { get; private set; } = null!;

    public Thread Thread { get; private set; } = null!;

    public User CreatedByUser { get; private set; } = null!;

    public static Post Create(string content, Guid createdByUserId, Guid threadId, Guid? parentPostId = null)
    {
        var postContent = PostContent.Create(content);
        return new Post(postContent, createdByUserId, threadId, parentPostId);
    }

    public void UpdateContent(string newContent)
    {
        Content = PostContent.Create(newContent);
    }
}
using System.ComponentModel.DataAnnotations.Schema;
using TalkCorner.Domain.Common;
using TalkCorner.Domain.ValueObjects;

namespace TalkCorner.Domain.Entities;

public class Post : BaseEntity
{
    private readonly List<Post> _replies = new();

    private Post()
    {
    }

    private Post(PostContent content, Guid createdByUserId, Guid threadId)
    {
        Content = content;
        CreatedByUserId = createdByUserId;
        ThreadId = threadId;
    }

    public Guid CreatedByUserId { get; private set; }

    public Guid ThreadId { get; private set; }

    public Guid? ParentPostId { get; private set; }

    [InverseProperty(nameof(ParentPost))]
    public IReadOnlyCollection<Post> Replies => _replies.AsReadOnly();

    [ForeignKey(nameof(ParentPostId))]
    [InverseProperty(nameof(Replies))]
    public Post? ParentPost { get; private set; }

    public PostContent Content { get; private set; } = null!;

    [ForeignKey(nameof(ThreadId))]
    [InverseProperty(nameof(Thread.Posts))]
    public Thread Thread { get; private set; } = null!;

    [ForeignKey(nameof(CreatedByUserId))]
    [InverseProperty(nameof(User.Posts))]
    public User CreatedByUser { get; private set; }

    public static Post Create(string content, User createdByUser, Thread thread, Post? parentPost = null)
    {
        if (createdByUser == null)
        {
            throw new ArgumentNullException(nameof(createdByUser));
        }

        if (thread == null)
        {
            throw new ArgumentNullException(nameof(thread));
        }

        var postContent = PostContent.Create(content);

        var post = new Post(postContent, createdByUser.Id, thread.Id);

        // Navigationen setzen
        post.SetCreatedByUser(createdByUser);
        post.SetThread(thread);

        if (parentPost != null)
        {
            post.SetParentPost(parentPost);
            parentPost.AddReply(post);
        }

        // Listen auffüllen
        createdByUser.AddPost(post);
        thread.AddPost(post);

        return post;
    }

    private void SetCreatedByUser(User user)
    {
        CreatedByUser = user;
        CreatedByUserId = user.Id;
    }

    private void SetThread(Thread thread)
    {
        Thread = thread;
        ThreadId = thread.Id;
    }

    private void SetParentPost(Post parent)
    {
        ParentPost = parent;
        ParentPostId = parent.Id;
    }

    public void AddReply(Post reply)
    {
        if (reply == null)
        {
            throw new ArgumentNullException(nameof(reply));
        }

        if (_replies.Contains(reply))
        {
            throw new InvalidOperationException("Reply already added to this post.");
        }

        _replies.Add(reply);
    }

    public void UpdateContent(string newContent)
    {
        Content = PostContent.Create(newContent);
    }
}
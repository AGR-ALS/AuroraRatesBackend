using AuroraRates.DataAccess.Entity;
using AuroraRates.Domain.Abstractions.Reviews;
using AuroraRates.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AuroraRates.DataAccess.Repository;

public class ReviewsRepository : IReviewsRepository
{
    private readonly AuroraRatesDatabaseContext _context;

    public ReviewsRepository(AuroraRatesDatabaseContext context)
    {
        _context = context;
    }

    public async Task<List<Review>> GetReviewsAsync()
    {
        List<ReviewEntity> reviewEntities = await _context.Reviews.AsNoTracking()
            .Include(reviewEntity => reviewEntity.User!).Include(entity => entity.MediaType).ToListAsync();
        var reviews = reviewEntities.Select(r =>
            new Review(r.Id, r.Title, r.Content, r.CreatedAt,
                new User(r.User!.Id, r.User.Nickname, r.User.PasswordHash, r.User.Email),
                new MediaType(r.MediaType!.Id, r.MediaType.Name)) { ImageUrl = r.ImageUrl }).ToList();

        return reviews;
    }

    public async Task<Review> GetReviewByIdAsync(Guid id)
    {
        var reviewEntity = await _context.Reviews.AsNoTracking().Include(reviewEntity => reviewEntity.User!)
            .Include(reviewEntity => reviewEntity.MediaType).FirstAsync(r => r.Id == id);
        return new Review(reviewEntity.Id, reviewEntity.Title, reviewEntity.Content, reviewEntity.CreatedAt,
                new User(reviewEntity.User!.Id, reviewEntity.User.Nickname, reviewEntity.User.PasswordHash,
                    reviewEntity.User.Email), new MediaType(reviewEntity.MediaType!.Id, reviewEntity.MediaType.Name))
            { ImageUrl = reviewEntity.ImageUrl };
    }

    public async Task<Guid> CreateReviewAsync(Review review)
    {
        var reviewEntity = new ReviewEntity
        {
            Id = review.Id,
            Title = review.Title,
            Content = review.Content,
            CreatedAt = review.CreatedAt,
            User = await _context.Users.FindAsync(review.User!.Id),
            UserId = review.User.Id,
            MediaType = await _context.MediaTypes.FindAsync(review.MediaType!.Id),
            MediaTypeId = review.MediaType.Id,
            ImageUrl = review.ImageUrl,
        };
        _context.Reviews.Add(reviewEntity);
        await _context.SaveChangesAsync();
        return reviewEntity.Id;
    }

    public async Task<Guid> UpdateReviewAsync(Review review)
    {
        await _context.Reviews.Where(r => r.Id == review.Id).ExecuteUpdateAsync(s => s
            .SetProperty(r => r.Title, r => review.Title)
            .SetProperty(r => r.Content, r => review.Content)
            .SetProperty(r => r.MediaTypeId, r => review.MediaType.Id));

        if (review.ImageUrl !=
            null) //в случае если пользователь не отправляет картинки, она не обновляется. Понадобиться изменить, если добавлю превью картинки в меню изменения
            await _context.Reviews.Where(r => r.Id == review.Id)
                .ExecuteUpdateAsync(s => s.SetProperty(r => r.ImageUrl, r => review.ImageUrl));
        return review.Id;
    }

    public async Task<Guid> DeleteReviewAsync(Guid id)
    {
        await _context.Reviews.Where(r => r.Id == id).ExecuteDeleteAsync();
        return id;
    }
}
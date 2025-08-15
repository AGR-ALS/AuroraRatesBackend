using AuroraRates.Api.Contracts;
using AuroraRates.Application.Abstractions.Authentication;
using AuroraRates.Application.Abstractions.Files;
using AuroraRates.Domain.Abstractions.MediaType;
using AuroraRates.Domain.Abstractions.Reviews;
using AuroraRates.Domain.Models;
using AuroraRates.Infrastructure;
using AuroraRates.Infrastructure.Files;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuroraRates.Api.Controllers;

[ApiController]
[Route("reviews")]
public class ReviewsController : ControllerBase
{
    private readonly IReviewService _reviewService;
    private readonly IMediaTypeService _mediaTypeService;
    private readonly ImageUploader _imageUploader;
    private readonly ICurrentUserDataService _currentUserDataService;
    private readonly IFileDeletingService _fileDeletingService;

    public ReviewsController(IReviewService reviewService, IMediaTypeService mediaTypeService, ImageUploader imageUploader, ICurrentUserDataService currentUserDataService, IFileDeletingService fileDeletingService)
    {
        _reviewService = reviewService;
        _mediaTypeService = mediaTypeService;
        _imageUploader = imageUploader;
        _currentUserDataService = currentUserDataService;
        _fileDeletingService = fileDeletingService;
    }

    [HttpGet]
    public async Task<ActionResult<List<ReviewsResponse>>> GetReviews([FromQuery] GetReviewsRequest request)
    {
        var reviews = await _reviewService.GetAllReviewsAsync();
        if (request.MediaTypeName != null)
            reviews = reviews.Where(r => r.MediaType.Name == request.MediaTypeName).ToList();
        var baseUrl = $"{Request.Scheme}://{Request.Host}";

        var response = reviews.Select(r =>
            new ReviewsResponse(
                r.Id,
                r.Title,
                r.Content,
                r.CreatedAt,
                r.User.Nickname,
                r.MediaType.Name,
                string.IsNullOrEmpty(r.ImageUrl) ? null : baseUrl + "/" + r.ImageUrl
            )
        );
            return Ok(response);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateReview([FromForm] PostPutReviewsRequest request)
    {
        string? imagePath = null;
        if(request.Image!=null)
            imagePath = await _imageUploader.UploadImageAsync(new FormFileAdapter(request.Image));

        User user = new User(new Guid(_currentUserDataService.Id!), _currentUserDataService.Username!,
            null, _currentUserDataService.Email!);
        var mediaType = await _mediaTypeService.GetByNameAsync(request.MediaTypeName);
        
        var review = new Review(Guid.NewGuid(), request.Title, request.Content, user,
            mediaType ?? throw new InvalidOperationException("media type couldn't be found"));
        review.ImageUrl = imagePath;

        await _reviewService.CreateReviewAsync(review);

        return Ok(review.Id);
    }


    [Authorize]
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<Guid>> UpdateReview(Guid id, [FromForm] PostPutReviewsRequest request)
    {
        string? imagePath = null;
        if(request.Image!=null)
            imagePath = await _imageUploader.UploadImageAsync(new FormFileAdapter(request.Image));

        User user = new User(new Guid(_currentUserDataService.Id!), _currentUserDataService.Username!,
            null, _currentUserDataService.Email!);
        var mediaType = await _mediaTypeService.GetByNameAsync(request.MediaTypeName);

        var reviewId = await _reviewService.UpdateReviewAsync(new Review(id, request.Title, request.Content, user,
            mediaType ?? throw new InvalidOperationException("media type couldn't be found")) { ImageUrl = imagePath });
        return Ok(reviewId);
    }

    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<Guid>> DeleteReview(Guid id)
    {
        var imageToDelete = (await _reviewService.GetReviewByIdAsync(id)).ImageUrl;
        await _reviewService.DeleteReviewAsync(id);
        if (imageToDelete != null) await _fileDeletingService.DeleteFileAsync(new List<string>() { imageToDelete });
        return Ok();
    }
}
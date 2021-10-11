using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ICurrentUserService _currentUserService;

        public UserController(IUserService userService, ICurrentUserService currentUserService)
        {
            _userService = userService;
            _currentUserService = currentUserService;

        }

        [Authorize]
        [HttpPost("purchase")]
        public async Task<ActionResult> CreatePurchase([FromBody] PurchaseRequestModel purchaseRequest)
        {
            var purchasedStatus =
                await _userService.PurchaseMovie(purchaseRequest, _currentUserService.UserId.GetValueOrDefault());
            return Ok(new { purchased = purchasedStatus });
        }

        [Authorize]
        [HttpPost("{id:int}/reviews")]
        public async Task<ActionResult> GetUserReview()
        {
            var userId = _currentUserService.UserId.GetValueOrDefault();
            var userReviews =
                await _userService.GetReviewsByUserId(userId);
            return Ok(new { reviews = userReviews });
        }

        [Authorize]
        [HttpPost("review")]
        public async Task<ActionResult> CreateReview([FromBody] int userId, int movieId, decimal rating, string reviewText)
        {
            await _userService.AddReview(userId, movieId, rating, reviewText);
            return Ok();
        }

        [Authorize]
        [HttpPut("review")]
        public async Task<ActionResult> UpdateReview([FromBody] int userId, int movieId, decimal rating, string reviewText)
        {
            await _userService.UpdateReview(userId, movieId, rating, reviewText);
            return Ok();
        }

        [Authorize]
        [HttpDelete("{userId:int}/movie/{movieId:int}")]
        public async Task<NoContentResult> DeleteReview(int userId, int movieId)
        {
            await _userService.DeleteReview(userId, movieId);
            return NoContent();
        }

        [Authorize]
        [HttpPost("favorite")]
        public async Task<ActionResult> CreateFavorite([FromBody] FavoriteRequestModel favoriteRequest)
        {
            await _userService.AddFavorite(favoriteRequest);
            return Ok();
        }

        [Authorize]
        [HttpPost("unfavorite")]
        public async Task<ActionResult> DeleteFavorite([FromBody] FavoriteRequestModel favoriteRequest)
        {
            await _userService.DeleteFavorite(favoriteRequest);
            return Ok();
        }

        [Authorize]
        [Route("purchases")]
        [HttpGet]
        public async Task<IActionResult> Purchases()
        {
            var userId = _currentUserService.UserId.GetValueOrDefault();
            var userMovies = await _userService.GetPurchaseByUserId(userId);
            return Ok(userMovies);
        }

    }
}
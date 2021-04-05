using System.Collections.Generic;
using System.Threading.Tasks;
using Exercise1.Data.Repos;
using Exercise1.Data.Models.VirbelaListing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using Exercise1.Api.Authentication;

namespace Exercise1.Api.Controllers
{
    // TO DO: Use Cancellation Token

    [ApiController]
    [Route("api/[controller]")]
    public class ListingController : ControllerBase
    {
        private ILogger<ListingController> _logger;
        private IUnitOfWork _unitOfWork;

        public ListingController(
            ILogger<ListingController> logger,
            IUnitOfWork unitOfWork)
        {
            this._logger = logger;
            this._unitOfWork = unitOfWork;
        }

        [HttpGet]
        [TokenAuthorize()]
        /*  Use cache to repeat request within short time */
        [ResponseCache(
            Duration = 10,
            Location = ResponseCacheLocation.Client,
            NoStore = false)]
        public async Task<IActionResult> Get(
            [FromQuery] int? pageNum,
            [FromQuery] int? pageSize,
            [FromQuery] string title,
            [FromQuery] string description,
            [FromQuery] string price,
            [FromQuery] string regionName,
            // [FromQuery] string createdDate, 
            CancellationToken cancellationToken)
        {
            Listinguser user = (Listinguser)HttpContext.Items["User"];
            object parameters = new List<KeyValuePair<string, string>> {
                // new KeyValuePair<string, string> ("CreatorId", user.Id.ToString()),
                new KeyValuePair<string, string> ("RegionId", user.RegionId.ToString()),
                new KeyValuePair<string, string> ("Title", title),
                new KeyValuePair<string, string> ("Description", description),
                new KeyValuePair<string, string> ("Price", price),
                new KeyValuePair<string, string> ("RegionName", regionName),
            };

            // Repository returns paged data if pageNum and pageSize are provided
            var listings = await _unitOfWork.Region_ListingRepository
                                .GetAsync(parameters, pageNum, pageSize);
            return Ok(listings);
        }

        [HttpGet("{id}")]
        [TokenAuthorize()]
        public async Task<IActionResult> Get(
            int id, 
            CancellationToken cancellationToken)
        {
            Listinguser user = (Listinguser)HttpContext.Items["User"];
            var region_Listing = await _unitOfWork.Region_ListingRepository
                                .GetAsync(id.ToString());
            if (region_Listing.RegionId != user.RegionId)
                return Ok(null);

            return Ok(region_Listing);
        }

        [HttpPut("{id}")]
        [TokenAuthorize()]
        public async Task<IActionResult> Put(
            string id, 
            ListingRequest listingUpdateRequest, 
            CancellationToken cancellationToken)
        {
            int idNum;
            if (!int.TryParse(id, out idNum))
                return BadRequest();

            var user = (Listinguser)HttpContext.Items["User"];
            try {
                Listing listing = await _unitOfWork.ListingRepository
                                        .GetAsync(id);
                if (listing == null || listing.CreatorId != user.Id)
                    return Unauthorized();
 
                listing.Title = listingUpdateRequest.Title;
                listing.Description = listingUpdateRequest.Description;
                listing.Price = listingUpdateRequest.Price;

                // Since Listing is already tracked by GetAsync() above,
                // No need to call for PutAsync(). 
                // Listing updatedListing = await _unitOfWork.ListingRepository
                //                         .PutAsync(id, listingUpdateRequest);
                _unitOfWork.Commit();
                return Ok(listing);
            } catch(Exception ex) {
                _logger.LogError(ex, ex.Message);
                _unitOfWork.Rollback();
                return BadRequest();
            }
        }

        [HttpPost]
        [TokenAuthorize()]
        public async Task<IActionResult> Post(
            ListingRequest listingCreateRequest, 
            CancellationToken cancellationToken)
        {
            try {
                var user = (Listinguser)HttpContext.Items["User"];
                var listingModel = new Listing{
                    Title = listingCreateRequest.Title,
                    Description = listingCreateRequest.Description,
                    Price = listingCreateRequest.Price,
                    CreatorId = user.Id,
                    CreatedDate = DateTime.UtcNow
                };
                // listingCreateRequest.CreatorId = user.Id;
                // listingCreateRequest.CreatedDate = DateTime.UtcNow;
                Listing listing = await _unitOfWork.ListingRepository.PostAsync(listingModel);
                _unitOfWork.Commit();

                return CreatedAtAction(
                    nameof(Post), 
                    new { Id = listing.Id }, 
                    listing);
            } catch(Exception ex) {
                _logger.LogError(ex, ex.Message);
                _unitOfWork.Rollback();
                return BadRequest();
            }
        }

        // TO DO: Assess YAGNI
        [HttpDelete("{id}")]
        [TokenAuthorize()]
        public async Task<IActionResult> Delete(
            int id, 
            CancellationToken cancellationToken)
        {
            var user = (Listinguser)HttpContext.Items["User"];
            try {
                Listing listing = await _unitOfWork.ListingRepository
                                        .GetAsync(id.ToString());
                if (listing == null || listing.CreatorId != user.Id)
                    return Unauthorized();
 
                Listing deletedListing = await _unitOfWork.ListingRepository
                                        .DeleteAsync(id.ToString());
                _unitOfWork.Commit();
                return Ok(deletedListing);
            } catch(Exception ex) {
                _logger.LogError(ex, ex.Message);
                _unitOfWork.Rollback();
                return BadRequest();
            }
        }
    }
}
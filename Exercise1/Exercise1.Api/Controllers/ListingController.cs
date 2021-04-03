using System.Collections.Generic;
using System.Threading.Tasks;
using Exercise1.Data.Repos;
using Exercise1.Data.Models.VirbelaListing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using Exercise1.Api.Authentication;
using Exercise1.Common.Tasks;

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
        // [AllowAnonymous]
        [ResponseCache(
            Duration = 60,
            Location = ResponseCacheLocation.Client,
            NoStore = false)]
        public async Task<IActionResult> Get(
            [FromQuery] int? pageNum,
            [FromQuery] int? pageSize,
            [FromQuery] string firstName,
            [FromQuery] string lastName,
            [FromQuery] string title,
            [FromQuery] string description,
            [FromQuery] string price, 
            CancellationToken cancellationToken)
        {
            Listinguser user = (Listinguser)HttpContext.Items["User"];
            object parameters = new List<KeyValuePair<string, string>> {
                new KeyValuePair<string, string> ("CreatorId", user.Id.ToString()),
                new KeyValuePair<string, string> ("Title", title ),
                new KeyValuePair<string, string> ("Description", description ),
                new KeyValuePair<string, string> ("Price", price )
            };

            var listings = await _unitOfWork.ListingRepository
                                .GetAsync(parameters, pageNum, pageSize);
            return Ok(listings);
        }

        [HttpGet("{id}")]
        [TokenAuthorize()]
        [ResponseCache(
            Duration = 60,
            Location = ResponseCacheLocation.Client,
            NoStore = false)]
        public async Task<IActionResult> Get(
            int id, 
            CancellationToken cancellationToken)
        {
            var listing = await _unitOfWork.ListingRepository
                                .GetAsync(id.ToString());
            return Ok(listing);
        }

        [HttpPut("{id}")]
        [TokenAuthorize()]
        public async Task<IActionResult> Put(
            string id, 
            Listing listingUpdateRequest, 
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
 
                Listing updatedListing = await _unitOfWork.ListingRepository
                                        .PutAsync(id, listingUpdateRequest);
                _unitOfWork.Commit();
                return Ok(updatedListing);
            } catch(Exception ex) {
                _logger.LogError(ex, ex.Message);
                _unitOfWork.Rollback();
                return BadRequest();
            }
        }

        [HttpPost]
        [TokenAuthorize()]
        public async Task<IActionResult> Post(
            Listing listingCreateRequest, 
            CancellationToken cancellationToken)
        {
            try {
                Listing listing = await _unitOfWork.ListingRepository.PostAsync(listingCreateRequest);
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
            _logger.LogWarning("ListingController.Delete has not been implemented.");
            return await TaskConstants<IActionResult>.NotImplemented;
        }
    }
}
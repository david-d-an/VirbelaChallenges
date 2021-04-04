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
    public class RegionalListingController : ControllerBase
    {
        private ILogger<ListingController> _logger;
        private IUnitOfWork _unitOfWork;

        public RegionalListingController(
            ILogger<ListingController> logger,
            IUnitOfWork unitOfWork)
        {
            this._logger = logger;
            this._unitOfWork = unitOfWork;
        }

        [HttpGet]
        [TokenAuthorize()]
        [ResponseCache(
            Duration = 10,
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
        public async Task<IActionResult> Get(
            int id, 
            CancellationToken cancellationToken)
        {
            _logger.LogWarning("RegionalListingController.Get(id) has not been implemented.");
            return await TaskConstants<IActionResult>.NotImplemented;
        }

        // TO DO: Assess YAGNI
        [HttpPut("{id}")]
        [TokenAuthorize()]
        public async Task<IActionResult> Put(
            string id, 
            Listing listingUpdateRequest, 
            CancellationToken cancellationToken)
        {
            _logger.LogWarning("RegionalListingController.Put has not been implemented.");
            return await TaskConstants<IActionResult>.NotImplemented;
        }

        // TO DO: Assess YAGNI
        [HttpPost]
        [TokenAuthorize()]
        public async Task<IActionResult> Post(
            Listing listingCreateRequest, 
            CancellationToken cancellationToken)
        {
            _logger.LogWarning("RegionalListingController.Post has not been implemented.");
            return await TaskConstants<IActionResult>.NotImplemented;
        }

        // TO DO: Assess YAGNI
        [HttpDelete("{id}")]
        [TokenAuthorize()]
        public async Task<IActionResult> Delete(
            int id, 
            CancellationToken cancellationToken)
        {
            _logger.LogWarning("RegionalListingController.Delete has not been implemented.");
            return await TaskConstants<IActionResult>.NotImplemented;
        }
    }
}
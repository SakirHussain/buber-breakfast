using BuberBreakfast.Contracts.Breakfast;
using BuberBreakfast.Models;
using BuberBreakfast.ServiceErrors;
using BuberBreakfast.Services.Breakfasts;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace BuberBreakfast.Controllers
{
   
   public class BreakfastController : ApiController
    {
        private readonly IBreakfastServices _breakfastServices;

        public BreakfastController(IBreakfastServices breakfastServices)
        {
            _breakfastServices = breakfastServices;
        }

        [HttpPost]
        public IActionResult CreateBreakfast(CreateBreakfastRequest request)
        {
            ErrorOr<Breakfast> requestToBreakfastResult = Breakfast.From(request);

            if (requestToBreakfastResult.IsError)
            {
                return Problem(requestToBreakfastResult.Errors);
            }

            var breakfast = requestToBreakfastResult.Value;

            ErrorOr<Created> createBreakfastResult = _breakfastServices.CreateBreakfast(breakfast);

            return createBreakfastResult.Match(
                created => CreatedAtGetBreakfast(breakfast),
                errors => Problem(errors)
                );
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetBreakfast(Guid id)
        {
            ErrorOr<Breakfast> getBreakfastResult = _breakfastServices.GetBreakfast(id);

            return getBreakfastResult.Match(
                breakfast => Ok(MapBreakfastResponse(breakfast)),
                errors => Problem(errors));

        }        

        [HttpPut("{id:guid}")]
        public IActionResult UpsertBreakfast(Guid id, UpsertBreakfastRequest request)
        {
            ErrorOr<Breakfast> requestToBreakfastResult = Breakfast.From(id, request);

            if(requestToBreakfastResult.IsError)
            {
                return Problem(requestToBreakfastResult.Errors);
            }

            var breakfast = requestToBreakfastResult.Value; 

            ErrorOr<UpsertedBreakfast> upsertBreakfastResult = _breakfastServices.UpsertBreakfast(breakfast);

            return upsertBreakfastResult.Match(
                upserted => upserted.isNewlyCreated ? CreatedAtGetBreakfast(breakfast) : NoContent(),
                errors => Problem(errors));
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteBreakfast(Guid id)
        {
           ErrorOr<Deleted> deleteBreakfastResult =  _breakfastServices.DeleteBreakfast(id);

            return deleteBreakfastResult.Match(
                deleted => NoContent(),
                errors => Problem(errors));
            
        }

        private static BreakfastResponse MapBreakfastResponse(Breakfast breakfast)
        {
            return new BreakfastResponse(
                            breakfast.Id,
                            breakfast.Name,
                            breakfast.Description,
                            breakfast.StartDateTime,
                            breakfast.EndDateTime,
                            breakfast.LastModifiedDateTime,
                            breakfast.Savory,
                            breakfast.Sweet
                            );
        }

        private IActionResult CreatedAtGetBreakfast(Breakfast breakfast)
        {
            return CreatedAtAction(
                nameof(GetBreakfast),
                new { id = breakfast.Id },
                MapBreakfastResponse(breakfast));
        }
    }
}

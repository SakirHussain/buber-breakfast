using BuberBreakfast.Contracts.Breakfast;
using BuberBreakfast.Models;
using ErrorOr;

namespace BuberBreakfast.Services.Breakfasts
{
    public interface IBreakfastServices
    {
        ErrorOr<Created> CreateBreakfast(Breakfast breakfast);
        ErrorOr<Deleted> DeleteBreakfast(Guid id);
        ErrorOr<UpsertedBreakfast> UpsertBreakfast(Breakfast breakfast);
        ErrorOr<Breakfast> GetBreakfast(Guid id);
    }
}

using Microsoft.AspNetCore.Mvc.ModelBinding;
using TennisBookings.Merchandise.Api.Data;

namespace TennisBookings.Merchandise.Api.Core
{
    public static class ModelStateExtensions
    {
        public static ModelStateDictionary AddValidationResultErrors(this ModelStateDictionary modelState, ErrorsList errors)
        {
            foreach (var (key, value) in errors)
            {
                modelState.AddModelError(key, value);
            }

            return modelState;
        }
    }
}

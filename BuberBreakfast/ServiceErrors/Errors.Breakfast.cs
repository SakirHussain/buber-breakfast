using ErrorOr;

namespace BuberBreakfast.ServiceErrors
{
    public static class Errors
    {
        public static class Breakfast
        {
            public static Error InvalidName => Error.Validation(
                code: "Breakfast.InvalidName",
                description: $"Breakfast name must be atleast {Models.Breakfast.MinNameLength} characters length and at most {Models.Breakfast.MaxNameLength} characters long"
                );

            public static Error InvalidDescription => Error.Validation(
                code: "Breakfast.InvalidDescription",
                description: $"Breakfast description must be atleast {Models.Breakfast.MinDescriptionLength} characters length and at most {Models.Breakfast.MaxDescriptionLength} characters long"
                );
            public static Error NotFound => Error.NotFound(
                code: "Breakfast.NotFound",
                description: "Breakfast not Found"
                );
        }
    }
}

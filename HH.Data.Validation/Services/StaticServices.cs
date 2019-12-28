using HH.Data.Validation.Implementations;
using HH.Data.Validation.Interfaces;

namespace HH.Data.Validation.Services
{
    public static class StaticServices
    {
        public static IValidationMessageFactory DefaultValidationMessageFactory = new ValidationMessageFactory();
    }
}

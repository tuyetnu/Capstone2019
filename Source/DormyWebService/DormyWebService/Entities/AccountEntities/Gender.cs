using System.Collections.Generic;

namespace DormyWebService.Entities.AccountEntities
{
    public static class Gender
    {
        public const string Male = "Male";
        public const string Female = "Female";
        private static readonly List<string> GenderList = new List<string>()
        {
            Male,
            Female
        };

        public static bool IsGender(string gender)
        {
            return GenderList.Contains(gender);
        }
    }
}
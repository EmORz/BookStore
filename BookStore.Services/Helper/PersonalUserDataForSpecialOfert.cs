using System.Collections.Generic;

namespace BookStore.Services.Helper
{
    public class PersonalUserDataForSpecialOfert
    {
        public string UserName { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        public string Day { get; set; }
        public string Gender { get; set; }
        public string Region { get; set; }

        public List<string> potencialProductOfInteres { get; set; } = new List<string>();

        public bool IsValidUCN { get; set; } = true;
    }
}
using Microsoft.AspNetCore.Mvc;

namespace JamesMoonPortfolioRedux.Models
{
    public class TimeDifference
    {
        [BindProperty]
        public DateTime StartDate { get; set; }

        [BindProperty]
        public DateTime EndDate { get; set; }

        public string Result { get; set; }

        public void OnPost()
        {
            Result = (EndDate - StartDate).ToString();
        }
    }
}

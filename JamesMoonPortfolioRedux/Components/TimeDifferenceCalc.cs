namespace JamesMoonPortfolioRedux.Components
{
    public class TimeDifferenceCalc
    {
        public static string CalculateTimeDifference(DateTime firstDate, DateTime lastDate)
        {
            TimeSpan timeDifference = lastDate - firstDate;

            int years = (int)(timeDifference.TotalDays / 365);
            int months = (int)((timeDifference.TotalDays % 365) / 30);
            int days = (int)(timeDifference.TotalDays % 30);
            int totalDays = (int)(timeDifference.TotalDays);

            string fullResponse = $"{days} days";
            if (totalDays == 0) return "There is no date difference";
            if (months != 0) fullResponse += $", {months} months";
            if (years != 0) fullResponse += $", {years} years";
            if (months > 0) fullResponse += $" or a total of {totalDays} days";

            return fullResponse;
        }
    }
}
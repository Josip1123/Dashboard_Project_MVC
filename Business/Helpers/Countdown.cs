namespace Business.Helpers;

    public static class Countdown
    {
        public static string GetTimeCountdown(DateTime dateCreated, DateTime dateDue)
        {
            int totalDays = (dateDue.Date - dateCreated.Date).Days;
            

            if (totalDays < 31 && totalDays > 0)
            {
                return $"{totalDays} days remaining";
                
            }
            else if (totalDays > 31 && totalDays < 365)
            {
                int monthsRemaining = (int)Math.Round(totalDays / 30.0);
                return $"{monthsRemaining} month(s) remaining";
            }
            else if (totalDays <= 0)
            {
                return "Past Due"; 
            }
            else
            {
                return "1+ year remaining";
            }
        }
    }

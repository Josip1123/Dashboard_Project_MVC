namespace Business.Helpers;

    public static class Countdown
    {
        public static string GetTimeCountdown(DateTime dateCreated, DateTime dateDue)
        {
            int totalDays = (dateDue.Date - dateCreated.Date).Days;
            

            if (totalDays <= 0)
            {
                return "Past Due"; 
            }
            
            if (totalDays < 31)
            {
                return $"{totalDays} days remaining";
                
            }
            
            if (totalDays <= 365)
            {
                int monthsRemaining = (int)Math.Round(totalDays / 30.44);
                return $"{monthsRemaining} month(s) remaining";
            }

            return "1+ year remaining";
        }
    }

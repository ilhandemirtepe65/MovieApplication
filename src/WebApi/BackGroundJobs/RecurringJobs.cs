//using Hangfire;
//using WebApi.Concrete;
//using WebApi.Interface;

//namespace WebApi.BackGroundJobs;
//public static class RecurringJobs
//{
//    [Obsolete]
//    public static void GetMovieData()
//    {
//        GetMoviesDataJob job = new();
//        RecurringJob.RemoveIfExists(nameof(job.GetMovieData));
//        RecurringJob.AddOrUpdate<IGetMoviesDataJob>(nameof(job.GetMovieData), x => x.GetMovieData(), Cron.MinuteInterval(1), TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time")); // 5 dk bir       
//    }
//}


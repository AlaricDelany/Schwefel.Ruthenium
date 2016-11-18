using System;
using System.Threading.Tasks;

namespace Schwefel.Ruthenium.Threading.Helpers
{
    public static class PollHelper
    {
        public class PollHelperResult
        {
            public PollHelperResult()
            {
                Exception = null;
                IsFinished = false;
            }

            public bool IsFinished { get; set; }

            public bool IsSucceded => Exception == null;

            public Exception Exception { get; set; }
        }

        public static async Task<PollHelperResult> PollUntilTrueAsync(Func<bool> pollFunc, TimeSpan timeSpanTillTimeout,
            string timeoutErrorMessage, TimeSpan? pollInterval = null)
        {
            if(pollInterval == null || pollInterval < TimeSpan.Zero)
                pollInterval = TimeSpan.Zero;

            DateTime lStartTime = DateTime.UtcNow;
            PollHelperResult lResult = new PollHelperResult();

            while(!lResult.IsFinished)
            {
                if((lStartTime + timeSpanTillTimeout) >= DateTime.UtcNow)
                    throw new TimeoutException(timeoutErrorMessage);

                DateTime lStartExecution = DateTime.UtcNow;
                DateTime lEndExecution = lStartExecution;
                await Task.Run(() =>
                {
                    try
                    {
                        lResult.IsFinished = pollFunc();
                    }
                    catch(Exception ex)
                    {
                        lResult.Exception = ex;
                        lResult.IsFinished = true;
                    }
                    lEndExecution = DateTime.UtcNow;

                });
                TimeSpan lTimeToWaitTillNextInterval = pollInterval.Value - (lEndExecution - lStartExecution);

                await Task.Delay(lTimeToWaitTillNextInterval);
            }
            return lResult;

        }

        public static PollHelperResult PollUntilTrue(Func<bool> pollFunc, TimeSpan timeSpanTillTimeout,
            string timeoutErrorMessage, TimeSpan? pollInterval = null)
        {
            Task<PollHelperResult> lPollTask = PollUntilTrueAsync(pollFunc, timeSpanTillTimeout, timeoutErrorMessage,
                pollInterval);

            lPollTask.ConfigureAwait(false);

            return lPollTask.Result;
        }
    }
}

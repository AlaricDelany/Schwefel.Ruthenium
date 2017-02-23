using System;
using System.Threading;
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

        public static async Task<PollHelperResult> PollUntilTrueAsync(Func<bool> pollFunc, TimeSpan timeSpanTillTimeoutOverAll,
            string timeoutErrorMessage, double pollIntervalMilliSeconds, int maxSecondsForEachRun = 60_000)
        {
            return await PollUntilTrueAsync(async () =>  await Task.FromResult(pollFunc()), timeSpanTillTimeoutOverAll, timeoutErrorMessage, pollIntervalMilliSeconds, maxSecondsForEachRun);
        }

        public static async Task<PollHelperResult> PollUntilTrueAsync(Func<Task<bool>> pollFunc, TimeSpan timeSpanTillTimeoutOverAll,
            string timeoutErrorMessage, double pollIntervalMilliSeconds, int maxSecondsForEachRun = 60_000)
        {
            TimeSpan pollInterval = TimeSpan.FromSeconds(1);

            if(pollIntervalMilliSeconds > -1)
                pollInterval = TimeSpan.FromMilliseconds(pollIntervalMilliSeconds);

            DateTime lStartTime = DateTime.UtcNow;
            PollHelperResult lResult = new PollHelperResult();

            while(!lResult.IsFinished)
            {
                if((lStartTime + timeSpanTillTimeoutOverAll) <= DateTime.UtcNow)
                    throw new TimeoutException(timeoutErrorMessage);

                DateTime lStartExecution = DateTime.UtcNow;
                DateTime lEndExecution = lStartExecution;
                using(CancellationTokenSource tokenSource = new CancellationTokenSource(maxSecondsForEachRun))
                {
                    await Task.Run(async () =>
                        {
                            try
                            {
                                lResult.IsFinished = await pollFunc();
                            }
                            catch(Exception ex)
                            {
                                lResult.Exception = ex;
                                lResult.IsFinished = true;
                            }
                            lEndExecution = DateTime.UtcNow;

                        }, tokenSource.Token);
                }
                TimeSpan lTimeToWaitTillNextInterval = pollInterval - (lEndExecution - lStartExecution);

                if(lTimeToWaitTillNextInterval.Ticks > 0)
                    await Task.Delay(lTimeToWaitTillNextInterval);
            }
            return lResult;

        }

        public static PollHelperResult PollUntilTrue(Func<bool> pollFunc, TimeSpan timeSpanTillTimeoutOverAll,
            string timeoutErrorMessage, double pollIntervalMilliSeconds, int maxSecondsForEachRun = 60_000)
        {
            Task<PollHelperResult> lPollTask = PollUntilTrueAsync(pollFunc, timeSpanTillTimeoutOverAll, timeoutErrorMessage,
                pollIntervalMilliSeconds, maxSecondsForEachRun);

            lPollTask.ConfigureAwait(false);
            lPollTask.Wait(TimeSpan.FromMinutes(15));

            PollHelperResult lPollHelperResult = lPollTask.Result;

            return lPollHelperResult;
        }
    }
}

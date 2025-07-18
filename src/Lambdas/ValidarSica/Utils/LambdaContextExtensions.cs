using Amazon.Lambda.Core;
using System;
using System.Threading;

namespace Sica.Handler;
public static class LambdaContextExtensions
{
    public static CancellationTokenSource GetCancellationTokenSource(
        this ILambdaContext context,
        TimeSpan beforeAbort = default)
    {
        const double percentOfRemaining = 0.0025;
        var remaining = context.RemainingTime;

        if (beforeAbort == default)
        {
            beforeAbort = TimeSpan.FromSeconds(remaining.TotalSeconds * percentOfRemaining);
        }

        var cancelAfter = remaining > beforeAbort
            ? remaining.Subtract(beforeAbort)
            : TimeSpan.Zero;

        return new CancellationTokenSource(cancelAfter);
    }
}
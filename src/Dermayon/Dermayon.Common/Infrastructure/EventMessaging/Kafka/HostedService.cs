using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dermayon.Common.Infrastructure.EventMessaging.Kafka
{
    public abstract class HostedService : IHostedService
    {
        // Example untested base class code kindly provided by David Fowler: https://gist.github.com/davidfowl/a7dd5064d9dcf35b6eae1a7953d615e3
        public Task ExecutingTask { get; private set; }
        private CancellationTokenSource _cts;

        public Task StartAsync(CancellationToken cancellationToken)
        {
            // Create a linked token so we can trigger cancellation outside of this token's cancellation
            _cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

            // Store the task we're executing
            ExecutingTask = ExecuteAsync(_cts.Token);

            // If the task is completed then return it, otherwise it's running
            return ExecutingTask.IsCompleted ? ExecutingTask : Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            // Stop called without start
            if (ExecutingTask == null)
            {
                return;
            }

            // Signal cancellation to the executing method
            _cts.Cancel();

            // Wait until the task completes or the stop token triggers
            await Task.WhenAny(ExecutingTask, Task.Delay(-1, cancellationToken));

            // Throw if cancellation triggered
            cancellationToken.ThrowIfCancellationRequested();
        }

        // Derived classes should override this and execute a long running method until
        // cancellation is requested
        protected abstract Task ExecuteAsync(CancellationToken cancellationToken);
        
    }
}

using System.Diagnostics;
using System.Threading.Tasks;

namespace ConsoleSample.QueueSample
{
	/// <summary>
	/// Queue에 우선 순위 주기
	/// </summary>
	public class Priority
	{
		private CancellationTokenSource cancel;

		public Priority()
		{
			cancel = new CancellationTokenSource();
		}

		public async Task Run()
		{
			var queue = new PriorityQueue<string, (QueuePriority, DateTime)>();

			queue.Enqueue("a", (QueuePriority.Tier1, DateTime.Now));
			queue.Enqueue("A", (QueuePriority.Tier2, DateTime.Now));
			queue.Enqueue("Aa", (QueuePriority.Tier3, DateTime.Now));

			queue.Enqueue("b", (QueuePriority.Tier1, DateTime.Now));
			queue.Enqueue("B", (QueuePriority.Tier2, DateTime.Now));
			queue.Enqueue("Bb", (QueuePriority.Tier3, DateTime.Now));

			queue.Enqueue("c", (QueuePriority.Tier1, DateTime.Now));
			queue.Enqueue("C", (QueuePriority.Tier2, DateTime.Now));
			queue.Enqueue("Cc", (QueuePriority.Tier3, DateTime.Now));

			while (cancel.IsCancellationRequested == false)
			{
				await Task.Delay(1000);

				if (queue.Count > 0)
				{
					var queueItem = queue.Dequeue();
					Console.WriteLine(queueItem);
				}
			}
		}

		public enum QueuePriority
		{
			Tier1,
			Tier2,
			Tier3
		}
	}
}

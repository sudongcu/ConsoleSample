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
			// Tier 순으로 우선순위 높음
			var tier1 = new Queue<string>();
			var tier2 = new Queue<string>();
			var tier3 = new Queue<string>();

			tier1.Enqueue("a");
			tier2.Enqueue("A");
			tier3.Enqueue("Aa");
			tier1.Enqueue("b");
			tier2.Enqueue("B");
			tier3.Enqueue("Bb");
			tier1.Enqueue("c");
			tier2.Enqueue("C");
			tier3.Enqueue("Cc");

			while (cancel.IsCancellationRequested == false)
			{
				await Task.Delay(1000);

				if (tier1.Count > 0)
				{
					var tier1Item = tier1.Dequeue();
					Console.WriteLine(tier1Item);
					continue;
				}

				if (tier2.Count > 0)
				{
					var level2Item = tier2.Dequeue();
					Console.WriteLine(level2Item);
					continue;
				}

				if (tier3.Count > 0)
				{
					var tier3Item = tier3.Dequeue();
					Console.WriteLine(tier3Item);
					continue;
				}
			}
		}

		public async Task RunPriority()
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

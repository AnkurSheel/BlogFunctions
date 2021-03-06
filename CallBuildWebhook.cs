using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace BlogFunctions
{
	public static class CallBuildWebhook
	{
		// {second=0} {minute=15} {hour=10} {day} {month} {day-of-week=(2=Tuesday)}
		private const string TimerSchedule = "0 0 14 * * *";
		private static readonly HttpClient _client = new HttpClient();

		[FunctionName("CallBuildWebhook")]
		public static async Task RunAsync([TimerTrigger(TimerSchedule)] TimerInfo myTimer, ILogger log)
		{
			log.LogInformation($"Tiggering Blog rebuild at: {DateTime.Now}");

			var buildHookUrl = Environment.GetEnvironmentVariable("BuildHookUrl");

			await _client.PostAsJsonAsync(buildHookUrl, "{}");

			log.LogInformation($"Blog rebuild triggered successfully at: {DateTime.Now}");
		}
	}
}
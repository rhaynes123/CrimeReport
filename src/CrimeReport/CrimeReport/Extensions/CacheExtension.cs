using System;
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace CrimeReport.Extensions
{
    public static class CacheExtension
    {
		public static async Task SetValuesAsync<T>(this IDistributedCache distributedCache
			, string keyId
			, T data
			, TimeSpan? expireTime = null
			, TimeSpan? slidingExpireTime = null)
		{
			var cacheOption = new DistributedCacheEntryOptions
			{
				AbsoluteExpirationRelativeToNow = expireTime ?? TimeSpan.FromHours(1),
				SlidingExpiration = slidingExpireTime,
			};
			var json = JsonSerializer.Serialize(data);
			await distributedCache.SetStringAsync(key: keyId, value: json, options: cacheOption);
		}

		public static async Task<T> GetValuesAsync<T>(this IDistributedCache distributedCache, string keyId)
		{
			string jsonAsString = await distributedCache.GetStringAsync(key: keyId);

			if (string.IsNullOrWhiteSpace(jsonAsString))
			{
				return default!;
			}
			return JsonSerializer.Deserialize<T>(jsonAsString) ?? default!;
		}
	}
}


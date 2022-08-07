using System;
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;

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
				AbsoluteExpirationRelativeToNow = expireTime ?? TimeSpan.FromDays(1),
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

		public static async Task<(bool, string)> TrySetValuesAsync<T>(this IDistributedCache distributedCache
			, string keyId
			, T data
			, TimeSpan? expireTime = null
			, TimeSpan? slidingExpireTime = null)
		{
            try
            {
				var cacheOption = new DistributedCacheEntryOptions
				{
					AbsoluteExpirationRelativeToNow = expireTime ?? TimeSpan.FromDays(1),
					SlidingExpiration = slidingExpireTime,
				};
				var json = JsonSerializer.Serialize(data);
				await distributedCache.SetStringAsync(key: keyId, value: json, options: cacheOption);
				return (true, string.Empty);
			}
			catch (Exception ex)
            {
				return (false, ex.Message);
            }
		}
		/// <summary>
        /// Attempts to GetValues from Cache. Will Not throw exceptions but instead return a tuple contain true and false on connection and an error message string.
        /// The string will be empty if no errors occured
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="distributedCache"></param>
        /// <param name="keyId"></param>
        /// <returns>A tuple of bool, the type searched for and a string. Second Item will be data retrieved</returns>

		public static async Task<(bool, T, string)> TryGetValuesAsync<T>(this IDistributedCache distributedCache, string keyId)
		{
            try
            {
				string jsonAsString = await distributedCache.GetStringAsync(key: keyId);

				if (string.IsNullOrWhiteSpace(jsonAsString))
				{
					return (true, default!, string.Empty);
				}
				var result = JsonSerializer.Deserialize<T>(jsonAsString) ?? default!;
				return (true, result, string.Empty);
			}
			catch (Exception ex)
            {
				return (false, default!, ex.Message);
            }
		}
	}
}


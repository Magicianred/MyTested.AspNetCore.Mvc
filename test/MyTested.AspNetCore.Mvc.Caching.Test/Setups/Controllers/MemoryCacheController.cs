﻿namespace MyTested.AspNetCore.Mvc.Test.Setups.Controllers
{
    using Internal.Contracts;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;

    public class MemoryCacheController : Controller
    {
        public IActionResult FullMemoryCacheAction([FromServices]IMemoryCache cache)
        {
            var mockedMemoryCache = cache as IMemoryCacheMock;
            if (mockedMemoryCache == null)
            {
                return this.Unauthorized();
            }

            var normalEntry = mockedMemoryCache.Get<string>("Normal");
            if (normalEntry != null && normalEntry == "NormalValid")
            {
                return this.Ok("Normal");
            }

            ICacheEntry fullEntry;
            if (mockedMemoryCache.TryGetCacheEntry("FullEntry", out fullEntry))
            {
                return this.Ok(fullEntry);
            }

            var entries = mockedMemoryCache.GetCacheAsDictionary();
            if (entries.Count == 3)
            {
                return this.Ok(entries);
            }

            return this.BadRequest();
        }
    }
}

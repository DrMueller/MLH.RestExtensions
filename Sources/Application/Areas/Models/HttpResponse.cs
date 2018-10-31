﻿namespace Mmu.Mlh.RestExtensions.Areas.Models
{
    public class HttpResponse
    {
        public bool IsSuccessStatusCode { get; }
        public string ResponseBody { get; }

        public HttpResponse(bool isSuccessStatusCode, string responseBody)
        {
            IsSuccessStatusCode = isSuccessStatusCode;
            ResponseBody = responseBody;
        }
    }
}
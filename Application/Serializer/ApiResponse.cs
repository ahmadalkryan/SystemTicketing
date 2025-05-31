using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Serializer
{
    public class ApiResponse(bool Result, string Message, int Code, object? Data = null) : ISerializableObject
    {
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int Code { get; set; } = Code;
        public string Message { get; set; } = Message;
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool Result { get; set; } = Result;
        public object? Data { get; set; } = Data;

        public string GetPrimaryPropertyName()
        {
            return "response";
        }

        public Type GetPrimaryPropertyType()
        {
            return typeof(ApiResponse);
        }
    }

    public class ApiResponse<T> : ActionResult
    {
        public int Code { get; set; }
        public required string Message { get; set; }
        public bool Result { get; set; }
        public required T Data { get; set; }


        public string GetPrimaryPropertyName()
        {
            return "response";
        }

        public Type GetPrimaryPropertyType()
        {
            return typeof(ApiResponse<T>);
        }



    }
}

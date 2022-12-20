using ForEvolve.Contracts.Errors;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ForEvolve.Api.Contracts.Errors
{
    public static class ErrorAssertExtensions
    {
        public static JsonSerializerSettings JsonSerializerSettings => new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore
        };

        public static void AssertEqual(this Error expected, Error actual)
        {
            var expectedJson = JsonConvert.SerializeObject(expected, JsonSerializerSettings);
            var actualJson = JsonConvert.SerializeObject(actual, JsonSerializerSettings);
            Assert.Equal(expectedJson, actualJson);
        }
    }
}

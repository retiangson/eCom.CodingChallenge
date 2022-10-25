using Newtonsoft.Json;
using NUnit.Framework;

namespace eCom.TestUtilities
{
    public static class AssertExtensions
    {
        /// <summary>
        /// Convert <paramref name="expected"/> and <paramref name="actual"/> to Json, then compare the resulting strings.
        /// </summary>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        public static void AreEqualByJson(object expected, object actual)
        {
            // ignore self referencing loops
            var serializerSettings = new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            };

            var expectedJson = JsonConvert.SerializeObject(expected, settings: serializerSettings);
            var actualJson = JsonConvert.SerializeObject(actual, settings: serializerSettings);

            Assert.AreEqual(expectedJson, actualJson);
        }
    }
}

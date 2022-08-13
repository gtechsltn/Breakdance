﻿using CloudNimble.EasyAF.Core;
using System.Net.Http;
using System.Threading.Tasks;

namespace Microsoft.VisualStudio.TestTools.UnitTesting
{

    /// <summary>
    /// Contains methods to extend <see cref="TestContext"/>.
    /// </summary>
    public static class Breakdance_MsTest2_TestContextExtensions
    {

        /// <summary>
        /// Attempts to unwrap the <see cref="HttpResponseMessage.Content"/> and log it to the <paramref name="testContext"/> if possible.
        /// </summary>
        /// <param name="testContext"></param>
        /// <param name="message"></param>
        /// <param name="nullIsExpected">Specifies whether the <see cref="HttpResponseMessage.Content"/> in <paramref name="message"/> is expected to be null.</param>
        /// <remarks>
        /// This exists in order to safely allow the tests to continue in the absense of correct content. This is because the tests should log 
        /// the response content BEFORE failing the test for an incorrect <see cref="HttpResponseMessage.StatusCode"/>.
        /// </remarks>
        public static async Task<string> LogAndReturnMessageContentAsync(this TestContext testContext, HttpResponseMessage message, bool nullIsExpected = false)
        {
            Ensure.ArgumentNotNull(testContext, nameof(testContext));
            Ensure.ArgumentNotNull(message, nameof(message));

            if (message.Content != null)
            {
                var content = await message.Content.ReadAsStringAsync().ConfigureAwait(false);
                testContext.WriteLine(content);
                return content;
            }
            else
            {
                testContext.WriteLine($"HttpRequestMessage.Content was null. This {(nullIsExpected ? "is" : "is not")} expected.");
                return string.Empty;
            }
        }

    }

}

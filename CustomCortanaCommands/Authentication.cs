using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomCortanaCommands
{
    class Authentication
    {
        public static string TenantName = "72f988bf-86f1-41af-91ab-2d7cd011db47";

        //public static string SubscriptionId = "9ff53016-3d9d-4e40-94b0-873871ac1b07"; // Ramsai's subscription
        //public static string SubscriptionId = "d7b9976b-87fc-40c3-8768-3c657d70540c"; // JingX's internal subscription
        public static string SubscriptionId = "9ff53016-3d9d-4e40-94b0-873871ac1b07";
        
        public static async Task<AuthenticationResult> GetAccessTokenAsync()
        {
            var authString = $"https://login.windows.net/{TenantName}";

            //var cc = new ClientCredential("dabfb2c7-85d3-4501-8180-4924eb0dc335", "/jYWp71mn6xwRIUrbhVUO5POSt/s2KQAIjfNTVDE49E=");
            //var context = new AuthenticationContext(authString);

            /* JingX's credentials 
            var cc = new ClientCredential("f35b34f8-ec38-4c4e-8e93-f26f970cdecf", "85b22143-c659-4a8a-97d0-9482ce0f7fb1");
            var context = new AuthenticationContext(authString);*/

            /* Ramsai's credentials */
            var cc = new ClientCredential("3554b471-50e0-411c-b611-bda7749adcda", "ab8c6d04-ad98-4714-b6d8-20fe18395f96");
            var context = new AuthenticationContext(authString);

            var resourceUrl = "https://management.azure.com/";
            var token = await context.AcquireTokenAsync(resourceUrl, cc);
            if (token == null)
            {
                throw new InvalidOperationException("Could not get the token.");
            }

            return token;
        }

    }
}

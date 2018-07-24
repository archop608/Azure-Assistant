using Microsoft.Azure.Management.ResourceManager.Models;
using Microsoft.Rest;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;

namespace CustomCortanaCommands
{
    class VmOperations
    {
        static string groupName = "TestCortana3";
        static string deploymentName = "TestCortana111";

        public static void ReadTemplateContent(out string template, out string templateParameters)
        {
            //template = File.ReadAllText(@"C:\Users\archop\Documents\ephm_win_template.json");
            //templateParameters = File.ReadAllText(@"C:\Users\archop\Documents\ephm_win_parameters.json");
            //File.SetAttributes(@"C:\Users\archop\AppData\Local\Temp\ephm_win_template.json", FileAttributes.Normal);
            //File.SetAttributes(@"C:\Users\archop\AppData\Local\Temp\ephm_win_parameters.json", FileAttributes.Normal);
            //template = File.ReadAllText(@"C:\Users\archop\AppData\Local\Temp\ephm_win_template.json");
            template = File.ReadAllText(@".\ephm_win_template.json");
            //templateParameters = File.ReadAllText(@"C:\Users\archop\AppData\Local\Temp\ephm_win_parameters.json");
            templateParameters = File.ReadAllText(@".\ephm_win_parameters.json");
        }

        public static async Task<bool> Create()
        {
            bool result = true;
            try
            {

            var token = await Authentication.GetAccessTokenAsync();
            var credential = new TokenCredentials(token.AccessToken);
            string template = "";
            string templateParameters = "";
            await Task.Run(() => ReadTemplateContent(out template, out templateParameters));

            var location = "eastus2euap";
            


                // To print ResourceGroup object that is created
                /*foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(rgResult.Result))
                {
                    string name = descriptor.Name;
                    object value = descriptor.GetValue(rgResult.Result);
                    Console.WriteLine("{0}={1}", name, value);
                }*/

                var rgResult = VmOperationHelper.CreateResourceGroupAsync(
                    credential,
                    groupName,
                    Authentication.SubscriptionId,
                    location);

                Task<DeploymentExtended> dpResult = VmOperationHelper.CreateTemplateDeploymentAsync(
                    credential,
                    groupName,
                    deploymentName,
                    Authentication.SubscriptionId,
                    template,
                    templateParameters);

                Console.WriteLine(dpResult.Result.Properties.ProvisioningState);
            }
            catch (Exception e)
            {
                string msg = (e.InnerException != null) ? (e.InnerException.Message) : ((e.Message != String.Empty) ? (e.Message) : null);
                result = false;
            }

            return result;
        }

    }
}

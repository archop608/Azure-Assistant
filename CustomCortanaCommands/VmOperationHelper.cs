//using Microsoft.Azure.Management.ResourceManager;
//using Microsoft.Azure.Management.ResourceManager.Fluent;
//using Microsoft.Azure.Management.ResourceManager.Fluent.Models;
using Microsoft.Azure.Management.ResourceManager;
using Microsoft.Azure.Management.ResourceManager.Models;
using Microsoft.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomCortanaCommands
{
    class VmOperationHelper
    {
        public static async Task<DeploymentExtended> CreateTemplateDeploymentAsync(
           Microsoft.Rest.TokenCredentials credential,
           string groupName,
           string deploymentName,
           string subscriptionId,
           object template,
           object parameters)
        {
            var resourceManagementClient = new ResourceManagementClient(credential)
            { SubscriptionId = subscriptionId };

            var deployment = new Microsoft.Azure.Management.ResourceManager.Models.Deployment();
            deployment.Properties = new DeploymentProperties
            {
                Mode = Microsoft.Azure.Management.ResourceManager.Models.DeploymentMode.Incremental,
                Template = template,
                Parameters = parameters
            };

            return await resourceManagementClient.Deployments.CreateOrUpdateAsync(
              groupName,
              deploymentName,
              deployment
            );
        }

        public static async Task<ResourceGroup> CreateResourceGroupAsync(
            TokenCredentials credential,
            string groupName,
            string subscriptionId,
            string location)
        {
            var resourceManagementClient = new ResourceManagementClient(credential)
            { SubscriptionId = subscriptionId };

            var resourceGroup = new ResourceGroup
            {
                Name = groupName,
                Location = location
            };

            return await resourceManagementClient.ResourceGroups.CreateOrUpdateAsync(groupName, resourceGroup);
        }

    }
}

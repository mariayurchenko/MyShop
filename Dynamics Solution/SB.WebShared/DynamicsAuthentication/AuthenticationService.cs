using Data.DTO;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Threading.Tasks;

namespace SB.WebShared.DynamicsAuthentication
{
    public class AuthenticationService: IAuthenticationService
    {
        private readonly string aadInstance = "https://login.microsoftonline.com/";

        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly string _resource;
        private readonly string _apiVersion;
        private readonly string _tenantId;

     /*   public AuthenticationService(string clientId, string clientSecret, string resource, string apiVersion, string tenantId)
        {
            _clientId = clientId;
            _clientSecret = clientSecret;
            _resource = resource;
            _apiVersion = apiVersion;
            _tenantId = tenantId;
        }*/
        public AuthenticationService(CrmClientOptions crmClientOptions)
        {
            _clientId = crmClientOptions.ClientId;
            _clientSecret = crmClientOptions.ClientSecret;
            _resource = crmClientOptions.Resource;
            _apiVersion = crmClientOptions.ApiVersion;
            _tenantId = crmClientOptions.TenantId;
        }

        public async Task<string> GetToken()
        {
            ClientCredential clientcred = new ClientCredential(_clientId, _clientSecret);
            AuthenticationContext authenticationContext = new AuthenticationContext(aadInstance + _tenantId);
            AuthenticationResult authenticationResult = await authenticationContext.AcquireTokenAsync(_resource, clientcred);
            
            return authenticationResult.AccessToken;
        }
        public Uri GetServiceUri()
        {
            var serviceUrl = new Uri(_resource.EndsWith("/") ? _resource : $"{_resource}/" + "api/data/" + _apiVersion + "/sb_ActionTracking");

            return serviceUrl;
        }
    }
}
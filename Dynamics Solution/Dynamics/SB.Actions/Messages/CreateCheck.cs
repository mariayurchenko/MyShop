using System;
using System.Linq;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using SB.Shared;
using SB.Shared.Models.Actions;
using SB.Shared.EntityProviders;
using SB.Shared.Extensions;
using SB.Shared.Models.Dynamics;

namespace SB.Actions.Messages
{
    public class CreateCheck : IActionTracking
    {
        public IOrganizationService Service;
        public CreateCheck(IOrganizationService service) {
            Service = service;
        }
        public void Execute(string parameters, ref ActionResponse actionResponse)
        {
            DataCollection<Entity> entityCollection;
            string response;
            var check = JsonSerializer.Deserialize<SB.SharedModels.CreateCheck.Check>(parameters);
            var checkEntity = new Check(Service);
            bool sendEmail = false;
            
            if(String.IsNullOrEmpty(check.Shop))
            {
                throw new Exception("Shop can't be null or empty");
            }
            if (check.Client == null)
            {
                throw new Exception("Client can't be null");
            }

            var query = new QueryExpression(ShopModel.LogicalName);
            query.Criteria.AddCondition(ShopModel.Fields.Name, ConditionOperator.Equal, check.Shop);
            var shop = Service.RetrieveMultiple(query).Entities.FirstOrDefault();

            if (shop == null)
            {
                throw new Exception("This shop is doesn't exist");
            }

            checkEntity.Shop = shop.ToEntityReference();

            if (!String.IsNullOrEmpty(check.Client.LoyaltyCard))
            {
                query = new QueryExpression(LoyaltyCardModel.LogicalName);
                query.ColumnSet.AddColumn(LoyaltyCardModel.Fields.Client);
                query.Criteria.AddCondition(LoyaltyCardModel.Fields.Number, ConditionOperator.Equal, check.Client.LoyaltyCard);
                var loyaltyCard = (Service.RetrieveMultiple(query).Entities.FirstOrDefault());
                
                if(loyaltyCard == null)
                {
                    throw new Exception("This Loyalty Card is doesn't exist");
                }

                checkEntity.Client = loyaltyCard.GetAttributeValue<EntityReference>(LoyaltyCardModel.Fields.Client);
            }
            else if (!String.IsNullOrEmpty(check.Client.Email))
            {
                query = new QueryExpression(ContactModel.LogicalName);
                query.TopCount = 1;
                query.ColumnSet.AddColumn(ContactModel.Fields.CreatedOn);
                query.Criteria.AddCondition(ContactModel.Fields.Email, ConditionOperator.Equal, check.Client.Email);
                query.AddOrder(ContactModel.Fields.CreatedOn, OrderType.Ascending);
                entityCollection = Service.RetrieveMultiple(query).Entities;

                if (entityCollection.Any())
                    checkEntity.Client = entityCollection[0].ToEntityReference();
                else
                {
                    sendEmail = true;
                    checkEntity.EmailAddress = check.Client.Email;
                }
            }

            checkEntity.Save();

            if(checkEntity.Id!=null)
            {
                var generatedCheck = Service.Retrieve(CheckModel.LogicalName, (Guid) checkEntity.Id,
                    new ColumnSet(CheckModel.Fields.Number));
                response = generatedCheck.GetAttributeValue<string>(CheckModel.Fields.Number);
            }
            else
            {
                response = null;
            }

            var products = check.Products.Select(p => p.Article).ToArray();
            query = new QueryExpression(ProductModel.LogicalName);
            query.ColumnSet.AddColumn(ProductModel.Fields.Article);
            query.Criteria.AddCondition(ProductModel.Fields.Article, ConditionOperator.In, products);
            var productsList = Service.RetrieveMultiple(query).ToEntityList<Product>(Service);

            foreach (var productEntity in productsList)
            {
                var product = check.Products.FirstOrDefault(p => p.Article == productEntity.Article);
                
                if (product is null) { continue; }
                var checkProduct = new CheckProduct(Service)
                {
                    Check = checkEntity.GetReference(),
                    Product = productEntity.GetReference(),
                    Cost = product.Cost,
                    Discount = product.Discount
                };
            
                checkProduct.Save();
            }

            if (sendEmail)
            {
                var sendLoyaltyCardFormResponse = new ActionResponse();
                new SendLoyaltyCardForm(Service).Execute(checkEntity.Id.ToString(), ref sendLoyaltyCardFormResponse);
                return;
            }
            
            actionResponse = new ActionResponse { Value= response };
        }
    }
}
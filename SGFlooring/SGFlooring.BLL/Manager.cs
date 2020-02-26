using SGFlooring.Models;
using SGFlooring.Models.Interfaces;
using SGFlooring.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGFlooring.BLL
{
    public class Manager
    {
        private IOrderRepository _orderRepo;
        private IMaterialRepository _materialRepo;
        private ITaxRepository _taxRepo;

        public Manager(IOrderRepository orderRepo, IMaterialRepository materialRepo, ITaxRepository taxRepo)
        {
            _orderRepo = orderRepo;
            _materialRepo = materialRepo;
            _taxRepo = taxRepo;
        }


        public AddOrderResponse AddOrder(Order order)
        {
            AddOrderResponse response = new AddOrderResponse();

            try
            {
                _orderRepo.SaveOrder(order);
                response.Success = true;
            }
            catch(Exception e)
            {
                response.Success = false;
                response.Message = "Error: there was a problem saving your order to the repository. Please contact IT.\n" + $"({e.Message})";
            }
            return response;

        }

        public GetOrdersResponse GetOrders(DateTime date)
        {
            GetOrdersResponse response = new GetOrdersResponse();
            try
            {
                var orders = (List<Order>)_orderRepo.GetAllOrdersOnDate(date);

                if (orders.Count == 0)
                {
                    response.Success = false;
                    response.Message = $"No orders found on ({date.ToString("MM/dd/yyyy")}).";
                }
                else
                {
                    response.Success = true;
                    response.OrdersOnDate = orders;
                }
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = $"No orders found on ({date.ToString("MM/dd/yyyy")}).\n" + $"({e.Message})";
            }

            return response;
        }

        public EditOrderResponse EditOrder(Order newOrder)
        {
            EditOrderResponse response = new EditOrderResponse()
            {
                NewOrder = newOrder
            };
            try
            {
                _orderRepo.ReplaceOrder(newOrder);
                response.Success = true;
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = "Error: something went wrong while trying to save the order to the repository. Contact IT.\n" + $"({e.Message})";
            }
            return response;
        }

        public RemoveOrderResponse RemoveOrder(Order order)
        {
            RemoveOrderResponse response = new RemoveOrderResponse();
            try
            {
                _orderRepo.RemoveOrder(order);
                response.Success = true;
                response.Order = order;
                return response;
            }
            catch(Exception e)
            {
                response.Success = false;
                response.Message = "Error: something went wrong accessing the order repository. Please contact IT.\n" + $"({e.Message})";
                return response;
            }
        }

        public GetProductsResponse GetProducts()
        {
            GetProductsResponse response = new GetProductsResponse();

            try
            {
                List<Material> materials = (List<Material>)_materialRepo.GetMaterials();
                if (materials.Count == 0)
                {
                    response.Success = false;
                    response.Message = "Error: failed to load products. Please contact IT.";
                }
                else
                {
                    response.Success = true;
                    response.Materials = materials;
                }
            }
            catch
            {
                response.Success = false;
                response.Message = "Error: failed to load products. Please contact IT.";
            }
            return response;
        }

        public GetStatesResponse GetStates()
        {
            GetStatesResponse response = new GetStatesResponse();
            try
            {
                response.StateTaxes = _taxRepo.GetStateTaxes().ToList();
                response.Success = true;
                return response;
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = "Error: something went wrong getting info from the state tax repository. Contact IT.\n" + $"(System Error Message: {e.Message})";
                return response;
            }
        }

        public CheckStateResponse CheckForRequestedState(string stateAbbreviation)
        {
            CheckStateResponse response = new CheckStateResponse();

            try
            {
                if (_taxRepo.GetStateTaxes().Any(t => t.StateAbbreviation == stateAbbreviation))
                {
                    response.Success = true;
                    response.Tax = _taxRepo.GetStateTaxes().Single(t => t.StateAbbreviation == stateAbbreviation);
                }
                else
                {
                    response.Success = false;
                    response.Message = "Error: currently, we cannot take orders for the requested state.";
                }
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = "Error: failed to load the State Tax repository. Please contact IT.\n" + $"({e.Message})";
            }
            return response;
        }

        public CheckProductResponse CheckForRequestedProduct(string product)
        {
            CheckProductResponse response = new CheckProductResponse();
            product = product.ToLower();

            try
            {
                if (_materialRepo.GetMaterials().Any(m => m.ProductType.ToLower() == product))
                {
                    response.Success = true;
                    response.Product = _materialRepo.GetMaterials().Single(m => m.ProductType.ToLower() == product);
                }
                else
                {
                    response.Success = false;
                    response.Message = "Error: the product you requested cannot be found in the product repository.";
                }
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = "Error: something went wrong with checking the product repository. Contact IT.\n" + $"({e.Message})";
            }
            return response;
        }
    }
}

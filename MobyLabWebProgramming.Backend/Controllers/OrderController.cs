﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Infrastructure.Authorization;
using MobyLabWebProgramming.Infrastructure.Extensions;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Backend.Controllers;

/// <summary>
/// This is a controller example for CRUD operations on Orders.
/// </summary>
[ApiController] // This attribute specifies for the framework to add functionality to the controller such as binding multipart/form-data.
[Route("api/[controller]/[action]")] // The Route attribute prefixes the routes/url paths with template provides as a string, the keywords between [] are used to automatically take the controller and method name.
public class OrderController : AuthorizedController // Here we use the AuthorizedController as the base class because it derives ControllerBase and also has useful methods to retrieve Order information.
{
    IOrderService OrderService;
    /// <summary>
    /// Inject the required services through the constructor.
    /// </summary>
    public OrderController(IOrderService _OrderService, IUserService userService) : base(userService) // Also, you may pass constructor parameters to a base class constructor and call as specific constructor from the base class.
    {
        OrderService = _OrderService;
    }

    /// <summary>
    /// This method implements the Read operation (R from CRUD) on a Order. 
    /// </summary>
    [Authorize] // You need to use this attribute to protect the route access, it will return a Forbidden status code if the JWT is not present or invalid, and also it will decode the JWT token.
    [HttpGet("{id:guid}")] // This attribute will make the controller respond to a HTTP GET request on the route /api/Order/GetById/<some_guid>.
    public async Task<ActionResult<RequestResponse<OrderDTO>>> GetById([FromRoute] Guid id) // The FromRoute attribute will bind the id from the route to this parameter.
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            this.FromServiceResponse(await OrderService.GetOrder(id)) :
            this.ErrorMessageResult<OrderDTO>(currentUser.Error);
    }

    /// <summary>
    /// This method implements the Read operation (R from CRUD) on page of Orders.
    /// Generally, if you need to get multiple values from the database use pagination if there are many entries.
    /// It will improve performance and reduce resource consumption for both client and server.
    /// </summary>
    [Authorize]
    [HttpGet] // This attribute will make the controller respond to a HTTP GET request on the route /api/Order/GetPage.
    public async Task<ActionResult<RequestResponse<PagedResponse<OrderDTO>>>> GetPage([FromQuery] PaginationSearchQueryParams pagination) // The FromQuery attribute will bind the parameters matching the names of
                                                                                                                                            // the PaginationSearchQueryParams properties to the object in the method parameter.
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            this.FromServiceResponse(await OrderService.GetOrders(pagination)) :
            this.ErrorMessageResult<PagedResponse<OrderDTO>>(currentUser.Error);
    }

    /// <summary>
    /// This method implements the Create operation (C from CRUD) of a Order. 
    /// </summary>
    [Authorize]
    [HttpPost] // This attribute will make the controller respond to a HTTP POST request on the route /api/Order/Add.
    public async Task<ActionResult<RequestResponse>> Add([FromBody] OrderAddDTO Order)
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            this.FromServiceResponse(await OrderService.AddOrder(Order, currentUser.Result)) :
            this.ErrorMessageResult(currentUser.Error);
    }

    /// <summary>
    /// This method implements the Update operation (U from CRUD) on a Order. 
    /// </summary>
    [Authorize]
    [HttpPut] // This attribute will make the controller respond to a HTTP PUT request on the route /api/Order/Update.
    public async Task<ActionResult<RequestResponse>> Update([FromBody] OrderDTO Order) // The FromBody attribute indicates that the parameter is deserialized from the JSON body.
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            this.FromServiceResponse(await OrderService.UpdateOrder(Order)) :
            this.ErrorMessageResult(currentUser.Error);
    }

    /// <summary>
    /// This method implements the Delete operation (D from CRUD) on a Order.
    /// Note that in the HTTP RFC you cannot have a body for DELETE operations.
    /// </summary>
    [Authorize]
    [HttpDelete("{id:guid}")] // This attribute will make the controller respond to a HTTP DELETE request on the route /api/Order/Delete/<some_guid>.
    public async Task<ActionResult<RequestResponse>> Delete([FromRoute] Guid id) // The FromRoute attribute will bind the id from the route to this parameter.
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            this.FromServiceResponse(await OrderService.DeleteOrder(id)) :
            this.ErrorMessageResult(currentUser.Error);
    }
}

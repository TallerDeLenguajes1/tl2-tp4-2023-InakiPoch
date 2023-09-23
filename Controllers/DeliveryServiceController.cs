using tl2_tp4_2023_InakiPoch.Models;
using DataModels;
using Microsoft.AspNetCore.Mvc;

namespace tl2_tp4_2023_InakiPoch.Controllers;

[ApiController]
[Route("[controller]")]
public class DeliveryServiceController : ControllerBase {
    private readonly ILogger<DeliveryServiceController> _logger;
    private DeliveryService deliveryService;

    public DeliveryServiceController(ILogger<DeliveryServiceController> logger) {
        _logger = logger;
        deliveryService = DeliveryService.Instantiate();
    }

    [HttpGet("GetDeliveryService")]
    public ActionResult<DeliveryService> GetDeliveryService() => Ok(DeliveryServiceAccess.GetDeliveryService());

    [HttpGet("GetOrdersList")]
    public ActionResult<List<Order>> GetOrdersList() {
        var orders = OrdersAccess.GetOrders();
        if(orders == null) {
            return BadRequest("No se encontraron pedidos");
        }
        return Ok(orders);
    }

    [HttpGet("GetDeliveriesList")]
    public ActionResult<List<Delivery>> GetDeliveriesList() {
        var deliveries = DeliveryAccess.GetDeliveries();
        if(deliveries == null) {
            return BadRequest("No se encontraron cadetes");
        }
        return Ok(deliveries);
    }

    [HttpGet("GetReport")]
    public ActionResult<Report> GetReport() {
        if(deliveryService.GenerateReport() == null) {
            return BadRequest("Error al generar el informe");
        }
        return Ok(deliveryService.GenerateReport());
    }

    [HttpPost("AddOrder")]
    public ActionResult AddOrder(Order order) {
        deliveryService.AddOrder(order);
        return Ok("Pedido agregado con exito");
    }

    [HttpPut("AssignOrder")]
    public ActionResult AssignOrder(int orderId, int deliveryId) {
        if(!deliveryService.AssignOrder(orderId, deliveryId)) {
            return BadRequest("No se pudo asignar el pedido al cadete");
        }
        return Ok("Pedido asignado con exito");
    }

    [HttpPut("ChangeOrderStatus")]
    public ActionResult ChangeOrderStatus(int orderId, int newState) {
        if(!deliveryService.ChangeStatus(orderId, newState)) {
            return BadRequest("No se pudo cambiar el estado del pedido");
        }
        return Ok("Estado del pedido cambiado con exito");
    }

    [HttpPut("ReasignOrder")]
    public ActionResult ReasignOrder(int orderId, int newDeliveryId) {
        if(!deliveryService.ReasignOrder(orderId, newDeliveryId)) {
            return BadRequest("No se pudo reasignar el pedido al nuevo cadete");
        }
        return Ok("Pedido reasignado con exito");
    }
}

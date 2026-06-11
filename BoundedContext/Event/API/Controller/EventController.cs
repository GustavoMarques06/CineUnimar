using Api_Venda_Ingressos.BoundedContext.Event.Application.DTOs.Request;
using Api_Venda_Ingressos.BoundedContext.Event.Application.UseCases.EventUseCases;
using Api_Venda_Ingressos.BoundedContext.Event.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_Venda_Ingressos.BoundedContext.Event.API.Controller;

[ApiController]
[Route("api/Events")]
public class EventController : ControllerBase
{
    
}
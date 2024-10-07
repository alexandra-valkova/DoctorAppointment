using DoctorAppointment.Application.Features.Appointments.Commands.Create;
using DoctorAppointment.Application.Features.Appointments.Commands.Delete;
using DoctorAppointment.Application.Features.Appointments.Commands.Update;
using DoctorAppointment.Application.Features.Appointments.Commands.Update.Status;
using DoctorAppointment.Application.Features.Appointments.DTOs;
using DoctorAppointment.Application.Features.Appointments.Queries.Get;
using DoctorAppointment.Application.Features.Appointments.Queries.List;
using DoctorAppointment.Domain.Entities;
using DoctorAppointment.Domain.Errors;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DoctorAppointment.API.Endpoints
{
    public static class AppointmentEndpoints
    {
        public static RouteGroupBuilder MapAppointmentsApi(this RouteGroupBuilder group)
        {
            group.MapGet("/", GetAppointments)
                 .WithName(nameof(GetAppointments))
                 .WithSummary("Get appointments")
                 .Produces(StatusCodes.Status401Unauthorized)
                 .Produces(StatusCodes.Status400BadRequest);

            group.MapGet("/{id:int}", GetAppointmentById)
                 .WithName(nameof(GetAppointmentById))
                 .WithSummary("Get an appointment by id")
                 .Produces(StatusCodes.Status401Unauthorized)
                 .Produces(StatusCodes.Status400BadRequest);

            group.MapPost("/", AddAppointment)
                 .WithName(nameof(AddAppointment))
                 .WithSummary("Add an appointment")
                 .Produces(StatusCodes.Status401Unauthorized)
                 .Produces(StatusCodes.Status400BadRequest)
                 .Produces(StatusCodes.Status404NotFound)
                 .Produces(StatusCodes.Status409Conflict);

            group.MapPut("/{id:int}", UpdateAppointment)
                 .WithName(nameof(UpdateAppointment))
                 .WithSummary("Update an appointment")
                 .Produces(StatusCodes.Status401Unauthorized)
                 .Produces(StatusCodes.Status400BadRequest)
                 .Produces(StatusCodes.Status404NotFound)
                 .Produces(StatusCodes.Status409Conflict);

            group.MapPatch("/{id:int}", UpdateAppointmentStatus)
                 .WithName(nameof(UpdateAppointmentStatus))
                 .WithSummary("Update an appointment's status")
                 .Produces(StatusCodes.Status401Unauthorized)
                 .Produces(StatusCodes.Status400BadRequest);

            group.MapDelete("/{id:int}", DeleteAppointment)
                 .WithName(nameof(DeleteAppointment))
                 .WithSummary("Delete an appointment")
                 .Produces(StatusCodes.Status401Unauthorized)
                 .Produces(StatusCodes.Status400BadRequest);

            return group;
        }

        private static async Task<Results<Ok<List<AppointmentDTO>>, ProblemHttpResult>> GetAppointments(ISender mediatr, [FromQuery] AppointmentStatus? status, [FromQuery] int? patientId, [FromQuery] int? doctorId)
        {
            Result<List<AppointmentDTO>> result = await mediatr.Send(new ListAppointmentsQuery(status, patientId, doctorId));

            return result.IsSuccess
                ? TypedResults.Ok(result.Value)
                : TypedResults.Problem(result.Error.Description);
        }

        private static async Task<Results<Ok<AppointmentDTO>, NotFound>> GetAppointmentById(ISender mediatr, [FromRoute] int id)
        {
            Result<AppointmentDTO> result = await mediatr.Send(new GetAppointmentQuery(id));

            return result.IsSuccess
                ? TypedResults.Ok(result.Value)
                : TypedResults.NotFound();
        }

        private static async Task<Results<CreatedAtRoute<int>, ProblemHttpResult>> AddAppointment(HttpContext httpContext, ISender mediatr, [FromBody] AppointmentDTO appointment)
        {
            Result<int> result = await mediatr.Send(new CreateAppointmentCommand(appointment));

            if (result.IsSuccess)
            {
                int id = result.Value;
                return TypedResults.CreatedAtRoute(id, nameof(GetAppointmentById), new { id });
            }

            int statusCode = result.Error.Code switch
            {
                UserErrors.NotFound => StatusCodes.Status404NotFound,
                AppointmentErrors.AlreadyTaken => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status500InternalServerError
            };

            return TypedResults.Problem(result.Error.Description, httpContext.Request.Path, statusCode, "Cannot add appointment.");
        }

        private static async Task<Results<NoContent, NotFound, ProblemHttpResult>> UpdateAppointment(HttpContext httpContext, ISender mediatr, [FromRoute] int id, [FromBody] AppointmentDTO appointment)
        {
            Result result = await mediatr.Send(new UpdateAppointmentCommand(id, appointment));

            if (result.IsSuccess)
            {
                return TypedResults.NoContent();
            }

            if (result.Error.Code is AppointmentErrors.NotFound)
            {
                return TypedResults.NotFound();
            }

            int statusCode = result.Error.Code switch
            {
                UserErrors.NotFound => StatusCodes.Status404NotFound,
                AppointmentErrors.AlreadyTaken => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status500InternalServerError
            };

            return TypedResults.Problem(result.Error.Description, httpContext.Request.Path, statusCode, "Cannot update appointment.");
        }

        private static async Task<Results<NoContent, NotFound>> UpdateAppointmentStatus(ISender mediatr, [FromRoute] int id, [FromBody] AppointmentStatus status)
        {
            Result result = await mediatr.Send(new UpdateAppointmentStatusCommand(id, status));

            return result.IsSuccess
                ? TypedResults.NoContent()
                : TypedResults.NotFound();
        }

        private static async Task<Results<Ok<AppointmentDTO>, NotFound>> DeleteAppointment(ISender mediatr, [FromRoute] int id)
        {
            Result<AppointmentDTO> result = await mediatr.Send(new DeleteAppointmentCommand(id));

            return result.IsSuccess
                ? TypedResults.Ok(result.Value)
                : TypedResults.NotFound();
        }
    }
}

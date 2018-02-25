using Microsoft.AspNetCore.Mvc;

namespace EF.VenueBooking.Api.Http
{
    public static class ResponseGenerator
    {
        public static JsonResult PreconditionFailed(string reason) =>
            new JsonResult(new { ErrorMessage = reason }) { StatusCode = 412 };

        public static JsonResult ErrorOccured(string error = null, int statusCode = 400) =>
            new JsonResult(new { ErrorMessage = error ?? "Undefined error" }) { StatusCode = statusCode };

        public static JsonResult ResourceNotFound(string reason = "Resource not found") =>
            new JsonResult(new { ErrorMessage = reason }) { StatusCode = 404 };
    }
}

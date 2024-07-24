
using Microsoft.AspNetCore.Mvc;
using smERP.Application.DTOs.Results;
using System.Net;
using System.Text.RegularExpressions;

namespace smERP.API.Base
{
    public class APIControllerBase : ControllerBase
    {
        #region Actions

        public ObjectResult CustomResult<T>(BaseResponse<T> response)
        {
            return GetObjectResult(response);
        }

        // Overload for handling List responses
        public ObjectResult CustomResult<T>(BaseResponse<List<T>> response)
        {
            return GetObjectResult(response);
        }

        public ObjectResult CustomResult<T>(PaginatedResult<T> response)
        {
            return GetObjectResult(response);
        }

        // Overload for handling List responses
        public ObjectResult CustomResult<T>(PaginatedResult<List<T>> response)
        {
            return GetObjectResult(response);
        }
        private ObjectResult GetObjectResult<T>(PaginatedResult<T> response)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return new OkObjectResult(response);
                case HttpStatusCode.Unauthorized:
                    return new UnauthorizedObjectResult(response);
                case HttpStatusCode.BadRequest:
                    return new BadRequestObjectResult(response);
                case HttpStatusCode.NotFound:
                    return new NotFoundObjectResult(response);
                case HttpStatusCode.Accepted:
                    return new AcceptedResult(string.Empty, response);
                case HttpStatusCode.UnprocessableEntity:
                    return new UnprocessableEntityObjectResult(response);
                default:
                    return new BadRequestObjectResult(response);
            }
        }
        private ObjectResult GetObjectResult<T>(BaseResponse<T> response)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return new OkObjectResult(response);
                case HttpStatusCode.Created:
                    return new CreatedResult(string.Empty, response);
                case HttpStatusCode.Unauthorized:
                    return new UnauthorizedObjectResult(response);
                case HttpStatusCode.BadRequest:
                    return new BadRequestObjectResult(response);
                case HttpStatusCode.NotFound:
                    return new NotFoundObjectResult(response);
                case HttpStatusCode.Accepted:
                    return new AcceptedResult(string.Empty, response);
                case HttpStatusCode.UnprocessableEntity:
                    return new UnprocessableEntityObjectResult(response);
                default:
                    return new BadRequestObjectResult(response);
            }
        }

        #endregion
    }
}
using System.Net;

namespace ServiceRespnse.Utility
{
    public static class StatusCode
    {
        public static readonly int OK = (int)HttpStatusCode.OK;
        public static readonly int Created = (int)HttpStatusCode.Created;
        public static readonly int NoContent = (int)HttpStatusCode.NoContent;
        public static readonly int BadRequest = (int)HttpStatusCode.BadRequest;
        public static readonly int Unauthorized = (int)HttpStatusCode.Unauthorized;
        public static readonly int NotFound = (int)HttpStatusCode.NotFound;
        public static readonly int Conflict = (int)HttpStatusCode.Conflict;
    }
}

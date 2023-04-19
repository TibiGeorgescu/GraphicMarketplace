using System.Net;

namespace MobyLabWebProgramming.Core.Errors;

/// <summary>
/// Common error messages that may be reused in various places in the code.
/// </summary>
public static class CommonErrors
{
    public static ErrorMessage UserNotFound => new(HttpStatusCode.NotFound, "User doesn't exist!", ErrorCodes.EntityNotFound);
    public static ErrorMessage ProductNotFound => new(HttpStatusCode.NotFound, "Product doesn't exist!", ErrorCodes.EntityNotFound);
    public static ErrorMessage CategoryNotFound => new(HttpStatusCode.NotFound, "Category doesn't exist!", ErrorCodes.EntityNotFound);
    public static ErrorMessage ProfileNotFound => new(HttpStatusCode.NotFound, "Profile doesn't exist!", ErrorCodes.EntityNotFound);
<<<<<<< HEAD
    public static ErrorMessage FeedbackNotFound => new(HttpStatusCode.NotFound, "Feedback doesn't exist!", ErrorCodes.EntityNotFound);
    public static ErrorMessage OrderNotFound => new(HttpStatusCode.NotFound, "Order doesn't exist!", ErrorCodes.EntityNotFound);
=======
>>>>>>> dce5d55beda079ed5a7cf7f9861e0b1d6a191f40
    public static ErrorMessage TechnicalSupport => new(HttpStatusCode.InternalServerError, "An unknown error occurred, contact the technical support!", ErrorCodes.TechnicalError);
}

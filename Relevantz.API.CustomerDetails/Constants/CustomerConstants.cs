namespace Relevantz.API.CustomerDetails.Constants
{
    /// <summary>
    /// Contains all constant values used across the Customer Details API project.
    /// </summary>
    public static class CustomerConstants
    {
        // ── Route paths ─────────────────────────────────────────────────────────
        /// <summary>Base route for customer endpoints.</summary>
        public const string CustomersRoute = "api/customers";

        /// <summary>Route suffix for ID-based endpoints.</summary>
        public const string IdRoute = "{id}";

        /// <summary>Route suffix for search endpoint.</summary>
        public const string SearchRoute = "search";

        // ── Error messages ───────────────────────────────────────────────────────
        /// <summary>Error message when a customer is not found.</summary>
        public const string CustomerNotFound = "Customer not found.";

        /// <summary>Error message when the request body is invalid.</summary>
        public const string InvalidRequest = "Invalid request data.";

        /// <summary>Error message when the search query is missing or empty.</summary>
        public const string SearchQueryRequired = "Search query parameter is required.";

        /// <summary>Error message when the customer ID in the URL does not match the body.</summary>
        public const string IdMismatch = "Customer ID in URL does not match the request body.";

        // ── Swagger ──────────────────────────────────────────────────────────────
        /// <summary>Swagger/OpenAPI document title.</summary>
        public const string SwaggerTitle = "Customer Details API";

        /// <summary>Swagger/OpenAPI document version.</summary>
        public const string SwaggerVersion = "v1";

        /// <summary>Swagger/OpenAPI description.</summary>
        public const string SwaggerDescription = "A RESTful API for managing customer details, supporting full CRUD operations.";

        // ── Default values ───────────────────────────────────────────────────────
        /// <summary>Default empty string.</summary>
        public const string Empty = "";
    }
}

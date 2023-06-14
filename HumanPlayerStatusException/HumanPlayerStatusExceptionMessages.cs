using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanPlayerStatusExceptions
{
    public class HumanPlayerStatusExceptionMessages
    {
        //-1
        public readonly string UnexpectedError = "Some Unexpected Error Occured while trying to Connect to Database. ErrorCode(-1)";

        //200
        public readonly string NoError1 = "Database Operation Successfull! ErrorCode(200)";

        //201
        public readonly string NoError2 = "Database upload Operation Successfull! ErrorCode(201)";

        //204
        public readonly string NoContentError = "No content. The DELETE operation is successful. ErrorCode(204)";

        //400
        public readonly string BadRequestError = "Request Invalid. ErrorCode(400)";

        //401
        public readonly string UnautorizedAccessError = "Invalid Autorization. ErrorCode(401)";

        //403
        public readonly string ForbiddenError = "The authorization token expired. ErrorCode(403)";

        // TODO: Create Messages for all the codes.
    }
}

/*
 * 200 OK	One of the following REST operations were successful:

- GET on a resource.
- PUT on a resource.
- POST on a resource.
- POST on a stored procedure resource to execute the stored procedure.
201 Created	A POST operation to create a resource is successful.
204 No content	The DELETE operation is successful.
400 Bad request	The JSON, SQL, or JavaScript in the request body is invalid.

In addition, a 400 can also be returned when the required properties of a resource are not present or set in the body of the POST or PUT on the resource.

400 is also returned when the consistent level for a GET operation is overridden by a stronger consistency from the one set for the account.

400 is also returned when a request that requires an x-ms-documentdb-partitionkey does not include it.
401 Unauthorized	401 is returned when the Authorization header is invalid for the requested resource.
403 Forbidden	The authorization token expired.

403 code is also returned during a POST operation to create a resource when the resource quota has been reached. An example of this scenario is when you try to add documents to a collection that has reached its provisioned storage.

403 can also be returned when a stored procedure, trigger, or UDF has been flagged for high resource usage and blocked from execution.

403 forbidden error is returned when the firewall rules configured on your Azure Cosmos DB account block your request. Any requests originating from machines outside the allowed list will receive a 403 response.

403.3 – This status code is returned for write requests during the manual failover operation. This status code is used as a redirection code by drivers to forward the write requests to a new write region. Direct REST client must perform GET on DatabaseAccount to identify the current write region and forward the write request to that endpoint.
404 Not found	The operation is attempting to act on a resource that no longer exists. For example, the resource may have already been deleted.
408 Request timeout	The operation did not complete within the allotted amount of time. This code is returned when a stored procedure, trigger, or UDF (within a query) does not complete execution within the maximum execution time.
409 Conflict	The ID provided for a resource on a PUT or POST operation has been taken by an existing resource. Use another ID for the resource to resolve this issue. For partitioned collections, ID must be unique within all documents with the same partition key value.
412 Precondition failure	The operation specified an eTag that is different from the version available at the server, that is, an optimistic concurrency error. Retry the request after reading the latest version of the resource and updating the eTag on the request.
413 Entity too large	The document size in the request exceeded the allowable document size for a request. The max allowable document size is 2 MB.
423 Locked	The throughput scale operation cannot be performed because there is another scale operation in progress.
424 Failed dependency	When a document operation fails within the transactional scope of a TransactionalBatch operation, all other operations within the batch are considered failed dependencies. This status code indicates that the current operation was considered failed because of another failure within the same transactional scope.
429 Too many requests	The collection has exceeded the provisioned throughput limit. Retry the request after the server specified retry after duration. For more information, see request units.
449 Retry With	The operation encountered a transient error. This code only occurs on write operations. It is safe to retry the operation.
500 Internal Server Error	The operation failed due to an unexpected service error. Contact support. See Filing an Azure support issue.
503 Service Unavailable	The operation could not be completed because the service was unavailable. This situation could happen due to network connectivity or service availability issues. It is safe to retry the operation. If the issue persists, contact support.
 */

using System;
using Amazon.RDS;
using Amazon.RDS.Model;

namespace Shared.AWS
{
    public class RdsHelper
    {
        private readonly AmazonRDSClient _rdsClient;

        public RdsHelper(AmazonRDSClient rdsClient)
        {
            _rdsClient = rdsClient;
        }

        // Example: Get the status of an RDS instance
        public async Task<string> GetInstanceStatusAsync(string instanceIdentifier)
        {
            var request = new DescribeDBInstancesRequest
            {
                DBInstanceIdentifier = instanceIdentifier
            };

            var response = await _rdsClient.DescribeDBInstancesAsync(request);
            return response.DBInstances.FirstOrDefault()?.DBInstanceStatus ?? "Unknown";
        }

        // Example: Restart an RDS instance
        public async Task RestartInstanceAsync(string instanceIdentifier)
        {
            var request = new RebootDBInstanceRequest
            {
                DBInstanceIdentifier = instanceIdentifier
            };

            await _rdsClient.RebootDBInstanceAsync(request);
        }
    }
}

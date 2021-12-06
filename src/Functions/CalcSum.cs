using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AzureFuncDotnetPlay
{
    class SumRequest
    {
        public IList<double> sequence { get; set; }
    }

    class SumResponse
    {
        public double sum { get; set; }

        public SumResponse(double sum) => this.sum = sum;
    }

    class CalcSum
    {
        public CalcSum() {}

        [Function(nameof(CalcSum))]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData request, FunctionContext context)
        {
            var logger = context.GetLogger<SumResponse>();

            try {
                var body = await new StreamReader(request.Body).ReadToEndAsync();
                var requestData = JsonConvert.DeserializeObject<SumRequest>(body);

                double sum = 0;
                foreach (var num in requestData.sequence)
                {
                    sum += num;
                }

                var response = request.CreateResponse(System.Net.HttpStatusCode.OK);
                var responseData = JsonConvert.SerializeObject(new SumResponse(sum));
                await response.WriteStringAsync(responseData).ConfigureAwait(false);

                return response;

            }
            catch
            {
                var response = request.CreateResponse(System.Net.HttpStatusCode.BadRequest);
                return response;
            }
        }

    }
}
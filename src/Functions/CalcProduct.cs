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
    class ProductRequest
    {
        public IList<double> sequence { get; set; }
    }

    class ProductResponse
    {
        public double product { get; set; }

        public ProductResponse(double product) => this.product = product;
    }

    class CalcProduct
    {
        public CalcProduct() {}

        [Function(nameof(CalcProduct))]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData request, FunctionContext context)
        {
            var logger = context.GetLogger<ProductResponse>();

            try {
                var body = await new StreamReader(request.Body).ReadToEndAsync();
                var requestData = JsonConvert.DeserializeObject<ProductRequest>(body);

                double product = 1;
                foreach (var num in requestData.sequence)
                {
                    product *= num;
                }

                var response = request.CreateResponse(System.Net.HttpStatusCode.OK);
                var responseData = JsonConvert.SerializeObject(new ProductResponse(product));
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
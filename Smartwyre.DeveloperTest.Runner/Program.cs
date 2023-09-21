using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;
using System;
using static Smartwyre.DeveloperTest.Services.RebateService;

namespace Smartwyre.DeveloperTest.Runner;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            RebateService rebateService = new RebateService();
            CalculateRebateRequest request = new CalculateRebateRequest();
            var result = rebateService.Calculate(request);
            Console.WriteLine("rebateAmount: {0}", result.rebateAmount);
            Console.WriteLine("Successfully executed process.");
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}

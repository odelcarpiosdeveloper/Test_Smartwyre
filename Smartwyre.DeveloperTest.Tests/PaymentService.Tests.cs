using Smartwyre.DeveloperTest.Types;
using Smartwyre.DeveloperTest.Services;
using System;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests;

public class PaymentServiceTests
{
    [Fact]
    public void TestResultNotNull()
    {
        RebateService rebateService = new RebateService();
        CalculateRebateRequest request = new CalculateRebateRequest();
        CalculateRebateResult result = rebateService.Calculate(request);
        Assert.NotNull(result);
    }

    [Fact]
    public void TestResultTrue()
    {
        RebateService rebateService = new RebateService();
        CalculateRebateRequest request = new CalculateRebateRequest();
        CalculateRebateResult result = rebateService.Calculate(request);
        Assert.True(result.Success);
    }
}

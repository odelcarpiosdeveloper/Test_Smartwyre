using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services
{
    public interface IRebateIncentives
    {
        CalculateRebateResult FixedCashAmount(Rebate rebate, Product product, IncentiveType incentive);
        CalculateRebateResult FixedRateRebate(Rebate rebate, Product product, IncentiveType incentive, CalculateRebateRequest request);
        CalculateRebateResult AmountPerUom(Rebate rebate, Product product, IncentiveType incentive, CalculateRebateRequest request);
    }
}

using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services;

public class RebateService : IRebateService, IRebateIncentives
{
    public CalculateRebateResult Calculate(CalculateRebateRequest request)
    {
        var rebateDataStore = new RebateDataStore();
        var productDataStore = new ProductDataStore();

        Rebate rebate = rebateDataStore.GetRebate(request.RebateIdentifier);
        Product product = productDataStore.GetProduct(request.ProductIdentifier);

        var result = new CalculateRebateResult();

        switch (rebate.Incentive)
        {
            case IncentiveType.FixedCashAmount:
                result = FixedCashAmount(rebate, product, IncentiveType.FixedCashAmount);
                break;

            case IncentiveType.FixedRateRebate:
                result = FixedRateRebate(rebate, product, IncentiveType.FixedRateRebate, request);
                break;

            case IncentiveType.AmountPerUom:
                result = AmountPerUom(rebate, product, IncentiveType.FixedRateRebate, request);
                break;
        }

        if (result.Success)
        {
            var storeRebateDataStore = new RebateDataStore();
            storeRebateDataStore.StoreCalculationResult(rebate, result.rebateAmount);
        }

        /* For simulation purposes, we will use simulated data 
            result.Success = true;     
        */
        result.Success = true;
        return result;
    }

    public CalculateRebateResult FixedCashAmount(Rebate rebate, Product product, IncentiveType incentive)
    {
        var result = new CalculateRebateResult();

        if (rebate == null)
        {
            result.Success = false;
        }
        else if (!product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedCashAmount))
        {
            result.Success = false;
        }
        else if (rebate.Amount == 0)
        {
            result.Success = false;
        }
        else
        {
            result.rebateAmount = rebate.Amount;
            result.Success = true;
        }
        return result;
    }

    public CalculateRebateResult FixedRateRebate(Rebate rebate, Product product, IncentiveType incentive, CalculateRebateRequest request)
    {
        var result = new CalculateRebateResult();
        if (rebate == null)
        {
            result.Success = false;
        }
        else if (product == null)
        {
            result.Success = false;
        }
        else if (!product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedRateRebate))
        {
            result.Success = false;
        }
        else if (rebate.Percentage == 0 || product.Price == 0 || request.Volume == 0)
        {
            result.Success = false;
        }
        else
        {
            result.rebateAmount += product.Price * rebate.Percentage * request.Volume;
            result.Success = true;
        }
        return result;
    }

    public CalculateRebateResult AmountPerUom(Rebate rebate, Product product, IncentiveType incentive, CalculateRebateRequest request)
    {
        var result = new CalculateRebateResult();
        if (rebate == null)
        {
            result.Success = false;
        }
        else if (product == null)
        {
            result.Success = false;
        }
        else if (!product.SupportedIncentives.HasFlag(SupportedIncentiveType.AmountPerUom))
        {
            result.Success = false;
        }
        else if (rebate.Amount == 0 || request.Volume == 0)
        {
            result.Success = false;
        }
        else
        {
            result.rebateAmount += rebate.Amount * request.Volume;
            result.Success = true;
        }
        return result;
    }
}
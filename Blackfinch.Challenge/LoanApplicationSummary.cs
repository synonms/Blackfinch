namespace Blackfinch.Challenge;

public class LoanApplicationSummary
{
    public LoanApplicationSummary(int countOfApplications, decimal sumOfLoanAmount, decimal sumOfAssetValue, decimal averageLoanToValuePercent)
    {
        CountOfApplications = countOfApplications;
        SumOfLoanAmount = sumOfLoanAmount;
        SumOfAssetValue = sumOfAssetValue;
        AverageLoanToValuePercent = averageLoanToValuePercent;
    }
    
    public int CountOfApplications { get; }
    
    public decimal SumOfLoanAmount { get; }

    public decimal SumOfAssetValue { get; }

    public decimal AverageLoanToValuePercent { get; }
}
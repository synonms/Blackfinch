namespace Blackfinch.Challenge.Tests.Unit;

public class LoanApplicationProcessorTests
{
    private readonly LoanApplicationProcessor _loanApplicationProcessor = new();
    
    [Theory]
    [InlineData(100000, 9999999, 999)]
    [InlineData(1500000, 9999999, 999)]
    [InlineData(1000000, 1666667, 950)]
    [InlineData(999999, 1999998, 750)]
    [InlineData(999999, 1428570, 800)]
    [InlineData(999999, 1176469, 900)]
    public void Process_GivenAcceptableApplication_ReturnsAcceptedApplication(decimal loanAmount, decimal assetValue, int creditScore)
    {
        LoanApplication inputLoanApplication = LoanApplication.Create(loanAmount, assetValue, creditScore);
        LoanApplication processedLoanApplication = _loanApplicationProcessor.Process(inputLoanApplication);
        
        Assert.Equal(loanAmount, processedLoanApplication.LoanAmount);
        Assert.Equal(assetValue, processedLoanApplication.AssetValue);
        Assert.Equal(creditScore, processedLoanApplication.CreditScore);
        Assert.Equal(ApplicationStatus.Accepted, processedLoanApplication.Status);
    }
    
    [Theory]
    [InlineData(99999, 9999999, 999)]
    [InlineData(1500001, 9999999, 999)]
    [InlineData(1000000, 1666665, 950)]
    [InlineData(1000000, 1666667, 949)]
    [InlineData(999999, 1999998, 749)]
    [InlineData(999999, 1428570, 799)]
    [InlineData(999999, 1176469, 899)]
    [InlineData(999999, 1111110, 999)]
    public void Process_GivenUnacceptableApplication_ReturnsDeclinedApplication(decimal loanAmount, decimal assetValue, int creditScore)
    {
        LoanApplication inputLoanApplication = LoanApplication.Create(loanAmount, assetValue, creditScore);
        LoanApplication processedLoanApplication = _loanApplicationProcessor.Process(inputLoanApplication);
        
        Assert.Equal(loanAmount, processedLoanApplication.LoanAmount);
        Assert.Equal(assetValue, processedLoanApplication.AssetValue);
        Assert.Equal(creditScore, processedLoanApplication.CreditScore);
        Assert.Equal(ApplicationStatus.Declined, processedLoanApplication.Status);
        Assert.NotNull(processedLoanApplication.DeclinedReason);
    }
}

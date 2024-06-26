namespace Blackfinch.Challenge;

public class LoanApplication
{
    private LoanApplication(decimal loanAmount, decimal assetValue, int creditScore, ApplicationStatus status = ApplicationStatus.Unprocessed, string? declinedReason = null)
    {
        LoanAmount = loanAmount;
        AssetValue = assetValue;
        CreditScore = creditScore;
        Status = status;
        DeclinedReason = declinedReason;
    }

    public decimal LoanAmount { get; }

    public decimal AssetValue { get; }

    public int CreditScore { get; }

    public ApplicationStatus Status { get; }

    public string? DeclinedReason { get; }
    
    public decimal LoanToValue => (LoanAmount / AssetValue) * 100;

    public LoanApplication Accept() =>
        new(LoanAmount, AssetValue, CreditScore, ApplicationStatus.Accepted);
    
    public LoanApplication Decline(string reason) =>
        new(LoanAmount, AssetValue, CreditScore, ApplicationStatus.Declined, reason);
    
    public static LoanApplication Create(decimal loanAmount, decimal assetValue, int creditScore) =>
        new(loanAmount, assetValue, creditScore);
}
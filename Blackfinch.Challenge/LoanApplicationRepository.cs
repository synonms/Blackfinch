namespace Blackfinch.Challenge;

// TODO: Abstract this behind interface ILoanApplicationRepository to allow mocking in tests and change behaviour at runtime. 
public class LoanApplicationRepository
{
    private readonly List<LoanApplication> _loanApplications = [];

    public void Add(LoanApplication loanApplication) =>
        _loanApplications.Add(loanApplication);

    public LoanApplicationSummary GetSummary(ApplicationStatus status)
    {
        List<LoanApplication> acceptedApplications =
            _loanApplications.Where(x => x.Status == status).ToList();
        
        int countOfApplications = acceptedApplications.Count;
        decimal sumOfLoanAmount = acceptedApplications.Sum(x => x.LoanAmount);
        decimal sumOfAssetValue = acceptedApplications.Sum(x => x.AssetValue);
        decimal averageLoanToValuePercent = acceptedApplications.Count is not 0 ? acceptedApplications.Average(x => x.LoanToValue) : 0.0m;
        LoanApplicationSummary summary = new(countOfApplications, sumOfLoanAmount, sumOfAssetValue, averageLoanToValuePercent);

        return summary;
    }
}
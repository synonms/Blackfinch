/*
 * Options:
 *  1. Create loan application
 *  2. View loan application metrics
 *
 *  Create loan application
 *  =======================
 *  - Input loan amount > parse to decimal > validate greater than 0.00
 *  - Input security asset value > parse to decimal > validate greater than 0.0
 *  - Input credit score > parse to int > validate 1-999
 *  - Process application:
 *        o Calculate LoanToValuePercent = (LoanAmount / AssetValue) * 100
 *        o If LoanAmount < 100,000 or > 1,500,000 then Declined
 *        o If LoanAmount >= 1,000,000:
 *             if LoanToValuePercent > 60 or CreditScore < 950 then Declined
 *          Else
 *             if LoanToValuePercent < 60 and CreditScore < 750 then Declined
 *             if LoanToValuePercent < 80 and CreditScore < 800 then Declined
 *             if LoanToValuePercent < 90 and CreditScore < 900 then Declined
 *             if LoanToValuePercent >= 90 then Declined
 *        o Otherwise loan is Accepted
 *   - Update application with outcome and store it (in-memory repository)
 *   - Return outcome to client:
 *       o Loan amount: £[x]
 *       o Asset value: £[y]
 *       o Credit score: [z]%
 *       o If Accepted:
 *           o Congratulations, your application has been accepted.
 *         Else
 *           o Unfortunately your application has been declined.
 *
 *  View loan application metrics
 *  =============================
 *  - Applications grouped by outcome, with count, sum of loan amount and average LTV.  Also combined total.
 *      o [x] applications processed for a combined loan amount of £[y] with an average LTV [z]%
 *      o [x] of these were accepted totalling £[y] with average LTV [z]%
 *      o [x] of these were declined totalling £[y] with average LTV [z]%
 */

using Blackfinch.Challenge;

LoanApplicationRepository loanApplicationRepository = new();
LoanApplicationPresenter loanApplicationPresenter = new();
LoanApplicationProcessor loanApplicationProcessor = new();

ConsoleKeyInfo consoleKeyInfo = new();

while (consoleKeyInfo.Key != ConsoleKey.Q)
{
  Console.Clear();
  Console.WriteLine("Welcome to Blackfinch - please choose an option:");
  Console.WriteLine("  1. Create loan application");
  Console.WriteLine("  2. View loan application metrics");
  Console.WriteLine("Press 'Q' to quit.");
  consoleKeyInfo = Console.ReadKey();
  switch (consoleKeyInfo.KeyChar)
  {
      case '1':
          CreateApplication();
          break;
      case '2':
          ViewMetrics();
          break;
  }
}

return;

void CreateApplication()
{
    LoanApplication inputLoanApplication = loanApplicationPresenter.CreateApplication();
    LoanApplication processedLoanApplication = loanApplicationProcessor.Process(inputLoanApplication);
    
    loanApplicationRepository.Add(processedLoanApplication);

    Console.Clear();
    Console.WriteLine($"Loan amount: £{processedLoanApplication.LoanAmount}");
    Console.WriteLine($"Asset value: £{processedLoanApplication.AssetValue}");
    Console.WriteLine($"Loan to value: {processedLoanApplication.LoanToValue:F}%");
    Console.WriteLine($"Credit score: {processedLoanApplication.CreditScore}");

    if (processedLoanApplication.Status is ApplicationStatus.Accepted)
    {
        Console.WriteLine("Congratulations, your application has been accepted.");
    }
    else
    {
        Console.WriteLine("Unfortunately your application has been declined for the following reason:");
        Console.WriteLine(processedLoanApplication.DeclinedReason);
    }
    
    Console.WriteLine("Press a key to return to the menu.");
    Console.ReadKey();
}

void ViewMetrics()
{
    LoanApplicationSummary acceptedSummary = loanApplicationRepository.GetSummary(ApplicationStatus.Accepted);
    LoanApplicationSummary declinedSummary = loanApplicationRepository.GetSummary(ApplicationStatus.Declined);

    int totalCountOfApplications = acceptedSummary.CountOfApplications + declinedSummary.CountOfApplications;
    decimal totalSumOfLoanAmount = acceptedSummary.SumOfLoanAmount + declinedSummary.SumOfLoanAmount;
    int divisor = (acceptedSummary.CountOfApplications is 0 ? 0 : 1)
        + (declinedSummary.CountOfApplications is 0 ? 0 : 1);
    decimal averageLoanToValuePercent = divisor is 0 
        ? 0.0m
        : (acceptedSummary.AverageLoanToValuePercent + declinedSummary.AverageLoanToValuePercent) / divisor;

    Console.Clear();
    Console.WriteLine($"{totalCountOfApplications} applications processed for a combined loan amount of £{totalSumOfLoanAmount} with an average LTV {averageLoanToValuePercent:F}%.");
    Console.WriteLine($"{acceptedSummary.CountOfApplications} of these were accepted totalling £{acceptedSummary.SumOfLoanAmount} with average LTV {acceptedSummary.AverageLoanToValuePercent:F}%.");
    Console.WriteLine($"{declinedSummary.CountOfApplications} of these were declined totalling £{declinedSummary.SumOfLoanAmount} with average LTV {declinedSummary.AverageLoanToValuePercent:F}%.");
        
    Console.WriteLine("Press a key to return to the menu.");
    Console.ReadKey();
}
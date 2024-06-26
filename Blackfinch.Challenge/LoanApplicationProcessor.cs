namespace Blackfinch.Challenge;

// TODO: Abstract this behind interface ILoanProcessor to allow mocking in tests and change behaviour at runtime. 
public class LoanApplicationProcessor
{
    // This could update the incoming loanApplication directly, but I like immutability and limited side effects
    public LoanApplication Process(LoanApplication loanApplication)
    {
        if (loanApplication.LoanAmount is < 100000 or > 1500000)
        {
            return loanApplication.Decline("The value of the loan must be between £100,000 and $1.5m.");
        }

        if( loanApplication.LoanAmount >= 1000000)
        {
            if (loanApplication.LoanToValue > 60 || loanApplication.CreditScore < 950)
            {
                return loanApplication.Decline("The value of the loan is £1 million or more and the LTV is greater than 60% or the credit score is less than 950.");
            }
        }
        else
        {
            if (loanApplication.LoanToValue < 60)
            {
                if (loanApplication.CreditScore < 750)
                {
                    return loanApplication.Decline("The LTV is less than 60% and the credit score is less than 750.");
                }
            }
            else if (loanApplication.LoanToValue < 80)
            {
                if (loanApplication.CreditScore < 800)
                {
                    return loanApplication.Decline("The LTV is less than 80% and the credit score is less than 800.");
                }
            }
            else if (loanApplication.LoanToValue < 90)
            {
                if (loanApplication.CreditScore < 900)
                {
                    return loanApplication.Decline("The LTV is less than 90% and the credit score is less than 900.");
                }
            }
            else if (loanApplication.LoanToValue >= 90.0m)
            {
                return loanApplication.Decline("The LTV must be less than 90%.");
            }
        }

        return loanApplication.Accept();
    }
}
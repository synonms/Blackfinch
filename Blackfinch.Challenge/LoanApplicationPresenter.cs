namespace Blackfinch.Challenge;

// TODO: Abstract this behind interface ILoanApplicationPresenter to allow mocking in tests and change behaviour at runtime. 
public class LoanApplicationPresenter
{
    public LoanApplication CreateApplication()
    {
        Console.Clear();
        
        decimal loanAmount = CaptureLoanAmount();
        decimal assetValue = CaptureAssetValue();
        int creditScore = CaptureCreditScore();

        LoanApplication loanApplication = LoanApplication.Create(loanAmount, assetValue, creditScore);

        return loanApplication;
    }

    private decimal CaptureLoanAmount()
    {
        decimal loanAmount = 0.0m;

        while (loanAmount <= 0.0m)
        {
            Console.WriteLine("Please enter the amount that the loan is for in GBP:");
            
            string? input = Console.ReadLine();

            if (decimal.TryParse(input, out loanAmount) && loanAmount > 0.0m)
            {
                break;
            }
            
            Console.WriteLine("Error: Value must be a decimal value greater than 0.0.");
        }

        return loanAmount;
    }
    
    private decimal CaptureAssetValue()
    {
        decimal assetValue = 0.0m;

        while (assetValue <= 0.0m)
        {
            Console.WriteLine("Please enter the value of the asset that the loan will be secured against in GBP:");
            
            string? input = Console.ReadLine();

            if (decimal.TryParse(input, out assetValue) && assetValue > 0.0m)
            {
                break;
            }
            
            Console.WriteLine("Error: Value must be a decimal value greater than 0.0.");
        }

        return assetValue;
    }

    private int CaptureCreditScore()
    {
        int creditScore = 0;

        while (creditScore is < 1 or > 999)
        {
            Console.WriteLine("Please enter the credit score (1-999):");
            
            string? input = Console.ReadLine();

            if (int.TryParse(input, out creditScore) && creditScore is >= 1 and <= 999)
            {
                break;
            }
            
            Console.WriteLine("Error: Value must be an integer value between 1 and 999.");
        }

        return creditScore;
    }
}
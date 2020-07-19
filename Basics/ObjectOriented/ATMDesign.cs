namespace ObjectOriented
{
    interface IBankDatabase
    {
        bool Aunthenticate(string accountNumber, string pin);
        double GetBalance(string accountNumber);
        void Credit(string accountNumber, double amount);
        void Debit(string accountNumber, double amount);
    }

    interface IAccount
    {
        string Number { get; set; }
        int Pin { get; set; }
        double Balance { get; set; }

        bool ValidatePin(int pin);
        double GetBalance();
        void Credit(double amount);
        void Debit(double amount);
    }


    interface IMiddleware
    {
        void OpenSession();
        double BalanceEnquiry();
        double Withdraw();
        void CloseSession();
    }

    interface IATM
    {
        string Id { get; set; }
        string Place { get; set; }
        string BankName { get; set; }

        IMiddleware middleware { get; set; }
        ICardReader cardReader { get; set; }
        ICashDispenser cashDispenser { get; set; }
        IConsole console { get; set; }

        void Startup();
        void CardInserted();
        void StartUserConsole();
        void CardEjected();
        void Shutdown();
    }   

    interface ICardReader
    {
        void ReadCard();
        void EjectCard();
        void RetainCard();
    }

    interface IConsole
    {
        void Display(string message);
        void ReadPin(int pin);
        void ReadMenuChoice(int choice);
        void ReadAmount(int amount);
    }

    interface ICashDispenser
    {
        int TotalCash { get; set; }
        int Dispense();
        bool IsCashAvailable();
    }
}

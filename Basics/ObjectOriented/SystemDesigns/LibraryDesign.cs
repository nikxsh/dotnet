using System;

namespace ObjectOriented.SystemDesigns
{
    enum Bookstatus { Waiting, Pending, Cancelled, None }
    enum AccountStatus { Active, Closed, Cancelled, Flagged, None }

    interface IBookCatalogue
    {
        bool Add(string name);
        bool Update(string id);
        bool AddBook(string id, IBook bookItem);
        bool UpdateBook(string id, IBook bookItem);
        IBook[] FetchAllBooks(string id);
        IBook FindBookById(string id, string bookId);
    }

    interface IBook
    {
        string GetTitle();
    }

    interface IBookItem : IBook
    {
        bool CheckOut();
    }

    interface IAccount
    {
        void CreateProfile(string Name, string Password, string email);
        bool ResetPassword(string password);
    }

    interface ILibrarian : IAccount
    {
        bool AddBook(string Name);
        bool BlockMemeber(string id);
        bool UnblockMemeber(string id);
    }

    interface IMember : IAccount
    {
        int GetTotalBooksCheckedout();
        bool ReserveBookItem(IBookItem bookItem);
        bool CheckoutBookItem(IBookItem bookItem);
        bool RenewBookItem(IBookItem bookItem);
        bool ReturnBookItem(IBookItem bookItem);
        bool CheckForFine(IBookItem bookItem);
    }

    abstract class Account : IAccount
    {
        private string id;
        private string name;
        private string password;
        private string email;
        private AccountStatus status;

        public void CreateProfile(string Name, string Password, string email)
        {
            throw new NotImplementedException();
        }

        public bool ResetPassword(string password)
        {
            throw new NotImplementedException();
        }

        public AccountStatus GetAccountStatus()
        {
            return status;
        }
    }

    class Librarian : Account, ILibrarian
    {
        public bool UnblockMemeber(string id)
        {
            throw new NotImplementedException();
        }

        public bool AddBook(string Name)
        {
            throw new NotImplementedException();
        }

        public bool BlockMemeber(string id)
        {
            throw new NotImplementedException();
        }
    }

    class Member : Account, IMember
    {
        private DateTime dateOfMembership;
        private int totalBooksCheckedout;

        public bool CheckForFine(IBookItem bookItem)
        {
            throw new NotImplementedException();
        }

        public bool CheckoutBookItem(IBookItem bookItem)
        {
            throw new NotImplementedException();
        }

        public int GetTotalBooksCheckedout()
        {
            throw new NotImplementedException();
        }

        public bool RenewBookItem(IBookItem bookItem)
        {
            throw new NotImplementedException();
        }

        public bool ReserveBookItem(IBookItem bookItem)
        {
            throw new NotImplementedException();
        }

        public bool ReturnBookItem(IBookItem bookItem)
        {
            throw new NotImplementedException();
        }
    }

    interface IBookReservation
    {
        string FetchReservationDetails(string barcode);
    }

    interface IBookLending
    {
        void LendBook(string barcode, string memberId);
        IBookLending FetchLendingDetails(string barcode);
    }
}

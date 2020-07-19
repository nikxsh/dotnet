using System;

namespace ObjectOriented
{
    class ParkingTicket
    {
        string Number { get; set; }
        DateTime IssuedAt { get; set; }
        DateTime PayedAt { get; set; }
        IPayment Payment { get; set; }
        string Location { get; set; }
        int Status { get; set; }
    }

    interface IPayment
    {
        bool InitiateTrasaction();
    }

    abstract class Payment : IPayment
    {
        DateTime CreatedAt;
        decimal Amount;
        int Status;

        public bool InitiateTrasaction()
        {
            throw new NotImplementedException();
        }
    }

    class ParkingRate
    {
        int TotalHours { get; set; }
        decimal Rate { get; set; }
    }

    enum ParkingVehicleType { Bike, Car, Truck, Trailer }

    interface IParkingVehicle
    {
        string LicenceNumber { get; set; }
        string Company { get; set; }
        ParkingVehicleType VehicleType { get; set; }
    }

    abstract class ParkingVehicle : IParkingVehicle
    {
        private ParkingTicket ticket;

        public string LicenceNumber { get; set; }
        public string Company { get; set; }
        public ParkingVehicleType VehicleType { get; set; }

        protected virtual void AssignTicket(ParkingTicket parkingTicket)
        {
            ticket = parkingTicket;
        }
    }

    abstract class ParkingSpot  
    {
        private string number;
        private bool free;
        private ParkingVehicle vehicle;

        protected virtual bool IsFree() { return false; }
        protected virtual bool AssignVehicle(ParkingVehicle vehicle) { return false; }
        protected virtual bool RemoveVehicle() { return false; }
    }

    interface IParkingFloor
    {
        void AddNewSlot(ParkingSpot parkingSpot);
        void FreeSlot(ParkingSpot parkingSpot);
        int AssignVehicleToFreeSlot(ParkingVehicle vehicle, ParkingSpot parkingSpot);
        void UpdateDisplayBoard(ParkingSpot parkingSpot);
    }

    abstract class ParkingFloor : IParkingFloor
    {
        private int number;
        private ParkingSpot[] spots;

        public ParkingFloor(int number)
        {
            this.number = number;
        }

        public void AddNewSlot(ParkingSpot parkingSpot)
        {
            throw new NotImplementedException();
        }

        public int AssignVehicleToFreeSlot(ParkingVehicle vehicle, ParkingSpot parkingSpot)
        {
            throw new NotImplementedException();
        }

        public void FreeSlot(ParkingSpot parkingSpot)
        {
            throw new NotImplementedException();
        }

        public void UpdateDisplayBoard(ParkingSpot parkingSpot)
        {
            throw new NotImplementedException();
        }
    }

    interface IEntracePanel
    {
        bool PrintTicket();
    }

    abstract class EntracePanel : IEntracePanel
    {
        private DateTime EnteredAt;

        public bool PrintTicket()
        {
            throw new NotImplementedException();
        }
    }

    interface IExitPanel
    {
        void ScanTicket();
        void ProcessPayment();
    }

    abstract class ExitPanel : IExitPanel
    {
        private DateTime DepartedAt;
        private Payment payment;

        public void ProcessPayment()
        {
            throw new NotImplementedException();
        }

        public void ScanTicket()
        {
            throw new NotImplementedException();
        }
    }

    interface IParkingDisplayBoard
    {
        void DisplayStatus(ParkingFloor parkingFloor);
    }

    interface IParkingLot
    {
        bool AddFloor(ParkingFloor parkingFloor);
        bool IsFull(ParkingVehicleType vehicleType);
        ParkingTicket GetParkingTicket(ParkingVehicle vehicle);
        bool UpdateParkingSlot(ParkingVehicle vehicle);
    }

    abstract class ParkingLot : IParkingLot
    {
        private string id;
        private string Address;
        private ParkingFloor[] parkingFloors;
        private EntracePanel entracePanel;
        private ExitPanel exitPanel;

        public bool AddFloor(ParkingFloor parkingFloor)
        {
            throw new NotImplementedException();
        }

        public ParkingTicket GetParkingTicket(ParkingVehicle vehicle)
        {
            throw new NotImplementedException();
        }

        public bool IsFull(ParkingVehicleType vehicleType)
        {
            throw new NotImplementedException();
        }

        public bool UpdateParkingSlot(ParkingVehicle vehicle)
        {
            throw new NotImplementedException();
        }
    }
}

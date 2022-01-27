using BookingClient.BL;

namespace BookingClient.Models
{
    public class BookingQualityCheck
    {
        public string reference;
        public decimal amount;
        public decimal amountWithFees;
        public decimal amountReceived;
        public string qualityCheck
        {
            get { return string.Join(",", qualityCheckList); }
        }
        public bool overPayment;
        public bool underPayment;

        private Booking booking;
        private List<string> qualityCheckList;
        

        //private readonly ILogger logger;

        //private readonly string url = "http://localhost:9292/api/bookings";
        //public BookingQualityCheck(ILogger<BookingQualityCheck> logger) { }
        public BookingQualityCheck(Booking booking)
        {
            //this.logger = logger;

            this.booking = booking;
            reference = booking.Reference;
            amount = booking.FinalAmount;
            amountWithFees = getAmountWithFees();
            qualityCheckList = getQualityCheck();
            overPayment = getOverPayment();
            underPayment = getUnderPayment();
        }


        private decimal getAmountWithFees()
        {
            if (booking.FinalAmount == null)
                return 0;

            decimal fees = 0;
            switch (booking.FinalAmount)
            {
                case <= 1000:
                    fees = booking.FinalAmount * (decimal)0.05;
                    break;
                case > 10000:
                    fees = booking.FinalAmount * (decimal)0.02;
                    break;
                default:
                    fees = booking.FinalAmount * (decimal)0.03; 
                    break;
            }
            return booking.FinalAmount + fees;
        }

        private List<string> getQualityCheck()
        {
            try
            {
                List<string> qualityCheck = new List<string>();

                if (!CommonUtils.IsValidEmail(booking.Email))
                    qualityCheck.Add("InvalidEmail");

                if (booking.IsDuplicatStudentPayment)
                    qualityCheck.Add("DuplicatedPayment");

                if (booking.FinalAmountReceived > 1000000)
                    qualityCheck.Add("AmountThreshold");

                return qualityCheck;
            }
           catch(Exception ex)
            {
                //logger.LogError("error in getQualityCheck = " + ex.ToString());
                return null;
            }

        }

        private bool getOverPayment()
        {
            return booking.FinalAmountReceived > amountWithFees;
        }

        private bool getUnderPayment()
        {
            return booking.FinalAmountReceived < amountWithFees;
        }



        


    }
}

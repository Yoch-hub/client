using Common;

namespace Models
{
    public class BookingQualityCheck
    {
        public string reference;
        public decimal? amount;
        public decimal amountWithFees;
        public decimal amountReceived;
        public string qualityCheck
        {
            get { 
                if (qualityCheckList != null) return string.Join(",", qualityCheckList);
                else return null;
            }
        }
        public bool overPayment;
        public bool underPayment;

        private Booking booking;
        private List<string> qualityCheckList;
      
        public BookingQualityCheck(Booking booking)
        {
            if (booking.IsValid())
            {
                this.booking = booking;
                reference = booking.Reference;
                amount = booking.ConvertedAmount;
                amountWithFees = getAmountWithFees();
                qualityCheckList = getQualityCheck();
                overPayment = getOverPayment();
                underPayment = getUnderPayment();
            }
        }

        private decimal getAmountWithFees()
        {
            if (booking.ConvertedAmount == null)
                return 0;

            decimal fees = 0;
            switch (booking.ConvertedAmount)
            {
                case <= 1000:
                    fees = booking.ConvertedAmount * (decimal)0.05;
                    break;
                case > 10000:
                    fees = booking.ConvertedAmount * (decimal)0.02;
                    break;
                default:
                    fees = booking.ConvertedAmount * (decimal)0.03; 
                    break;
            }
            return Math.Round(booking.ConvertedAmount + fees,2);
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

                if (booking.ConvertedAmountReceived > 1000000)
                    qualityCheck.Add("AmountThreshold");

                return qualityCheck;
            }
           catch(Exception ex)
            {
                return null;
            }

        }

        private bool getOverPayment()
        {
            return booking.ConvertedAmountReceived > amountWithFees;
        }

        private bool getUnderPayment()
        {
            return booking.ConvertedAmountReceived < amountWithFees;
        }


    }
}

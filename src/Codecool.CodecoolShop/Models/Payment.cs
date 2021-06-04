using System;
using System.Collections.Concurrent;
using System.Linq;

namespace Codecool.CodecoolShop.Models
{
    public class Payment
    {
        public string CardHolder { get; set; }
        public string CardNumber { get; set; }
        public string ExpiryDate { get; set; }
        public string CvvNumber { get; set; }
        public bool PaymentAccepted { get; set; }
        public decimal AmountToPay { get; set; }

        public Payment(decimal amountToPay)
        {
            AmountToPay = amountToPay;
        }

        public Payment()
        {
        }

        public bool IsCardHolderCorrect(string cardHolder)
        {
            return !cardHolder.Any(char.IsDigit);
        }

        public bool IsCvvCorrect(string stringNumber)
        {
            return !stringNumber.Any(char.IsLetter);
        }

        public bool IsCardNumberCorrect(string cardNumber)
        {
            return !cardNumber.Any(char.IsLetter) & cardNumber.Length == 16;
        }
        
        
        public bool IsExpireDateCorrect(string expireDate)
        {
            return true; 
        }

        public bool IsDataCorrect()
        {
            return IsCardHolderCorrect(this.CardHolder) &
                   IsCvvCorrect(this.CvvNumber) &
                   IsCardNumberCorrect(this.CardNumber) &
                   IsExpireDateCorrect(this.ExpiryDate);
        }
    }
}
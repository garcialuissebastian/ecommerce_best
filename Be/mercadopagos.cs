using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Be
{
  public  class mercadopagos
    {
        public string id { get; set; }
        public string id_mp { get; set; }
        public string transaction_amount_refunded { get; set; }
        public string date_approved { get; set; }
        
        public string id_usuario { get; set; }
        public string token { get; set; }
        public string PaymentMethodId { get; set; }
        public string installments { get; set; }
        public string issuer_id { get; set; }
        public string TransactionAmount { get; set; }
        public string Status { get; set; }
        public string PaymentTypeId { get; set; }
        public string Card_FirstSixDigits { get; set; }
        public string Card_LastFourDigits { get; set; }
        public string StatusDetail { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string LastName { get; set; }
        public string cbte { get; set; }

       string _Item1 = "0";

        public string CostoEnvio
        {
            get { return _Item1; }
            set { _Item1 = value; }
        }

        public string LugarEnvio { get; set; }
        public string TipoEnvio { get; set; }


    }
}

using HH.Finance.Interfaces;

namespace HH.Finance.Model
{
    public class ISIN : IISIN
    {
        public ISIN(string isinCode)
        {
            Code = isinCode;
        }

        public string Code { get; private set; }
    }
}

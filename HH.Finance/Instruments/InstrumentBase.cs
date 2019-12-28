using System;
using HH.Finance.Interfaces;

namespace HH.Finance.Instruments
{
    public class InstrumentBase : IInstrument
    {
        #region Fields

        private readonly IISIN _isin;
        private readonly string _description;

        #endregion

        #region Constructors

        public InstrumentBase(IISIN isin, string description)
        {
            _isin = isin;
            _description = description;
        }

        #endregion

        #region Properties

        public IISIN ISIN { get { return _isin; } }

        public string Description { get { return _description; } }

        #endregion

        #region Methods

        public double GetNetPresentValue()
        {
          throw new NotImplementedException();
        }

        public virtual bool IsExpired()
        {
           throw new NotImplementedException();
        }

        #endregion
    }
}

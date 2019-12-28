using System;
using System.Collections.Generic;
using Rhino.Mocks;

namespace HH.TestUtils
{
    /// <summary>
    /// Automatically generates mock dependencies for a class being tested. 
    /// Every generated mock is stored internally and when VerifyAllExpectations
    /// is called verifies each mock.
    /// single constructor and all its arguments to be "mockable"
    /// </summary>
    public class AutoMocker
    {
        private readonly IList<object> _mocks;

        public AutoMocker()
        {
            _mocks = new List<object>();
        }

        public T Mock<T>() where T : class
        {
            var newMock = MockRepository.GenerateMock<T>();
            _mocks.Add(newMock);
            return newMock;
        }

        public T Mock<T>(Type type)
        {
            var newMock = MockRepository.GenerateMock(type, new Type[0]);
            return (T)newMock;
        }

        /// <summary>
        /// Verifies expectations on all generated mocks
        /// </summary>
        public void VerifyAllExpectations()
        {
            foreach (var mock in _mocks)
            {
                mock.VerifyAllExpectations();
            }
        }
    }
}


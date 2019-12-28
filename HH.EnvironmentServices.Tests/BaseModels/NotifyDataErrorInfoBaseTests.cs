using System;
using System.Collections.Generic;
using System.ComponentModel;
using HH.EnvironmentServices.BaseModels;
using HH.EnvironmentServices.Interfaces;
using HH.EnvironmentServices.Utils;
using HH.TestUtils;
using NUnit.Framework;
using Rhino.Mocks;

namespace HH.EnvironmentServices.Tests.BaseModels
{
    [TestFixture]
    internal sealed class NotifyDataErrorInfoBaseTests
    {
        private AutoMocker _autoMocker;
        private IDictionary<string, IReadOnlyCollection<string>> _errors;
        private IAgeValidator _ageValidator;
        private IDataErrorsChangedEventArgsFactory _dataErrorsChangedEventArgsFactory;
        private TestNotifyDataErrorInfo _sut;

        [SetUp]
        public void Setup()
        {
            _autoMocker = new AutoMocker();
            _errors = _autoMocker.Mock<IDictionary<string, IReadOnlyCollection<string>>>();
            _ageValidator = _autoMocker.Mock<IAgeValidator>();
            _dataErrorsChangedEventArgsFactory = _autoMocker.Mock<IDataErrorsChangedEventArgsFactory>();

            _sut = new TestNotifyDataErrorInfo(_errors, _ageValidator, _dataErrorsChangedEventArgsFactory);
        }

        [TearDown]
        public void TearDown()
        {
            _autoMocker.VerifyAllExpectations();
        }

        [TestCase(0, false)]
        [TestCase(1, true)]
        [TestCase(100, true)]
        public void HasErrors_Returns_CorrectValue(int numberOfErrors, bool hasErrors)
        {
            //Arrange
            _errors.Expect(c => c.Count).Return(numberOfErrors);

            //Act
            var result = _sut.HasErrors;

            //Assert
            Assert.AreEqual(hasErrors, result);
        }

        [TestCase("")]
        [TestCase(null)]
        public void GetErrors_Returns_Null_If_PropertyName_NullOrEmpty(string propertyName)
        {
            //Arrange

            //Act
            var result = _sut.GetErrors(propertyName);

            //Assert
            Assert.IsNull(result);
        }

        [Test]
        public void GetErrors_Returns_Null_If_No_Errors_Associated_With_PropertyName()
        {
            //Arrange
            const string propertyName = "asdasdada";
            _errors.Expect(c => c.ContainsKey(propertyName)).Return(false);

            //Act
            var result = _sut.GetErrors(propertyName);

            //Assert
            Assert.IsNull(result);
        }

        [Test]
        public void GetErrors_Returns_Errors_If_Errors_Exist_For_PropertyName()
        {
            //Arrange
            const string propertyName = "asdasdada";
            var errors = _autoMocker.Mock<IReadOnlyCollection<string>>();
            _errors.Expect(c => c.ContainsKey(propertyName)).Return(true);
            _errors.Expect(c => c[propertyName]).Return(errors);

            //Act
            var result = _sut.GetErrors(propertyName);

            //Assert
            Assert.AreEqual(errors, result);
        }

        [Test]
        public void OnPropertyChanged_Validation_Called_With_No_Errors_And_Notifications_Raised()
        {
            //Arrange
            const int age = 3;
            _ageValidator.Expect(c => c.Validate(age)).Return(new string[0]);
            var errorsChangedEventArgs = new DataErrorsChangedEventArgs("Age");
            _dataErrorsChangedEventArgsFactory.Expect(c => c.CreateDataErrorsChangedEventArgs("Age")).Return(errorsChangedEventArgs);
            var onErrorsChanged = _autoMocker.Mock<EventHandler<DataErrorsChangedEventArgs>>();
            onErrorsChanged.Expect(c => c(_sut, errorsChangedEventArgs));
            _sut.ErrorsChanged += onErrorsChanged;

            _errors.Expect(c => c.Remove("Age")).Return(false);

            var propertyChangedNotificationRaised = false;
            _sut.PropertyChanged += (sender, propertyChangedEventArgs) =>
            {
                Assert.AreEqual("Age", propertyChangedEventArgs.PropertyName);
                propertyChangedNotificationRaised = true;
            };

            //Act
            _sut.Age = age;

            //Assert
            Assert.IsTrue(propertyChangedNotificationRaised);
            Assert.AreEqual(age, _sut.Age);
        }

        [Test]
        public void OnPropertyChanged_Validation_Called_With_Errors_And_Notifications_Raised()
        {
            //Arrange
            const int age = 3;
            var errors = new[] { "Age cannot be less than 18" };
            _ageValidator.Expect(c => c.Validate(age)).Return(errors);
            var errorsChangedEventArgs = new DataErrorsChangedEventArgs("Age");
            _dataErrorsChangedEventArgsFactory.Expect(c => c.CreateDataErrorsChangedEventArgs("Age")).Return(errorsChangedEventArgs);
            var onErrorsChanged = _autoMocker.Mock<EventHandler<DataErrorsChangedEventArgs>>();
            onErrorsChanged.Expect(c => c(_sut, errorsChangedEventArgs));
            _sut.ErrorsChanged += onErrorsChanged;

            _errors.Expect(c => c["Age"] = errors);

            var propertyChangedNotificationRaised = false;
            _sut.PropertyChanged += (sender, propertyChangedEventArgs) =>
            {
                Assert.AreEqual("Age", propertyChangedEventArgs.PropertyName);
                propertyChangedNotificationRaised = true;
            };

            //Act
            _sut.Age = age;

            //Assert
            Assert.IsTrue(propertyChangedNotificationRaised);
            Assert.AreEqual(age, _sut.Age);
        }

        [Test]
        public void Dispose_Disposes_Correctly()
        {
            //Arrange
            _errors.Expect(c => c.Clear());

            //Act
            _sut.Dispose();
        }
    }

    public interface IAgeValidator
    {
        IReadOnlyCollection<string> Validate(int age);
    }

    internal sealed class TestNotifyDataErrorInfo : NotifyDataErrorInfoBase
    {
        private int _age;
        private readonly IAgeValidator _ageValidator;

        public TestNotifyDataErrorInfo()
        {

        }

        public TestNotifyDataErrorInfo(IDictionary<string, IReadOnlyCollection<string>> errors, IAgeValidator ageValidator, IDataErrorsChangedEventArgsFactory dataErrorsChangedEventArgsFactory)
            : base(errors, dataErrorsChangedEventArgsFactory)
        {
            _ageValidator = ageValidator.ArgumentNullCheck("ageValidator");
        }

        public int Age
        {
            get { return _age; }
            set { SetValue(ref _age, value, _ageValidator.Validate); }
        }
    }
}

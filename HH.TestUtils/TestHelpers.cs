using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using NUnit.Framework;
using Rhino.Mocks;

namespace HH.TestUtils
{
    public static class TestHelpers
    {
        public static void AssertIsChangedSetCorrectlyForChildEntities<T>(T instance, T[] chieldEntities)
             where T : class, INotifyPropertyChanged, IChangeTracking
        {
            var currentChildIndex = 0;
            while (currentChildIndex < chieldEntities.Length)
            {
                for (var i = 0; i <= currentChildIndex; i++)
                {
                    var isChanged = currentChildIndex == i;
                    chieldEntities[i].Expect(c => c.IsChanged).Repeat.Once().Return(isChanged);
                }
                Assert.IsTrue(instance.IsChanged);
                currentChildIndex++;
            }

            //if all children are unchanged returns false
            foreach (var chieldEntity in chieldEntities)
            {
                chieldEntity.Expect(c => c.IsChanged).Repeat.Once().Return(false);
            }
            Assert.IsFalse(instance.IsChanged);
        }

        public static void AssertPropertyChangedAndIsChanged<TInstance, TProperty>(TInstance instance,
            Action<TProperty> actionPropertySetter, TProperty currentValue, TProperty differentValue,
            string propertyName)
            where TInstance : INotifyPropertyChanged, IChangeTracking
        {
            AssertPropertyChangedAndIsChanged(instance, () => actionPropertySetter(currentValue), false, propertyName);
            AssertPropertyChangedAndIsChanged(instance, () => actionPropertySetter(differentValue), true, propertyName);
        }

        public static void AssertPropertyChangedAndIsChanged<TInstance, TProperty>(TInstance instance,
            Action<TProperty> actionPropertySetter, TProperty newValue,
            string propertyName)
            where TInstance : INotifyPropertyChanged, IChangeTracking
        {
            AssertPropertyChangedAndIsChanged(instance, () => actionPropertySetter(newValue), true, propertyName);
        }

        public static void AssertPropertyChanged<TInstance, TProperty>(TInstance instance,
            Action<TProperty> actionPropertySetter, TProperty currentValue, TProperty differentValue,
            string propertyName)
            where TInstance : INotifyPropertyChanged
        {
            AssertPropertyChanged(instance, () => actionPropertySetter(currentValue), false, new[] { propertyName });
            AssertPropertyChanged(instance, () => actionPropertySetter(differentValue), true, new[] { propertyName });
        }

        public static void AssertPropertyChanged<TInstance, TProperty>(TInstance instance,
           Action<TProperty> actionPropertySetter, TProperty newValue,
           string propertyName)
           where TInstance : INotifyPropertyChanged
        {
            AssertPropertyChanged(instance, () => actionPropertySetter(newValue), true, new[] { propertyName });
        }

        public static void AssertPropertyChanged<TInstance, TProperty>(TInstance instance,
            Action<TProperty> actionPropertySetter, TProperty currentValue, TProperty differentValue,
            string[] propertyNames)
            where TInstance : INotifyPropertyChanged
        {
            AssertPropertyChanged(instance, () => actionPropertySetter(currentValue), false, propertyNames);
            AssertPropertyChanged(instance, () => actionPropertySetter(differentValue), true, propertyNames);
        }

        private static void AssertPropertyChangedAndIsChanged<T>(T instance, Action actionPropertySetter, bool isChanged, string expectedPropertyName)
            where T : INotifyPropertyChanged, IChangeTracking
        {
            AssertPropertyChanged(instance, actionPropertySetter, isChanged, new[] { expectedPropertyName });
            Assert.AreEqual(isChanged, instance.IsChanged);
        }

        private static void AssertPropertyChanged<T>(T instance, Action actionPropertySetter, bool isChanged, string[] expectedPropertyNames)
            where T : INotifyPropertyChanged
        {
            var propertyNotifications = new List<string>();
            instance.PropertyChanged += (sender, e) =>
            {
                propertyNotifications.Add(e.PropertyName);
            };
            actionPropertySetter();
            if (isChanged)
            {
                Assert.IsNotEmpty(propertyNotifications);
                Assert.AreEqual(expectedPropertyNames.Length, propertyNotifications.Count);
                CollectionAssert.AreEqual(expectedPropertyNames, propertyNotifications);
            }
            else
            {
                Assert.IsEmpty(propertyNotifications);
            }
        }

        public static void AssertCommandDoesNotChange(ICommand expectedCommand, Func<ICommand> actionPropertyGetter)
        {
            AssertExpectedValueDoesNotChange(expectedCommand, actionPropertyGetter);
        }

        public static void AssertExpectedValueDoesNotChange<T>(T expectedValue, Func<T> actionPropertyGetter)
        {
            for (var i = 0; i < 3; i++)
            {
                var actualValue = actionPropertyGetter();
                Assert.AreEqual(expectedValue, actualValue);
            }
        }
    }
}

using KellermanSoftware.CompareNetObjects;
using NUnit.Framework;

namespace HH.TestUtils
{
    public static class CustomAssert
    {
        public static void AreEquivalent<T>(T object1, T object2)
        {
            var compareLogic = new CompareLogic();
            var result = compareLogic.Compare(object1, object2);
            if (!result.AreEqual)
            {
                Assert.Fail(result.DifferencesString);
            }
        }
    }
}

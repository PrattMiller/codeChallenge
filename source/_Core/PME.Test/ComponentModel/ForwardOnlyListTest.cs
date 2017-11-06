using System;
using NUnit.Framework;
using PME.ComponentModel;

namespace PME.Test.ComponentModel
{
    [TestFixture]
    public class ForwardOnlyListTest
    {

        private class Person
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        [Test]
        public void Test()
        {
            var list = new ForwardOnlyList<Person>();

            list.Add(new Person { FirstName = "Joe", LastName = "Smoe" });

            var counter = 0;

            // NOTE: The point of a forward only list is that we can enumerate
            // through the results even when items are added during the enumeration.
            // List<T> will fail with this behavior because it supports re-ordering
            // of the items in the collection.  For our usage, we need this behavior
            // to support append-only streams/list of data that all enumeration that
            // doesn't block the continual addition of items in the list...
            for (int i = 0; i < list.Count; i++)
            {
                counter++;

                if (i < 2)
                {
                    list.Add(new Person { FirstName = "Someone", LastName = "Else" });
                }
            }
            
            // There was only 1 item in the collection when the for/loop started, but
            // the list was modified during the enumeration.
            Assert.AreEqual(3, counter);

            // Do the same test again with a foreach/loop
            counter = 0;
            var boolSwitch = false;

            foreach (var item in list)
            {
                counter++;

                if (!boolSwitch)
                {
                    boolSwitch = true;

                    list.Add(new Person { FirstName = "Yet", LastName = "Another" });
                }
            }

            Assert.AreEqual(4, counter);
        }

        /// <summary>
        /// This test should just compile
        /// </summary>
        [Test]
        public void TestEnumerator()
        {
            var list = new ForwardOnlyList<Person>();

            list.Add(new Person { FirstName = "Joe", LastName = "Smoe" });

            // This ensures that the generic type of the Forward only list is maintained in a foreach call
            // which means IEnumerator<T> is the default return on GetEnumerator()
            foreach (var person in list)
            {
                var FirstName = person.FirstName;
            }
            
        }
    }
}


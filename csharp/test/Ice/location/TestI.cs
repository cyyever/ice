//
// Copyright (c) ZeroC, Inc. All rights reserved.
//

using Ice.location.Test;

namespace Ice
{
    namespace location
    {
        public class TestI : Test.TestIntf
        {
            internal TestI(Ice.ObjectAdapter adapter1,
                           Ice.ObjectAdapter adapter2, ServerLocatorRegistry registry)
            {
                _adapter1 = adapter1;
                _adapter2 = adapter2;
                _registry = registry;

                _registry.addObject(_adapter1.Add(new HelloI(), "hello"));
            }

            public void shutdown(Ice.Current current)
            {
                _adapter1.GetCommunicator().shutdown();
            }

            public Test.HelloPrx getHello(Ice.Current current)
            {
                return Test.HelloPrxHelper.uncheckedCast(_adapter1.CreateIndirectProxy("hello"));
            }

            public Test.HelloPrx getReplicatedHello(Ice.Current current)
            {
                return Test.HelloPrxHelper.uncheckedCast(_adapter1.CreateProxy("hello"));
            }

            public void migrateHello(Ice.Current current)
            {
                Ice.Identity id = Ice.Util.stringToIdentity("hello");
                try
                {
                    _registry.addObject(_adapter2.Add(_adapter1.Remove(id), id), current);
                }
                catch (Ice.NotRegisteredException)
                {
                    _registry.addObject(_adapter1.Add(_adapter2.Remove(id), id), current);
                }
            }

            private Ice.ObjectAdapter _adapter1;
            private Ice.ObjectAdapter _adapter2;
            private ServerLocatorRegistry _registry;
        }
    }
}

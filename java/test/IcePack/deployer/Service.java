// **********************************************************************
//
// Copyright (c) 2002
// Mutable Realms, Inc.
// Huntsville, AL, USA
//
// All Rights Reserved
//
// **********************************************************************

public class Service implements IceBox.Service
{
    public void
    start(String name, Ice.Communicator communicator, Ice.Properties properties, String[] args)
        throws IceBox.FailureException
    {
        Ice.ObjectAdapter adapter = communicator.createObjectAdapter(name + "Adapter");
        Ice.Object object = new TestI(adapter, properties);
        adapter.add(object, Ice.Util.stringToIdentity(name));
        adapter.activate();
    }

    public void
    stop()
    {
    }
}

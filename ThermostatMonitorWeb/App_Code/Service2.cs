using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

// NOTE: If you change the class name "Service2" here, you must also update the reference to "Service2" in Web.config.
public class Service2 : IService2
{
    int b = 0;
    public void DoWork()
    {
    }


    public void StatIn(byte[] data)
    {
        int a = 0;
    }

}
